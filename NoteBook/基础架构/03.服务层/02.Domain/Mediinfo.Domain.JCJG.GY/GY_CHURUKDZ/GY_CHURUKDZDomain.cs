using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHURUKDZ
	{
        /// <summary>
        /// �޸ĳ�������
        /// </summary>
        /// <param name="crkdz"></param>
        public GY_CHURUKDZ Update(E_GY_CHURUKDZ crkdz)
        {
            Delete();
            Add(crkdz);
            return this;
        }

        /// <summary>
        /// ɾ��������������
        /// </summary>
        /// <returns></returns>
        public GY_CHURUKDZ Delete()
        {
            this.RegisterDelete<GY_CHURUKDZ>(this);
            return this;
        }

        /// <summary>
        /// ����������������
        /// </summary>
        /// <param name="crkdz"></param>
        /// <returns></returns>
        public GY_CHURUKDZ Add(E_GY_CHURUKDZ crkdz)
        {
            var churukdz = crkdz.EToDB<E_GY_CHURUKDZ, GY_CHURUKDZ>();
            this.RegisterAdd<GY_CHURUKDZ>(churukdz);
            return this;
        }
	}
}
