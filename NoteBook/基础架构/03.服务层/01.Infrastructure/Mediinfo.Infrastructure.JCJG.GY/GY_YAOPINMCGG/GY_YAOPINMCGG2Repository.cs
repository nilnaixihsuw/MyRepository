using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINMCGG2Repository : RepositoryBase<GY_YAOPINMCGG2>, IGY_YAOPINMCGG2Repository
	{
		public GY_YAOPINMCGG2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 根据大规格ID,取名称规格信息
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINMCGG2> GetList(string daGuiGeID)
        {
            var list = this.Set<GY_YAOPINMCGG2>().Where(o=>o.DAGUIGID==daGuiGeID && o.ZUOFEIBZ==0).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 根据小规格ID,取名称规格信息
        /// </summary>
        /// <param name="xiaoGuiGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINMCGG2> GetXiaoGuiGeList(string xiaoGuiGeID)
        {
            var list = this.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == xiaoGuiGeID && o.GUIGEID != o.DAGUIGID).ToList().WithContext(this,ServiceContext);
            return list;
        }

        public List<GY_YAOPINMCGG2> GetGuiGeList(string GuiGeID)
        {
            var list = this.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == GuiGeID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YAOPINMCGG2> QueryGuiGeList(string GuiGeID)
        {
            var list = this.QuerySet<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == GuiGeID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YAOPINMCGG2> GetListByYaoPinID(string yaoPinID ,int zuoFeiBz =0 )
        {
            var yaoPinMCGG2List = this.Set<GY_YAOPINMCGG2>().Where(c => c.YAOPINID == yaoPinID && c.ZUOFEIBZ == zuoFeiBz).ToList().WithContext(this, ServiceContext);
            return yaoPinMCGG2List;
        }

        public List<GY_YAOPINMCGG2> GetAllDaGuiList()
        {
            var list = this.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == o.DAGUIGID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 根据大小规格ID,取中间规格信息
        /// </summary>
        /// <param name="xiaoGuiGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINMCGG2> GetZhongJianGGList(string daGuiGeID,string xiaoGuiGeID)
        {
            var list = this.Set<GY_YAOPINMCGG2>().Where(o => o.DAGUIGID == daGuiGeID && o.XIAOGUIGID ==o.XIAOGUIGID && o.GUIGEID!=o.DAGUIGID && o.GUIGEID!=o.XIAOGUIGID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YAOPINMCGG2> GetListByGuiGeID(List<string> guiGeIDList)
        {
            var list = this.Set<GY_YAOPINMCGG2>().Where(o => guiGeIDList.Contains(o.GUIGEID)).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
