using Mediinfo.DTO.HIS.XT;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Domain.JCJG.XT
{
	public partial class XT_HANZIKU
	{
        /// 实现延迟加载
        private List<XT_HANZIKU2> _duoYingZiList = null;

        //不需要实现Set方法
        public List<XT_HANZIKU2> DBDuoYinZiList
        {
            get
            {
                if (null == _duoYingZiList)
                    _duoYingZiList = GetByPredicate<XT_HANZIKU2>(c => c.HANZI == HANZI).ToList();
                return _duoYingZiList;
            }
        }

        /// <summary>
        /// 修改汉字
        /// </summary>
        public void Update(E_XT_HanZiKu e_HanZiKu)
        {
            this.MargeDTO<XT_HANZIKU, E_XT_HanZiKu>(e_HanZiKu);
            
        } 

        public void Delete()
        {
            IRepositoyBase.RegisterDelete(this);
            this.DBDuoYinZiList.RemoveAll(o => o.HANZI == HANZI);
        }
        public void AddDuoYinZi(List<E_XT_HanZiKu2> DuoYinZiList)
        {
            var duoyingzi = DuoYinZiList.EToDB<E_XT_HanZiKu2, XT_HANZIKU2>();
            this.DBDuoYinZiList.AddRange(duoyingzi);
            IRepositoyBase.RegisterAdd<XT_HANZIKU2>(duoyingzi);
        }

        public void UpdateDuoYinZi(List<E_XT_HanZiKu2> DuoYinZiList)
        {
            this.DBDuoYinZiList.ForEach(o => {
                var duoyinzi = DuoYinZiList.Where(p => p.CIZU == o.CIZU).FirstOrDefault();
                if (duoyinzi != null)
                {
                    o.MargeDTO<XT_HANZIKU2, E_XT_HanZiKu2>(duoyinzi);
                }
            });
        }

        public void DeleteDuoYinZi(List<E_XT_HanZiKu2> DuoYinZiList)
        {
            DuoYinZiList.ForEach(o =>
            {
                var i = this.DBDuoYinZiList.FindIndex(p => p.CIZU == o.CIZU);
                if (i > -1)
                {
                    this.DBDuoYinZiList.RemoveAt(i);
                }
            });
        }
 
    }
}
