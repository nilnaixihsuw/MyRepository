namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINZKZYSZ
	{
        /// <summary>
        /// 更新 公用_药品专科专用设置
        /// </summary>
        /// <param name="eYaoPinZKZYSZ"></param>
        /// <returns></returns>
        //public GY_YAOPINZKZYSZ Update(E_GY_YAOPINZKZYSZ eYaoPinZKZYSZ)
        //{
        //    this.MargeDTO<GY_YAOPINZKZYSZ, E_GY_YAOPINZKZYSZ>(eYaoPinZKZYSZ);
        //    this.XIUGAISJ = GetSYSTime();
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    return this;
        //}
        /// <summary>
        /// 删除 公用_药品专科专用设置
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINZKZYSZ>(this);
        }
    }
}
