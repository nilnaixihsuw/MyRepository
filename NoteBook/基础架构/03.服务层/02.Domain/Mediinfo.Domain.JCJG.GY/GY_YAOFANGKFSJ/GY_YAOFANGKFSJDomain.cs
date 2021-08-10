using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOFANGKFSJ
	{
        /// <summary>
        /// 更新药房开发时间
        /// </summary>
        /// <param name="eYaoFangKFSJ"></param>
        /// <returns></returns>
        //public GY_YAOFANGKFSJ Update(E_GY_YAOFANGKFSJ eYaoFangKFSJ)
        //{
        //    this.MargeDTO<GY_YAOFANGKFSJ, E_GY_YAOFANGKFSJ>(eYaoFangKFSJ);
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    this.XIUGAISJ = GetSYSTime();
        //    return this;
        //}
        /// <summary>
        /// 删除药房开发时间
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOFANGKFSJ>(this);
        }
    }
}
