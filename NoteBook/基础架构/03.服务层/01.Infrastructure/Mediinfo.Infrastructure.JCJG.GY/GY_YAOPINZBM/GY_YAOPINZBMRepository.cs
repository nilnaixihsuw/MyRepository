using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINZBMRepository : RepositoryBase<GY_YAOPINZBM>, IGY_YAOPINZBMRepository
	{
		public GY_YAOPINZBMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 通过药品ID获取别名
        /// </summary>
        /// <param name="yaoPinID"></param>
        /// <returns></returns>
        
        public List<GY_YAOPINZBM> GetListByYaoPinID(string yaoPinID)
        {
            var yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.YAOPINID == yaoPinID).ToList().WithContext(this, ServiceContext);
            return yaoPinZBMList;
        }

        /// <summary>
        ///  通过规格ID获取别名
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZBM> GetListByGuiGeID(string guiGeID)
        {
           var  yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.GUIGEID == guiGeID ).ToList().WithContext(this,ServiceContext);
            return yaoPinZBMList;
        }

        /// <summary>
        ///  通过价格ID获取别名
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZBM> GetListByJiaGeID(string jiaGeID )
        {
            var yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.JIAGEID == jiaGeID).ToList().WithContext(this, ServiceContext);
            return yaoPinZBMList;
        }

        /// <summary>
        /// 获取全部主别名
        /// </summary>
        /// <returns></returns>
        public List<GY_YAOPINZBM> GetList()
        {
            var yaoPinZBMList = this.Set<GY_YAOPINZBM>().ToList().WithContext(this, ServiceContext);
            return yaoPinZBMList;
        }

        public List<GY_YAOPINZBM> GetYaoPinZBM(  Dictionary<string, string> YaoPinXXDictionary)
        {
           // List<GY_YAOPINZBM> yaoPinZBMDomainList = new List<GY_YAOPINZBM>();

            List<GY_YAOPINZBM> yaoPinZBMList = new List<GY_YAOPINZBM>();
            if (YaoPinXXDictionary.Keys.Contains("药品名称ID") && !string.IsNullOrEmpty(YaoPinXXDictionary["药品名称ID"]))
            {  
                string yaoPinID = YaoPinXXDictionary["药品名称ID"];

                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.YAOPINID == yaoPinID && c.ZHUMINGID == null).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("药品规格ID") && !string.IsNullOrEmpty(YaoPinXXDictionary["药品规格ID"]))
            {
                string guiGeID = YaoPinXXDictionary["药品规格ID"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.GUIGEID == guiGeID && c.ZHUMINGID == null).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("药品价格ID") && !string.IsNullOrEmpty(YaoPinXXDictionary["药品价格ID"]))
            {
                string jiaGeID = YaoPinXXDictionary["药品价格ID"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.JIAGEID == jiaGeID && c.ZHUMINGID == null).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("药品规格ID全部") && !string.IsNullOrEmpty(YaoPinXXDictionary["药品规格ID全部"]))
            {
                string guiGeID = YaoPinXXDictionary["药品规格ID全部"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.GUIGEID == guiGeID).OrderBy(o => o.BIEMINGID).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("药品价格ID全部") && !string.IsNullOrEmpty(YaoPinXXDictionary["药品价格ID全部"]))
            {
                string jiaGeID = YaoPinXXDictionary["药品价格ID全部"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.JIAGEID == jiaGeID).OrderBy(o => o.BIEMINGID).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("药品名称ID全部") && !string.IsNullOrEmpty(YaoPinXXDictionary["药品名称ID全部"]))
            {
                string yaopinid = YaoPinXXDictionary["药品名称ID全部"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.YAOPINID == yaopinid).OrderBy(o => o.BIEMINGID).ToList().WithContext(this, ServiceContext);
            } 
            return yaoPinZBMList;
        }
    }
}
