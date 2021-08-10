using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_QINGLINGDAN1
	{
        private List<GY_QINGLINGDAN2> _DBQingLingDan2List = null;
        [NotMapped]
        public List<GY_QINGLINGDAN2> DBQingLingDan2List
        {
            get
            {
                if (_DBQingLingDan2List == null)
                {
                    _DBQingLingDan2List = GetByPredicate<GY_QINGLINGDAN2>(o => o.QINGLINGDID == QINGLINGDID).ToList();
                }
                return _DBQingLingDan2List;
            }
        }

        /// <summary>
        /// 拒绝请领
        /// </summary>
        /// <param name="qld"></param>
        /// <returns></returns>
        //public GY_QINGLINGDAN1 UpdateJuJueQL(E_YK_QINGLINGYPMX qld)
        //{
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    this.XIUGAISJ = this.GetSYSTime();
        //    this.MargeDTO<GY_QINGLINGDAN1, E_YK_QINGLINGYPMX>(qld);
        //    JuJueQL(qld);
        //    return this;
        //}

        /// <summary>
        /// 拒绝请领
        /// </summary>
        /// <param name="qld"></param>
        //private List<GY_QINGLINGDAN2> JuJueQL(E_YK_QINGLINGYPMX qld)
        //{
        //    this.DBQingLingDan2List.ForEach(o =>
        //    {
        //        if (o.QINGLINGDMXID == qld.QINGLINGDMXID)
        //        {
        //            qld.QINGLINGZT = "4";
        //            o.MargeDTO<GY_QINGLINGDAN2, E_YK_QINGLINGYPMX>(qld);
        //            o.SHOULIRQ = this.GetSYSTime();
        //        }
        //    });
        //    return this.DBQingLingDan2List;
        //}

        /// <summary>
        /// 修改请领单状态
        /// </summary>
        /// <param name="qld"></param>
        //private List<GY_QINGLINGDAN2> UpdateQingLingZT(E_YK_QINGLINGYPMX qld)
        //{
        //    this.DBQingLingDan2List.ForEach(o =>
        //        {
        //            if (o.QINGLINGDMXID == qld.QINGLINGDMXID)
        //            {
        //                qld.QINGLINGZT = "4";
        //                o.MargeDTO<GY_QINGLINGDAN2, E_YK_QINGLINGYPMX>(qld);
        //            }
        //        });
        //    return this.DBQingLingDan2List;
        //}

        
        /// <summary>
        /// 删除主表并且删除明细表
        /// </summary>
        //public void Delete()
        //{
        //    this.RegisterDelete<GY_QINGLINGDAN1>(this);
        //    DeleteQingLingDan2();
        //}

        /// <summary>
        /// 删除主表
        /// </summary>
        public void Delete1()
        {
            this.RegisterDelete<GY_QINGLINGDAN1>(this);
        }

        /// <summary>
        /// 新增请领单
        /// </summary>
        /// <param name="eQingLingDan2DTOList"></param>
        //public void Add(List<E_GY_GONGYONGQL> eQingLingDan2DTOList)
        //{
        //    InsertQingLingDan2(eQingLingDan2DTOList, this.QINGLINGDID);
        //}

        /// <summary>
        /// 新增请领单2数据
        /// </summary>
        /// <param name="eQingLingDan2DTOList"></param>
        /// <param name="QINGLINGDID"></param>
        //private void InsertQingLingDan2(List<E_GY_GONGYONGQL> eQingLingDan2DTOList, string QINGLINGDID)
        //{
        //    var qingLingDan2EntityList = eQingLingDan2DTOList.EToDB<E_GY_GONGYONGQL, GY_QINGLINGDAN2>();
        //    string mxID = this.GetOrder("GY_QINGLINGDAN2", ServiceContext.YUANQUID)[0].ToString();
        //    qingLingDan2EntityList.ForEach(qingLingDan2Entity =>
        //    {
        //        qingLingDan2Entity.QINGLINGDMXID = mxID;
        //        qingLingDan2Entity.QINGLINGDID = QINGLINGDID;
        //        qingLingDan2Entity.LEIBIEID = "0";
        //        qingLingDan2Entity.SHUNXUHAO = 0;
        //        qingLingDan2Entity.SHENQINGRQ = this.GetSYSTime();
        //        qingLingDan2Entity.JIYONGBZ = string.IsNullOrEmpty(qingLingDan2Entity.JIYONGBZ.ToString()) ? 0 : qingLingDan2Entity.JIYONGBZ;
        //        qingLingDan2Entity.QINGLINGZT = string.IsNullOrEmpty(qingLingDan2Entity.QINGLINGZT) ? "2" : qingLingDan2Entity.QINGLINGZT;
        //        qingLingDan2Entity.KUCUNSL = string.IsNullOrEmpty(qingLingDan2Entity.KUCUNSL.ToString()) ? 0 : qingLingDan2Entity.KUCUNSL;
        //        qingLingDan2Entity.QINGLINGSL = qingLingDan2Entity.QINGLINGSL;
        //    });
            

        //    DBQingLingDan2List.AddRange(qingLingDan2EntityList);
        //    this.RegisterAdd<GY_QINGLINGDAN2>(qingLingDan2EntityList);
        //}

        //private void DeleteQingLingDan2()
        //{
        //    DBQingLingDan2List.ForEach(eQingLingDan2DTO =>
        //    {
        //        //DBQingLingDan2List.Remove(eQingLingDan2DTO);
        //        this.RegisterDelete(eQingLingDan2DTO);
        //    });
        //    DBQingLingDan2List.Clear();
        //}
        /// <summary>
        /// 删除请领单2
        /// </summary>
        /// <param name="qinglingdanxxList">请领单2列表</param>
        //public void DeleteQingLingDan2(List<E_GY_GONGYONGQL> qinglingdanxxList)
        //{
         
        //    qinglingdanxxList.ForEach(eQingLingDan2 =>
        //    {
        //        var item = this.DBQingLingDan2List.Where(p => p.QINGLINGDMXID == eQingLingDan2.QINGLINGDMXID && p.QINGLINGDID == eQingLingDan2.QINGLINGDID).FirstOrDefault();
        //        if (item != null)
        //        {
        //            this.DBQingLingDan2List.Remove(item);
        //            this.RegisterDelete(item);
        //        }
        //    });
        //}
        /// <summary>
        /// 更新请领单2
        /// </summary>
        /// <param name="qinglingdan2List">请领单2列表</param>
        //public void UpdateQingLingDan2(List<E_GY_GONGYONGQL> qinglingdan2List)
        //{
          
        //    qinglingdan2List.ForEach(eQingLingDan2 =>
        //    {
        //        var ruKuDan2 = DBQingLingDan2List.Where(o => o.QINGLINGDMXID == eQingLingDan2.QINGLINGDMXID).FirstOrDefault();
        //        if (ruKuDan2 != null)
        //        {
        //            ruKuDan2.MargeDTO<GY_QINGLINGDAN2, E_GY_GONGYONGQL>(eQingLingDan2);
        //            this.RegisterUpdate(ruKuDan2);
        //        }
        //    });
        //}
    }
}
