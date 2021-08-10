using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YEWUDJ
	{


        public string GetKuoZhanXX()
        {
            //add by chenchao For HB6-1774(461895)
            string kuoZhanXX = "科室:#01;病区:#02;科室ID:#03;病区ID:#04;应用ID:#05;应用名称:#06;IP地址:#07";
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
