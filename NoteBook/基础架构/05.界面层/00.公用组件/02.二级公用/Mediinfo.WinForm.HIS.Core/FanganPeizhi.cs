using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core
{
    public class FanganPeizhi
    {
        /// <summary>
        /// 方案查询sql 
        /// </summary>
        public string QuerySQL { get; set; }

        /// <summary>
        /// LookUpEdit界面显示列对应名称
        /// </summary>
        public string XianShiLMC { get; set; }

        /// <summary>
        /// LookUpEdit实际值对应列名称
        /// </summary>
        public string ShiJiLMC { get; set; }

        /// <summary>
        /// 是否是过滤
        /// </summary>
        public bool IsGuoLv { get; set; }

        /// <summary>
        /// 过滤字段
        /// </summary>
        public string[] FilterField { get; set; }

        /// <summary>
        /// 过滤类型
        /// </summary>
        public string[] FilterType { get; set; }

        /// <summary>
        /// 弹出框具体要显示的列信息
        /// </summary>
        public List<string[]> ColumnInfo { get; set; }

        /// <summary>
        /// 每个列名所对应的位置
        /// </summary>
        public Dictionary<string, int> ColumnIndex { get; set; }

        /// <summary>
        /// 弹出框所对应的宽度
        /// </summary>
        public int PopformWidth { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public List<string[]> OrderList { get; set; }
    }

    public class FangAnInParam
    {
        /// <summary>
        /// 项目    对应 xt_selectsql1 的sqlid
        /// </summary>
        public List<string> XiangMu = new List<string>();

        /// <summary>
        /// 方案名  对应 xt_selectsql2 的方案名  fanganmc
        /// </summary>
        public List<string> FangAnMing = new List<string>();

        /// <summary>
        /// 方案对应参数 多个参数以|分割   第一个入参对应@01  第二个入参对应@02 以此类推
        /// </summary>
        public List<string> CanShu = new List<string>();
    }
}
