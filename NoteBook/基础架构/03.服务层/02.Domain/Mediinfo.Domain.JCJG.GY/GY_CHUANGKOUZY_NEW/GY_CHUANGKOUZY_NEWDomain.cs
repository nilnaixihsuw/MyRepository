using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGKOUZY_NEW
	{

        /// <summary>
        /// ��ȡ�ؼ����ƣ��������ռ䣩
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return string.Format("{0}.{1}.{2}", this.NAMESPACE, this.FORMNAME, this.CONTROLNAME);
            
        }

        /// <summary>
        /// ��ؼ���Ӧ���ı������������ƣ�
        /// </summary>
        /// <returns></returns>
        public string GetFullText()
        {
            return this.FULLTEXT;
            
        }

        /// <summary>
        /// Ȩ������
        /// </summary>
        /// <returns></returns>
        public string QUANXIANMC()
        {
            return string.Format("{0}-{1}", this.FORMNAME, this.CONTROLNAME);
        }
        /// <summary>
        /// �ؼ��Ƿ���Ҫ����Ȩ��
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
        //        throw new DomainException("��β���Ϊ�գ�");
        //    }

        //    DBContext.Set<GY_CHUANGKOUZY_NEW>().Add(this.DBChuangKouZY);
        //}

        public void Update(E_GY_CHUANGKOUZY_NEW eChuangKouZY)
        {
            //���洫���ʱ�����dto������û�и��ٹ��仯�ģ����Ա���Ҫȫ���ж�mergeһ��

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
