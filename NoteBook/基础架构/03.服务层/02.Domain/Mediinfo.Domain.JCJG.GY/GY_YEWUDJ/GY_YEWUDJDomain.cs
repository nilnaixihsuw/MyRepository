using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YEWUDJ
	{


        public string GetKuoZhanXX()
        {
            //add by chenchao For HB6-1774(461895)
            string kuoZhanXX = "����:#01;����:#02;����ID:#03;����ID:#04;Ӧ��ID:#05;Ӧ������:#06;IP��ַ:#07";
            kuoZhanXX=kuoZhanXX.Replace("#01", this.ServiceContext.KESHIMC);
            kuoZhanXX=kuoZhanXX.Replace("#02", ServiceContext.DANGQIANBQMC);
            kuoZhanXX=kuoZhanXX.Replace("#03", ServiceContext.JIUZHENKSID ?? ServiceContext.KESHIID);
            kuoZhanXX=kuoZhanXX.Replace("#04", ServiceContext.DANGQIANBQ);
            kuoZhanXX=kuoZhanXX.Replace("#05", ServiceContext.YINGYONGID);
            kuoZhanXX=kuoZhanXX.Replace("#06", ServiceContext.YINGYONGMC);
            kuoZhanXX=kuoZhanXX.Replace("#07", ServiceContext.IP);
            return kuoZhanXX;
        }

    }
}
