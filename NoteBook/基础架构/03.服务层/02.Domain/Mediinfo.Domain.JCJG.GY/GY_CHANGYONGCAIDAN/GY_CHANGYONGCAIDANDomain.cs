using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHANGYONGCAIDAN
	{
        /// <summary>
        /// 更新常用菜单
        /// </summary>
        /// <param name="isFavorite">是否为常用菜单</param>
        /// <returns></returns>
        public GY_CHANGYONGCAIDAN UpdateChangYongCaiDan(int isFavorite,int xuhao)
        {
            this.ISCHANGYONG = isFavorite;
            this.PAIXU = xuhao;
            this.RegisterUpdate<GY_CHANGYONGCAIDAN>(this);
            return this;
        }

        /// <summary>
        /// 更新全局常用菜单
        /// </summary>
        /// <param name="isALLFavorite">是否为全局常用菜单</param>
        /// <returns></returns>
        public GY_CHANGYONGCAIDAN UpdateALLChangYongCaiDan(int isALLFavorite,int xuhao)
        {
            this.ISQUANJVCY = isALLFavorite;
            this.PAIXU = xuhao;
            this.RegisterUpdate<GY_CHANGYONGCAIDAN>(this);
            return this;
        }
    }
}
