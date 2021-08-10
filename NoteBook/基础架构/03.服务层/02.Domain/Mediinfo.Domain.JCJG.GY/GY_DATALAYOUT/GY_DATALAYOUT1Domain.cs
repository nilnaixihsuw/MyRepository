using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DATALAYOUT1
	{

        /// <summary>
        /// DataLayout2
        /// </summary>
        private List<GY_DATALAYOUT2> _dbDataLayout2List { get; set; }

        private static object _obj = new object();

        /// <summary>
        /// DBDataLayout2List
        /// </summary>
        [NotMapped]
        public List<GY_DATALAYOUT2> DBDataLayout2List
        {
            get
            {
                lock (_obj)
                {
                    if (null == _dbDataLayout2List)
                    {
                        _dbDataLayout2List = GetByPredicate<GY_DATALAYOUT2>(c => c.DATALAYOUTID == DATALAYOUTID).ToList();
                    }
                    return _dbDataLayout2List;
                }
            }

          
        }

        //public List<GY_DATALAYOUT2> GetDBDataLayout2List()
        //{

        //    if (null == _dbDataLayout2List)
        //    {
        //        _dbDataLayout2List = GetByPredicate<GY_DATALAYOUT2>(c => c.DATALAYOUTID == DATALAYOUTID).ToList();
        //    }
        //    return _dbDataLayout2List;
        //}


        /// <summary>
        /// 更新DataLayout1
        /// </summary>
        /// <param name="layout1"></param>
        public void Update(E_GY_DATALAYOUT1 layout1)
        {
            this.MargeDTO<GY_DATALAYOUT1, E_GY_DATALAYOUT1>(layout1, false);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = this.GetSYSTime();
        }

        /// <summary>
        /// 删除（包括DataLayout2)
        /// </summary>
        /// <param name="layout1"></param>
        public void Delete()
        {
            //删除子表数据
         
               
            if (DBDataLayout2List.Count >0)
            {
                DBDataLayout2List.ForEach(o =>
                {
                    var item = this.DBDataLayout2List.Where(p => p.DATALAYOUTMXID == o.DATALAYOUTMXID && p.DATALAYOUTID == o.DATALAYOUTID).FirstOrDefault();
                    if (item != null)
                    {
                        //this.DBDataLayout2List.Remove(item);
                        this.IRepositoyBase.RegisterDelete<GY_DATALAYOUT2>(item);

                    }
                });
            }
           

            //删除主表
            this.IRepositoyBase.RegisterDelete<GY_DATALAYOUT1>(this);
        }

        /// <summary>
        /// 增加子项
        /// </summary>
        /// <param name="dataLayout2"></param>
        public void AddDataLayout2(E_GY_DATALAYOUT2 dataLayout2)
        {
            GY_DATALAYOUT2 layout2 = dataLayout2.EToDB<E_GY_DATALAYOUT2, GY_DATALAYOUT2>();

            layout2.DATALAYOUTMXID = GetOrder("GY_DATALAYOUT2")[0];
            layout2.DATALAYOUTID =  DATALAYOUTID;
            layout2.XIUGAIREN = ServiceContext.USERID;
            layout2.XIUGAISJ = GetSYSTime();

            this.XIUGAIREN = layout2.XIUGAIREN;
            this.XIUGAISJ = layout2.XIUGAISJ;

            this.DBDataLayout2List.Add(layout2);

            this.IRepositoyBase.RegisterAdd<GY_DATALAYOUT2>(layout2);

        }

        /// <summary>
        /// 更新DataLayout2
        /// </summary>
        /// <param name="layout2"></param>
        public void UpdateDataLayout2(E_GY_DATALAYOUT2 layout2)
        {
            if (string.IsNullOrWhiteSpace(layout2.DATALAYOUTMXID))
                return;

            GY_DATALAYOUT2 entry = this.DBDataLayout2List.Where(c => c.DATALAYOUTMXID == layout2.DATALAYOUTMXID).FirstOrDefault();
            if (null == entry)
                return;

            entry.MargeDTO(layout2);
            entry.MargeDTO<GY_DATALAYOUT2, E_GY_DATALAYOUT2>(layout2, false);
            entry.XIUGAIREN = ServiceContext.USERID;
            entry.XIUGAISJ = this.GetSYSTime();

            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = this.GetSYSTime();


        }

        /// <summary>
        /// 删除明细项
        /// </summary>
        /// <param name="dataLayoutMXID"></param>
        public void RemoveDataLayout2(string dataLayoutMXID)
        {
            if (string.IsNullOrWhiteSpace(dataLayoutMXID))
                return;

            var entry = this.DBDataLayout2List.Where(c => c.DATALAYOUTMXID == dataLayoutMXID).FirstOrDefault();

            if (null == entry)
                return;

            this.IRepositoyBase.RegisterDelete<GY_DATALAYOUT2>(entry);
            this.DBDataLayout2List.Remove(entry);
        }
    }
}
