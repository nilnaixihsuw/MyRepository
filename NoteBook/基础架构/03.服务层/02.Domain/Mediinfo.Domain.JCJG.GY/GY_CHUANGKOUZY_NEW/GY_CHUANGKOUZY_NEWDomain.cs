using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGKOUZY_NEW
	{

        /// <summary>
        /// 获取控件名称（含命名空间）
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return string.Format("{0}.{1}.{2}", this.NAMESPACE, this.FORMNAME, this.CONTROLNAME);
            
        }

        /// <summary>
        /// 获控件对应的文本（含窗体名称）
        /// </summary>
        /// <returns></returns>
        public string GetFullText()
        {
            return this.FULLTEXT;
            
        }

        /// <summary>
        /// 权限名称
        /// </summary>
        /// <returns></returns>
        public string QUANXIANMC()
        {
            return string.Format("{0}-{1}", this.FORMNAME, this.CONTROLNAME);
        }
        /// <summary>
        /// 控件是否需要控制权限
        /// </summary>
        /// <returns></returns>
        public bool NeedQianXian()
        {
            return (this.CONTROLTYPE == 0);
        }

        //public void Insert()
        //{
        //    if(this.DBChuangKouZY==null)
        //    {
        //        throw new DomainException("入参不能为空！");
        //    }

        //    DBContext.Set<GY_CHUANGKOUZY_NEW>().Add(this.DBChuangKouZY);
        //}

        public void Update(E_GY_CHUANGKOUZY_NEW eChuangKouZY)
        {
            //外面传入的时候，这个dto对象是没有跟踪过变化的，所以必须要全部列都merge一遍

            this.MargeDTO<GY_CHUANGKOUZY_NEW, E_GY_CHUANGKOUZY_NEW>(eChuangKouZY, false);

            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = this.GetSYSTime();
        }


        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_CHUANGKOUZY_NEW>(this);
        }


    }
}
