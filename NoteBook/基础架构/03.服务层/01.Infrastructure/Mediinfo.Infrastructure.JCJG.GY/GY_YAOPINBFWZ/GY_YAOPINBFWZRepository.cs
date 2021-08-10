using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YAOPINBFWZRepository : RepositoryBase<GY_YAOPINBFWZ>, IGY_YAOPINBFWZRepository
	{
		public GY_YAOPINBFWZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}


        public void SetCache(string yingYongID)
        {
            var yaoPinBFWZList = GetFromCache<List<GY_YAOPINBFWZ>>("YPBFWZLIST|" + yingYongID);
            if (yaoPinBFWZList == null )
            {
                 yaoPinBFWZList = this.QuerySet<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID).ToList();
                AddToCache<List<GY_YAOPINBFWZ>>("YPBFWZLIST|" + yingYongID, yaoPinBFWZList);
            }
           
        }

        public List<GY_YAOPINBFWZ> GetListByJiaGeID(string yingYongID, string guanLiLB, string jiaGeID)
        { 
            var  yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.JIAGEID == jiaGeID && o.WEIZHISM != null).ToList();
            return yaoPinBFWZ;
        }

        public List<GY_YAOPINBFWZ> GetListByJiaGeIDFromCache(string yingYongID,string guanLiLB ,string jiaGeID )
        {

            //var yaoPinBFWZ = GetFromCache<List<GY_YAOPINBFWZ>>("YAOPINBFWZ" + yingYongID + "|" + guanLiLB + "|" + jiaGeID);
            //if (yaoPinBFWZ == null)
            //{ 
            //    yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.JIAGEID == jiaGeID && o.WEIZHISM != null).ToList();
            //    AddToCache<GY_YAOPINCDJG2>("YAOPINBFWZ" + yingYongID + "|" + guanLiLB + "|" + jiaGeID, yaoPinBFWZ);
            //}

            var yaoPinBFWZ = new List<GY_YAOPINBFWZ>();
            var yaoPinBFWZList = GetFromCache<List<GY_YAOPINBFWZ>>("YPBFWZLIST|" + yingYongID);
            if (yaoPinBFWZList == null)
            {
                yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.JIAGEID == jiaGeID && o.WEIZHISM != null).ToList();

            }
            else
            {
                yaoPinBFWZ = yaoPinBFWZList.Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.JIAGEID == jiaGeID && o.WEIZHISM != null).ToList();
            }
             
            return yaoPinBFWZ;
        }

        public List<GY_YAOPINBFWZ> GetListByJiaGeID(string yingYongID, string guanLiLB, string jiaGeID,int menZhenZYBZ)
        {
            var list = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.JIAGEID == jiaGeID && o.WEIZHISM != null && o.MENZHENZYBZ == menZhenZYBZ).ToList();
            return list;
        }

        public List<GY_YAOPINBFWZ> GetListByGuiGeID(string yingYongID, string guanLiLB, string guiGeID)
        {
            var list = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.GUIGEID == guiGeID && o.WEIZHISM != null).ToList();
            return list;
        }

        public List<GY_YAOPINBFWZ> GetListByGuiGeIDFromCache(string yingYongID, string guanLiLB, string guiGeID)
        {
            //var yaoPinBFWZ = GetFromCache<List<GY_YAOPINBFWZ>>("YAOPINBFWZ" + yingYongID + "|" + guanLiLB + "|" + guiGeID);
            //if (yaoPinBFWZ == null)
            //{
            //     yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.GUIGEID == guiGeID && o.WEIZHISM != null).ToList();
            //    AddToCache<GY_YAOPINCDJG2>("YAOPINBFWZ" + yingYongID + "|" + guanLiLB + "|" + guiGeID, yaoPinBFWZ);
            //}
            //return yaoPinBFWZ;

           var  yaoPinBFWZ = new List<GY_YAOPINBFWZ>();
            var yaoPinBFWZList = GetFromCache<List<GY_YAOPINBFWZ>>("YPBFWZLIST|" + yingYongID);
            if (yaoPinBFWZList == null)
            {
                  yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.GUIGEID == guiGeID && o.WEIZHISM != null).ToList();

            }
            else
            {
                  yaoPinBFWZ = yaoPinBFWZList.Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.GUIGEID == guiGeID && o.WEIZHISM != null).ToList();

            }

            return yaoPinBFWZ;
        }


        public List<GY_YAOPINBFWZ> GetListByGuiGeID(string yingYongID, string guanLiLB, string guiGeID, int menZhenZYBZ)
        {
            var list = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.GUIGEID == guiGeID && o.WEIZHISM != null && o.MENZHENZYBZ == menZhenZYBZ).ToList();
            return list;
        }

        public List<GY_YAOPINBFWZ> GetListByYaoPinID(string yingYongID, string guanLiLB, string yaoPinID)
        {
            var list = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.YAOPINID == yaoPinID && o.WEIZHISM != null).ToList();
            return list;
        }

        public List<GY_YAOPINBFWZ> GetListByYaoPinIDFromCache(string yingYongID, string guanLiLB, string yaoPinID)
        {
            //var yaoPinBFWZ = GetFromCache<List<GY_YAOPINBFWZ>>("YAOPINBFWZ" + yingYongID + "|" + guanLiLB + "|" + yaoPinID);
            //if (yaoPinBFWZ == null)
            //{
            //    yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.YAOPINID == yaoPinID && o.WEIZHISM != null).ToList();
            //    AddToCache<GY_YAOPINCDJG2>("YAOPINBFWZ" + yingYongID + "|" + guanLiLB + "|" + yaoPinID, yaoPinBFWZ);
            //}
            //return yaoPinBFWZ;

            var yaoPinBFWZ = new List<GY_YAOPINBFWZ>();
            var yaoPinBFWZList = GetFromCache<List<GY_YAOPINBFWZ>>("YPBFWZLIST|" + yingYongID);
            if (yaoPinBFWZList == null)
            {
                  yaoPinBFWZ = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.YAOPINID == yaoPinID && o.WEIZHISM != null).ToList();

            }
            else
            {
                  yaoPinBFWZ = yaoPinBFWZList.Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.YAOPINID == yaoPinID && o.WEIZHISM != null).ToList();

            }

            return yaoPinBFWZ;


        }
        public List<GY_YAOPINBFWZ> GetListByYaoPinID(string yingYongID, string guanLiLB, string yaoPinID, int menZhenZYBZ)
        {
            var list = this.Set<GY_YAOPINBFWZ>().Where(o => o.YINGYONGID == yingYongID && o.GUANLILB == guanLiLB && o.YAOPINID == yaoPinID && o.WEIZHISM != null && o.MENZHENZYBZ == menZhenZYBZ).ToList();
            return list;
        }

    }
}
