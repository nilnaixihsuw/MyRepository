using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    #region 枚举定义

    /// <summary>
    /// 处理文本方式
    /// </summary>
    public enum ProcessText
    {
        /// <summary>
        /// 输入一个字符后后需要继续执行后面的处理
        /// </summary>
        [Description("需要继续处理")]
        YES = 0,

        /// <summary>
        /// 不处理当前输入内容
        /// </summary>
        [Description("不处理当前输入内容")]
        NO = 1,

        /// <summary>
        /// 后续输入都不处理
        /// </summary>
        [Description("后续输入都不处理")]
        ALLNO = 2
    }

    /// <summary>
    /// 输入框编辑模式
    /// </summary>
    public enum EditStyle
    {
        /// <summary>
        /// 未设置时默认为下拉
        /// </summary>
        [Description("未设置时默认为下拉")]
        Default = 9,

        /// <summary>
        /// 下拉列表、不可输入
        /// </summary>
        [Description("下拉列表、不可输入")]
        DropDownList = 0,

        /// <summary>
        /// 只能输入
        /// </summary>
        [Description("只能输入")]
        Input = 1,

        /// <summary>
        /// 可下拉、可输入
        /// </summary>
        [Description("可下拉、可输入")]
        Both = 2
    }

    #endregion 枚举定义

    /// <summary>
    /// 控件仓储，存储相关属性配置等信息
    /// </summary>
    [UserRepositoryItem("RegisterMediGridLookUpEdit")]
    public class RepositoryItemMediGridLookUpEdit : RepositoryItemGridLookUpEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        #region 原控件自带内容
        static RepositoryItemMediGridLookUpEdit()
        {
            RegisterMediGridLookUpEdit();
        }

        public const string CustomEditName = "MediGridLookUpEdit";

        /// <summary>
        /// 构造函数
        /// </summary>
        ///
        public RepositoryItemMediGridLookUpEdit()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                //F4快捷键冲突  [HR6-2000] 【市二门诊】增加接诊界面中快捷键功能
                this.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
                this.NullText = "";
                this.BeforePopup += RepositoryItemMediGridLookUpEdit_BeforePopup;
                this.Popup += RepositoryItemMediGridLookUpEdit_Popup;
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }


        private void RepositoryItemMediGridLookUpEdit_BeforePopup(object sender, EventArgs e)
        {
            Form CurrentControlParentFrm = ((MediGridLookUpEdit)sender).FindForm();
            if (this.MediinfoIMEMode == MediInfoImeMode.CHS)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                    short[] langIDs = TSFFacade.GetLangIDs();
                    if (langIDs.Length > 0)
                    {
                        string[] list = TSFFacade.GetInputMethodList(langIDs[0]);
                        if (langIDs != null)
                        {
                            if (list.Length > 0)
                                TSFFacade.ActiveInputMethodWithDesc(langIDs[0], list[0]);

                        }
                    }
                    if (CurrentControlParentFrm != null)
                        CurrentControlParentFrm.ImeMode = ImeMode.On;
                }
                else
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    int iMode = 1025;
                    int iSentence = 0;
                    IMMModeHelper.ImmSetConversionStatus(IMMModeHelper.intPtr, iMode, iSentence);
                }
            }
            else if (this.MediinfoIMEMode == MediInfoImeMode.EN)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                    short[] langIDs = TSFFacade.GetLangIDs();
                    if (langIDs.Length > 0)
                    {
                        string[] list = TSFFacade.GetInputMethodList(langIDs[0]);
                        if (langIDs != null)
                        {
                            if (list.Length > 0)
                                TSFFacade.ActiveInputMethodWithDesc(langIDs[0], list[0]);
                        }
                    }
                    if (CurrentControlParentFrm != null)
                        CurrentControlParentFrm.ImeMode = ImeMode.Disable;
                }
                else
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    int iMode = 0;
                    int iSentence = 0;
                    IMMModeHelper.ImmSetConversionStatus(IMMModeHelper.intPtr, iMode, iSentence);
                }
            }
            else
            {

                if (OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows8 && OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows10)
                {
                    int globalconversion = IMMModeHelper.globalconversion;
                    int globalsentence = IMMModeHelper.globalsentence;

                    IMMModeHelper.ImmGetConversionStatus(IMMModeHelper.intPtr, ref globalconversion, ref globalsentence);
                    IMMModeHelper.globalconversion = globalconversion;
                    IMMModeHelper.globalsentence = globalsentence;
                }
            }
        }

        private void RepositoryItemMediGridLookUpEdit_Popup(object sender, EventArgs e)
        {
            Form CurrentControlParentFrm = ((MediGridLookUpEdit)sender).FindForm();
            if (this.MediinfoIMEMode == MediInfoImeMode.CHS)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                    short[] langIDs = TSFFacade.GetLangIDs();
                    if (langIDs.Length > 0)
                    {
                        string[] list = TSFFacade.GetInputMethodList(langIDs[0]);
                        if (langIDs != null)
                        {
                            if (list.Length > 0)
                                TSFFacade.ActiveInputMethodWithDesc(langIDs[0], list[0]);
                        }
                    }
                    if (CurrentControlParentFrm != null)
                        CurrentControlParentFrm.ImeMode = ImeMode.On;
                }
                else
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    int iMode = 1025;
                    int iSentence = 0;
                    IMMModeHelper.ImmSetConversionStatus(IMMModeHelper.intPtr, iMode, iSentence);
                }
            }
            else if (this.MediinfoIMEMode == MediInfoImeMode.EN)
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {
                    short[] langIDs = TSFFacade.GetLangIDs();
                    if (langIDs.Length > 0)
                    {
                        string[] list = TSFFacade.GetInputMethodList(langIDs[0]);
                        if (langIDs != null)
                        {
                            if (list.Length > 0)
                                TSFFacade.ActiveInputMethodWithDesc(langIDs[0], list[0]);
                        }
                    }
                    if (CurrentControlParentFrm != null)
                        CurrentControlParentFrm.ImeMode = ImeMode.Disable;
                }
                else
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    int iMode = 0;
                    int iSentence = 0;
                    IMMModeHelper.ImmSetConversionStatus(IMMModeHelper.intPtr, iMode, iSentence);
                }
            }
            else
            {
                if (OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows8 && OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows10)
                {
                    int globalconversion = IMMModeHelper.globalconversion;
                    int globalsentence = IMMModeHelper.globalsentence;
                    IMMModeHelper.ImmSetConversionStatus(IMMModeHelper.intPtr, globalconversion, globalsentence);
                }
            }
        }


        /// <summary>
        /// 编辑对象名
        /// </summary>
        public override string EditorTypeName => CustomEditName;

        public static void RegisterMediGridLookUpEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediGridLookUpEdit), typeof(RepositoryItemMediGridLookUpEdit), typeof(MediGridLookUpEditViewInfo), new MediGridLookUpEditPainter(), true, img));
        }

        #endregion 原控件自带内容

        #region 重写原来方法或事件

        /// <summary>
        /// 对象拷贝
        /// </summary>
        /// <param name="item"></param>
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediGridLookUpEdit source = item as RepositoryItemMediGridLookUpEdit;
                if (source == null)
                {
                    return;
                }
                else
                {
                    //存储控件中的部分新增属性内容
                    this.UserDefinedChanging = source.UserDefinedChanging;
                    this.EditStyle = source.EditStyle;
                    if (string.IsNullOrEmpty(this.QuerySql))
                    {
                        this.QuerySql = source.QuerySql;
                    }
                    if (string.IsNullOrEmpty(this.Sql))
                    {
                        this.Sql = source.Sql;
                    }
                    this.ProfixerText = source.ProfixerText;
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        /// <summary>
        /// 获取显示文本
        /// </summary>
        /// <param name="format"></param>
        /// <param name="editValue"></param>
        /// <returns></returns>
        public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue)
        {
            var text = base.GetDisplayText(format, editValue);

            if ((text == "[EditValue is null]" || string.IsNullOrEmpty(text)) && editValue != null && !string.IsNullOrEmpty(editValue.ToString()) && editValue.ToString() != "System.Object")
            {
                text = GetBindText(editValue.ToString());
            }
            return text;
        }

        /// <summary>
        /// 根据Value值获取对应显示值
        /// </summary>
        /// <param name="KeyText"></param>
        /// <returns></returns>
        public string GetBindText(string KeyText)
        {
            string returnValue = KeyText;
            if (this.BindDataSource != null)
            {
                if (this.BindDataSource is Hashtable ht && ht.ContainsKey(KeyText))
                {
                    returnValue = ht[KeyText].ToString();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 获取数据源行数
        /// </summary>
        /// <returns></returns>
        internal int GetSourceCount()
        {
            int count = 0;
            if (this.DataSource != null)
            {
                if (this.DataSource.GetType().Name.Contains("Dictionary"))
                {
                    count = ((IDictionary)this.DataSource).Count;
                }
                else if (this.DataSource.GetType().Name.Contains("List"))
                {
                    count = ((IList)this.DataSource).Count;
                }
                else if (this.DataSource.GetType() == typeof(DataTable))
                {
                    count = ((DataTable)this.DataSource).Rows.Count;
                }
            }
            return count;
        }

        /// <summary>
        /// 建立弹出框gridcontrol控件
        /// </summary>
        /// <returns></returns>
        protected override GridControl CreateGrid()
        {
            //return base.CreateGrid();
            MediGridControl control = new MediGridControl();
            control.Dock = DockStyle.None;
            return control;
        }

        #endregion 重写原来方法或事件

        #region 方案相关内容

        /// <summary>
        /// 外部添加负责方案类
        /// </summary>
        /// <param name="param"></param>
        public void AddPorjectByParam(E_GY_FANGANPZ_INPARM param)
        {
            this.FangAnParm = param;
        }

        /// <summary>
        /// 执行获取到方案的具体信息
        /// </summary>
        public void ExecuteParm()
        {
            string fangAnMing = "";
            string xiangMuMing = "";
            try
            {
                if (this.IsClearProject)
                {
                    ClearProjectXM();
                }
                if (this.FangAnParm == null)
                {
                    return;
                }
                if (this.FangAnParm.XIANGMU != null)
                {
                    for (int i = 0; i < this.FangAnParm?.XIANGMU?.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.FangAnParm.XIANGMU[i]))
                        {
                            fangAnMing = this.FangAnParm.XIANGMU[i];
                            xiangMuMing = this.FangAnParm.FANGANMING[i];
                            ExecutePorject(this.FangAnParm.XIANGMU[i], this.FangAnParm.FANGANMING[i]);
                        }
                    }
                }
                if (this.FangAnParm?.CANSHU != null)
                {
                    var array = this.FangAnParm.CANSHU;
                    Dictionary<string, string> inParamDic = new Dictionary<string, string>();
                    for (var i = 0; i < array.Count; i++)
                    {
                        if (this.FangAnParm.CANSHU[i].IsNullOrWhiteSpace()) continue;
                        string[] RuCan = this.FangAnParm.CANSHU[i].Split('|');
                        for (var j = 0; j < RuCan.Length; j++)
                        {
                            string key = "";
                            string value = "";
                            if (!string.IsNullOrWhiteSpace(RuCan[j]))
                            {
                                key = "@" + (j + 1).ToString().PadLeft(2, '0');
                                value = RuCan[j];
                            }
                            else
                            {
                                key = "@" + (j + 1).ToString().PadLeft(2, '0');
                                value = "";
                            }

                            if (!inParamDic.ContainsKey(key))
                            {
                                inParamDic.Add(key, value);
                            }
                        }
                    }
                    ExecuteQueryParam(inParamDic);
                }
                else if (this.FangAnParm.DicCanShu != null)
                {
                    ExecuteQueryParam(this.FangAnParm.DicCanShu);
                }
                else
                {
                    ExecuteQueryParam(new Dictionary<string, string>());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("执行方案【" + fangAnMing + "】项目【" + xiangMuMing + "】时错误，信息：" + ex.Message + " \r\n堆栈信息：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 添加单个方案
        /// </summary>
        /// <param name="xiangMu">项目名</param>
        /// <param name="fangAnMing">方案名</param>
        public void AddPorject(string xiangMu, string fangAnMing)
        {
            this.DataSource = null;
            if (this.FangAnParm == null)
            {
                E_GY_FANGANPZ_INPARM parm = new E_GY_FANGANPZ_INPARM();
                parm.XIANGMU = new List<string>() { xiangMu };
                parm.FANGANMING = new List<string>() { fangAnMing };
                this.FangAnParm = parm;
            }
            else
            {
                //当项目名和方案名一样时不添加
                if (!this.FangAnParm.XIANGMU.Contains(xiangMu) || !this.FangAnParm.FANGANMING.Contains(fangAnMing))
                {
                    this.FangAnParm.XIANGMU.Add(xiangMu);
                    this.FangAnParm.FANGANMING.Add(fangAnMing);
                }
            }
        }

        /// <summary>
        /// 内部执行得到方案的具体配置信息
        /// </summary>
        /// <param name="xiangMu"></param>
        /// <param name="fangAnMing"></param>
        private void ExecutePorject(string xiangMu, string fangAnMing)
        {
            ControlsQuery query = new ControlsQuery();
            // 用缓存的方式查询方案配置信息
            FanganPeizhi fanganPeizhi = query.GetFanAn(xiangMu, fangAnMing, this.IsAllLoad);
            // 如果返回null直接返回
            if (fanganPeizhi == null)
                return;

            if (this.IsUnionAll)
                this.Sql = (string.IsNullOrEmpty(this.Sql) ? "" : this.Sql + " union all ") + fanganPeizhi.QuerySQL;
            else
                this.Sql = fanganPeizhi.QuerySQL;

            this.QuerySql = this.Sql;
            // 当添加多个方案时，后面方案的配置不改变前面方案的内容，只sql语句变化
            // 当方案参数改变时也要执行下面操作，不然弹出框的显示列信息没有，把所有列都查出来了
            this.XiangMuMing = xiangMu;
            this.TextEditStyle = TextEditStyles.Standard;
            this.ImmediatePopup = true;
            this.AutoComplete = false;
            //绑定值列
            if (string.IsNullOrEmpty(this.ValueMember))
            {
                this.ValueMember = fanganPeizhi.ShiJiLMC;
                if (string.IsNullOrEmpty(this.FangAnParm.ValueMember))
                {
                    this.FangAnParm.ValueMember = this.ValueMember;
                }
            }

            //绑定显示列
            if (string.IsNullOrEmpty(this.DisplayMember))
            {
                this.DisplayMember = fanganPeizhi.XianShiLMC;
                if (string.IsNullOrEmpty(this.FangAnParm.DisplayMember))
                {
                    this.FangAnParm.DisplayMember = this.DisplayMember;
                }
            }
            this.XianShiLie = fanganPeizhi.XianShiLMC;
            this.ColumnIndex = fanganPeizhi.ColumnIndex;

            this.IsPeiZhiCX = true;
            this.IsGuoLv = fanganPeizhi.IsGuoLv;
            this.FilterField = fanganPeizhi.FilterField;
            this.FilterType = fanganPeizhi.FilterType;
            this.OrderList = fanganPeizhi.OrderList;

            //弹出框需显示的列信息设置
            if (fanganPeizhi.ColumnInfo.Count > 0)
            {
                this.View.Columns.Clear();

                for (int i = 0; i < fanganPeizhi.ColumnInfo.Count; i++)
                {
                    GridColumn columnInfo = new GridColumn();
                    columnInfo.FieldName = fanganPeizhi.ColumnInfo[i][0];
                    if (fanganPeizhi.ColumnInfo[i][1] != "")
                    {
                        columnInfo.Caption = fanganPeizhi.ColumnInfo[i][1];
                    }
                    columnInfo.Width = Convert.ToInt32(fanganPeizhi.ColumnInfo[i][2]);
                    columnInfo.VisibleIndex = i;
                    columnInfo.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
                    columnInfo.AppearanceCell.Options.UseFont = true;
                    columnInfo.Name = this.ProfixerText + "|" + columnInfo.FieldName;
                    this.View.Columns.Add(columnInfo);
                }

                //需排序的列未在显示列中，添加对应列并设为不可见
                if (fanganPeizhi.OrderList != null)
                {
                    foreach (string[] item in fanganPeizhi.OrderList)
                    {
                        var col = this.View.Columns.Where(o => o.FieldName == item[0]);
                        if (!col.Any())
                        {
                            var columnInfo = new GridColumn
                            {
                                FieldName = item[0],
                                VisibleIndex = 100,
                                Visible = false
                            };
                            columnInfo.Name = "$|" + columnInfo.FieldName;
                            this.View.Columns.Add(columnInfo);
                        }
                    }
                }
            }

            this.PopformWidth = fanganPeizhi.PopformWidth;
            this.IsJiXuZX = ProcessText.YES;
            if (this.EditStyle == EditStyle.DropDownList)
            {
                this.TextEditStyle = TextEditStyles.DisableTextEditor;
            }
            else if (this.EditStyle == Controls.EditStyle.Both)
            {
            }
            else
            {
                this.EditStyle = Controls.EditStyle.Input;
            }
            if (this.ProFixFangAn == null && string.IsNullOrEmpty(this.ProfixerText))
            {
                var dic = new Dictionary<string, object[]>
                {
                    {"*", new object[1] {(this.Tag as GridLookUpEditConfig)?.Clone()}}
                };
                this.ProfixerText = "*";
                this.ProFixFangAn = dic;
            }
        }

        /// <summary>
        /// 设置清除项目方案内容
        /// </summary>
        public void ClearProject()
        {
            this.ValueMember = null;
            ClearProjectXM();
            this.IsClearProject = true;
        }

        /// <summary>
        /// 内部处理执行清除处理
        /// </summary>
        private void ClearProjectXM()
        {
            this.Sql = "";
            this.QuerySql = "";
            this.ColumnIndex = null;
            this.View.Columns.Clear();
            this.DataSource = null;
            this.BindDataSource = null;
            this.IsClearProject = false;
            this.PopformWidth = 0;
        }

        /// <summary>
        /// 设置需要替换方案sql语句的参数对应值
        /// </summary>
        /// <param name="param"></param>
        public void SetQueryParam(Dictionary<string, string> param)
        {
            if (this.FangAnParm != null)
            {
                this.FangAnParm.DicCanShu = param;
            }
            else
            {
                ExecuteQueryParam(param);
            }
        }

        /// <summary>
        /// 添加方案对应参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParam(string name, object value)
        {
            if (this.FangAnParm.DicCanShu == null)
            {
                var dic = new Dictionary<string, string>();
                dic.Add(name, value.ToString());
                this.FangAnParm.DicCanShu = dic;
            }
            else
            {
                if (this.FangAnParm.DicCanShu.ContainsKey(name))
                {
                    this.FangAnParm.DicCanShu[name] = value.ToString();
                }
                else
                {
                    this.FangAnParm.DicCanShu.Add(name, value.ToString());
                }
            }
        }

        /// <summary>
        /// 重置参数
        /// </summary>
        public void ResetParam()
        {
            if (this.FangAnParm != null)
            {
                this.FangAnParm.DicCanShu = null;
            }
        }

        /// <summary>
        /// 执行替换需要带入sql语句的参数
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteQueryParam(Dictionary<string, string> param)
        {
            string querysql = this.Sql;
            DataTable dt = this.DataSource as DataTable;
            if (!string.IsNullOrEmpty(querysql))
            {
                if (!param.ContainsKey("@01"))
                {
                    param.Add("@01", HISClientHelper.SHURUMLX);
                }
                if (!param.ContainsKey("@02"))
                {
                    param.Add("@02", "EditText");
                }

                //部分没传的默认参数为null
                for (int i = 3; i < 51; i++)
                {
                    if (!param.ContainsKey("@" + i.ToString().PadLeft(2, '0')))
                    {
                        param.Add("@" + i.ToString().PadLeft(2, '0'), null);
                    }
                }

                //当参数中有输入码类型时，将输入码类型做为过滤条件
                foreach (var item in param)
                {
                    if (dt != null && item.Value != null && dt.Columns.Contains(item.Value))
                    {
                        if (this.FilterField == null && item.Value == HISClientHelper.SHURUMLX)
                        {
                            this.FilterField = new string[] { item.Value };
                        }
                    }
                    querysql = querysql.Replace(item.Key, item.Value);
                }
            }
            this.QuerySql = querysql;
        }

        /// <summary>
        /// 设置绑定配置，通常在一列后台显示代码界面显示名字，例后台科室代码，界面显示科室名称
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="valueColumn"></param>
        /// <param name="displayColumn"></param>
        public void SetBindConfig(dynamic dataSource, string valueColumn, string displayColumn)
        {
            if (dataSource == null)
            {
                return;
            }
            Hashtable hashTable = new Hashtable();
            if (dataSource.GetType().Name == "List`1")
            {
                IEnumerable<object> list = dataSource as IEnumerable<object>;
                foreach (var dtItem in list)
                {
                    string text = dtItem.GetType().GetProperty(valueColumn).GetValue(dtItem, null).ToString();
                    if (!hashTable.ContainsKey(text))
                    {
                        hashTable.Add(text, dtItem.GetType().GetProperty(displayColumn).GetValue(dtItem, null).ToStringEx());
                    }
                }
            }
            else if (dataSource.GetType().Name == "DataTable")
            {
                DataTable dt = dataSource as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!hashTable.ContainsKey(dr[valueColumn].ToString()))
                    {
                        hashTable.Add(dr[valueColumn].ToString(), dr[displayColumn].ToString());
                    }
                }
            }
            else if (dataSource.GetType().Name.IndexOf("Dictionary") != -1)
            {
                foreach (var item in dataSource)
                {
                    if (!hashTable.ContainsKey(item.Value))
                    {
                        hashTable.Add(item.Value, item.Key);
                    }
                }
            }
            this.BindDataSource = hashTable;
            this.BindKey = valueColumn;
            this.BindValue = displayColumn;

            //如果是非方案设置的，直接设置对应数据源
            if (this.FangAnParm == null)
            {
                BindSource(dataSource, valueColumn, displayColumn);
            }
        }

        /// <summary>
        /// 设置绑定数据源，值列，显示列
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="valueColumn">值列</param>
        /// <param name="displayColumn">显示列</param>
        public void BindSource(object dataSource, string valueColumn, string displayColumn)
        {
            this.ValueMember = valueColumn;
            this.DisplayMember = displayColumn;
            if (dataSource.GetType().Name == "DataTable")
            {
                if (this.XianShiLie == null)
                {
                    this.XianShiLie = this.DisplayMember;
                }
                dataSource = CreateShuRuMaColumn(dataSource as DataTable);
            }


            this.DataSource = dataSource;
        }

        /// <summary>
        /// 生成输入码列(修改传入datasource类型，datatable添加输入码类型，其他处理FilterField，用户过滤数据)
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public DataTable CreateShuRuMaColumn(dynamic dataSource)
        {
            if (dataSource != null && dataSource.GetType().Name == "DataTable")
            {
                if (!(dataSource is DataTable dt))
                {
                    return new DataTable();
                }
                if (!string.IsNullOrWhiteSpace(HISClientHelper.SHURUMLX))
                {
                    if (HISClientHelper.SHURUMLX.ToUpper() == "SHURUMA2")
                    {
                        ShuRuMaColumn(dt, "SHURUMA2");
                    }
                    else if (HISClientHelper.SHURUMLX.ToUpper() == "SHURUMA3")
                    {
                        ShuRuMaColumn(dt, "SHURUMA3");
                    }
                    else
                    {
                        ShuRuMaColumn(dt, "SHURUMA1");
                    }

                }
                else
                {
                    ShuRuMaColumn(dt, "SHURUMA1");
                }

            }

            if (this.FilterField == null || !this.FilterField.Any())
            {
                if (this.EditStyle == EditStyle.Both)
                {
                    List<string> list = this.View.Columns.Select(o => o.FieldName).ToList();
                    list.Add(HISClientHelper.SHURUMLX);
                    this.FilterField = list.ToArray();
                }//如果为下拉的话，不赋值过滤项（会导致下拉的时候也进行过滤536031）
                else if (this.EditStyle == EditStyle.Input)
                {
                    this.FilterField = new string[] { HISClientHelper.SHURUMLX };
                }
            }
            else if (!this.FilterField.Contains(HISClientHelper.SHURUMLX))
            {
                List<string> filter = this.FilterField.ToList();
                filter.Add(HISClientHelper.SHURUMLX);
                this.FilterField = filter.ToArray();
            }
            //非datatable类型的只处理FilterField，也要返回datatable
            if (dataSource != null && dataSource.GetType().Name == "DataTable")
            {
                return dataSource;
            }

            return new DataTable();
        }

        private void ShuRuMaColumn(DataTable dt, string columnName)
        {
            if (!dt.Columns.Contains(columnName) && this.XianShiLie != null)
            {
                dt.Columns.Add(new DataColumn(columnName, typeof(string)));
                foreach (DataRow dr in dt.Rows)
                {
                    string neiRong = dr[this.XianShiLie].ToString();
                    ShuRuMaHelper.GetShuRuMa(neiRong.ToUpper(), out var SHURUMA1, out var SHURUMA2, out var SHURUMA3);
                    if (columnName == "SHURUMA1") dr[columnName] = SHURUMA1;
                    if (columnName == "SHURUMA2") dr[columnName] = SHURUMA2;
                    if (columnName == "SHURUMA3") dr[columnName] = SHURUMA3;
                }
            }
        }

        /// <summary>
        /// 用户自定义事件，输入框输入内容时会执行这个事件
        /// </summary>
        /// <param name="userDefinedChangingText"></param>
        public void OnUserDefinedChanging(string userDefinedChangingText)
        {
            if (this.UserDefinedChanging != null)
            {
                Console.WriteLine("\n字符" + userDefinedChangingText + "触发事件");
                this.UserDefinedChanging(this, new EventArgs());   //发出警报
            }
        }

        /// <summary>
        /// 下拉列表添加一个选项 例如数据源中加一个全选
        /// </summary>
        /// <param name="display">显示值</param>
        /// <param name="value">实际值</param>
        public void AddSelectItem(string display, string value)
        {
            if (AddSelectItemDic == null)
            {
                AddSelectItemDic = new Dictionary<string, string>();
            }
            if (AddSelectItemDic.ContainsKey(display))
            {
                AddSelectItemDic[display] = value;
            }
            else
            {
                AddSelectItemDic.Add(display, value);
            }
        }

        #endregion 方案相关内容

        #region 自定义属性

        //由于在ItemLookUpEdit中新定义的属性无法传值，特殊处理将值都统一存到原属性tag里面

        //1.声明关于事件的委托；
        public delegate void UserDefinedChangingEventHandler(object sender, EventArgs e);

        //2.声明事件；
        public event UserDefinedChangingEventHandler UserDefinedChanging;

        /// <summary>
        /// 方案查询SQL
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Sql
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).Sql;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).Sql = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { Sql = value };
                }
            }
        }

        /// <summary>
        /// 过滤的字段，即根据这个里面的字段名进行搜索过滤
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] FilterField
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).FilterField;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).FilterField = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { FilterField = value };
                }
            }
        }

        /// <summary>
        /// 过滤类型 内容中包含元素即开始位置匹配startwith,默认为contains
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] FilterType
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    //20200805 modi by zhengcj 添加用户自定义设置
                    string[] coll = (this.Tag as GridLookUpEditConfig).FilterType;
                    if (this.IsCache && !string.IsNullOrWhiteSpace(HISClientHelper.MoHuCX) && FilterField != null && FilterField.Length > 0)
                    {
                        string[] fts = new string[FilterField.Length];
                        string type = "";
                        if (HISClientHelper.MoHuCX.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                        {
                            type = "0";
                        }
                        else if (HISClientHelper.MoHuCX.Equals("false", StringComparison.CurrentCultureIgnoreCase))
                        {
                            type = "1";
                        }
                        if (type != "")
                        {
                            for (int i = 0; i < fts.Length; i++)
                            {
                                fts[i] = type
;
                            }
                            coll = fts;
                        }
                    }
                    return coll;
                }

                return null;
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).FilterType = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { FilterType = value };
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string QuerySql
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).QuerySql;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).QuerySql = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { QuerySql = value };
                }
            }
        }

        /// <summary>
        /// 方案项目数组
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object[] Param
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).Param;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).Param = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { Param = value };
                }
            }
        }

        /// <summary>
        /// 是否为配置查询项目，即通过方案配置的项目
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPeiZhiCX
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsPeiZhiCX;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsPeiZhiCX = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsPeiZhiCX = value };
                }
            }
        }


        /// <summary>
        /// 是否启用表格上下键属性(true：上下键能选中上下行 false：只能在方案得到信息中上下选择数据)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BiaoGeUpDown
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).BiaoGeUpDown;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).BiaoGeUpDown = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { BiaoGeUpDown = value };
                }
            }
        }



        /// <summary>
        /// 延迟间隔时间
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 YanChiJG
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).YanChiJG;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).YanChiJG = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { YanChiJG = value };
                }
            }
        }




        /// <summary>
        /// 是否unionall
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUnionAll
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsUnionAll;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsUnionAll = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsUnionAll = value };
                }
            }
        }

        /// <summary>
        /// 是否分页
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFenYe
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsFenYe;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsFenYe = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsFenYe = value };
                }
            }
        }
        /// <summary>
        /// 是否显示项目名称
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowFangAnMc
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).ShowFangAnMc;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).ShowFangAnMc = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { ShowFangAnMc = value };
                }
            }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageSize
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).PageSize;
                }
                else
                {
                    return 10;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).PageSize = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { PageSize = value };
                }
            }
        }

        /// <summary>
        /// 过滤后的总行数，用于底栏显示
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FilterRowCount
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).FilterRowCount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).FilterRowCount = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { FilterRowCount = value };
                }
            }
        }

        /// <summary>
        /// 弹出框宽度，用于设置弹出窗口的宽度
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PopformWidth
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).PopformWidth;
                }

                return 0;
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).PopformWidth = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { PopformWidth = value };
                }
            }
        }

        /// <summary>
        /// 弹出框宽度，用于设置弹出窗口的宽度
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PopformHeight
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).PopformHeight;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).PopformHeight = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { PopformHeight = value };
                }
            }
        }

        /// <summary>
        /// 是否需要进行过滤
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGuoLv
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsGuoLv;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsGuoLv = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsGuoLv = value };
                }
            }
        }

        /// <summary>
        /// 绑定数据源，这个数据源主要用来取对应实际代码的名称 例如后台存科室代码，界面要显示科室名称，通过此数据源进行转换
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object BindDataSource
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    if ((this.Tag as GridLookUpEditConfig).BindDataSource == null || (this.Tag as GridLookUpEditConfig).BindDataSource.GetType() == typeof(Int32))
                    {
                        (this.Tag as GridLookUpEditConfig).BindDataSource = new Hashtable();
                    }
                    return (this.Tag as GridLookUpEditConfig).BindDataSource;
                }
                else
                {
                    return new Hashtable();
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).BindDataSource = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { BindDataSource = value };
                }
            }
        }

        /// <summary>
        /// 绑定的代码字段名 例如 KESHIDM
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string BindKey
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).BindKey;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).BindKey = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { BindKey = value };
                }
            }
        }

        /// <summary>
        /// 代码值需要显示为对应列的值 ，例如KESHIMC
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string BindValue
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).BindValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).BindValue = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { BindValue = value };
                }
            }
        }

        /// <summary>
        /// 实际值列
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ItemValue
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).ItemValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).ItemValue = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { ItemValue = value };
                }
            }
        }

        /// <summary>
        /// 显示列所对应的列序号位置
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, int> ColumnIndex
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).ColumnIndex;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).ColumnIndex = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { ColumnIndex = value };
                }
            }
        }

        /// <summary>
        /// 特殊切换字符，用来标示当前方案的特殊字符
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProfixerText
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).ProfixerText;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).ProfixerText = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { ProfixerText = value };
                }
            }
        }

        /// <summary>
        /// 是否全部加载
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllLoad
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsAllLoad;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsAllLoad = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsAllLoad = value };
                }
            }
        }

        /// <summary>
        /// 是否不清除手工输入的字符
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNotClearValue
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsNotClearValue;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsNotClearValue = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsNotClearValue = value };
                }
            }
        }

        /// <summary>
        /// 排序字段列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string[]> OrderList
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).OrderList;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).OrderList = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { OrderList = value };
                }
            }
        }

        /// <summary>
        /// 显示列
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string XianShiLie
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).XianShiLie;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).XianShiLie = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { XianShiLie = value };
                }
            }
        }

        /// <summary>
        /// 方案的项目名
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string XiangMuMing
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).XiangMuMing;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).XiangMuMing = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { XiangMuMing = value };
                }
            }
        }

        /// <summary>
        /// 特殊方案字典，内部使用
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, object[]> ProFixFangAn
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).ProFixFangAn;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).ProFixFangAn = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { ProFixFangAn = value };
                }
            }
        }

        /// <summary>
        /// 当前方案，内部使用，
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DangQianFangAn
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).DangQianFangAn;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).DangQianFangAn = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { DangQianFangAn = value };
                }
            }
        }

        /// <summary>
        /// 是否清除当前项目方案
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsClearProject
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsClearProject;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsClearProject = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsClearProject = value };
                }
            }
        }

        /// <summary>
        /// 控件的操作方式 Default、默认为下拉选择方式 , DropDownList、下拉选择, Input、输入字符过滤方式  ，Both、即可下拉也可选择方式
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditStyle EditStyle
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).EditStyle;
                }
                else
                {
                    return EditStyle.Default;
                }
            }
            set
            {
                //处理下拉三角按钮，这个控件只有一个按钮
                foreach (var item in this.Buttons)
                {
                    ((EditorButton)item).Visible = value != EditStyle.Input;
                }

                if (value == EditStyle.DropDownList || value == EditStyle.Default)
                {
                    this.TextEditStyle = TextEditStyles.DisableTextEditor;
                }
                else if (value == EditStyle.Both)
                {
                    this.TextEditStyle = TextEditStyles.Standard;
                }
                else
                {
                    this.TextEditStyle = TextEditStyles.Standard;
                }
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).EditStyle = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { EditStyle = value };
                }
            }
        }

        /// <summary>
        /// 是否继续执行  YES 、需要继续执行下面操作 ， NO、本次不执行（通常在输入切换方案的特殊符号的时候） ，ALLNO、 后续输入的都不执行后续操作（通常在需要有方案又需要自由输入时使用）        ///
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProcessText IsJiXuZX
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsJiXuZX;
                }
                else
                {
                    return ProcessText.YES;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsJiXuZX = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsJiXuZX = value };
                }
            }
        }

        /// <summary>
        /// 选中项目后焦点是否跳到下一个控件，默认为是 ，特殊情况自己设置选中后跳到非顺序编辑列需设此项为否
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelectedToNextControl
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsSelectedToNextControl;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsSelectedToNextControl = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsSelectedToNextControl = true };
                }
            }
        }

        /// <summary>
        /// 缓存方案参数
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public E_GY_FANGANPZ_INPARM FangAnParm
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).FangAnParm;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).FangAnParm = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { FangAnParm = value };
                }
            }
        }

        /// <summary>
        /// 当前方案的列信息
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<GridColumn> GridColumnList
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).GridColumnList;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).GridColumnList = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { GridColumnList = value };
                }
            }
        }

        /// <summary>
        /// 当前输入的字符
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurrentKeyChar
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).CurrentKeyChar;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).CurrentKeyChar = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { CurrentKeyChar = value };
                }
            }
        }

        /// <summary>
        /// 添加选择项 内容为 显示值|实际值
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary<string, string> AddSelectItemDic
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    var item = (this.Tag as GridLookUpEditConfig).AddSelectItem;
                    if (item == null)
                    {
                        (this.Tag as GridLookUpEditConfig).AddSelectItem = new Dictionary<string, string>();
                    }
                    return (this.Tag as GridLookUpEditConfig).AddSelectItem;
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).AddSelectItem = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { AddSelectItem = value };
                }
            }
        }

        /// <summary>
        /// 是否添加空选项
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAddNullItem
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsAddNullItem;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsAddNullItem = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsAddNullItem = value };
                }
                if (value)
                {
                    if (!AddSelectItemDic.ContainsKey(""))
                    {
                        AddSelectItem("", "");
                    }
                }
            }
        }
        /// <summary>
        /// 下拉框是否显示行号
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsShowIndexNumber
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsShowIndexNumber;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsShowIndexNumber = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsShowIndexNumber = value };
                }
            }
        }

        //20200727 add by zhengcj begin
        /// <summary>
        /// 是否启用缓存数据源
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCache
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).IsCache;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).IsCache = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { IsCache = value };
                }
            }
        }

        /// <summary>
        /// 缓存数据源
        /// </summary>
        public object CacheDataSource
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    return (this.Tag as GridLookUpEditConfig).CacheDataSource;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "GridLookUpEditConfig")
                {
                    (this.Tag as GridLookUpEditConfig).CacheDataSource = value;
                }
                else
                {
                    this.Tag = new GridLookUpEditConfig() { CacheDataSource = value };
                }
            }
        }
        //20200727 add by zhengcj end


        public class GridLookUpEditConfig : ICloneable
        {
            /// <summary>
            /// 过滤字段
            /// </summary>
            public string[] FilterField { get; set; }

            /// <summary>
            /// 过滤类型
            /// </summary>
            public string[] FilterType { get; set; }

            /// <summary>
            /// 原始SQL
            /// </summary>
            public string Sql { get; set; }

            /// <summary>
            /// 查询SQL
            /// </summary>
            public string QuerySql { get; set; }

            /// <summary>
            /// 查询入参
            /// </summary>
            public object[] Param { get; set; }

            /// <summary>
            /// 是否为配置查询的
            /// </summary>
            public bool IsPeiZhiCX { get; set; }

            /// <summary>
            /// 选中某个选项后是否直接跳到下一个控件，默认是的
            /// </summary>
            public bool IsSelectedToNextControl = true;

            /// <summary>
            /// 是否为配置查询的
            /// </summary>
            public int FilterRowCount { get; set; }

            /// <summary>
            /// 弹出窗口宽度
            /// </summary>
            public int PopformWidth { get; set; }

            /// <summary>
            /// 弹出窗口高度
            /// </summary>
            public int PopformHeight { get; set; }

            /// <summary>
            /// 是否使用过滤  当查询字段中包含过滤字段时使用过滤，否则使用查询
            /// </summary>
            public bool IsGuoLv { get; set; }

            /// <summary>
            /// 绑定用数据源
            /// </summary>
            public object BindDataSource { get; set; }

            /// <summary>
            /// 绑定用Key
            /// </summary>
            public string BindKey { get; set; }

            /// <summary>
            /// 绑定用Value
            /// </summary>
            public string BindValue { get; set; }

            /// <summary>
            /// 保存列名对应的索引,根据名称取值时使用
            /// </summary>
            public Dictionary<string, int> ColumnIndex { get; set; }

            /// <summary>
            /// 弹出框关闭时的返回值
            /// </summary>
            public string ItemValue { get; set; }

            /// <summary>
            /// 过滤内容串
            /// </summary>
            public string ProfixerText { get; set; }

            /// <summary>
            /// 是否全加载，即添加方案时是否将数据全部加载，在点击按钮的时候就有数据，非全加载时是在输第一个字符时进行过滤
            /// </summary>
            public bool IsAllLoad { get; set; }

            /// <summary>
            /// 输入数据在数据源中没找到对应数据时，允许输入数据直接保存，不清空对应内容
            /// </summary>
            public bool IsNotClearValue { get; set; }

            /// <summary>
            /// 排序列表
            /// </summary>
            public List<string[]> OrderList { get; set; }

            /// <summary>
            /// 显示列
            /// </summary>
            public string XianShiLie { get; set; }

            /// <summary>
            /// 方案项目名
            /// </summary>
            public string XiangMuMing { get; set; }

            /// <summary>
            /// 存存多方案信息字典
            /// </summary>
            public Dictionary<string, object[]> ProFixFangAn { get; set; }

            /// <summary>
            /// 当前方案
            /// </summary>
            public string DangQianFangAn { get; set; }

            /// <summary>
            /// 回调事件
            /// </summary>
            public List<string> UserDefinedChangingList { get; set; }//需要触发回调事件的内容

            /// <summary>
            /// 当前输入内容
            /// </summary>
            public string CurrentKeyChar { get; set; }//当前输入内容

            /// <summary>
            /// 输入一个内容后，是否继续执行下面的查方案，在方案输入项中需要输普通内容时使用
            /// </summary>
            public ProcessText IsJiXuZX { get; set; }//是否继续执行

            /// <summary>
            /// 是否清除项目内容
            /// </summary>
            public bool IsClearProject { get; set; }//是否继续执行

            /// <summary>
            /// 方案参数，用于内部存储
            /// </summary>
            public E_GY_FANGANPZ_INPARM FangAnParm { get; set; } //内部存方案

            /// <summary>
            /// 显示字段列表
            /// </summary>
            public List<GridColumn> GridColumnList { get; set; }

            /// <summary>
            /// 输入方式样式
            /// </summary>
            public EditStyle EditStyle { get; set; }

            /// <summary>
            /// 延迟间隔时间
            /// </summary>
            public Int32 YanChiJG { get; set; }

            /// <summary>
            /// 是否分页
            /// </summary>
            public bool IsFenYe { get; set; }

            /// <summary>
            /// 是否unionall
            /// </summary>
            public bool IsUnionAll { get; set; }

            /// <summary>
            /// 是否启用表格上下键属性
            /// </summary>
            public bool BiaoGeUpDown { get; set; }

            /// <summary>
            /// 每页显示数据条数
            /// </summary>
            public int PageSize { get; set; }

            /// <summary>
            /// 数据源添加项
            /// </summary>
            public Dictionary<string, string> AddSelectItem { get; set; }

            /// <summary>
            /// 克隆
            /// </summary>
            /// <returns></returns>
            public Object Clone()
            {
                GridLookUpEditConfig newPerson = (GridLookUpEditConfig)this.MemberwiseClone();//先调用默认的复制机制
                return newPerson;
            }

            /// <summary>
            /// 是否添加空选项
            /// </summary>
            public bool IsAddNullItem { get; set; }

            /// <summary>
            /// 下拉框是否显示行号
            /// </summary>
            public bool IsShowIndexNumber { get; set; }

            //add by zhengcj
            public bool IsCache { get; set; }

            //add by zhengcj
            public object CacheDataSource { get; set; }
            /// <summary>
            /// 是否显示项目名称
            /// </summary>
            public bool ShowFangAnMc { get; set; } = true;
        }

        #endregion 自定义属性
    }

    /// <summary>
    /// 控件主设置
    /// </summary>
    [ToolboxItem(true)]
    public class MediGridLookUpEdit : GridLookUpEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        #region 控件初始设置

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression
        {
            get; set;
        }

        static MediGridLookUpEdit()
        {
            RepositoryItemMediGridLookUpEdit.RegisterMediGridLookUpEdit();
        }

        private bool focusAllText = true;
        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get { return focusAllText; } set { focusAllText = value; } }

        /// <summary>
        /// 当前页面
        /// </summary>
        public int curPage = 1;

        private int totalCount = 0;
        /// <summary>
        /// 分页时的数据总数
        /// </summary>
        [Description("分页时的数据总数"), DefaultValue(0), Browsable(false)]
        public int TotalCount { get { return totalCount; } set { totalCount = value; } }


        private bool enter = false, needSelect = false;

        /// <summary>
        /// 当前按键是否是回车键
        /// </summary>
        private bool IsKeyEnter = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }
        /// <summary>
        /// 启用中文输入
        /// </summary>
        public bool enableCHS = false;
        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public MediGridLookUpEdit()
        {

            if (!SkinCat.Instance.IsDesignMode)
            {
                Init();
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }

        }



        /// <summary>
        /// 初始化一些本地内容
        /// </summary>
        public void Init()
        {
            this.MouseUp -= MediGridLookUpEdit_MouseUp;
            this.MouseUp += MediGridLookUpEdit_MouseUp;
            this.Enter -= MediGridLookUpEdit_Enter;
            this.Enter += MediGridLookUpEdit_Enter;
            this.MouseDown -= MediGridLookUpEdit_MouseDown;
            this.MouseDown += MediGridLookUpEdit_MouseDown;
            this.Properties.NullText = "";
            this.EnterMoveNextControl = true;
            this.MinimumSize = new Size(0, 26);
            this.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(Keys.None);
            this.SetEditorsCustomSkin();
        }
       
        private void MediGridLookUpEdit_Enter(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                if (this.FindForm().IsHandleCreated)
                {
                    enter = true;
                    BeginInvoke(new MethodInvoker(ResetEnterFlag));
                    if (this.Properties.TextEditStyle == TextEditStyles.DisableTextEditor && !this.Enabled && ReadOnly)
                        BeginInvoke(new MethodInvoker(delegate { ((GridLookUpEdit)sender).ShowPopup(); }));
                    else if (this.Properties.TextEditStyle == TextEditStyles.Standard)
                    {
                        BeginInvoke(new MethodInvoker(delegate { this.SelectAll(); }));
                    }
                }
            }

        }

        private void MediGridLookUpEdit_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private void MediGridLookUpEdit_MouseUp(object sender, MouseEventArgs e)
        {

            if (needSelect)
                if ((sender as MediGridLookUpEdit).Properties.TextEditStyle == TextEditStyles.Standard)
                    (sender as TextEdit).SelectAll();

        }


        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
            {
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                this.Enabled = false;
            }
            else
            {
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.Enabled = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediGridLookUpEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediGridLookUpEdit;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediGridLookUpEdit.CustomEditName;
            }
        }

        /// <summary>
        /// 当前行索引
        /// </summary>
        private int currentRowIndex = 0;


        #endregion 控件初始设置

        #region 重写原方法或事件

        /// <summary>
        /// 弹出窗口界面
        /// </summary>
        /// <returns></returns>
        protected override PopupBaseForm CreatePopupForm()
        {
            var PopupForm = new MediGridLookUpEditPopupForm(this);
            return PopupForm;
        }

        /// <summary>
        /// 点击鼠标时触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            int total = 0;
            // 当控件为下拉模式时，当前控件宽度大于弹出框宽度时，弹出框宽度为控件宽度
            if ((this.Properties.EditStyle == EditStyle.Default || this.Properties.EditStyle == EditStyle.DropDownList || this.Properties.EditStyle == EditStyle.Both))
            {
                if (this.Width > this.Properties.PopformWidth)
                {
                    this.Properties.PopupFormMinSize = new Size() { Width = this.Width, Height = this.Properties.PopupFormMinSize.Height };
                    if (this.Properties.PopformWidth == 0)
                    {
                        this.Properties.PopformWidth = this.Width;
                    }
                }

                //在鼠标点击右侧箭头时查询数据(去除在gotfocus时查询数据)
                if (this.Properties.DataSource == null && this.Properties.IsPeiZhiCX && e.X > this.Width - 21 && !string.IsNullOrWhiteSpace(this.Text))
                {
                    ControlsQuery query = new ControlsQuery();
                    string sql = "";
                    if (this.Properties.QuerySql != null && this.Properties.QuerySql.Length > 10)
                    {
                        sql = this.Properties.QuerySql.Replace("EditText", "%");
                    }
                    else if (!string.IsNullOrWhiteSpace(this.Sql))//gxl 添加这个条件，避免this.Sql为空的情况
                    {
                        sql = this.Sql.Replace("EditText", "%");
                    }

                    if (sql != "")
                    {
                        DataTable dt = query.QuerySql(sql);
                        dt = this.Properties.CreateShuRuMaColumn(dt);
                        if (dt == null || dt.Rows.Count < 1)
                        {
                            this.Properties.DataSource = null;
                            this.Properties.FilterRowCount = 0;
                            return;
                        }
                        this.Properties.DataSource = dt;
                    }
                }

                if (this.Text == "" && (e.X > this.Width - 21 || this.Properties.EditStyle != EditStyle.Both))
                {
                    this.BeginUpdate();
                    if (this.Properties.IsPeiZhiCX)
                    {
                        if (this.Properties.ColumnIndex != null)
                        {
                            if (this.Properties.ColumnIndex.Where(o => o.Key == this.Properties.ValueMember.ToString()).ToList().Count < 1)
                            {
                                MediMsgBox.Warn("请注意，您的输入框数据源中不包含所设置的ValueMember对应列【" + this.Properties.ValueMember + "】");
                            }
                            else if (this.Properties.ColumnIndex.Where(o => o.Key == this.Properties.DisplayMember.ToString()).ToList().Count < 1)
                            {
                                MediMsgBox.Warn("请注意，您的输入框数据源中不包含所设置的DisplayMember对应列【" + this.Properties.DisplayMember + "】");
                            }
                        }

                        #region 处理方案查询语句并获得结果

                        ControlsQuery query = new ControlsQuery();
                        string sql = "";
                        if (this.Properties.QuerySql != null && this.Properties.QuerySql.Length > 10)
                        {
                            sql = this.Properties.QuerySql.Replace("EditText", "%");
                        }
                        else if (!string.IsNullOrEmpty(Sql))
                        {
                            if (this.Sql != null)
                                sql = this.Sql.Replace("EditText", "%");
                        }

                        if (!string.IsNullOrEmpty(sql))
                        {
                            DataTable dt = new DataTable();
                            if (this.Properties.IsFenYe)
                            {
                                dt = query.QueryPageSql(sql, 1, this.Properties.PageSize, out total);
                                this.totalCount = total;
                            }
                            else
                            {
                                dt = query.QuerySql(sql);
                            }

                            dt = this.Properties.CreateShuRuMaColumn(dt);
                            if (dt == null || dt.Rows.Count < 1)
                            {
                                this.Properties.DataSource = null;
                                this.Properties.FilterRowCount = 0;
                            }
                            if (this.Properties.EditStyle == EditStyle.Default || this.Properties.EditStyle == EditStyle.DropDownList)
                            {
                                if (this.Properties.AddSelectItemDic != null && this.Properties.AddSelectItemDic.Count > 0)
                                {
                                    foreach (var item in this.Properties.AddSelectItemDic)
                                    {
                                        var dataRow = dt.NewRow();
                                        dataRow[this.Properties.DisplayMember] = item.Key;
                                        dataRow[this.Properties.ValueMember] = item.Value;
                                        dt.Rows.InsertAt(dataRow, 0);
                                    }
                                }
                            }
                            this.Properties.DataSource = dt;
                        }
                    }
                    else
                    {
                        if (this.Properties.EditStyle == EditStyle.Default || this.Properties.EditStyle == EditStyle.DropDownList)
                        {
                            //插入空行
                            try
                            {
                                if (this.Properties.AddSelectItemDic != null && this.Properties.AddSelectItemDic.Count > 0)
                                {
                                    var dataSource = this.Properties.DataSource;
                                    if (dataSource.GetType().Name.Contains("Dictionary"))
                                    {
                                        Dictionary<string, string> dicSource = (Dictionary<string, string>)dataSource;
                                        Dictionary<string, string> newDicSource = new Dictionary<string, string>();
                                        foreach (var item in this.Properties.AddSelectItemDic)
                                        {
                                            newDicSource.Add(item.Key, item.Value);
                                        }
                                        foreach (var item in dicSource)
                                        {
                                            newDicSource.Add(item.Key, item.Value);
                                        }
                                        this.Properties.DataSource = newDicSource;
                                    }
                                    else if (dataSource.GetType().Name.Contains("List"))
                                    {
                                        dynamic list = dataSource;
                                        Type type = list[0].GetType();
                                        int i = 0;
                                        foreach (var item in this.Properties.AddSelectItemDic)
                                        {
                                            dynamic obj = Activator.CreateInstance(type, true);
                                            PropertyInfo pikey = type.GetProperty(this.Properties.DisplayMember, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                                            if (pikey != null)
                                            {
                                                pikey.SetValue(obj, item.Key, null);
                                            }
                                            PropertyInfo piValue = type.GetProperty(this.Properties.ValueMember, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                                            if (piValue != null)
                                            {
                                                piValue.SetValue(obj, item.Value, null);
                                            }
                                            list.Insert(i, obj.Clone());
                                            i++;
                                        }
                                        this.Properties.DataSource = list;
                                    }
                                    else if (dataSource.GetType() == typeof(DataTable))
                                    {
                                        DataTable dt = (DataTable)dataSource;
                                        foreach (var item in this.Properties.AddSelectItemDic)
                                        {
                                            var dataRow = dt.NewRow();
                                            dataRow[this.Properties.DisplayMember] = item.Key;
                                            dataRow[this.Properties.ValueMember] = item.Value;
                                            dt.Rows.InsertAt(dataRow, 0);
                                        }
                                        this.Properties.DataSource = dt;
                                    }
                                }
                            }
                            catch (Exception ex)
                            { }
                        }
                        this.Properties.AddSelectItemDic.Clear();
                    }

                    #endregion 处理方案查询语句并获得结果

                    this.EndUpdate();
                }
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// 检测输入值
        /// </summary>
        /// <param name="partial"></param>
        /// <returns></returns>
        protected override bool CheckInputNewValue(bool partial)
        {
            var result = base.CheckInputNewValue(partial);
            if (this.Properties.IsNotClearValue)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 解析控件值
        /// </summary>
        protected override void ParseEditorValue()
        {
            base.ParseEditorValue();
            if ((EditValue == null || string.IsNullOrEmpty(EditValue.ToString())) && this.Properties.IsNotClearValue)
            {
                EditValue = MaskBox.MaskBoxText;
            }
        }

        /// <summary>
        /// 延迟处理查询
        /// </summary>
        /// <param name="value"></param>
        private void Monitor(object value)
        {
            if (this.IsDisposed)
                return;
            timer1.Dispose();
            if (this.IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    var helper = (KeyPressHelper)value;
                    OnTextChanged(helper);

                    string text = helper.Text;

                    this.Text = text;
                    //这里直接是分页情况，把 text != this.Text这个情况去掉（）
                    if (helper.CharValue != (char)8 && helper.CharValue != (char)9 && helper.CharValue != (char)13 && !this.Properties.ProfixerText.IsNullOrEmpty() && text != this.Properties.ProfixerText)
                    {
                        if (helper.CharValue.ToString().ToLower().Contains("\u0016"))  // 粘贴
                        {
                            IDataObject iData = Clipboard.GetDataObject();
                            if (iData.GetDataPresent(DataFormats.Text))
                            {
                                this.Text = (String)iData.GetData(DataFormats.Text);
                            }
                            OnTextChanged(new KeyPressHelper(this.Text, this.Text.Length));

                        }
                        else if (helper.CharValue.ToString().ToLower().Contains("\u0003"))     // 复制
                        {
                            if (!String.IsNullOrWhiteSpace(this.Text))
                            {
                                Clipboard.SetDataObject(this.Text);
                            }
                            return;
                        }
                        else   // 其他快捷键-
                        {
                            //现在只允许输入字母和数字，是否需要输入其他的字符再确定
                            Regex reg = new Regex(@"^[A-Za-z0-9]$", RegexOptions.IgnoreCase);
                            if (reg.IsMatch(helper.CharValue.ToString()))
                                this.Text = text;
                        }
                    }
                    //HR6 - 1794] LookUpEdit 控件增加方案绑定后支持手工录入   gxl 去掉 !this.Properties.IsNotClearValue
                    if (helper.CharValue != (char)9 && helper.CharValue != (char)13 && this.Properties.DataSource != null && !string.IsNullOrEmpty(helper.Text))// && !this.Properties.IsNotClearValue)
                    {
                        this.ShowPopup();
                    }
                }));
            }

        }

        /// <summary>
        /// 按下按键时处理加载源数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEditorKeyPress(KeyPressEventArgs e)
        {
            var helper = (IsMaskBoxAvailable
                ? new KeyPressHelper(MaskBox.MaskBoxText, SelectionStart, SelectionLength, Properties.MaxLength)
                : new KeyPressHelper(AutoSearchText, Properties.MaxLength));
            //添加 当上一次的字符是全选时，也触发切换方案事件  多加了一个条件 helper.Text.Length == helper.SelectionLength
            if (helper.Text.Length == 0 || helper.Text.Length == helper.SelectionLength)
            {
                this.Properties.CurrentKeyChar = e.KeyChar.ToString();
                this.Properties.OnUserDefinedChanging(e.KeyChar.ToString());
                this.rowView = null;
            }
            if (this.Properties.FangAnParm != null)//存在使用中切换方案的情况，所以这里再判断一次
            {
                this.Properties.ExecuteParm();
                this.Properties.FangAnParm = null;//清除
                this.Properties.GridColumnList = this.Properties.View.Columns.ToList();
            }
            if (this.Properties.IsJiXuZX == ProcessText.NO)
            {
                base.OnEditorKeyPress(e);
                this.Properties.IsJiXuZX = ProcessText.YES;
                return;
            }
            if (this.Properties.IsJiXuZX == ProcessText.ALLNO)
            {
                base.OnEditorKeyPress(e);
                return;
            }
            helper.ProcessChar(e.KeyChar);
            if (this.Properties.DataSource != null && !this.Properties.IsPeiZhiCX)
            {
                var text = helper.Text;
                base.OnEditorKeyPress(e);
                if (e.KeyChar != (char)8 && e.KeyChar != (char)9 && e.KeyChar != (char)13 && !this.Properties.ProfixerText.IsNullOrEmpty() && text != this.Properties.ProfixerText && text != this.Text)
                {
                    if (e.KeyChar.ToString().ToLower().Contains("\u0016"))  // 粘贴
                    {
                        IDataObject iData = Clipboard.GetDataObject();
                        if (iData.GetDataPresent(DataFormats.Text))
                        {
                            this.Text = (String)iData.GetData(DataFormats.Text);
                        }
                        OnTextChanged(new KeyPressHelper(this.Text, this.Text.Length));

                        e.Handled = true;
                    }
                    else if (e.KeyChar.ToString().ToLower().Contains("\u0003"))     // 复制
                    {
                        if (!String.IsNullOrWhiteSpace(this.Text))
                        {
                            Clipboard.SetDataObject(this.Text);
                        }
                        return;
                    }
                    else   // 其他快捷键-
                    {
                        bool isContains = helper.Text.ToLower().Contains(e.KeyChar);
                        this.Text = !isContains ? text : "";
                        return;
                    }
                }
                //HR6 - 1794] LookUpEdit 控件增加方案绑定后支持手工录入   gxl 去掉 !this.Properties.IsNotClearValue
                if (e.KeyChar != (char)8 && e.KeyChar != (char)9 && e.KeyChar != (char)13 && this.Properties.DataSource != null && !string.IsNullOrEmpty(helper.Text))// && !this.Properties.IsNotClearValue)
                {
                    //如果绑定数据源输入字符时，要重新设置FilterField，进行过滤
                    this.Properties.CreateShuRuMaColumn(this.Properties.DataSource);
                    this.ShowPopup();
                }
            }
            else if (this.Properties.IsPeiZhiCX)
            {
                if (!this.Properties.IsFenYe && this.Properties.YanChiJG == 0)
                {
                    if (helper.Text == "")//内容为空时关闭弹出窗
                    {
                        this.Properties.DataSource = null;
                        this.rowView = null;
                        if (IsPopupOpen)
                        {
                            this.ClosePopup();
                        }

                    }//  gxl    2019.10.18     添加当输入“/s”时，删除s时，关闭弹出框
                    else if (helper.Text == this.Properties.ProfixerText && e.KeyChar == (char)8)
                    {
                        this.Properties.DataSource = null;

                        if (IsPopupOpen)
                        {
                            this.ClosePopup();
                        }
                        return;
                    }
                    else if (helper.Text == this.Properties.ProfixerText && e.KeyChar != (char)8)
                    {
                        if (helper.CharValue == (char)8)
                        {
                            this.Properties.DataSource = null;
                            this.SelectionStart = 1;
                        }
                        this.Properties.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.True;
                        this.ClosePopup();
                        return;
                    }
                    else if ((helper.Text.Length == 1 || (helper.Text.Length == 2 && helper.Text.Substring(0, 1) == this.Properties.ProfixerText)) && e.KeyChar.ToString() != "\b") //第一个输入进行数据库查询
                    {
                        OnTextChanged(helper);
                    }

                    string text = helper.Text;
                    base.OnEditorKeyPress(e);

                    if (e.KeyChar != (char)8 && e.KeyChar != (char)9 && e.KeyChar != (char)13 && !this.Properties.ProfixerText.IsNullOrEmpty() && text != this.Properties.ProfixerText && text != this.Text)
                    {
                        if (e.KeyChar.ToString().ToLower().Contains("\u0016"))  // 粘贴
                        {
                            IDataObject iData = Clipboard.GetDataObject();
                            if (iData != null && iData.GetDataPresent(DataFormats.Text))
                            {
                                this.Text = (string)iData.GetData(DataFormats.Text);
                            }
                            OnTextChanged(new KeyPressHelper(this.Text, this.Text.Length));

                            e.Handled = true;
                        }
                        else if (e.KeyChar.ToString().ToLower().Contains("\u0003"))     // 复制
                        {
                            if (!String.IsNullOrWhiteSpace(this.Text))
                            {
                                Clipboard.SetDataObject(this.Text);
                            }
                            return;
                        }
                        else   // 其他快捷键-
                        {
                            bool isContains = text.ToLower().Contains(e.KeyChar);
                            this.Text = !isContains ? text : "";
                            this.Select(this.Text.Length, 0);
                            return;
                        }
                    }
                    //HR6 - 1794] LookUpEdit 控件增加方案绑定后支持手工录入   gxl 去掉 !this.Properties.IsNotClearValue
                    if (e.KeyChar != (char)9 && e.KeyChar != (char)13 && this.Properties.DataSource != null && !string.IsNullOrEmpty(helper.Text))//&& !this.Properties.IsNotClearValue)
                    {
                        this.ShowPopup();
                    }
                }
                else
                {
                    //先清空数据源，防止延迟执行的时候打开form
                    this.Properties.DataSource = null;
                    if (helper.Text == "")//内容为空时关闭弹出窗
                    {
                        this.Properties.DataSource = null;
                        this.rowView = null;
                        this.Text = "";
                        e.Handled = true;
                        if (IsPopupOpen)
                        {
                            this.ClosePopup();
                        }
                    }
                    else if (helper.Text == this.Properties.ProfixerText && e.KeyChar != (char)8)
                    {
                        if (helper.CharValue == (char)8)
                        {
                            this.Properties.DataSource = null;
                            this.SelectionStart = 1;
                        }
                        this.Properties.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.True;
                        this.Text = helper.Text;
                        e.Handled = true;
                        this.ClosePopup();
                        return;
                    }
                    else if (e.KeyChar.ToString() == "\b")//如果输入的是删除，没有延时，直接查询
                    {
                        this.Text = helper.Text;
                        e.Handled = true;
                        OnTextChanged(helper);
                        //HR6 - 1794] LookUpEdit 控件增加方案绑定后支持手工录入   gxl 去掉 !this.Properties.IsNotClearValue
                        if (this.Properties.DataSource != null && !string.IsNullOrEmpty(helper.Text))// && !this.Properties.IsNotClearValue)
                        {
                            this.ShowPopup();
                        }
                    }
                    else if (((helper.Text.Length == 1 || (helper.Text.Length == 2 && helper.Text.Substring(0, 1) == this.Properties.ProfixerText))) && e.KeyChar.ToString() != "\b") //第一个输入进行数据库查询
                    {
                        this.Text = helper.Text;
                        e.Handled = true;//必须要加上，表示已经处理过了，否则text会加两遍，变成aa了
                        firstShuRuTime = DateTime.Now;
                        timer1 = new System.Threading.Timer(Monitor, helper, this.Properties.YanChiJG, Timeout.Infinite);
                    }
                    else if (helper.Text.Length >= 2)
                    {
                        //输入多个字符也触发查询
                        this.Text = helper.Text;
                        e.Handled = true;
                        TimeSpan cha1 = new TimeSpan();
                        secondShuRuTime = DateTime.Now;
                        if (helper.Text.Length == 2)
                        {
                            chaTime = secondShuRuTime - firstShuRuTime;
                            if (chaTime.TotalMilliseconds < this.Properties.YanChiJG)
                            {
                                timer1.Dispose();
                            }
                        }
                        else
                        {
                            cha1 = secondShuRuTime - firstShuRuTime;
                            if (cha1.TotalMilliseconds - chaTime.TotalMilliseconds < this.Properties.YanChiJG)
                            {
                                timer1.Dispose();
                            }
                            chaTime = cha1;
                        }
                        timer1 = new System.Threading.Timer(Monitor, helper, this.Properties.YanChiJG, Timeout.Infinite);
                    }
                }
            }
        }

        /// <summary>
        /// 处理上下按键功能
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var gridview = ((GridView)this.Properties.View.GridControl.MainView);

            if (this.Properties.BiaoGeUpDown)
            {
                if (e.KeyData.ToString() == "Down")
                {
                    if (this.IsPopupOpen)
                    {
                        if (gridview.FocusedRowHandle < 0)
                        {
                            gridview.FocusedRowHandle = 0;
                        }
                        else
                        {
                            if (gridview.FocusedRowHandle >= 0 && gridview.RowCount > gridview.FocusedRowHandle + 1)
                            {
                                gridview.FocusedRowHandle = (gridview.FocusedRowHandle + 1);
                            }
                        }
                        e.Handled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (e.KeyData.ToString() == "Up")
                {
                    if (this.IsPopupOpen)
                    {
                        if (gridview.FocusedRowHandle > 0)
                        {
                            gridview.FocusedRowHandle = (gridview.FocusedRowHandle - 1);
                        }
                        e.Handled = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {

                if (e.KeyData.ToString() == "Down")
                {
                    if (gridview.RowCount > 0)
                    {
                        if (gridview.FocusedRowHandle < 0)
                        {
                            gridview.FocusedRowHandle = 0;
                        }
                        else
                        {
                            if (gridview.FocusedRowHandle >= 0 && gridview.RowCount > gridview.FocusedRowHandle + 1)
                            {
                                gridview.FocusedRowHandle = (gridview.FocusedRowHandle + 1);
                            }
                        }
                        //gxl  2019.10.09  当向下箭头时，如果弹出框未弹出，值也要随着箭头变化
                        if (!this.IsPopupOpen)
                        {
                            var rowCount = this.Properties.GetSourceCount();
                            currentRowIndex = this.Properties.GetIndexByKeyValue(this.EditValue);
                            if (rowCount > 0 && currentRowIndex >= 0)
                            {
                                if (rowCount > currentRowIndex + 1)
                                {
                                    currentRowIndex++;
                                }
                                else
                                {
                                    currentRowIndex = 0;
                                }
                                this.EditValue = this.Properties.GetKeyValue(currentRowIndex);
                            }
                            return;
                        }
                        e.Handled = true;
                    }

                }
                else if (e.KeyData.ToString() == "Up")
                {
                    if (gridview.RowCount > 0)
                    {
                        if (gridview.FocusedRowHandle > 0)
                        {
                            gridview.FocusedRowHandle = (gridview.FocusedRowHandle - 1);
                        }
                        if (!this.IsPopupOpen)
                        {
                            //处理未弹出选择框情况下，按上下键选择项目
                            var rowCount = this.Properties.GetSourceCount();
                            currentRowIndex = this.Properties.GetIndexByKeyValue(this.EditValue);
                            if (rowCount > 0 && currentRowIndex >= 0)
                            {
                                if (currentRowIndex > 0)
                                {
                                    currentRowIndex--;
                                }
                                else
                                {
                                    currentRowIndex = rowCount - 1;
                                }
                                this.EditValue = this.Properties.GetKeyValue(currentRowIndex);
                            }
                            return;

                        }
                        e.Handled = true;
                    }
                }
            }

            if (e.KeyData.ToString() == "Escape")
            {
                gridview.FocusedRowHandle = -1;
                if (this.Properties.IsPeiZhiCX)
                {
                    this.Properties.DataSource = null;
                }

                this.EditValue = "";
                this.Text = "";
                this.ClosePopup();
                e.Handled = true;
            }
            else if (e.KeyData.ToString() == "PageUp")//上一页
            {
                if (!this.Properties.IsFenYe)
                {
                    gridview.MovePrevPage();
                }
                else
                {
                    foreach (var item in this.Properties.View.GridControl.Controls)
                    {
                        if (item is MediNavigator)
                        {
                            var mediNavigator = item as MediNavigator;
                            mediNavigator.prevPage();
                            break;
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.KeyData.ToString() == "Next")//下一页
            {
                if (!this.Properties.IsFenYe)
                {
                    gridview.MoveNextPage();
                }
                else
                {
                    foreach (var item in this.Properties.View.GridControl.Controls)
                    {
                        if (item is MediNavigator mediNavigator)
                        {
                            mediNavigator.nextPage();
                            break;
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.KeyData.ToString() == "Home")//首页
            {
                if (!this.Properties.IsFenYe)
                {
                    gridview.MoveFirst();
                }
                else
                {
                    foreach (var item in this.Properties.View.GridControl.Controls)
                    {
                        if (item is MediNavigator mediNavigator)
                        {
                            mediNavigator.homePage();
                            break;
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.KeyData.ToString() == "End")//尾页
            {
                if (!this.Properties.IsFenYe)
                {
                    gridview.MoveLast();
                }
                else
                {
                    foreach (var item in this.Properties.View.GridControl.Controls)
                    {
                        if (item is MediNavigator)
                        {
                            var mediNavigator = item as MediNavigator;
                            mediNavigator.endPage();
                            break;
                        }
                    }
                }
                e.Handled = true;
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    IsKeyEnter = true;
                    //gxl  2019.10.14   当删除后回车时，text为空，清空后台的值，防止又恢复为原来的值
                    if (string.IsNullOrEmpty(this.Text))
                    {
                        this.fOldEditValue = null;
                    }
                }
                else
                    IsKeyEnter = false;

                base.OnKeyDown(e);
            }
        }

        /// <summary>
        /// 光标离开控件时触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.fOldEditValue = null;//防止删除文本后，后台值还存在
            }
            base.OnLeave(e);
        }

        /// <summary>
        /// 内容改变时触发过滤条件改变
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEditValueChanging(ChangingEventArgs e)
        {
            var Text = this.Text;
            if ((this.Properties.EditStyle == EditStyle.Both && e.NewValue == null) && !Text.Contains("System."))
            {
                return;
            }
            base.OnEditValueChanging(e);

            if (this.IsHandleCreated)//添加过滤字段操作
            {
                this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
                {
                    if (this.Properties.FilterField != null)
                    {
                        SetGridLookUpEditMoreColumnFilter(this, this.Properties.FilterField, this.Properties.FilterType);
                    }
                }));
            }
        }

        /// <summary>
        /// 输入框内容改变后，光标指到最后
        /// </summary>
        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
        }

        /// <summary>
        /// 弹出框操作处理
        /// </summary>
        public override void ShowPopup()
        {
            // 注释by jzy 不应该有直接修改全局环境的代码.该代码导致都昌控件无法正确注册
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InputLanguage CurrentInput = InputLanguage.CurrentInputLanguage;
            // 控制前面输入的时候才弹出窗口，避免后面输入
            //可以输入两个字符查询时
            if (this.Properties.IsJiXuZX == ProcessText.ALLNO)
            {
                return;
            }
            if (!IsPopupOpen)
            {
                base.ShowPopup();
            }
            InputLanguage.CurrentInputLanguage = CurrentInput;

            //显示分页显示数据时的总条数和当前页数
            //每次查询不创建新的弹出窗体，就调用已经存在的PopupForm来进行初始化为第一页
            GridView MainView1 = (GridView)this.Properties.View.GridControl.MainView;
            if (this.Properties.IsFenYe)
            {
                if (!MainView1.OptionsView.ShowFooter)
                {
                    //防止出现不显示footer时，数据被分页控件遮挡的问题
                    MainView1.OptionsView.ShowFooter = true;
                }

                if (this.PopupForm.Controls.Find("mediNavigator", true).Length > 0)
                {
                    ((MediNavigator)this.PopupForm.Controls.Find("mediNavigator", true)[0]).RefreshPagerBar(this.Properties.PageSize, this.TotalCount, 1);
                }
            }
            MainView1.RowCellStyle += MediGridLookUpEditPopupForm_RowCellStyle;
        }

        /// <summary>
        /// 箭头选择时背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridLookUpEditPopupForm_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0 || hand != ((GridView)this.Properties.View.GridControl.MainView).FocusedRowHandle)
                return;
            e.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(215)))), ((int)(((byte)(255)))));
        }


        /// <summary>
        /// 输入内容后，默认选择第一行
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        protected override int FindItem(string text, int startIndex)
        {
            return -1;
        }

        protected override void OnCreateControl()
        {
            //设置默认行高
            this.Properties.View.RowHeight = 24;
            base.OnCreateControl();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            //双击选中全部内容
            this.SelectAll();
        }

        /// <summary>
        /// 关闭弹出框处理
        /// </summary>
        /// <param name="closeMode"></param>
        protected override void OnPopupClosed(PopupCloseMode closeMode)
        {
            if (this.EditValue != null && !((Hashtable)this.Properties.BindDataSource).ContainsKey(this.EditValue))
            {
                ((Hashtable)this.Properties.BindDataSource).Add(this.EditValue, this.Text);
            }
            base.OnPopupClosed(closeMode);
            //选中后跳到下一个控件上          
            if ((IsKeyEnter || (closeMode == PopupCloseMode.Normal && this.rowView != null)) && this.Properties.IsSelectedToNextControl && (this.EnterMoveNextControl || this.Properties.View.OptionsNavigation.EnterMoveNextColumn) && this.Text != "" && this.EditValue != null && this.EditValue.ToString() != "")
            {
                IsKeyEnter = false;
                // 发送消息(回车)
                SendMessage(Parent.Handle, 256, 13, 0);
                // 判断上级是否为表格
                ((Parent as MediGridControl)?.MainView as MediGridView)?.ReSetCurrentColumn();
            }

            //gxl  2020.1.7  弹出框没有选中和多选的情况清空value，
            // zhukunpin 下拉框中直接点击外面的时候没有情况value，
            if (closeMode == PopupCloseMode.Cancel || closeMode == PopupCloseMode.Immediate)
            {
                GridView view = (GridView)this.Properties.View.GridControl.MainView;
                var index = view.FocusedRowHandle;
                if (!view.OptionsSelection.MultiSelect && index < 0)
                {
                    this.EditValue = "";
                    this.Text = "";
                }
            }
            //光标定位到末尾
            this.Select(this.Text.Length, 0);//文本框内文本左端被遮住 zll/2020/08/04
        }

        #region Window API

        /// <summary>
        /// 将指定的消息发送到一个或多个窗口
        /// </summary>
        /// <param name="hwnd">窗口的句柄</param>
        /// <param name="wMsg">要发送的消息</param>
        /// <param name="wParam">其他特定于消息的信息</param>
        /// <param name="lParam">其他特定于消息的信息</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        #endregion

        /// <summary>
        /// 当获取焦点的时候加载数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (this.Properties.FangAnParm != null)
            {
                this.Properties.ExecuteParm();
                this.Properties.FangAnParm = null;
                this.Properties.GridColumnList = this.Properties.View.Columns.ToList();
                this.Properties.DisplayMember = this.Properties.DisplayMember.ToUpper();//默认用大写
                this.Properties.ValueMember = this.Properties.ValueMember.ToUpper();//默认用大写
            }
            //20200727 add by zhengcj begin
            if (this.Properties.IsCache && !this.Properties.QuerySql.IsNullOrWhiteSpace())
            {
                string canShuZhi = "0";
                HISCacheManager.GetCanShu(HISClientHelper.XITONGID, "公用_方案缓存版本", ref canShuZhi);
                string value = GYCanShuHelper.GetCanShu("公用_方案缓存版本", "0");
                int now = value.ToInt();
                if (now > 0)
                {
                    if (canShuZhi != value)
                    {
                        HISCacheManager.ClearFangAnCX();
                        QueryDataSource(true);
                    }
                    else if (HISCacheManager.ContainsFangAnCX(this.Properties.QuerySql))
                    {
                        if (this.Properties.CacheDataSource == null)
                        {
                            DataTable dataTable = HISCacheManager.GetFangAnCX(this.Properties.QuerySql);
                            if (dataTable == null)
                            {
                                QueryDataSource();
                            }
                            else
                            {
                                this.Properties.CacheDataSource = dataTable;
                            }
                        }
                    }
                    else
                    {
                        QueryDataSource();
                    }
                }
                else if (canShuZhi != "0" && canShuZhi != value)
                {
                    HISCacheManager.ClearFangAnCX();
                    this.Properties.CacheDataSource = null;
                }
                else
                {
                    this.Properties.CacheDataSource = null;
                }
            }
            //20200727 add by zhengcj end
            if (this.Properties.GridColumnList != null && this.Properties.GridColumnList.Count > 0 && this.Properties.View.Columns.Count != this.Properties.GridColumnList.Count())
            {
                if (this.Properties.View.Columns.Count > 0)
                {
                    this.Properties.View.Columns.Clear();
                }
                foreach (var column in this.Properties.GridColumnList)
                {
                    GridColumn columnInfo = new GridColumn();
                    columnInfo.FieldName = column.FieldName;
                    columnInfo.Caption = column.Caption;
                    columnInfo.Width = column.Width;
                    columnInfo.VisibleIndex = column.VisibleIndex + 100;
                    columnInfo.Visible = !string.IsNullOrEmpty(column.Caption);
                    columnInfo.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
                    columnInfo.AppearanceCell.Options.UseFont = true;
                    columnInfo.Name = this.Properties.ProfixerText + "|" + columnInfo.FieldName;
                    this.Properties.View.Columns.Add(columnInfo);
                }
            }
            if (!this.Properties.IsPeiZhiCX && this.Properties.View.Columns.Count == 0 && !string.IsNullOrEmpty(this.Properties.DisplayMember))
            {
                GridColumn columnInfo = new GridColumn();
                columnInfo.FieldName = this.Properties.DisplayMember;
                columnInfo.Visible = true;
                columnInfo.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
                columnInfo.AppearanceCell.Options.UseFont = true;
                columnInfo.Name = this.Properties.ProfixerText + "|" + columnInfo.FieldName;
                if (this.Properties.PopformWidth == 0)
                {
                    this.Properties.PopformWidth = this.Width;
                }
                this.Properties.View.Columns.Add(columnInfo);
                this.Properties.ShowFooter = false;
                this.Properties.View.OptionsView.ShowColumnHeaders = false;
            }
            if (string.IsNullOrEmpty(this.Properties.ValueMember) && this.Properties.View.Columns.Count > 0)
            {
                this.Properties.ValueMember = this.Properties.View.Columns[0].FieldName;
            }
            if (string.IsNullOrEmpty(this.Properties.DisplayMember) && this.Properties.View.Columns.Count > 0)
            {
                this.Properties.DisplayMember = this.Properties.View.Columns[0].FieldName;
            }

            //如果时分页查询，得到焦点时不查询所有数据
            if (this.Properties.IsFenYe)
                return;

            //非方案，但要即可输入也可下拉时生成输入码
            if (this.Properties.EditStyle == EditStyle.Both && this.Properties.DataSource != null && !this.Properties.IsPeiZhiCX && this.Properties.DataSource.GetType() == typeof(DataTable))
            {
                DataTable dt = (DataTable)this.Properties.DataSource;
                if (this.Properties.XianShiLie == null)
                {
                    this.Properties.XianShiLie = this.Properties.DisplayMember;
                }
                dt = this.Properties.CreateShuRuMaColumn(dt);
                if (dt == null || dt.Rows.Count < 1)
                {
                    this.Properties.DataSource = null;
                    this.Properties.FilterRowCount = 0;
                    return;
                }
                this.Properties.DataSource = dt;
            }
            base.OnGotFocus(e);
        }

        private void QueryDataSource(bool reset = false)
        {
            if (reset)
                this.Properties.CacheDataSource = null;
            if (this.Properties.CacheDataSource != null)
            {
                if (this.Properties.CacheDataSource is DataTable)
                {
                    if (HISCacheManager.ContainsFangAnCX(this.Properties.QuerySql))
                        return;
                }
                else if (this.Properties.CacheDataSource is Task)
                {
                    return;
                }
            }
            this.Properties.CacheDataSource = Task.Factory.StartNew(() =>
            {
                ControlsQuery query = new ControlsQuery();
                DataTable table = query.QuerySql(this.Properties.QuerySql.Replace("'EditText'", "'%'").Replace("EditText", ""));
                if (table != null)
                {
                    table = this.Properties.CreateShuRuMaColumn(table);
                    HISCacheManager.SetFangAnCX(this.Properties.QuerySql, table);
                }
                this.Properties.CacheDataSource = table;
            });
        }

        /// <summary>
        /// 当失去焦点的时候加载数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            this.Select(0, 0);//文本框内文本左端被遮住 zll/2020/08/04
        }
        #endregion 重写原方法或事件

        #region 添加处理方法

        /// <summary>
        /// 替换对应条件前的查询语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 替换对应查询条件的最终查询语句
        /// </summary>
        public string QuerySql { get; set; }

        /// <summary>
        /// 保存选项的行值
        /// </summary>
        public DataRow rowView;
        /// <summary>
        /// 延迟加载定时器
        /// </summary>
        public System.Threading.Timer timer1;
        /// <summary>
        /// 第一次输入字符的时间
        /// </summary>
        private DateTime firstShuRuTime;
        /// <summary>
        /// 第二次输入字符的时间
        /// </summary>
        private DateTime secondShuRuTime;
        /// <summary>
        /// 两次书输入字符的时间差
        /// </summary>
        private TimeSpan chaTime;

        /// <summary>
        /// 初始化项目，根据传入的方案类
        /// </summary>
        /// <param name="param"></param>
        public void AddPorjectByParam(E_GY_FANGANPZ_INPARM param)
        {
            this.Properties.FangAnParm = param;
        }

        /// <summary>
        /// 外部添加方案项目
        /// <param name="xiangMu">项目名</param>
        /// <param name="fangAnMing">方案名</param>
        /// </summary>
        public void AddPorject(string xiangMu, string fangAnMing)
        {
            this.Properties.DataSource = null;
            if (this.Properties.IsClearProject)
            {
                ClearProjectXM();
            }
            if (this.Properties.FangAnParm == null)
            {
                E_GY_FANGANPZ_INPARM parm = new E_GY_FANGANPZ_INPARM
                {
                    XIANGMU = new List<string>() { xiangMu },
                    FANGANMING = new List<string>() { fangAnMing }
                };
                this.Properties.FangAnParm = parm;
            }
            else
            {
                if (!this.Properties.FangAnParm.XIANGMU.Contains(xiangMu) || !this.Properties.FangAnParm.FANGANMING.Contains(fangAnMing))
                {
                    this.Properties.FangAnParm.XIANGMU.Add(xiangMu);
                    this.Properties.FangAnParm.FANGANMING.Add(fangAnMing);
                }
            }
        }

        /// <summary>
        /// 执行获取到项目的方案信息
        /// <param name="xiangMu">项目名</param>
        /// <param name="fangAnMing">方案名</param>
        /// </summary>
        private void ExexutePorject(string xiangMu, string fangAnMing)
        {
            ControlsQuery query = new ControlsQuery();
            FanganPeizhi fanganPeizhi = query.GetFanAn(xiangMu, fangAnMing, this.Properties.IsAllLoad);
            //this.Sql = (string.IsNullOrEmpty(this.Sql) ? "" : this.Properties.Sql + " union all ") + fanganPeizhi.QuerySQL;//一个控件使用多个方案
            this.Sql = fanganPeizhi.QuerySQL;
            this.QuerySql = this.Sql;
            this.Properties.XiangMuMing = xiangMu;
            //当添加多个方案时，后面方案的配置不改变前面方案的内容，只sql语句变化
            if (string.IsNullOrEmpty(this.Properties.ValueMember))
            {
                this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                this.Properties.ImmediatePopup = true;
                this.Properties.AutoComplete = false;

                //绑定值列
                this.Properties.ValueMember = fanganPeizhi.ShiJiLMC;

                //绑定显示列
                this.Properties.DisplayMember = fanganPeizhi.XianShiLMC;
                this.Properties.XianShiLie = fanganPeizhi.XianShiLMC;
                this.Properties.ColumnIndex = fanganPeizhi.ColumnIndex;

                this.Properties.IsPeiZhiCX = true;
                this.Properties.IsGuoLv = fanganPeizhi.IsGuoLv;

                this.Properties.FilterField = fanganPeizhi.FilterField;

                this.Properties.FilterType = fanganPeizhi.FilterType;
                this.Properties.OrderList = fanganPeizhi.OrderList;
                //弹出框需显示的列信息设置
                if (fanganPeizhi.ColumnInfo.Count > 0)
                {
                    for (int i = 0; i < fanganPeizhi.ColumnInfo.Count; i++)
                    {
                        GridColumn columnInfo = new GridColumn();
                        columnInfo.FieldName = fanganPeizhi.ColumnInfo[i][0];
                        if (fanganPeizhi.ColumnInfo[i][1] != "")
                        {
                            columnInfo.Caption = fanganPeizhi.ColumnInfo[i][1];
                        }
                        columnInfo.Width = Convert.ToInt32(fanganPeizhi.ColumnInfo[i][2]);
                        columnInfo.VisibleIndex = i;
                        columnInfo.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
                        columnInfo.AppearanceCell.Options.UseFont = true;
                        this.Properties.View.Columns.Add(columnInfo);
                    }

                    //需排序的列未在显示列中，添加对应列并设为不可见
                    if (fanganPeizhi.OrderList != null)
                    {
                        foreach (string[] item in fanganPeizhi.OrderList)
                        {
                            var col = this.Properties.View.Columns.Where(o => o.FieldName == item[0]);
                            if (col.Count() == 0)
                            {
                                GridColumn columnInfo = new GridColumn();
                                columnInfo.FieldName = item[0];
                                columnInfo.VisibleIndex = 100;
                                columnInfo.Visible = false;
                                this.Properties.View.Columns.Add(columnInfo);
                            }
                        }
                    }
                }
                if (this.Properties.PopformWidth == 0)
                {
                    this.Properties.PopformWidth = fanganPeizhi.PopformWidth;
                }
            }
        }

        /// <summary>
        /// 清除项目（设置清除标志）
        /// </summary>
        public void ClearProject()
        {
            //设置时默认清除一次，因为存在后面从方案直接切换为附数据源的模式
            ClearProjectXM();

            //设置清除标志，因为项目方案是在光标到到控件时加载，在这之前清除是没效果的
            this.Properties.IsClearProject = true;
        }

        /// <summary>
        /// 设置绑定数据源，值列，显示列
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="valueColumn">值列</param>
        /// <param name="displayColumn">显示列</param>
        public void BindSource(object dataSource, string valueColumn, string displayColumn)
        {
            this.Properties.BindSource(dataSource, valueColumn, displayColumn);
        }

        /// <summary>
        /// 执行的清除项目信息
        /// </summary>
        private void ClearProjectXM()
        {
            this.Sql = "";
            this.QuerySql = "";
            this.Properties.Sql = "";
            this.Properties.QuerySql = "";
            this.Properties.Tag = null;
            this.Properties.ValueMember = null;
            this.Properties.ColumnIndex = null;
            this.Properties.DataSource = null;
            this.Properties.BindDataSource = null;
            this.Properties.IsClearProject = false;
            this.Properties.PopupResizeMode = ResizeMode.LiveResize;
            this.Properties.PopformWidth = 0;
            this.EditValue = "";
            this.Text = "";
        }

        protected override void OnMaskBox_GotFocus(object sender, EventArgs e)
        {
            base.OnMaskBox_GotFocus(sender, e);
           

        }

        protected override void OnMaskBox_LostFocus(object sender, EventArgs e)
        {
            base.OnMaskBox_LostFocus(sender, e);
           
        }

        /// <summary>
        /// 设置方案需要替换的入参
        /// </summary>
        /// <param name="param"></param>
        public void SetQueryParam(Dictionary<string, string> param)
        {
            this.Properties.FangAnParm.DicCanShu = param;
        }

        /// <summary>
        /// 添加方案对应参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParam(string name, object value)
        {
            this.Properties.AddParam(name, value);
        }

        /// <summary>
        /// 重置参数
        /// </summary>
        public void ResetParam()
        {
            this.Properties.ResetParam();
        }

        /// <summary>
        /// 根据替换后的sql语句获取对应数据
        /// </summary>
        /// <param name="text"></param>
        private void QueryData(string text)
        {
            int total = 0;
            if (!this.Properties.IsGuoLv || (this.Properties.IsGuoLv && text.Length >= 1) || text == "ALL*")
            {
                if (string.IsNullOrEmpty(this.QuerySql))
                {
                    return;
                }
                // 20200727 modi by zhengcj begin
                DataTable dt = new DataTable();
                if (this.Properties.IsCache && this.Properties.CacheDataSource is DataTable)
                {
                    if (this.Properties.DataSource == null || (this.Properties.DataSource != null && this.Properties.DataSource != this.Properties.CacheDataSource))
                    {
                        dt = (DataTable)this.Properties.CacheDataSource;
                        this.Properties.DataSource = dt;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    ControlsQuery query = new ControlsQuery();
                    if (this.Properties.IsFenYe)
                    {
                        dt = query.QueryPageSql(this.QuerySql, 1, this.Properties.PageSize, out total);
                        this.totalCount = total;
                    }
                    else
                    {
                        dt = query.QuerySql(this.QuerySql);
                    }

                    dt = this.Properties.CreateShuRuMaColumn(dt);
                    var oldText = this.Text;
                    this.Properties.DataSource = dt;

                    if (this.Properties.IsFenYe)
                        this.Text = oldText;
                }

                //设置过滤字段
                if (this.Properties.OrderList != null)
                {
                    GridView view = (GridView)this.Properties.View.GridControl.MainView;
                    if (view.Columns.Count > 0)
                    {
                        int XuHao = 0;
                        foreach (string[] item in this.Properties.OrderList)
                        {
                            if (dt.Columns.Contains(item[0]))
                            {
                                view.Columns[item[0]].SortIndex = XuHao;
                                view.Columns[item[0]].SortOrder = (item[1] == "DESC" ? DevExpress.Data.ColumnSortOrder.Descending : DevExpress.Data.ColumnSortOrder.Ascending);
                                XuHao++;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当输入文本改变时，查询后台
        /// </summary>
        /// <param name="help"></param>
        protected void OnTextChanged(KeyPressHelper help)
        {
            //HB6-13255 医生站开立诊疗输入（/'）报错
            if (help.Text == null || help.Text.Contains("'"))
            {
                return;
            }
            string tText = help.Text;

            if (!string.IsNullOrEmpty(this.Properties.ProfixerText))
            {
                //gxl   HB6-6046(536168)  将原来的replace函数当输入为两个“//”时，替换后为“”了， 现在“//”截取后变成“/”
                if (tText.Contains(this.Properties.ProfixerText))
                {
                    var index = tText.IndexOf(this.Properties.ProfixerText, StringComparison.Ordinal);
                    tText = tText.Substring(index + 1);
                }
            }
            this.QuerySql = this.Properties.Sql == null ? this.Sql.Replace("EditText", tText.ToUpper()) : this.Properties.QuerySql.Replace("EditText", tText.ToUpper());
            QueryData(tText);
        }

        /// <summary>
        /// 获取选中行的所有列数据,每列按|分隔,返回对应拼接的字符串
        /// </summary>
        /// <returns></returns>
        public string GetRowString()
        {
            string returnVal = "";
            var item = rowView.ItemArray;
            for (int i = 0; i < item.Length; i++)
            {
                returnVal += item[i] + "|";
            }
            return returnVal;
        }

        /// <summary>
        /// 根据列名获取对应列值
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetValueByName(string columnName)
        {
            if (this.Properties.ColumnIndex != null && this.Properties.ColumnIndex.ContainsKey(columnName) && this.rowView != null)
            {
                return GetCell(this.Properties.ColumnIndex[columnName]);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据列序号获取列值
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetCell(int column)
        {
            if (rowView.ItemArray.Length > column)
            {
                return rowView.ItemArray[column].ToString();
            }

            return null;
        }

        /// <summary>
        /// 设置过滤字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="FilterField"></param>
        /// <param name="FilterType"></param>
        public void SetGridLookUpEditMoreColumnFilter(object sender, string[] FilterField, string[] FilterType)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit == null || string.IsNullOrEmpty(edit.Text)&& edit.Properties.DataSource == null)
            {
                return;
            }

            GridView view = edit.Properties.View;
            //获取GriView私有变量
            FieldInfo extraFilter = view.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
            string filterCondition = "";

            if (FilterField != null && FilterField.Length > 0) //有设置的按设置过滤字段，未设置按显示列除ValueMember列外过滤
            {
                GroupOperator groupOperator = new GroupOperator() { OperatorType = GroupOperatorType.Or };
                int i = 0;
                foreach (string Field in FilterField)
                {
                    if (!string.IsNullOrEmpty(Field))
                    {
                        List<CriteriaOperator> list = new List<CriteriaOperator>();
                        list.Add(new OperandProperty() { PropertyName = Field });
                        var EditText = edit.Text.ToUpper();
                        if (!string.IsNullOrEmpty(this.Properties.ProfixerText))
                        {
                            EditText = EditText.Replace(this.Properties.ProfixerText, "");
                        }
                        list.Add(new OperandValue() { Value = EditText });
                        var type = FunctionOperatorType.StartsWith;
                        if (FilterType != null && FilterType.Length > i && FilterType[i] != "1")
                        {
                            type = FunctionOperatorType.Contains;
                        }
                        groupOperator.Operands.Add(new FunctionOperator(type, list));
                        i++;
                    }
                }
                filterCondition = groupOperator.ToString();
            }
            else
            {
                List<CriteriaOperator> columnsOperators = new List<CriteriaOperator>();
                foreach (GridColumn col in view.VisibleColumns)
                {
                    if (col.Visible && col.ColumnType == typeof(string))
                        columnsOperators.Add(new FunctionOperator(FunctionOperatorType.Contains,
                            new OperandProperty(col.FieldName),
                            new OperandValue(edit.Text.ToUpper())));
                }
                filterCondition = new GroupOperator(GroupOperatorType.Or, columnsOperators).ToString();
            }
            extraFilter.SetValue(view, filterCondition);
            try
            {
                //获取GriView中处理列过滤的私有方法
                MethodInfo ApplyColumnsFilterEx = view.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
                if (ApplyColumnsFilterEx != null) ApplyColumnsFilterEx.Invoke(view, null);
            }
            catch (Exception)
            {
                // 这个地方在下拉框输入值后再输入空格会报错，所以不能去掉这个try...catch...
            }
        }

        #endregion 添加处理方法
    }

    public class MediGridLookUpEditViewInfo : GridLookUpEditBaseViewInfo
    {
        public MediGridLookUpEditViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediGridLookUpEditPainter : ButtonEditPainter
    {
        public MediGridLookUpEditPainter()
        {
        }
    }

    #region 弹出窗口界面

    public class MediGridLookUpEditPopupForm : PopupGridLookUpEditForm
    {
        public MediGridLookUpEdit MainEdit;
        public MediDataNavigator mediDataNavigator;
        public MediNavigator mediNavigator;

        public MediGridLookUpEditPopupForm(MediGridLookUpEdit ownerEdit)
            : base(ownerEdit)
        {
            MainEdit = ownerEdit;
            if (MainEdit.Properties.IsFenYe && ownerEdit.Properties.ShowFooter)
            {
                //添加表格显示footer
                GridView MainView1 = ((GridView)MainEdit.Properties.View.GridControl.MainView);
                MainView1.OptionsView.ShowFooter = true;

                mediNavigator = new MediNavigator();
                mediNavigator.Height = 30;
                mediNavigator.PageSize = 10;
                mediNavigator.Width = 500;
                mediNavigator.Name = "mediNavigator";
                mediNavigator.Dock = DockStyle.Bottom;

                bool cunZai = false;
                foreach (var item in MainEdit.Properties.View.GridControl.Controls)
                {
                    if (item is MediNavigator)
                    {
                        cunZai = true;
                        break;
                    }
                }

                if (!cunZai)
                    MainEdit.Properties.View.GridControl.Controls.Add(mediNavigator);

                mediNavigator.myPagerEvents += MediNavigator_MyPagerEvent;
                this.mediNavigator.RefreshPagerBar(MainEdit.Properties.PageSize, MainEdit.TotalCount, 1);
            }
            else if (ownerEdit.Properties.ShowFooter)
            {
                Label lb = new Label();
                lb.Name = "lab_TiShi";
                lb.Height = 20;
                lb.Width = 150;
                lb.MouseHover += lb_MouseHover;
                lb.MouseLeave += lb_MouseLeave;
                string mingcheng = "";
                if (this.Width > 250)
                {
                    if (!string.IsNullOrEmpty(MainEdit.Properties.XiangMuMing))
                    {
                        mingcheng = "--【" + MainEdit.Properties.XiangMuMing + "】";
                        lb.Width = this.Width - 20;
                    }
                }
                if (ownerEdit.Properties.ShowFangAnMc)
                {
                    lb.Text = "共有" + ownerEdit.Properties.View.GridControl.MainView.RowCount + " 条记录" + mingcheng;
                }
                else
                {
                    lb.Text = "共有" + ownerEdit.Properties.View.GridControl.MainView.RowCount + " 条记录";

                }
                this.Controls.Add(lb);
            }
            this.Resize += MediGridLookUpEditPopupForm_Resize;   
            GridView MainView = ((GridView)MainEdit.Properties.View.GridControl.MainView);
            MainView.RowCellStyle += MediGridLookUpEditPopupForm_RowCellStyle;
            if (!MainEdit.Properties.IsPeiZhiCX)
            {
                foreach (GridColumn column in MainView.Columns)
                {
                    column.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
                    column.AppearanceCell.Options.UseFont = true;
                }
            }
            MainEdit.Properties.View.GridControl.MouseMove += GridControl_MouseMove;
        }

        private void MediNavigator_MyPagerEvent(int curpage, int pagesize)
        {
            var oldText = MainEdit.Text;
            ControlsQuery query = new ControlsQuery();
            if (MainEdit.QuerySql != null && MainEdit.QuerySql.Length > 10)
            {
                DataTable dt = query.QueryPageSql(MainEdit.QuerySql, curpage, pagesize, out var total);
                MainEdit.TotalCount = total;
                dt = MainEdit.Properties.CreateShuRuMaColumn(dt);

                if (dt == null || dt.Rows.Count < 1)
                {
                    MainEdit.Properties.FilterRowCount = 0;
                    dt = new DataTable();
                }
                if ((MainEdit.Properties.EditStyle == EditStyle.Default
                     || MainEdit.Properties.EditStyle == EditStyle.DropDownList)
                    && MainEdit.Properties.AddSelectItemDic != null
                    && MainEdit.Properties.AddSelectItemDic.Count > 0)
                {
                    foreach (var item in MainEdit.Properties.AddSelectItemDic)
                    {
                        var dataRow = dt.NewRow();
                        dataRow[this.Properties.DisplayMember] = item.Key;
                        dataRow[this.Properties.ValueMember] = item.Value;
                        dt.Rows.InsertAt(dataRow, 0);
                    }
                }

                this.mediNavigator.CurrentPage = curpage;
                MainEdit.curPage = curpage;
                MainEdit.Properties.DataSource = null;
                MainEdit.Properties.DataSource = dt;
                MainEdit.Text = oldText;
                MainEdit.ShowPopup();
                this.mediNavigator.RefreshPagerBar(pagesize, total, curpage);

                if (MainEdit.Properties.FilterField != null)
                {
                    MainEdit.SetGridLookUpEditMoreColumnFilter(MainEdit, MainEdit.Properties.FilterField, MainEdit.Properties.FilterType);
                }
            }
        }
        /// <summary>
        /// 设置当前输入法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows8 && OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows10)
            {
                switch (IMMModeHelper.globalconversion)
                {
                    case 1:
                        this.Grid.ImeMode = ImeMode.Off;
                        break;
                    case 1025:
                        this.Grid.ImeMode = ImeMode.Hangul;
                        break;
                    case 0:
                        this.Grid.ImeMode = ImeMode.Off;
                        break;
                    case 268435457:
                        this.Grid.ImeMode = ImeMode.Off;
                        break;
                    case 268436481:
                        this.Grid.ImeMode = ImeMode.Hangul;
                        break;
                    case 268435456:
                        this.Grid.ImeMode = ImeMode.Off;
                        break;
                    default:
                        this.Grid.ImeMode = ImeMode.Hangul;
                        break;
                }
            }
          
          
        }
        /// <summary>
        /// 记录当前输入法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows8 && OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows10)
            {
                int globalconversion = 0;
                int globalsentence = 0;
                IntPtr prt = IMMModeHelper.ImmGetContext(this.Grid.Handle);
                IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                IMMModeHelper.globalconversion = globalconversion;
                IMMModeHelper.globalsentence = globalsentence;
            }
        }
        private void lb_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Text = "共有" + MainEdit.Properties.View.GridControl.MainView.RowCount + "条记录";
        }

        private void lb_MouseHover(object sender, EventArgs e)
        {
            if (MainEdit.Properties.ShowFangAnMc)
            {
                ((Label)sender).Text = "【" + MainEdit.Properties.XiangMuMing + "】";
            }
            else
            {
                ((Label)sender).Text = "共有" + MainEdit.Properties.View.GridControl.MainView.RowCount + "条记录";
            }
        }

        private void GridControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > 10 && e.Y > 24 && MainEdit.Properties.View.RowCount > 9)
            {
                MainEdit.Properties.View.GridControl.Focus();
            }
        }

        private Color ClickColor;

        /// <summary>
        /// 特殊处理显示列背景空白
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridLookUpEditPopupForm_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0 || hand != ((GridView)MainEdit.Properties.View.GridControl.MainView).FocusedRowHandle)
                return;

            if (e.Appearance.BackColor.B == 255)
            {
                e.Appearance.BackColor = ClickColor;
            }

            e.Appearance.BackColor = Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(215)))), ((int)(((byte)(255)))));
        }

        /// <summary>
        /// 弹出窗口大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridLookUpEditPopupForm_Resize(object sender, EventArgs e)
        {
            if (this.Controls.Count > 3)
            {
                Rectangle ScreenArea = Screen.GetBounds(this);
                int y = this.Height - 22;
                var PingMuHeight = MainEdit.PointToScreen(new Point(0, 0));
                if ((this.Height + PingMuHeight.Y + 60) > ScreenArea.Height)
                {
                    y = 5;
                }
                this.Controls[3].Location = new Point { X = 30, Y = y };
            }
        }

        /// <summary>
        /// 获取结果值
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="allowFireNewValue"></param>
        /// <returns></returns>
        protected override object GetResultValue(int rowHandle, bool allowFireNewValue)
        {
            if (string.IsNullOrWhiteSpace(OwnerEdit.Text))
            {
                if (MainEdit.Properties.EditStyle == EditStyle.Input)
                {
                    return "";
                }
                if (MainEdit.Properties.EditStyle == EditStyle.Both && rowHandle < 0)
                {
                    return "";
                }
            }
            else if (rowHandle < 0)
            {
                if (MainEdit.Properties.IsNotClearValue)
                {
                    return OwnerEdit.Text;
                }

                return "";
            }

            var data = this.MainEdit.Properties.View.GetRow(rowHandle);
            if (data == null)
            {
                return "";
            }

            var baseType = this.MainEdit.Properties.View.GetRow(rowHandle).GetType().BaseType;
            if (baseType != null && baseType.Name == "DTOBase")
            {
                MainEdit.rowView = EntityToDataRow(data);
            }
            else
            {
                MainEdit.rowView = this.MainEdit.Properties.View.GetDataRow(rowHandle);
            }
            var result = base.GetResultValue(rowHandle, allowFireNewValue);
            if (result == null || result.GetType().Name == "DataRowView")
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 处理鼠标按下事件(有提示数据条数信息和分页显示总条数和当前页信息)
        /// </summary>
        /// <param name="e"></param>
        public override void ProcessKeyUp(KeyEventArgs e)
        {
            if (this.Controls.Find("lab_TiShi", true).Length > 0)
            {
                string mingcheng = "";
                if (this.Width > 250)
                {
                    mingcheng = "--【" + MainEdit.Properties.XiangMuMing + "】";
                }
                if (MainEdit.Properties.ShowFangAnMc)
                {
                    ((Label)this.Controls.Find("lab_TiShi", true)[0]).Text = "共有" + this.Grid.MainView.RowCount + "条记录" + mingcheng;
                }
                else
                {
                    ((Label)this.Controls.Find("lab_TiShi", true)[0]).Text = "共有" + this.Grid.MainView.RowCount + "条记录";

                }

            }

            base.ProcessKeyUp(e);
        }

        /// <summary>
        /// 处理鼠标在弹出框上的按键操作（重新启用2019.9.10  gxl 输入字符弹出弹出框后，移动鼠标聚焦到弹出框时，响应按键操作）
        /// </summary>
        /// <param name="e"></param>
        public override void ProcessKeyDown(KeyEventArgs e)
        {
            if (!Focused)
            {
                base.ProcessKeyDown(e);

                if (e.KeyData == Keys.Enter)
                {
                    e.Handled = true;
                    ClosePopup();
                }
                //gxl  2019.9.10  输入字符弹出弹出框后，移动鼠标聚焦到某一行，然后按删除键，关闭弹出框，清空输入框中字符
                if (e.KeyData == Keys.Back && this.MainEdit.Text.Length == 1)
                {
                    var gridview = ((GridView)this.Properties.View.GridControl.MainView);
                    gridview.FocusedRowHandle = -1;
                    ClosePopup();
                    this.MainEdit.Text = "";
                    this.MainEdit.EditValue = "";
                    e.Handled = true;
                }
                RepositoryItemMediGridLookUpEdit source = this.Properties as RepositoryItemMediGridLookUpEdit;
                if (e.KeyData == Keys.Back && this.MainEdit.Text.Length == 2 && !string.IsNullOrEmpty(source.ProfixerText) && this.MainEdit.Text.StartsWith(source.ProfixerText))
                {
                    var gridview = ((GridView)this.Properties.View.GridControl.MainView);
                    gridview.FocusedRowHandle = -1;
                    ClosePopup();
                    this.MainEdit.Text = source.ProfixerText;
                    this.MainEdit.Select(source.ProfixerText.Length, 0);
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 弹出显示内容窗口
        /// </summary>
        public override void ShowPopupForm()
        {
            var MainView = (GridView)MainEdit.Properties.View.GridControl.MainView;
            MainView.RowHeight = 24;

            //如果分页时，有多少条就显示多少条数据，不要有滚动条
            if (MainEdit.Properties.IsFenYe)
            {
                MainEdit.Properties.PopformHeight = MainView.RowCount * MainView.RowHeight + 64;
                if (MainEdit.Properties.PopformHeight > SystemInformation.WorkingArea.Height)
                    MainEdit.Properties.PopformHeight = SystemInformation.WorkingArea.Height - 48;//防止出现弹出框宽度大于工作区的高度
            }
            else
            {
                if ((MainEdit.Properties.EditStyle == EditStyle.DropDownList ||
                     MainEdit.Properties.EditStyle == EditStyle.Default) && MainView.RowCount < 9 &&
                    MainView.RowCount > 0)
                    MainEdit.Properties.PopformHeight = MainView.RowCount * MainView.RowHeight + 15;
                else
                    MainEdit.Properties.PopformHeight = 9 * MainView.RowHeight + 15;
            }

            if (MainEdit.Properties.ShowFooter) MainEdit.Properties.PopformHeight += 24;
            if (MainEdit.Properties.View.OptionsView.ShowColumnHeaders) MainEdit.Properties.PopformHeight += 24;

            if (MainEdit.Properties.PopupFormSize.Height > 0)
                this.Size = new Size { Width = this.Size.Width, Height = MainEdit.Properties.PopupFormSize.Height };

            if (MainEdit.Properties.PopformWidth > 100)
                this.Size = new Size { Width = MainEdit.Properties.PopformWidth, Height = this.Size.Height };

            if (!MainEdit.Properties.IsPeiZhiCX && MainEdit.Properties.View.Columns.Count == 1)
            {
                ((GridView)this.Properties.View.GridControl.MainView).OptionsView.ShowIndicator = false;
                this.Size = new Size { Width = MainEdit.Properties.PopformWidth, Height = this.Size.Height };
            }

            if (this.Size.Width < MainEdit.Width)
                this.Size = new Size { Width = MainEdit.Width, Height = this.Size.Height };

            #region 在控件靠近右边时弹出框位置会不对，这里重新计算弹出框X轴位置

            Point p = MainEdit.PointToScreen(new Point(0, 0));
            int ScreenWith = Screen.GetBounds(this).Width;
            var parentForm = MainEdit.FindForm();

            Point pp = parentForm.PointToScreen(new Point(0, 0));
            if (ScreenWith - p.X < this.Size.Width)
            {
                p.X = p.X + MainEdit.Width - this.Size.Width;//弹出框和lookupedit右侧对齐
                if (p.X < 0)
                {
                    if (ScreenWith - pp.X > this.Size.Width)
                        p.X = pp.X; //再弹出的窗体的左侧对齐
                    else
                    {
                        if (this.Size.Width < pp.X + parentForm.Width)
                            p.X = pp.X + parentForm.Width - this.Size.Width; //在弹出框的右侧对齐
                        else
                            p.X = 0;
                    }

                }
            }

            var y = p.Y + MainEdit.Height;
            if (MainEdit.Properties.PopformHeight > 0) //当设置过弹出框高度的时候
            {
                int ScreenHeight = SystemInformation.WorkingArea.Height;
                if (ScreenHeight - y < MainEdit.Properties.PopformHeight)
                {
                    y = p.Y - MainEdit.Properties.PopformHeight;
                }
                this.Size = new Size { Width = this.Size.Width, Height = MainEdit.Properties.PopformHeight };
            }
            if (y < 0)
                y = 0;
            base.SetBounds(p.X, y, this.Size.Width, this.Size.Height);
            MainEdit.Properties.PopformHeight = 0;

            #endregion 在控件靠近右边时弹出框位置会不对，这里重新计算弹出框X轴位置

            //gxl添加判断防止没有提示条数信息，为了刷新显示的数据条数
            if (this.Controls.Find("lab_TiShi", true).Length > 0)
                ((Label)this.Controls.Find("lab_TiShi", true)[0]).Text = "共有" + this.Grid.MainView.RowCount + "条记录";

            base.ShowPopupForm();

            //显示分页显示数据时的总条数和当前页数
            if (MainEdit.Properties.IsFenYe)
            {
                if (this.Controls.Find("mediNavigator", true).Length > 0)
                {
                    ((MediNavigator)this.Controls.Find("mediNavigator", true)[0]).RefreshPagerBar(MainEdit.Properties.PageSize, MainEdit.TotalCount, this.mediNavigator.CurrentPage);
                }
            }
            //延迟加载时 默认选择第一条数据  gxl  2019.11.26 
            if (MainEdit.Properties.YanChiJG > 0)
            {
                var gridview = ((GridView)this.Properties.View.GridControl.MainView);
                gridview.FocusedRowHandle = 0;
            }
            //add by zhukunpin , 草药按1-9快捷输入的时候，需要显示序号
            if (MainEdit.Properties.IsShowIndexNumber)
            {
                DrawRowIndicator(MainView, 40);
            }

        }

        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (MainEdit.Text == "")
            {
                MainEdit.Focus();
                this.ClosePopup();
            }
        }

        /// <summary>
        /// GridView  显示行号   设置行号列的宽度
        /// </summary>
        /// <param name="gv">GridView 控件名称</param>
        /// <param name="width">行号列的宽度 如果为null或为0 默认为30</param>
        public void DrawRowIndicator(GridView gv, int width)
        {
            gv.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gv.IndicatorWidth = width != 0 ? width : 30;
        }

        /// <summary>
        /// 行号设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 实体转DataRow
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private DataRow EntityToDataRow(object entity)
        {
            DataTable dataTable = new DataTable(entity.GetType().Name);
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                dataTable.Columns.Add(propertyInfo.Name != "CTimestamp"
                    ? new DataColumn(propertyInfo.Name)
                    : new DataColumn(propertyInfo.Name, typeof(DateTime)));
            }
            DataRow dataRow = dataTable.NewRow();
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                dataRow[propertyInfo.Name] = propertyInfo.GetValue(entity, null);
            }
            return dataRow;
        }
    }

    #endregion 弹出窗口界面
}