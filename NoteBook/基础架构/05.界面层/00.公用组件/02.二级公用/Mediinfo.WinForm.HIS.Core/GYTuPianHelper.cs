using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.IO;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    public static class GYTuPianHelper
    {
        private static JCJGGongYongService service;

        static GYTuPianHelper()
        {
            service = new JCJGGongYongService();
        }

        /// <summary>
        /// 取医生签名信息
        /// </summary>
        /// <param name="zhiGongID"></param>
        /// <returns></returns>
        public static string GetYiShengQM(string zhiGongID)
        {
            string image = null;
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                string tuPianID = string.Format("医生签名|{0}", zhiGongID);
                // 如果pic文件夹下面有这个签名图片，返回这个地址
                if (File.Exists(@"..\PIC\" + tuPianID.Replace("|", "") + ".png"))
                {
                    image = @"..\PIC\" + tuPianID.Replace("|", "") + ".png";
                }
                else   // 本地没有图片，从数据库中读取数据并将图片存放到pic文件夹下面。
                {
                    var result = service.GetTuPianXX(tuPianID);
                    if (result.ReturnCode == ReturnCode.SUCCESS)
                    {
                        var tuPianList = result.Return;
                        if (tuPianList != null && tuPianList.Count > 0)
                        {
                            var list = tuPianList.OrderBy(o => o.XUHAO).Select(o => o.NEIRONG).ToArray();
                            var fullImageStr = string.Join("", list);

                            image = CreateImageFromByte(@"..\PIC\", tuPianID, fullImageStr);
                        }
                    }
                }
            }

            return image;
        }

        /// <summary>
        /// 将从数据库中读取的图片信息，存储到本地pic文件夹中
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="imageName"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string CreateImageFromByte(string filePath,string imageName,string image)
        {
            var buffer = Convert.FromBase64String(image);
            string file = filePath + imageName.Replace("|","");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            // 删除原有的文件已保证生成的文件最新
            if (File.Exists(file))
                File.Delete(file);
            // 生成图片文件

            //MemoryStream ms = new System.IO.MemoryStream(buffer);
            //Image img =Image.FromStream(ms);
            //img.Save("D:\\程序\\linchuang\\PIC\\gao.png", System.Drawing.Imaging.ImageFormat.Png);

            File.WriteAllBytes(file, buffer);
            return file;
        }
    }
}
