using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core.Cache;
using Mediinfo.Infrastructure.Core.DBContext;
using Mediinfo.Infrastructure.Core.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mediinfo.Infrastructure.JCJG
{
    /// <summary>
    /// HIS工作单元
    /// </summary>
    public class HISUnitOfWork : UnitOfWorkBase, IHISUnitOfWork
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextCache"></param>
        public HISUnitOfWork(ContextCache contextCache) : base(contextCache, HISDBContextFactory.Create())
        {
            var pluginDll = Mediinfo.Enterprise.Config.MediinfoConfig.GetValue("SystemConfig.xml", "MessagePlugin");
            if (!string.IsNullOrEmpty(pluginDll))
            {
                var ptypes = Assembly.Load(pluginDll).GetTypes().Where(p => p.IsSubclassOf(typeof(MessagePlugin)));
                var type = ptypes.FirstOrDefault();
                this.MessagePlugin = (IMessagePlugin)Activator.CreateInstance(type, dbContext);
            }
        }

        /// <summary>
        /// 锁表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        public override List<T> LockTable<T>(string where, int waitTime)
        {
            string sql = string.Format("select * from {0} where {1} for update wait {2}", typeof(T).Name, where, waitTime);
            return this.dbContext.Database.SqlQuery<T>(sql).ToList();
        }

        /// <summary>
        /// 获取序号
        /// </summary>
        /// <param name="XuHaoMC">序号名</param>
        /// <param name="QianZhui">前缀</param>
        /// <param name="Count">获取数量</param>
        /// <returns></returns>
        public override List<string> GetOrder(string XuHaoMC, string QianZhui = null, int Count = 1)
        {
            string sqlSEQ = " Select {0}.nextval from dual ";
            string seqCount = " CONNECT BY LEVEL<= {1} ";
            if (QianZhui == null)
            {
                QianZhui = string.Empty;
            }
            if (String.IsNullOrWhiteSpace(XuHaoMC))
            {
                throw new DomainException("序号名称不能为空");
            }
            bool isYaoFang = false;
            int val = 0;
            if (XuHaoMC.Length > 3 && int.TryParse(XuHaoMC.Substring(1, 2), out val))
            {
                isYaoFang = true;
            }

            #region tttttttttt

            List<string> ids = new List<string>();
            GY_XuHao XuHao = null;

            if (isYaoFang)
            {
                XuHao = this.dbContext.Database.SqlQuery<GY_XuHao>("select XULIEMC, DANGQIANZHI,ZUHEFS,CHANGDU from GY_XUHAO where XUHAOMC = :Name for update wait 1", XuHaoMC).FirstOrDefault();
                if (XuHao == null)
                {
                    var success = this.dbContext.Database.ExecuteSqlCommand("insert into GY_XUHAO (XUHAOMC, DANGQIANZHI, ZUIXIAOZHI, ZUIDAZHI, CHANGDU, ZUHEFS) values (:Name ,:ZUIXIAOZHI ,1 ,999999 ,12 ,'1')", XuHaoMC, Count);
                    if (success < 0)
                    {
                        throw new DBException("序号表新增失败");
                    }
                    else
                    {
                        XuHao = new GY_XuHao()
                        {
                            DANGQIANZHI = Count,
                            CHANGDU = 12,
                            ZUHEFS = "1"
                        };
                        for (int i = 1; i <= Count; i++)
                        {
                            ids.Add(i.ToString());
                        }
                    }

                }
                else
                {
                    var success = this.dbContext.Database.ExecuteSqlCommand("update GY_XUHAO set DANGQIANZHI = :DANGQIANZHI where XUHAOMC = :Name", XuHao.DANGQIANZHI + Count, XuHaoMC);
                    if (success < 0)
                    {
                        throw new DBException("序号表更新失败");
                    }
                    else
                    {
                        for (int i = XuHao.DANGQIANZHI + 1; i <= XuHao.DANGQIANZHI + Count; i++)
                        {
                            ids.Add(i.ToString());
                        }
                    }
                }

            }
            else
            {
                XuHao = this.dbContext.Database.SqlQuery<GY_XuHao>("select XULIEMC, DANGQIANZHI,ZUHEFS,CHANGDU from GY_XUHAO where XUHAOMC = :Name", XuHaoMC).FirstOrDefault();
                if (XuHao != null)
                {
                    if (Count > 1)
                    {
                        var sql = sqlSEQ + " " + seqCount;
                        var xuhao = this.dbContext.Database.SqlQuery<Decimal>(string.Format(sql, XuHao.XULIEMC, Count.ToString())).ToList();
                        ids.AddRange(xuhao.Select(o => o.ToString()));
                    }
                    else
                    {

                        ids.Add(this.dbContext.Database.SqlQuery<Decimal>(string.Format(sqlSEQ, XuHao.XULIEMC)).FirstOrDefault().ToString());

                    }
                }
                else
                {
                    throw new DomainException("序号名称在序号表中不存在");
                }
            }


            #endregion

            //#region tttttttttt

            //List<string> ids = new List<string>();
            //GY_XuHao XuHao = null;

            //XuHao = this.dbContext.Database.SqlQuery<GY_XuHao>("select XULIEMC, DANGQIANZHI,ZUHEFS,CHANGDU from GY_XUHAO where XUHAOMC = :Name for update wait 1", XuHaoMC).FirstOrDefault();
            //if (XuHao == null)
            //{
            //    if (isYaoFang)
            //    {
            //        var success = this.dbContext.Database.ExecuteSqlCommand("insert into GY_XUHAO (XUHAOMC, DANGQIANZHI, ZUIXIAOZHI, ZUIDAZHI, CHANGDU, ZUHEFS) values (:Name ,:ZUIXIAOZHI ,1 ,999999 ,12 ,'1')", XuHaoMC, Count);
            //        if (success < 0)
            //        {
            //            throw new DBException("序号表新增失败");
            //        }
            //        else
            //        {
            //            XuHao = new GY_XuHao()
            //            {
            //                DANGQIANZHI = Count,
            //                CHANGDU = 12,
            //                ZUHEFS = "1"
            //            };
            //            for (int i = 1; i <= Count; i++)
            //            {
            //                ids.Add(i.ToString());
            //            }
            //        }
            //    }
            //    else
            //    {
            //        throw new DomainException("序号名称在序号表中不存在");
            //    }
            //}
            //else
            //{
            //    if (isYaoFang)
            //    {
            //        var success = this.dbContext.Database.ExecuteSqlCommand("update GY_XUHAO set DANGQIANZHI = :DANGQIANZHI where XUHAOMC = :Name", XuHao.DANGQIANZHI + Count, XuHaoMC);
            //        if (success < 0)
            //        {
            //            throw new DBException("序号表更新失败");
            //        }
            //        else
            //        {
            //            for (int i = XuHao.DANGQIANZHI + 1; i <= XuHao.DANGQIANZHI + Count; i++)
            //            {
            //                ids.Add(i.ToString());
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //for (int i = 0; i < Count; i++)
            //        //{
            //         //   ids.Add(this.dbContext.Database.SqlQuery<Decimal>(string.Format(sqlSEQ, XuHao.XULIEMC)).FirstOrDefault().ToString());
            //       // }

            //        if(Count > 1 )
            //        {
            //            var sql = sqlSEQ + " " + seqCount;
            //            var xuhao = this.dbContext.Database.SqlQuery<Decimal>(string.Format(sql, XuHao.XULIEMC, Count.ToString())).ToList();
            //            ids.AddRange(xuhao.Select(o => o.ToString()));
            //        }
            //        else
            //        {

            //            ids.Add(this.dbContext.Database.SqlQuery<Decimal>(string.Format(sqlSEQ, XuHao.XULIEMC)).FirstOrDefault().ToString());

            //        }
            //    }
            //}

            //#endregion

            List<string> IDS = new List<string>();
            if ((XuHao.ZUHEFS == null || XuHao.ZUHEFS == "0") && !XuHao.CHANGDU.HasValue)
            {
                return ids;
            }
            else if ((XuHao.ZUHEFS == null || XuHao.ZUHEFS == "0") && XuHao.CHANGDU.HasValue)
            {
                ids.ForEach(o =>
                {
                    if (o.Length <= XuHao.CHANGDU)
                    {
                        IDS.Add(o.PadLeft(XuHao.CHANGDU.Value, '0'));
                    }
                });
                return IDS;
            }
            else if (XuHao.ZUHEFS != null && XuHao.ZUHEFS != "0" && (!XuHao.CHANGDU.HasValue || XuHao.CHANGDU.Value == 0))
            {
                ids.ForEach(o =>
                {
                    IDS.Add(QianZhui + o);
                });
                return IDS;
            }
            else if (XuHao.ZUHEFS != null && XuHao.ZUHEFS != "0" && XuHao.CHANGDU.HasValue)
            {
                ids.ForEach(o =>
                {
                    if ((o.Length + QianZhui.Length) > XuHao.CHANGDU)
                    {
                        throw new DomainException("序号超出定义的长度");
                    }
                    else if ((o.Length + QianZhui.Length) == XuHao.CHANGDU)
                    {
                        IDS.Add(QianZhui + o);
                    }
                    else
                    {
                        IDS.Add(QianZhui + o.PadLeft(XuHao.CHANGDU.Value - QianZhui.Length, '0'));
                    }
                });
                return IDS;
            }
            return ids;
        }
    }

    /// <summary>
    /// 序号
    /// </summary>
    class GY_XuHao
    {
        public string XULIEMC { get; set; }
        public int DANGQIANZHI { get; set; }
        public int? CHANGDU { get; set; }
        public string ZUHEFS { get; set; }
    }
    /// <summary>
    /// 参数
    /// </summary>
    internal class CanShuDTO
    {
        public string YINGYONGID { get; set; }
        public string CANSHUID { get; set; }
        public string CANSHUZHI { get; set; }
    }
}
