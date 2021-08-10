using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZIFUBLRepository : IRepository<GY_ZIFUBL>, IDependency
	{
        /// <summary>
        /// ��ȡ�Ը�����
        /// </summary>
        /// <param name="feiYongKZID">���ÿ���ID</param>
        /// <param name="xiangMuID">��ĿID</param>
        /// <param name="xiangMuLX">��Ŀ����</param>
        /// <param name="faShengRQ">��������</param>
        /// <param name="menZhenZYBZ">����סԺ��־</param>
        /// <returns>���ص�ǰ��Ŀ���Ը������Ƿ����</returns>
        bool GetZiFuBL(string feiYongKZID, string xiangMuID, string xiangMuLX,
                       DateTime faShengRQ, int menZhenZYBZ,out Decimal ziFuBL);
        /// <summary>
        /// ��ȡhis1���Ը�����
        /// </summary>
        /// <param name="xiangMuID">��ĿID</param>
        /// <param name="feiYongXZ">��������</param>
        /// <returns></returns>
        bool GetZiFuBL(string xiangMuID, string feiYongXZ,out decimal ziFuBL);

        List<GY_ZIFUBL> GetList(List<string> xiangMuIDS, string feiYongXZ);
         
	}
}
