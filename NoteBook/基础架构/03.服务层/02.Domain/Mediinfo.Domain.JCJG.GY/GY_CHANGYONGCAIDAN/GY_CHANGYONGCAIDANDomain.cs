using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHANGYONGCAIDAN
	{
        /// <summary>
        /// ���³��ò˵�
        /// </summary>
        /// <param name="isFavorite">�Ƿ�Ϊ���ò˵�</param>
        /// <returns></returns>
        public GY_CHANGYONGCAIDAN UpdateChangYongCaiDan(int isFavorite,int xuhao)
        {
            this.ISCHANGYONG = isFavorite;
            this.PAIXU = xuhao;
            this.RegisterUpdate<GY_CHANGYONGCAIDAN>(this);
            return this;
        }

        /// <summary>
        /// ����ȫ�ֳ��ò˵�
        /// </summary>
        /// <param name="isALLFavorite">�Ƿ�Ϊȫ�ֳ��ò˵�</param>
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
