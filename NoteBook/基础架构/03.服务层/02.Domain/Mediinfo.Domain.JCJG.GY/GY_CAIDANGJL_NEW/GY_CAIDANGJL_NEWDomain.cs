using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CAIDANGJL_NEW
	{
        /// <summary>
        /// ����Ӧ�ò˵�������
        /// </summary>
        /// <param name="eCaiDanGJLNew"></param>
        /// <returns></returns>
        public GY_CAIDANGJL_NEW Update(E_GY_CAIDANGJL_NEW eCaiDanGJLNew)
        {
            this.MargeDTO<GY_CAIDANGJL_NEW, E_GY_CAIDANGJL_NEW>(eCaiDanGJLNew);
            return IRepositoyBase.RegisterUpdate(this);           
        }
        /// <summary>
        /// ɾ���˵�������
        /// </summary>
        /// <returns></returns>
        public GY_CAIDANGJL_NEW Delete()
        {
            return IRepositoyBase.RegisterDelete<GY_CAIDANGJL_NEW>(this);
        }
    }
}
