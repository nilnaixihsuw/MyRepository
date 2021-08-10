using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YINGYONGRepository : RepositoryBase<GY_YINGYONG>, IGY_YINGYONGRepository
	{

		public GY_YINGYONGRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        void SetCache()
        {
            var yingYongList = GetFromCache<List<GY_YINGYONG>>("YINGYONG");
            if(yingYongList == null)
            { 
                yingYongList = this.Set<GY_YINGYONG>().ToList();
                AddToCache<List<GY_YINGYONG>>("YINGYONG", yingYongList);
            }
        }
        public List<GY_YINGYONG> GetList(string xiTongID)
        {
            var list = this.Set<GY_YINGYONG>().Where(o => o.XITONGID == xiTongID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YINGYONG> GetListS(string[] yingYongIDS)
        {
            var list = this.Set<GY_YINGYONG>().Where(o => yingYongIDS.Contains(o.YINGYONGID)).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YINGYONG> GetByKeyFromCache(string yingYongID)
        {
            var yingYongList = GetFromCache<List<GY_YINGYONG>>("YINGYONG");
            if (yingYongList == null)
            {
                SetCache();
                yingYongList = GetFromCache<List<GY_YINGYONG>>("YINGYONG");
            } 
            var list = yingYongList.Where(o => o.YINGYONGID == yingYongID).ToList().WithContext(this, ServiceContext);

            return list; 
        }


        /// <summary>
        /// ȡ���ݺ�(Prc_GY_GetDanJuHao)
        /// </summary>
        /// <param name="prmXuHaoMC"></param>
        /// <param name="prmYingYongID"></param>
        /// <returns></returns>
        public string GetDanJuHao(string prmYingYongID,  string prmXuHaoMC)
        {
            //��ȡ���
            string danjuhao = this.GetOrder(prmXuHaoMC, null)[0].ToString();

            //���ݺ�һ�㶼��6λ ǰ��6λ�� Ӧ��ID+00 ��� �� 050200000067       
            if (danjuhao.Length > 6)
            {
                danjuhao = danjuhao.Substring(danjuhao.Length - 6, 6);
            }
            if (danjuhao.Length < 6)
            {
                danjuhao = danjuhao.PadLeft(6, '0');
            }
            danjuhao = prmYingYongID + "00" + danjuhao;
            return danjuhao;
        }

        /// <summary>
        /// ȡҩ�ⷿӦ��
        /// </summary>
        /// <returns></returns>
        public List<GY_YINGYONG> GetList()
        {
            var list = this.Set<GY_YINGYONG>().Where(o => (new string[] { "05", "06", "07", "13", "33", "08" }).Contains(o.YINGYONGID.Substring(0, 2))).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// ���ÿؼ�ȡӦ������
        /// </summary>
        /// <returns></returns>
        public List<GY_YINGYONG> GetGYList()
        {
            var list = this.Set<GY_YINGYONG>().ToList().WithContext(this, ServiceContext);
            return list;
        }

    }
}
