using Mediinfo.DTO.HIS.XT;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Mediinfo.WinForm.HIS.Core
{
    public class ControlsQuery
    {
        /// <summary>
        /// 内部查询sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable QuerySql(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                return null;

            JCJGQuerySqlService querySqlService = new JCJGQuerySqlService();
            var result = querySqlService.GetDataTableBySql(sql);
            if (result.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                return result.Return.TableContent;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="total">数据总条数</param>
        /// <returns></returns>
        public DataTable QueryPageSql(string sql, int pageIndex, int pageSize, out int total)
        {
            JCJGQuerySqlService querySqlService = new JCJGQuerySqlService();
            var result = querySqlService.GetPagedDataTableBySql(sql, pageIndex, pageSize);
            if (result.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                throw new Exception("查询SQL[" + sql + "]异常" + result.ReturnMessage);
            }

            // DataTable resultdt = new DataTable(result.Return.PageData);
            total = Convert.ToInt32(result.Return.TotalRecords);
            return result.Return.PageData;
        }

        /// <summary>
        /// 获取LookUpEdit的下拉选项方案信息
        /// </summary>
        /// <param name="xiangMu"></param>
        /// <param name="fangAnMing"></param>
        /// <returns></returns>
        public E_XT_SELECTSQL2_EX GetLookUpEditFangAn(string xiangMu, string fangAnMing)
        {
            E_XT_SELECTSQL2_EX fanganpz = new E_XT_SELECTSQL2_EX();
            List<E_XT_SELECTSQL2_EX> fangAn1 = new List<E_XT_SELECTSQL2_EX>();
            E_XT_SELECTSQL2_EX fanganpz1 = new E_XT_SELECTSQL2_EX();
            int flag = 0;
            string querySqlWhere = "";
            string sql = "";
            string[] fangAnArr = fangAnMing.TrimEnd('|').Split('|');
            for (int i = 0; i < fangAnArr.Length; i++)
            {
                var fangan = GYFangAnHelper.GetFangAnPZ(xiangMu, fangAnArr[i]);

                if (fangan != null && fangan.Count > 0)
                {
                    fanganpz = fangan[xiangMu + "-" + fangAnArr[i]];

                    fanganpz1 = fanganpz.Clone() as E_XT_SELECTSQL2_EX;

                    string sqlwhere = fanganpz.SQLWHERE.ToStringEx();

                    sql = fanganpz.SQL.ToStringEx();

                    // 如果方案方案名带@的，用对应的sql
                    if (fangan.Count > 1)
                    {
                        sqlwhere = fangan[xiangMu + "-" + fangAnArr[i] + "@"].SQLWHERE.ToStringEx();
                    }

                    if (flag < 1)
                    {
                        querySqlWhere = sqlwhere;
                    }
                    else
                    {
                        querySqlWhere = querySqlWhere + " and " + sqlwhere;
                    }

                    if (fangan.Count > 2)
                    {
                        sql = fangan[xiangMu + "@" + "-" + fangAnArr[i]].SQL.ToStringEx();
                    }

                }

                flag++;
            }

            fanganpz1.SQLWHERE = querySqlWhere;
            fanganpz1.SQL = sql;
            return fanganpz1;
        }

        /// <summary>
        /// 获取方案
        /// </summary>
        /// <param name="xiangMu">项目</param>
        /// <param name="fangAnMing">方案名</param>
        /// <param name="isAllLoad">是否全部加载</param>
        /// <returns></returns>
        public FanganPeizhi GetFanAn(string xiangMu, string fangAnMing, bool isAllLoad = false)
        {
            FanganPeizhi peizhi = new FanganPeizhi();
            peizhi.ColumnInfo = new List<string[]>();
            peizhi.ColumnIndex = new Dictionary<string, int>();
            
            var fangAn = GetLookUpEditFangAn(xiangMu, fangAnMing);
            if (fangAn == null)
            {
                return null;
            }

            string yuanSql = fangAn.SQL.ToStringEx().Replace("%s", " where 1=0");
            string sql = "";

            // 如果为全部加载的话 且后面有需要替换参数的时候
            if (isAllLoad && fangAn.SQLWHERE.ToStringEx().IndexOf("@", StringComparison.Ordinal) >= 0)
            {
                sql = fangAn.SQL.ToStringEx().Replace("%s", " where 1=1");
            }
            else
            {
                if (fangAn.SQLWHERE.ToStringEx().TrimStart().ToUpper().IndexOf("ORDER", StringComparison.Ordinal) == 0)
                {
                    sql = fangAn.SQL.ToStringEx().Replace("%s", " ") + fangAn.SQLWHERE.ToStringEx();
                }
                else
                {
                    sql = fangAn.SQL.ToStringEx().Replace("%s", " where ") + fangAn.SQLWHERE.ToStringEx();
                }
            }
            sql = TiHuanGongYongCanShu(sql);
            peizhi.QuerySQL = sql;
            if (yuanSql.IndexOf("@", StringComparison.Ordinal) != -1)
            {
                for (int i = 1; i < 100; i++)
                {
                    yuanSql = yuanSql.Replace("@" + i.ToString().PadLeft(2, '0'), "1");
                }
            }
            DataTable dtColum = QuerySql(yuanSql);

            // 如果方案中的sql语句有错误，直接返回null
            if (dtColum == null)
                return null;

            // 显示列宽
            string[] TanChuKXSLK = fangAn.XIANSHILIE.Split(',');

            // 显示列名称
            string[] TanChuKXSLMC = fangAn.LIEMINGCHENG.Split(',');

            if (fangAn.PAIXULIE != null)
            {
                string paiXuLie = fangAn.PAIXULIE;
                List<string[]> orderList = new List<string[]>();
                if (paiXuLie.IndexOf("@", StringComparison.Ordinal) != -1)
                {
                    string[] paiXu = paiXuLie.Split(',');
                    foreach (var t in paiXu)
                    {
                        if (t.Trim() != "")
                        {
                            for (int k = 0; k < TanChuKXSLMC.Length; k++)
                            {
                                if (t.Contains(TanChuKXSLMC[k]))
                                {
                                    string type = t.ToUpper().IndexOf("DESC", StringComparison.Ordinal) != -1 ? "DESC" : "ASC";
                                    string columnName = t.Replace("@", "").Replace(TanChuKXSLMC[k], dtColum.Columns[k].ColumnName);
                                    // 去除排序内容
                                    columnName = columnName.ToUpper().Replace(" DESC", "").Replace(" ASC", "").Trim();
                                    orderList.Add(new string[2] { columnName, type });
                                }
                            }
                        }
                    }
                }
                peizhi.OrderList = orderList;
            }

            // 绑定值列
            // 添加返回值大于0的判断，防止fanhuilie为0的情况
            if ((int.Parse(fangAn.FANHUILIE.ToString()) - 1) < dtColum.Columns.Count && (int.Parse(fangAn.FANHUILIE?.ToString()) > 0))
            {
                peizhi.ShiJiLMC = dtColum.Columns[int.Parse(fangAn.FANHUILIE.ToString()) - 1].ToString();
            }
            else
            {
                peizhi.ShiJiLMC = dtColum.Columns[0].ToString();
            }

            #region 特殊处理没有输入码的 指定特定列 生成输入码

            int mingChengWz = 0;
            // 中文名列与字段位置需转换下
            string mingchenglie = fangAn.MINGCHENGLIE?.ToString();
            if (string.IsNullOrEmpty(mingchenglie))     // 未设置名称列，根据输入名称里取是否有名称的列
            {
                for (int i = 0; i < TanChuKXSLMC.Length; i++)
                {
                    if (TanChuKXSLMC[i].IndexOf("名称", StringComparison.Ordinal) != -1)
                    {
                        mingchenglie = TanChuKXSLMC[i];
                        peizhi.IsGuoLv = true;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(mingchenglie))  // 如果都没有对应列，取第一列为名称列
            {
                mingChengWz = 0;
            }
            else
            {
                if (TanChuKXSLMC.Contains(mingchenglie))
                {
                    for (int i = 0; i < TanChuKXSLMC.Length; i++)
                    {
                        if (TanChuKXSLMC[i] == mingchenglie)
                        {
                            mingChengWz = i;
                            break;
                        }
                    }
                }
            }
            // 绑定显示列
            peizhi.XianShiLMC = dtColum.Columns[mingChengWz < 1 ? 0 : mingChengWz].ToString();

            #endregion

            // 生成需要的列项目
            for (int i = 0; i < dtColum.Columns.Count; i++)
            {
                // 添加判断xianshilie是否可以转化为数值，避免字母或者中文逗号情况
                int zhi = 0;
                if (TanChuKXSLK.Length > i && int.TryParse(TanChuKXSLK[i], out zhi) && zhi > 0)
                {
                    string[] columnInfo = new string[3];
                    columnInfo[0] = dtColum.Columns[i].ColumnName;
                    if (TanChuKXSLMC.Length > i)
                    {
                        columnInfo[1] = TanChuKXSLMC[i];
                    }
                    int width = int.Parse((int.Parse(TanChuKXSLK[i]) * 1.5).ToString("f0"));
                    columnInfo[2] = width.ToString();
                    peizhi.PopformWidth += (width * 5);
                    peizhi.ColumnInfo.Add(columnInfo);
                }

                if (TanChuKXSLMC.Length > i && !peizhi.ColumnIndex.ContainsKey(TanChuKXSLMC[i]))
                {
                    peizhi.ColumnIndex.Add(TanChuKXSLMC[i], i);
                }
                if (!peizhi.ColumnIndex.ContainsKey(dtColum.Columns[i].ColumnName))
                {
                    peizhi.ColumnIndex.Add(dtColum.Columns[i].ColumnName, i);
                }
            }
            if (fangAn.GUOLVLIE != null)
            {
                peizhi.IsGuoLv = true;
                peizhi.FilterField = fangAn.GUOLVLIE?.ToString().Split(',');
                
                if (!string.IsNullOrEmpty(fangAn.GUOLVLX?.ToString()))
                {
                    peizhi.FilterType = fangAn.GUOLVLX.ToString().Split(',');
                }

            }
            else
            {
                List<string> listFile = new List<string>();
                foreach (DataColumn col in dtColum.Columns)
                {
                    if (col.ColumnName != "1")
                    {
                        listFile.Add(col.ColumnName);
                    }
                    else
                    {
                        if (!listFile.Contains("SHURUMA1"))
                        {
                            listFile.Add("SHURUMA1");
                        }
                    }
                }
                peizhi.FilterField = listFile.ToArray();
            }
            return peizhi;
        }

        /// <summary>
        /// 获取方案数据
        /// </summary>
        /// <param name="xiangMu">项目名</param>
        /// <param name="fangAnMing">方案名</param>
        /// <param name="returnObj">返回对象</param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="dicCanShu">方案参数</param>
        /// <returns></returns>
        [Obsolete("该方法没有启用缓存，建议使用GetShuJuZD")]
        public bool GetFangAnData<T>(string xiangMu, string fangAnMing, out List<T> returnObj, out string errMsg, Dictionary<string, string> dicCanShu = null) where T : class, new()
        {
            bool success = false;
            List<T> ts = new List<T>();
            errMsg = "";
            var fangan = GetFanAn(xiangMu, fangAnMing);

            if (dicCanShu != null)
            {
                foreach (var item in dicCanShu)
                {
                    fangan.QuerySQL = fangan.QuerySQL.Replace(item.Key, item.Value);
                }
            }
            DataTable dt = QuerySql(fangan.QuerySQL);
            if (dt != null && dt.Rows.Count >= 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    // 获得此模型的公共属性    
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertys)
                    {
                        var tempName = pi.Name;
                        if (dt.Columns.Contains(tempName))
                        {
                            // 判断此属性是否有Setter      
                            if (!pi.CanWrite) continue;
                            object value = dr[tempName];
                            if (value != DBNull.Value)
                            {
                                object defaultVal;
                                if (pi.PropertyType.Name.Equals("String"))
                                    defaultVal = "";
                                else if (pi.PropertyType.Name.Equals("Boolean"))
                                {
                                    defaultVal = false;
                                    value = (value.Equals("1") || value.Equals("on")).ToString();
                                }
                                else if (pi.PropertyType.Name.Equals("Decimal"))
                                    defaultVal = 0M;
                                else
                                    defaultVal = 0;

                                object obj = null;
                                if (!pi.PropertyType.IsGenericType)
                                    obj = string.IsNullOrEmpty(value.ToString())
                                        ? defaultVal
                                        : Convert.ChangeType(value, pi.PropertyType);
                                else
                                {
                                    Type genericTypeDefinition = pi.PropertyType.GetGenericTypeDefinition();
                                    if (genericTypeDefinition == typeof(Nullable<>))
                                        obj = string.IsNullOrEmpty(value.ToString())
                                            ? defaultVal
                                            : Convert.ChangeType(value, Nullable.GetUnderlyingType(pi.PropertyType));
                                }

                                pi.SetValue(t, obj, null);
                            }
                        }
                    }

                    ts.Add(t);
                }

                success = true;
            }

            returnObj = ts;
            return success;
        }

        /// <summary>
        /// 获取方案数据
        /// </summary>
        /// <param name="xiangMu">项目名</param>
        /// <param name="fangAnMing">方案名</param>
        /// <param name="returnObj">返回对象</param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="dicCanShu">方案参数</param>
        /// <returns></returns>
        [Obsolete("该方法没有启用缓存，建议使用GetShuJuZD")]
        public bool GetFangAnData(string xiangMu, string fangAnMing, out DataTable returnObj, out string errMsg, Dictionary<string, string> dicCanShu = null)
        {
            bool success = false;
            errMsg = "";
            var fangan = GetFanAn(xiangMu, fangAnMing);
            DataTable dt = QuerySql(fangan.QuerySQL);
            if (dt == null || dt.Rows.Count < 1)
            {
                success = false;
            }
            returnObj = dt;
            return success;
        }

        /// <summary>
        /// 替换公用参数
        /// </summary>
        private string TiHuanGongYongCanShu(string sql)
        {
            sql = sql.Replace("@52", HISClientHelper.YUANQUID);
            sql = sql.Replace("@53", HISClientHelper.XITONGID);
            sql = sql.Replace("@54", HISClientHelper.KUCUNYYID);
            sql = sql.Replace("@55", HISClientHelper.USERID);
            sql = sql.Replace("@56", HISClientHelper.ZHIGONGGH);
            sql = sql.Replace("@57", HISClientHelper.ZHIGONGGH);
            sql = sql.Replace("@58", HISClientHelper.KESHIID);
            sql = sql.Replace("@59", HISClientHelper.BINGQUID);
            return sql;
        }
    }
}
