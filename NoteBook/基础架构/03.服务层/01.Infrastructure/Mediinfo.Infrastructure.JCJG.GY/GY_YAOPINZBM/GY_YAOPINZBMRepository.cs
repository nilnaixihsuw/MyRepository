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
        /// ͨ��ҩƷID��ȡ����
        /// </summary>
        /// <param name="yaoPinID"></param>
        /// <returns></returns>
        
        public List<GY_YAOPINZBM> GetListByYaoPinID(string yaoPinID)
        {
            var yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.YAOPINID == yaoPinID).ToList().WithContext(this, ServiceContext);
            return yaoPinZBMList;
        }

        /// <summary>
        ///  ͨ�����ID��ȡ����
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZBM> GetListByGuiGeID(string guiGeID)
        {
           var  yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.GUIGEID == guiGeID ).ToList().WithContext(this,ServiceContext);
            return yaoPinZBMList;
        }

        /// <summary>
        ///  ͨ���۸�ID��ȡ����
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZBM> GetListByJiaGeID(string jiaGeID )
        {
            var yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.JIAGEID == jiaGeID).ToList().WithContext(this, ServiceContext);
            return yaoPinZBMList;
        }

        /// <summary>
        /// ��ȡȫ��������
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
            if (YaoPinXXDictionary.Keys.Contains("ҩƷ����ID") && !string.IsNullOrEmpty(YaoPinXXDictionary["ҩƷ����ID"]))
            {  
                string yaoPinID = YaoPinXXDictionary["ҩƷ����ID"];

                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.YAOPINID == yaoPinID && c.ZHUMINGID == null).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("ҩƷ���ID") && !string.IsNullOrEmpty(YaoPinXXDictionary["ҩƷ���ID"]))
            {
                string guiGeID = YaoPinXXDictionary["ҩƷ���ID"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.GUIGEID == guiGeID && c.ZHUMINGID == null).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("ҩƷ�۸�ID") && !string.IsNullOrEmpty(YaoPinXXDictionary["ҩƷ�۸�ID"]))
            {
                string jiaGeID = YaoPinXXDictionary["ҩƷ�۸�ID"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.JIAGEID == jiaGeID && c.ZHUMINGID == null).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("ҩƷ���IDȫ��") && !string.IsNullOrEmpty(YaoPinXXDictionary["ҩƷ���IDȫ��"]))
            {
                string guiGeID = YaoPinXXDictionary["ҩƷ���IDȫ��"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.GUIGEID == guiGeID).OrderBy(o => o.BIEMINGID).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("ҩƷ�۸�IDȫ��") && !string.IsNullOrEmpty(YaoPinXXDictionary["ҩƷ�۸�IDȫ��"]))
            {
                string jiaGeID = YaoPinXXDictionary["ҩƷ�۸�IDȫ��"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.JIAGEID == jiaGeID).OrderBy(o => o.BIEMINGID).ToList().WithContext(this, ServiceContext);
            }
            else if (YaoPinXXDictionary.Keys.Contains("ҩƷ����IDȫ��") && !string.IsNullOrEmpty(YaoPinXXDictionary["ҩƷ����IDȫ��"]))
            {
                string yaopinid = YaoPinXXDictionary["ҩƷ����IDȫ��"];
                yaoPinZBMList = this.Set<GY_YAOPINZBM>().Where(c => c.YAOPINID == yaopinid).OrderBy(o => o.BIEMINGID).ToList().WithContext(this, ServiceContext);
            } 
            return yaoPinZBMList;
        }
    }
}
