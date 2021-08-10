using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_DAIMARepository : RepositoryBase<GY_DAIMA>, IGY_DAIMARepository
    {
        public GY_DAIMARepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public List<GY_DAIMA> GetList(string daiMaLB, string daiMaId)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == daiMaLB && o.DAIMAID == daiMaId).ToList();
            return list;
        }

        /// <summary>
        /// 根据代码类别获取代码信息
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        public List<GY_DAIMA> GetList(string daiMaLB)
        {
            var list = this.QuerySet<GY_DAIMA>().Where(o => o.DAIMALB == daiMaLB).ToList();
            return list;
        }
        /// <summary>
        /// 根据代码类别获取未作废代码信息 --xieyz 2019-05-31
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        public List<GY_DAIMA> GetListBydaiMaLB(string daiMaLB)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == daiMaLB&&o.ZUOFEIBZ==0).ToList();
            return list;
        }
        /// <summary>
        /// 公用控件取代码数据
        /// </summary>
        /// <returns></returns>
        public List<GY_DAIMA> GetGYList(string DAIMALB, int MENZHENSY, int ZHUYUANSY, int ZUOFEIBZ)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == DAIMALB && o.MENZHENSY == MENZHENSY && o.ZHUYUANSY == ZHUYUANSY && o.ZUOFEIBZ == ZUOFEIBZ).ToList();
            return list;
        }
        /// <summary>
        /// 获取公用频次类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetPinCiLX(string key)
        {
            int? pinCiLX = null;
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMAID == key).ToList();

            if (list != null)
            {
                pinCiLX = list.FirstOrDefault().BIAOZHIWEI2;
            }
            return pinCiLX;
        }

        // PKG_MZ_ZHENJIAN.PRC_ZJ_CREATEGUAHAOXX 7735-7753
        public string GetGuaHaoLB(string leiBieGL)
        {
            var shunxuhao = (from p in Set<GY_DAIMA>()
                             where p.ZIFU1 == leiBieGL && p.DAIMALB == "0025" && p.ZUOFEIBZ == 0
                             select p.SHUNXUHAO).ToList().Min();

            return (from o in Set<GY_DAIMA>()
                    where o.DAIMALB == "0025" &&
                          o.ZUOFEIBZ == 0 &&
                          o.SHUNXUHAO == shunxuhao
                    select o.DAIMAID).FirstOrDefault();

        }

        // PKG_MZ_ZHENJIAN.PRC_ZJ_CREATEGUAHAOXX 7760-7763  HB3-21017
        public int GetZiFu1(string S_GUAHAOLB)
        {
            var zifu1 = (from o in Set<GY_DAIMA>()
                         where o.DAIMALB == "0025" && o.ZUOFEIBZ == 0 && o.DAIMAID == S_GUAHAOLB
                         select o.ZIFU1).FirstOrDefault();
            return Convert.ToInt32(zifu1);
        }

        /// <summary>
        /// 取长期医嘱的频次类型
        /// </summary>
        /// <param name="pinCiLX"></param>
        /// <returns></returns>
        public List<string> GetYiZhuLXList(int pinCiLX)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.BIAOZHIWEI2 != pinCiLX && o.DAIMALB == "0021" && o.ZUOFEIBZ != 1).Select(o => o.DAIMAID).ToList();
            return list;
        }

        /// <summary>
        /// 由频次ID取频次信息
        /// </summary>
        /// <param name="pinCiLX"></param>
        /// <returns></returns>
        public List<GY_DAIMA> GetPinCiList(string pinCiID)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMAID == pinCiID && o.DAIMALB == "0021" && o.ZUOFEIBZ != 1).ToList();
            return list;
        }

        /// <summary>
        /// 获取术后代码列表
        /// </summary>
        /// <returns></returns>
        public List<GY_DAIMA> GetShuHouDMList()
        {
            var list = this.Set<GY_DAIMA>().Where(o =>  o.DAIMALB == "0021" && o.ZUOFEIBZ == 0).ToList().Where(o => o.DAIMAMC.Contains("术后"));
            return list.ToList();

        }

        public int GetShunXuHao(string daimalb)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == daimalb).ToList();
            int? Shunxuhao = list.Max(o => o.SHUNXUHAO);
            if (Shunxuhao != null && Shunxuhao > 0)
            {
                return Convert.ToInt32(Shunxuhao) + 1;
            }
            else
            {
                return 1;
            }
        }

        public bool CheckDaiMaID(string daimaid, string daimalb)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMAID == daimaid && o.DAIMALB == daimalb).ToList();
            if (list.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
