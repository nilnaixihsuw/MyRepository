using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.ListControls;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using DevExpress.Data.Filtering;
using Mediinfo.HIS.L4.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data.Filtering.Helpers;
using Mediinfo.HIS.L2.DTO.GY;
using Mediinfo.Utility;
using System.Diagnostics;
using System.Reflection;
using Mediinfo.HIS.L3.Common;



namespace Mediinfo.HIS.L4.Controls
{
    [UserRepositoryItem("RegisterMediLookUpEdit")]
    public class RepositoryItemMediLookUpEdit : RepositoryItemLookUpEdit
    {
        static RepositoryItemMediLookUpEdit()
        {
            RegisterMediLookUpEdit();

        }
        public const string CustomEditName = "MediLookUpEdit";   

        public RepositoryItemMediLookUpEdit()
        {
            //设置行高
            this.DropDownItemHeight = 24;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Font= new Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          
        }

        public override string EditorTypeName
        {
            get
            {
                return CustomEditName;
            }
        }

        public static void RegisterMediLookUpEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediLookUpEdit), typeof(RepositoryItemMediLookUpEdit), typeof(MediLookUpEditViewInfo), new MediLookUpEditPainter(), true, img));
            
        }


        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediLookUpEdit source = item as RepositoryItemMediLookUpEdit;

                if (source == null) return;
               
            }
            finally
            {
                EndUpdate();
            }
        }

        protected override LookUpListDataAdapter CreateDataAdapter()
        {
            return new MediLookUpEditListDataAdapter(this);
        }

        protected override void OnDataSourceChanged()
        {
            ActivateDataSource(ActivationMode.BindingContext);
            base.OnDataSourceChanged();
        }


        #region 数据集操作部分
        public class MediLookUpEditListDataAdapter : LookUpListDataAdapter
        {
            RepositoryItemMediLookUpEdit thisEdit;
            public MediLookUpEditListDataAdapter(RepositoryItemMediLookUpEdit ownerEdit)
                : base(ownerEdit)
            {
                thisEdit = ownerEdit;

            }
            public override string FilterField
            {
                get
                {
                    string result = "";

                    if (thisEdit.IsPeiZhiCX && !thisEdit.IsGuoLv)
                    {
                        return "";// obj[0];                     
                    }
                    if (thisEdit.FilterField != null)
                    {
                        result = thisEdit.FilterField[0];
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        result = Item.DisplayMember;
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        result = Item.ValueMember;
                    }
                    if (string.IsNullOrEmpty(result) && Columns.Count == 1)
                    {
                        result = Columns[0].Name;
                    }
                    return result;
                }
            }


            protected override CriteriaOperator CreateFilterCriteria()
            {
                if (thisEdit.FilterField == null)
                {
                    thisEdit.FilterField = new string[1] { thisEdit.DisplayMember };
                }
                var lookUpListDA = ((DevExpress.XtraEditors.ListControls.LookUpListDataAdapter)(this));
                GroupOperator groupOperator = new GroupOperator() { OperatorType = GroupOperatorType.Or };
                int i = 0;
                foreach (string Field in thisEdit.FilterField)
                {
                    if (!string.IsNullOrEmpty(Field))
                    {
                        List<CriteriaOperator> list = new List<CriteriaOperator>();
                        list.Add(new OperandProperty() { PropertyName = Field });
                        list.Add(new OperandValue() { Value = lookUpListDA.FilterPrefix });
                        var type = FunctionOperatorType.Contains;
                        if (thisEdit.FilterType != null && thisEdit.FilterType.Length > i && thisEdit.FilterType[i] == "1")
                        {
                            type = FunctionOperatorType.StartsWith;
                        }
                        groupOperator.Operands.Add(new FunctionOperator(type, list));
                        i++;
                    }
                }
                return groupOperator;
            }



            protected override void OnFilterExpressionChanged()
            {
                if (thisEdit.ProfixerText != null)
                {
                    this.FilterPrefix = this.FilterPrefix.Replace(thisEdit.ProfixerText, "");
                }
                if (thisEdit.IsPeiZhiCX && this.DataSource != null && !thisEdit.IsGuoLv && !thisEdit.IsAllLoad)
                {
                    return;
                }
                base.OnFilterExpressionChanged();

                thisEdit.FilterRowCount = this.VisibleListSourceRowCount;

            }
          
            protected override void OnListSourceChanged()
            {
                base.OnListSourceChanged();
                thisEdit.FilterRowCount = this.VisibleListSourceRowCount;
            }

        }

        protected override void RaiseClosed(ClosedEventArgs e)
        {
            base.RaiseClosed(e);
        }
     
        /// <summary>
        /// 设置显示文本
        /// </summary>
        /// <param name="format"></param>
        /// <param name="editValue"></param>
        /// <returns></returns>
        public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue)
        {           
            var text =  base.GetDisplayText(format, editValue);
            if ((text=="[EditValue is null]" ||string.IsNullOrEmpty(text)) && editValue != null && !string.IsNullOrEmpty(editValue.ToString()))
            {
                text = GetBindText(editValue.ToString());
            }
            return text;
        }

        protected override void OnContainerLoaded()
        {
            base.OnContainerLoaded();
            LoadAllData();
        }
        public delegate void ProfixerTextTriggerEventHandle(object sender, EventArgs e);
        public event ProfixerTextTriggerEventHandle ProfixerTextTrigger;

        public void ProfixerTrigger()
        {
            ProfixerTextTrigger(null,null);
        }
     
        #endregion

        #region  自定义操作部分

        //保存选项的行值
        public DataRowView rowView;

        public string GetCell(int Column)
        {
            if (rowView.Row.ItemArray.Length > Column)
            {
                return rowView.Row.ItemArray[Column].ToString();
            }
            else
            {
                throw new Exception("没有对应的列数[" + Column + "],请确认！");
            }
        }

        protected string sqlStr;
        public string Sql
        {
            get { return sqlStr; }
            set { sqlStr = value; }
        }

      


        /// <summary>
        /// 初始化项目，根据传入的方案类
        /// </summary>
        /// <param name="param"></param>
        public void AddPorjectByParam(E_GY_FANGANPZ_INPARM param)
        {
            ClearProject();
            FAPZ_INPARM = param;
            this.TextEditStyle = TextEditStyles.Standard;
            ProcessParm(param);
        }

        public void ProcessParm(E_GY_FANGANPZ_INPARM param)
        {
            if (param.XIANGMU != null)
            {
                for (int i = 0; i < param.XIANGMU.Count; i++)
                {
                    if (!string.IsNullOrEmpty(param.XIANGMU[i]))
                    {
                        InitPorject(param.XIANGMU[i], param.FANGANMING[i]);
                    }
                }
            }
            if (param.DicCanShu != null)
            {
                SetParam(param.DicCanShu);
            }
            else if (param.CANSHU != null)
            {
                Dictionary<string, string> inParamDic = new Dictionary<string, string>();
                string[] RuCan = param.CANSHU[0].Split('|');
                for (int i = 0; i < RuCan.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(RuCan[i]))
                    {
                        string xuhao = "@" + (i + 1).ToString().PadLeft(2, '0');
                        inParamDic.Add(xuhao, RuCan[i]);
                    }
                    else
                    {
                        string xuhao = "@" + (i + 1).ToString().PadLeft(2, '0');
                        inParamDic.Add(xuhao, "");
                    }
                }
                SetParam(inParamDic);
            }
        }

         /// <summary>
        /// 初始方案
        /// </summary>
        /// <param name="XiangMu">项目名</param>
        /// <param name="FangAnMing">方案名</param>
        public void AddPorject(string XiangMu, string FangAnMing)
        {
            if (this.FAPZ_INPARM == null)
            {
                this.FAPZ_INPARM = new E_GY_FANGANPZ_INPARM() { XIANGMU=new List<string>(),FANGANMING=new List<string>(),CANSHU=new List<string>(),DicCanShu=new Dictionary<string,string>()};
            }
            this.FAPZ_INPARM.XIANGMU.Add(XiangMu);
            this.FAPZ_INPARM.FANGANMING.Add(FangAnMing);
            if (this.TextEditStyle != TextEditStyles.Standard)
            {
              this.TextEditStyle = TextEditStyles.Standard;
            }
            ProcessParm(this.FAPZ_INPARM);
        }

        /// <summary>
        /// 初始方案
        /// </summary>
        /// <param name="XiangMu">项目名</param>
        /// <param name="FangAnMing">方案名</param>
        private void InitPorject(string XiangMu, string FangAnMing)
        {
            ControlsQuery query = new ControlsQuery();
            FanganPeizhi fanganPeizhi = query.GetFanAn(XiangMu, FangAnMing, this.IsAllLoad);
            this.Sql = (string.IsNullOrEmpty(this.Sql) ? "" : this.Sql + " union all ") + fanganPeizhi.QuerySQL;
            this.QuerySql = this.Sql;
            this.XiangMuMing = XiangMu;
            
            //当添加多个方案时，后面方案的配置不改变前面方案的内容，只sql语句变化
            if (string.IsNullOrEmpty(this.XianShiLie))
            {
                //绑定值列
                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    this.ValueMember = fanganPeizhi.ShiJiLMC;
                }

                //绑定显示列
                if (string.IsNullOrEmpty(this.DisplayMember))
                {
                    this.DisplayMember = fanganPeizhi.XianShiLMC;
                }

                this.ColumnIndex = fanganPeizhi.ColumnIndex;
                this.XianShiLie = fanganPeizhi.XianShiLMC;
                this.IsPeiZhiCX = true;
                this.IsGuoLv = fanganPeizhi.IsGuoLv;
                this.FilterField = fanganPeizhi.FilterField;
                this.FilterType = fanganPeizhi.FilterType;


                //弹出框需显示的列信息设置
                if (fanganPeizhi.ColumnInfo.Count > 0)
                {
                    List<LookUpColumnInfo> listinfo = new List<LookUpColumnInfo>();
                    for (int i = 0; i < fanganPeizhi.ColumnInfo.Count; i++)
                    {
                        LookUpColumnInfo columnInfo = new LookUpColumnInfo();
                        columnInfo.FieldName = fanganPeizhi.ColumnInfo[i][0];
                        if (fanganPeizhi.ColumnInfo[i][1] != "")
                        {
                            columnInfo.Caption = fanganPeizhi.ColumnInfo[i][1];
                        }
                        columnInfo.Width = Convert.ToInt32(fanganPeizhi.ColumnInfo[i][2]);
                        listinfo.Add(columnInfo);
                       
                    }
                    this.Columns.AddRange(listinfo.ToArray());
                    this.FangAnColumns = listinfo;
                }

                this.PopformWidth = fanganPeizhi.PopformWidth;

                this.TextEditStyle = TextEditStyles.Standard;
              
            }
           
        }

        /// <summary>
        /// 加载所有数据
        /// </summary>
        private void LoadAllData()
        {
            if (this.IsAllLoad && this.DataSource == null)
            {
                ControlsQuery query = new ControlsQuery();
                string sql = "";
                if (this.QuerySql != null)
                {
                    sql = this.QuerySql.Replace("EditText", "%");
                }
                else
                {
                    sql = this.Sql.Replace("EditText", "%");
                }
                DataTable dt = query.QuerySql(sql);
                if (dt == null || dt.Rows.Count < 1)
                {
                    this.DataSource = null;
                    this.FilterRowCount = 0;
                    return;
                }
                this.DataSource = dt;
                this.FilterRowCount = dt.Rows.Count;
            }
        }

       /// <summary>
       /// 清除项目
       /// </summary>
        public void ClearProject()
        {
            this.Sql = "";
            this.QuerySql = "";
            //this.Tag = null;
            this.ValueMember = null;
            this.ProfixerText = null;
            this.ColumnIndex = null;
            this.XianShiLie = "";
            this.Columns.Clear();
        }

        /// <summary>
        /// 替换需要带入sql语句的参数
        /// </summary>
        /// <param name="param"></param>
        public void SetQueryParam(Dictionary<string, string> param)
        {
            if (this.FAPZ_INPARM != null)
            {
                this.FAPZ_INPARM.DicCanShu = param;
            }
            SetParam(param);
        }

        /// <summary>
        /// 替换需要带入sql语句的参数
        /// </summary>
        /// <param name="param"></param>
        public void SetParam(Dictionary<string, string> param)
        {
            string querysql = this.Sql;
            DataTable dt = this.DataSource as DataTable;
            foreach (var item in param)
            {
                if (dt != null && dt.Columns.Contains(item.Value))
                {
                    if (this.FilterField == null && (item.Value == "SHURUMA1" || item.Value == "SHURUMA2" || item.Value == "HZSRM1"))
                    {
                        this.FilterField = new string[] { item.Value };
                    }
                }
                querysql = querysql.Replace(item.Key, item.Value);
            }
            this.QuerySql = querysql;
        }


        /// <summary>
        /// 设置绑定配置，通常在一列后台显示代码界面显示名字，例后台科室代码，界面显示科室名称
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="ValueColumn"></param>
        /// <param name="DisplayColumn"></param>
        public void SetBindConfig(object DataSource, string ValueColumn, string DisplayColumn)
        {
            if (DataSource == null)
            {
                return;
            }
            ValueColumn = ValueColumn.ToUpper();
            DisplayColumn = DisplayColumn.ToUpper();
            Hashtable hashTable = new Hashtable();
            if (DataSource.GetType().Name == "List`1")
            {
                IEnumerable<object> list = DataSource as IEnumerable<object>;
                foreach (var dtItem in list)
                {
                    string text = dtItem.GetType().GetProperty(ValueColumn).GetValue(dtItem).ToString();
                    if (!hashTable.ContainsKey(text))
                    {
                        hashTable.Add(text, dtItem.GetType().GetProperty(DisplayColumn).GetValue(dtItem).ToString());
                    }
                }
            }
            else if (DataSource.GetType().Name == "DataTable")
            {
                DataTable dt = DataSource as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!hashTable.ContainsKey(dr[ValueColumn].ToString()))
                    {
                        hashTable.Add(dr[ValueColumn].ToString(), dr[DisplayColumn].ToString());
                    }
                }
            }
            this.BindDataSource = hashTable;
            this.BindKey = ValueColumn;
            this.BindValue = DisplayColumn;
        }

        /// <summary>
        /// 设置为ComboBox类型下拉框,对应设定数据源，值列，显示列，及是否要根据输入码搜索
        /// </summary>
        /// <param name="DataSource">数据源</param>
        /// <param name="ValueColumn">实际值列</param>
        /// <param name="DisplayColumn">显示列</param>
        /// <param name="isKuSuSS">是否根据输入码搜索</param>
        public void SetComboBox(object DataSource, string ValueColumn, string DisplayColumn,bool isKuSuSS=false)
        {
            if (DataSource == null)
            {
                return;
            }
            ValueColumn = ValueColumn.ToUpper();
            DisplayColumn = DisplayColumn.ToUpper();
            DataTable Table = new DataTable();
            //Table.Columns.Add(DisplayColumn);
            //Table.Columns.Add(ValueColumn);
            //Table.Columns.Add("SHURUMA1");
            //Table.Columns.Add("SHURUMA2");
            if (isKuSuSS)
            {
                this.TextEditStyle = TextEditStyles.Standard;
                if (HISClientHelper.SHURUMLX != null)
                {
                    if (this.FilterField == null)
                    {
                        this.FilterField = new string[] { HISClientHelper.SHURUMLX };
                    }
                    else
                    {
                        List<string> list = this.FilterField.ToList();
                        list.Add(HISClientHelper.SHURUMLX);
                        this.FilterField = list.ToArray();
                    }
                }
                else
                {
                    if (this.FilterField == null)
                    {
                        this.FilterField = new string[] { "SHURUMA1" };
                    }
                    else
                    {
                        List<string> list = this.FilterField.ToList();
                        list.Add("SHURUMA1");
                        this.FilterField = list.ToArray();
                    }
                }
            }
            if (DataSource.GetType().Name == "List`1")
            {
                IEnumerable<object> list = DataSource as IEnumerable<object>;
                //if (isKuSuSS)
                //{
                //    foreach (var dtItem in list)
                //    {

                //        string text = dtItem.GetType().GetProperty(ValueColumn).GetValue(dtItem).ToString();
                //        string SHURUMA1 = "";
                //        string SHURUMA2 = "";
                //        string SHURUMA3 = "";
                //        if (isKuSuSS)
                //        {
                //            ShuRuMaHelper.GetShuRuMa(DisplayColumn, out SHURUMA1, out SHURUMA2, out SHURUMA3);
                //        }
                //        Table.Rows.Add(text, dtItem.GetType().GetProperty(DisplayColumn).GetValue(dtItem).ToString(), SHURUMA1, SHURUMA2);
                //    }
                //}
                //else
                //{
                    this.DataSource = DataSource;
                //}
            }
            else if (DataSource.GetType().Name == "DataTable")
            {
                   DataTable dt = (DataSource as DataTable);
                   if (isKuSuSS && !dt.Columns.Contains(HISClientHelper.SHURUMLX))
                    {
                        Table = dt.Copy();
                        Table.Columns.Add("SHURUMA1");
                        Table.Columns.Add("SHURUMA2");
                        foreach (DataRow dr in Table.Rows)
                        {
                            string SHURUMA1 = "";
                            string SHURUMA2 = "";
                            string SHURUMA3 = "";
                           
                            ShuRuMaHelper.GetShuRuMa(DisplayColumn, out SHURUMA1, out SHURUMA2, out SHURUMA3);

                            dr["SHURUMA1"] = SHURUMA1;
                            dr["SHURUMA2"] = SHURUMA2;
                        }
                        this.DataSource = Table;
                   }
                   else
                   {
                       this.DataSource = dt;              
                   }
            }         
            this.ValueMember = ValueColumn;
            this.DisplayMember = DisplayColumn;
            this.Columns.Add(new LookUpColumnInfo { FieldName = DisplayColumn });
            this.ShowFooter = false;
            this.ShowHeader = false;
            this.NullText = "";
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
                Hashtable ht = this.BindDataSource as Hashtable;
                if (ht != null && ht.ContainsKey(KeyText))
                {
                    returnValue = ht[KeyText].ToString();
                }
                //else if (!this.IsNotClearValue)
                //{
                //    returnValue = "";
                //}

            }
            return returnValue;
        }


        #region  由于在ItemLookUpEdit中新定义的属性无法传值，特殊处理将值都统一存到原属性tag里面
        public string[] FilterField
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).FilterField;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).FilterField = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { FilterField = value };
                }
            }
        }

        public string[] FilterType
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).FilterType;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).FilterType = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { FilterType = value };
                }
            }
        }

        public string QuerySql
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).QuerySql;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).QuerySql = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { QuerySql = value };
                }
            }
        }

        public object[] Param
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).Param;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).Param = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { Param = value };
                }
            }
        }


        public bool IsPeiZhiCX
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).IsPeiZhiCX;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).IsPeiZhiCX = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { IsPeiZhiCX = value };
                }
            }
        }

        public int FilterRowCount
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).FilterRowCount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).FilterRowCount = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { FilterRowCount = value };
                }
            }
        }

        public int PopformWidth
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).PopformWidth;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).PopformWidth = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { PopformWidth = value };
                }
            }
        }

        public bool IsGuoLv
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).IsGuoLv;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).IsGuoLv = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { IsGuoLv = value };
                }
            }
        }

        public object BindDataSource
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).BindDataSource;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).BindDataSource = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { BindDataSource = value };
                }
            }
        }

        public string BindKey
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).BindKey;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).BindKey = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { BindKey = value };
                }
            }
        }

        public string BindValue
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).BindValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).BindValue = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { BindValue = value };
                }
            }
        }

        public string ItemValue
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).ItemValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).ItemValue = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { ItemValue = value };
                }
            }
        }

        public Dictionary<string, int> ColumnIndex
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).ColumnIndex;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).ColumnIndex = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { ColumnIndex = value };
                }
            }
        }

        public string ProfixerText
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).ProfixerText;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).ProfixerText = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { ProfixerText = value };
                }
            }
        }

        public bool IsAllLoad
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).IsAllLoad;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).IsAllLoad = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { IsAllLoad = value };
                }
            }
        }

        public bool IsNotClearValue
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).IsNotClearValue;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).IsNotClearValue = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { IsNotClearValue = value };
                }
            }
        }

        public string XianShiLie
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).XianShiLie;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).XianShiLie = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { XianShiLie = value };
                }
            }
        }

        public string XiangMuMing
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).XiangMuMing;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).XiangMuMing = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { XiangMuMing = value };
                }
            }
        }

        public MediLookUpEdit OwnerObj
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).OwnerObj;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).OwnerObj = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { OwnerObj = value };
                }
            }
        }

        public E_GY_FANGANPZ_INPARM FAPZ_INPARM
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).FAPZ_INPARM;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).FAPZ_INPARM = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { FAPZ_INPARM = value };
                }
            }
        }

        public bool IsLoad
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).IsLoad;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).IsLoad = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { IsLoad = value };
                }
            }
        }

        public string  CurrentKeyChar
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).CurrentKeyChar;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).CurrentKeyChar = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { CurrentKeyChar = value };
                }
            }
        }
        /// <summary>
        /// 方案配置列
        /// </summary>
        public List<LookUpColumnInfo> FangAnColumns
        {
            get
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    return (this.Tag as LookUpEditConfig).FangAnColumns;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (this.Tag != null && this.Tag.GetType().Name == "LookUpEditConfig")
                {
                    (this.Tag as LookUpEditConfig).FangAnColumns = value;
                }
                else
                {
                    this.Tag = new LookUpEditConfig() { FangAnColumns = value };
                }
            }
        }

        public class LookUpEditConfig
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
            /// 是否为配置查询的
            /// </summary>
            public int FilterRowCount { get; set; }

            /// <summary>
            /// 弹出窗口宽度
            /// </summary>
            public int PopformWidth { get; set; }

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
            /// 显示列
            /// </summary>
            public string XianShiLie { get; set; }

            /// <summary>
            /// 方案项目名
            /// </summary>
            public string XiangMuMing { get; set; }

            public MediLookUpEdit OwnerObj { get; set; }

            public E_GY_FANGANPZ_INPARM FAPZ_INPARM { get; set; }

            /// <summary>
            /// 是否已加载
            /// </summary>
            public bool IsLoad { get; set; }

            public List<LookUpColumnInfo> FangAnColumns { get; set; }

            public string CurrentKeyChar { get; set; }

        }
        #endregion

        #endregion
    }



    #region 输入框功能

    [ToolboxItem(true)]
    public class MediLookUpEdit : LookUpEdit
    {
        static MediLookUpEdit()
        {
            RepositoryItemMediLookUpEdit.RegisterMediLookUpEdit();
        }

        public MediLookUpEdit()
        {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediLookUpEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediLookUpEdit;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediLookUpEdit.CustomEditName;
            }
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediLookUpEditPopupForm(this);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //
            this.Properties.CurrentKeyChar = e.KeyChar.ToString();
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.EditValue = "";
                if (this.Properties.IsPeiZhiCX)
                {
                    this.Properties.DataSource = null;
                }
                this.Text = "";
            }
            base.OnKeyPress(e);
          
        }
        protected override void OnMaskBox_ValueChanged(object sender, EventArgs e)
        {
            string YuanText = this.Text;
            string keyText = this.Text;
            this.BeginUpdate();
            this.Properties.FilterRowCount = 0;
            if (this.Properties.ProfixerText != null)
            {
                if (this.Text == this.Properties.ProfixerText)
                {
                    this.EndUpdate();
                    this.Properties.ProfixerTrigger();
                    return;
                }
                keyText = YuanText.Replace(this.Properties.ProfixerText, "");
            }

            if (this.Properties.QuerySql == null && this.Properties.IsPeiZhiCX && (!this.Properties.IsGuoLv || (this.Properties.IsGuoLv && (keyText.Length == 0 || (keyText.Length == 1 && this.Properties.CurrentKeyChar != "\b")))))
            {
                this.QuerySql = this.Sql.Replace("EditText", keyText.ToUpper());
                QueryData(keyText);
                //第一次加载全部的，需要进行过滤
                if (this.Properties.IsAllLoad)
                {
                    if (this.Properties.FilterField == null)
                    {
                        this.Properties.FilterField = new string[1] { this.Properties.DisplayMember };
                    }
                    base.OnMaskBox_ValueChanged(sender, e);
                }
            }
            else if (this.Properties.QuerySql != null && this.Properties.IsPeiZhiCX && (!this.Properties.IsGuoLv || (this.Properties.IsGuoLv && (keyText.Length == 0 || (keyText.Length == 1 && this.Properties.CurrentKeyChar != "\b")))))
            {
                this.QuerySql = this.Properties.QuerySql.Replace("EditText", keyText.ToUpper());
                QueryData(keyText);

                //第一次加载全部的，需要进行过滤
                if (this.Properties.IsAllLoad)
                {
                    if (this.Properties.FilterField == null)
                    {
                        this.Properties.FilterField = new string[1] { this.Properties.DisplayMember };
                    }
                    base.OnMaskBox_ValueChanged(sender, e);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(YuanText))
                {
                    if (this.Properties.FilterField == null)
                    {
                        this.Properties.FilterField = new string[1] { this.Properties.DisplayMember };
                    }
                    base.OnMaskBox_ValueChanged(sender, e);
                }


            }

            if (string.IsNullOrEmpty(YuanText))
            {
               if (this.Properties.IsPeiZhiCX )
                {
                    this.Properties.DataSource = null;
                    this.Properties.FilterRowCount = 0;
                }
                else
                {
                    this.ClosePopup();
                }
                this.EditValue = "";
            }
          
            this.Text = YuanText;
          

            this.EndUpdate();

            if (this.Properties.DataSource == null)
            {
                return;
            }
            if (this.Properties.Columns.Count == 1)
            {
                this.PopupForm.Width = this.Width;
            }
            else if (this.PopupForm != null && this.PopupForm.Width < this.Properties.PopformWidth)
            {
                this.PopupForm.Width = this.Properties.PopformWidth;
            }
            if (this.PopupForm != null)
            {
                this.PopupForm.SelectedIndex = 0;
                if (this.PopupForm.Controls.Find("lab_TiShi", true).Length > 0)
                {
                    ((Label)this.PopupForm.Controls.Find("lab_TiShi", true)[0]).Text = "共有" + this.Properties.FilterRowCount + "条记录";
                }
            }

        }



        /// <summary>
        /// 必须要的
        /// </summary>
        /// <param name="e"></param>
        protected override void ProcessAutoSearchChar(KeyPressEventArgs e)
        {
            //base.ProcessAutoSearchChar(e);
            if (string.IsNullOrEmpty(this.Text))
            {
                this.EditValue = "";
            }

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //if (this.Properties.FAPZ_INPARM != null && !this.Properties.IsLoad)
            //{
            //    if (isWaiChengKJ)
            //    {
            //        ProcessParam(this.Properties.FAPZ_INPARM);
            //    }
            //    else
            //    {
            //        this.Properties.ProcessParm(this.Properties.FAPZ_INPARM);
            //    }
            //    this.Properties.IsLoad = true;//设置为已经加载过
            //}
            //if (this.Properties.FangAnColumns != null && this.Properties.Columns.Count==0)
            //{
            //    this.Properties.Columns.AddRange(this.Properties.FangAnColumns.ToArray());
            //}

            #region 当设置为可全部加载时  ，点击三角按钮时加载数据
            if (this.Properties.IsPeiZhiCX && this.Properties.IsAllLoad  && (this.Width - e.X) < 12 )
            {
                this.LoadAllData();
            }
            #endregion

            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left && this.Text != "")
            {
                this.SelectionStart = 0;
                this.SelectAll();
            }
        }

        protected override void ParseEditorValue()
        {
            base.ParseEditorValue();
        }
        protected override int FindItem(string text, bool partialSearch, int startIndex)
        {
            return -1;
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();

            //光标定位到末尾
            this.Select(this.Text.Length, 0);
        }

        //protected override void OnGotFocus(EventArgs e)
        //{
        //    if (Properties.OwnerObj == null)
        //    {
        //        Properties.OwnerObj = this;
        //    }
        //    base.OnGotFocus(e);
        //}

        /// <summary>
        /// 窗口关闭时更新editvalue
        /// </summary>
        /// <param name="closeMode"></param>
        /// <param name="acceptValue"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        protected override void UpdateEditValueOnClose(PopupCloseMode closeMode, bool acceptValue, object newValue, object oldValue)
        {
            base.UpdateEditValueOnClose(closeMode, acceptValue, newValue, oldValue);
            try
            {
                var rowObj = GetSelectedDataRow();
                if (rowObj == null)
                {
                    return;
                }
                if (rowObj.GetType().Name == "DataRowView")
                {
                    rowView = (DataRowView)rowObj;
                }
                else
                {
                    rowView = (DataRowView)ToDataTableRow(rowObj);
                }
                //rowView = Properties.GetDataSourceRowByKeyValue(newValue) as DataRowView; //(DataRowView)GetSelectedDataRow();
                //自定义查询返回row的时候
                if (this.Properties.IsPeiZhiCX)
                {
                    
                    if (rowView != null)
                    {
                        Properties.ItemValue = newValue.ToString();
                    }
                    else if (!Properties.IsNotClearValue)
                    {
                        Properties.OwnerEdit.EditValue = "";
                        newValue = "";
                    }
                    else
                    {
                        newValue = "";
                    }
                }
                if (Properties.IsNotClearValue && (newValue == null || string.IsNullOrEmpty(newValue.ToString())) && !string.IsNullOrEmpty(this.Text))
                {
                    Properties.OwnerEdit.EditValue = this.Text;
                }

                if (!Properties.IsNotClearValue && (newValue == null || string.IsNullOrEmpty(newValue.ToString()) && !string.IsNullOrEmpty(this.Text)))
                {
                    this.Text = "";
                }
            }
            catch (Exception ex)
            { }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
        }


        #region  //自定义操作

        //保存选项的行值
        public DataRowView rowView;


        private bool isWaiChengKJ = false;
        /// <summary>
        /// 根据列名获取对应列值
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public string GetValueByName(string ColumnName)
        {
            if (this.Properties.ColumnIndex.ContainsKey(ColumnName))
            {
                return GetCell(this.Properties.ColumnIndex[ColumnName]);
            }
            else
            {
                return "列表无此项目";
            }
        }

        /// <summary>    
        /// 将集合类转换成DataTable    
        /// </summary>    
        /// <param name="list">集合</param>    
        /// <returns></returns>    
        private static DataRowView ToDataTableRow(object entiy)
        {
            DataTable result = new DataTable();

            PropertyInfo[] propertys = entiy.GetType().GetProperties();

            foreach (PropertyInfo pi in propertys)
            {
                Type colType = pi.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                result.Columns.Add(new DataColumn(pi.Name, colType));

            }
       
            ArrayList tempList = new ArrayList();
            foreach (PropertyInfo pi in propertys)
            {
                object obj = pi.GetValue(entiy, null);
                tempList.Add(obj);
            }
            object[] array = tempList.ToArray();
            result.LoadDataRow(array, true);
            foreach (DataRowView item in result.DefaultView)
            {
                return item;
            }
            return null;
        }


        /// <summary>
        /// 获取选中行的所有列数据,每列按|分隔,返回对应拼接的字符串
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public string GetRowString()
        {
            string returnVal="";
            var item=rowView.Row.ItemArray;
            for (int i = 0; i < item.Length;i++ )
            {
                returnVal += item[i] + "|";
            }
            return returnVal;
        }

        /// <summary>
        /// 根据列序号获取列值
        /// </summary>
        /// <param name="Column"></param>
        /// <returns></returns>
        public string GetCell(int Column)
        {
            if (rowView.Row.ItemArray.Length > Column)
            {
                return rowView.Row.ItemArray[Column].ToString();
            }
            else
            {
                throw new Exception("没有对应的列数[" + Column + "],请确认！");
            }
        }

        /// <summary>
        /// 替换对应条件前的查询语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 替换对应查询条件的最终查询语句
        /// </summary>
        private string QuerySql { get; set; }


        /// <summary>
        /// 初始化项目，根据传入的方案类
        /// </summary>
        /// <param name="param"></param>
        public void AddPorjectByParam(E_GY_FANGANPZ_INPARM param)
        {
            ClearProject();
            isWaiChengKJ = true;//是否外层控件
            this.Properties.FAPZ_INPARM = param;
            this.Properties.TextEditStyle = TextEditStyles.Standard;
            ProcessParam(param);
        }

        private void ProcessParam(E_GY_FANGANPZ_INPARM param)
        {
            if (param.XIANGMU != null)
            {
                for (int i = 0; i < param.XIANGMU.Count; i++)
                {
                    if (!string.IsNullOrEmpty(param.XIANGMU[i]))
                    {
                        InitPorject(param.XIANGMU[i], param.FANGANMING[i]);
                    }
                }
            }
            if (param.DicCanShu != null)
            {
                SetParam(param.DicCanShu);
            }
            else if (param.CANSHU != null)
            {
                Dictionary<string, string> inParamDic = new Dictionary<string, string>();
                string[] RuCan = param.CANSHU[0].Split('|');
                for (int i = 0; i < RuCan.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(RuCan[i]))
                    {
                        string xuhao = "@" + (i + 1).ToString().PadLeft(2, '0');
                        inParamDic.Add(xuhao, RuCan[i]);
                    }
                    else
                    {
                        string xuhao = "@" + (i + 1).ToString().PadLeft(2, '0');
                        inParamDic.Add(xuhao, "");
                    }
                }
                SetParam(inParamDic);
            }
        }

        /// <summary>
        /// 初始方案
        /// </summary>
        /// <param name="XiangMu">项目名</param>
        /// <param name="FangAnMing">方案名</param>
        public void AddPorject(string XiangMu, string FangAnMing)
        {
            if (this.Properties.FAPZ_INPARM == null)
            {
                this.Properties.FAPZ_INPARM = new E_GY_FANGANPZ_INPARM() { XIANGMU = new List<string>(), FANGANMING = new List<string>(), CANSHU = new List<string>(), DicCanShu = new Dictionary<string, string>() };
              
            }
            this.Properties.FAPZ_INPARM.XIANGMU.Add(XiangMu);
            this.Properties.FAPZ_INPARM.FANGANMING.Add(FangAnMing);
            this.Properties.TextEditStyle = TextEditStyles.Standard;
            ProcessParam(this.Properties.FAPZ_INPARM);
        }
        /// <summary>
        /// 初始化各选项
        /// <param name="XiangMu">项目名</param>
        /// <param name="FangAnMing">方案名</param>
        /// </summary>
        private void InitPorject(string XiangMu, string FangAnMing)
        {
            ControlsQuery query = new ControlsQuery();
            FanganPeizhi fanganPeizhi = query.GetFanAn(XiangMu, FangAnMing, this.Properties.IsAllLoad);
            this.Sql = (string.IsNullOrEmpty(this.Sql) ? "" : this.Sql + " union all ") + fanganPeizhi.QuerySQL;
            this.QuerySql = this.Sql;
            this.Properties.XiangMuMing = XiangMu;
            this.Text = "";
            //当添加多个方案时，后面方案的配置不改变前面方案的内容，只sql语句编号
            if (string.IsNullOrEmpty(this.Properties.XianShiLie))
            {
                //绑定值列
                if (string.IsNullOrEmpty(this.Properties.ValueMember))
                {
                    this.Properties.ValueMember = fanganPeizhi.ShiJiLMC;
                }

                //绑定显示列
                if (string.IsNullOrEmpty(this.Properties.DisplayMember))
                {
                    this.Properties.DisplayMember = fanganPeizhi.XianShiLMC;
                }
                this.Properties.XianShiLie = fanganPeizhi.XianShiLMC;
                this.Properties.ColumnIndex = fanganPeizhi.ColumnIndex;

                this.Properties.IsPeiZhiCX = true;
                this.Properties.IsGuoLv = fanganPeizhi.IsGuoLv;
                this.Properties.FilterField = fanganPeizhi.FilterField;
                this.Properties.FilterType = fanganPeizhi.FilterType;
                //弹出框需显示的列信息设置
                if (fanganPeizhi.ColumnInfo.Count > 0)
                {
                    for (int i = 0; i < fanganPeizhi.ColumnInfo.Count; i++)
                    {
                        LookUpColumnInfo columnInfo = new LookUpColumnInfo();
                        columnInfo.FieldName = fanganPeizhi.ColumnInfo[i][0];
                        if (fanganPeizhi.ColumnInfo[i][1] != "")
                        {
                            columnInfo.Caption = fanganPeizhi.ColumnInfo[i][1];
                        }
                        columnInfo.Width = Convert.ToInt32(fanganPeizhi.ColumnInfo[i][2]);
                        this.Properties.Columns.Add(columnInfo);
                    }
                }
                this.Properties.PopformWidth = fanganPeizhi.PopformWidth;

                if (this.Properties.IsAllLoad)
                {
                    DataTable dt = query.QuerySql(this.QuerySql);
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        this.Properties.DataSource = null;
                        this.Properties.FilterRowCount = 0;
                        return;
                    }
                    this.Properties.DataSource = dt;
                    this.Properties.FilterRowCount = dt.Rows.Count;
                }
            }

        }

        /// <summary>
        /// 加载所有数据
        /// </summary>
        private void LoadAllData()
        {
            if (this.Properties.IsAllLoad && this.Properties.DataSource == null )
            {
                ControlsQuery query = new ControlsQuery();
                string sql = "";
                if (this.Properties.QuerySql != null)
                {
                  sql=  this.Properties.QuerySql.Replace("EditText", "%");
                }
                else
                {
                  sql= this.Sql.Replace("EditText", "%");
                }
                DataTable dt = query.QuerySql(sql);
                if (dt == null || dt.Rows.Count < 1)
                {
                    this.Properties.DataSource = null;
                    this.Properties.FilterRowCount = 0;
                    return;
                }
                this.Properties.DataSource = dt;
                this.Properties.FilterRowCount = dt.Rows.Count;            
            }           
        }

      

        /// <summary>
        /// 清除项目
        /// </summary>
        public void ClearProject()
        {
            this.Sql = "";
            this.QuerySql = "";
            this.Properties.Tag = null;
            this.Properties.ValueMember = null;
            this.EditValue = "";
            this.Properties.ProfixerText = null;
            this.Properties.ColumnIndex = null;
            this.Properties.XianShiLie = "";
            this.Properties.Columns.Clear();
        }

        /// <summary>
        /// 设置方案需要替换的入参
        /// </summary>
        /// <param name="param"></param>
        public void SetQueryParam(Dictionary<string, string> param)
        {
            this.Properties.FAPZ_INPARM.DicCanShu = param;           
        }

        /// <summary>
        /// 设置方案需要替换的入参
        /// </summary>
        /// <param name="param"></param>
        public void SetParam(Dictionary<string, string> param)
        {
            string querysql = this.Sql;
            DataTable dt = this.Properties.DataSource as DataTable;
            foreach (var item in param)
            {
                if (dt != null && dt.Columns.Contains(item.Value))
                {
                    if (this.Properties.FilterField == null && (item.Value == "SHURUMA1" || item.Value == "SHURUMA2" || item.Value == "HZSRM1"))
                    {
                        this.Properties.FilterField = new string[] { item.Value };
                        //this.IsGuoLv = true;
                    }
                }
                querysql = querysql.Replace(item.Key, item.Value);
            }
            this.Sql = querysql;
        }

        /// <summary>
        /// 根据替换后的sql语句获取对应数据
        /// </summary>
        /// <param name="Text"></param>
        private void QueryData(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                this.Properties.DataSource = null;
                this.Properties.FilterRowCount = 0;
                return;
            }
            if (!this.Properties.IsGuoLv || (this.Properties.IsGuoLv && Text.Length == 1))
            {
                ControlsQuery query = new ControlsQuery();
                Debug.WriteLine("【" + this.Properties.XiangMuMing + "】方案执行查询语句：【" + this.QuerySql + "】");
                DataTable dt = query.QuerySql(this.QuerySql);

                var FilterType = "SHURUMA1"; 
                if (HISClientHelper.SHURUMLX=="SHURUMA2")
                {
                    FilterType = "SHURUMA2";
                }
                if (!dt.Columns.Contains(FilterType) && this.Properties.XianShiLie != null)
                {
                    dt.Columns.Add("SHURUMA1");
                    dt.Columns.Add("SHURUMA2");
                    foreach (DataRow dr in dt.Rows)
                    {
                        string SHURUMA1 = "";
                        string SHURUMA2 = "";
                        string SHURUMA3 = "";
                        string NeiRong = dr[this.Properties.XianShiLie].ToString();
                        ShuRuMaHelper.GetShuRuMa(NeiRong.ToUpper(),out SHURUMA1,out SHURUMA2,out SHURUMA3 );                        
                        dr["SHURUMA1"] = SHURUMA1;
                        dr["SHURUMA2"] = SHURUMA2;
                    }
                    if (this.Properties.FilterField == null || !this.Properties.FilterField.Contains(FilterType))
                    {
                        this.Properties.FilterField = new string[] { FilterType };
                    }
                    else if (!this.Properties.FilterField.Contains(FilterType))
                    {
                        List<string> filter = this.Properties.FilterField.ToList();
                        filter.Add(FilterType);
                        this.Properties.FilterField = filter.ToArray();
                    }
                }

                if (dt == null || dt.Rows.Count < 1)
                {
                    this.Properties.DataSource = null;
                    this.Properties.FilterRowCount = 0;
                    return;
                }
                this.Properties.DataSource = dt;
                this.Properties.FilterRowCount = dt.Rows.Count;
            }

        }
        #endregion
    }
    #endregion

    #region 继承基类 未改动
    public class MediLookUpEditViewInfo : LookUpEditViewInfo
    {
        public MediLookUpEditViewInfo(RepositoryItem item)
            : base(item)
        {
        }
    }

    public class MediLookUpEditPainter : ButtonEditPainter
    {
        public MediLookUpEditPainter()
        {
        }
    }
    #endregion

    #region 弹出窗口
    public class MediLookUpEditPopupForm : PopupLookUpEditForm
    {
        MediLookUpEdit mainEdit;
        public MediLookUpEditPopupForm(MediLookUpEdit ownerEdit)
            : base(ownerEdit)
        {
            if (ownerEdit.Properties.ShowFooter)
            {
                Label lb = new Label();
                lb.Name = "lab_TiShi";
                lb.Height = 20;
                lb.MouseHover+=lb_MouseHover;
                lb.MouseLeave+=lb_MouseLeave;
                this.Controls.Add(lb);   
                
            }
            mainEdit = ownerEdit;
            if (mainEdit.Properties.Columns.Count == 1)
            {
                mainEdit.Properties.PopupFormMinSize = new Size((mainEdit.Width - 16), mainEdit.Properties.PopupFormMinSize.Height);
            }
            this.Resize += MediLookUpEditPopupForm_Resize;
        }

        private void lb_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Text = "共有" + mainEdit.Properties.FilterRowCount + "条记录";
        }

        private void lb_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Text = "【"+mainEdit.Properties.XiangMuMing+"】";
        }

        private void MediLookUpEditPopupForm_Resize(object sender, EventArgs e)
        {
            if (this.Controls.Count > 3)
            {
                //int y = this.Height - 22;
                //int GridCtrolY = this.Controls[2].Location.Y;
                //if (GridCtrolY > 5)
                //{
                //    y = 5;
                //}                
                //this.Controls[3].Location = new Point { X = 30, Y = y };
                Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
                int y = this.Height - 22;
                var PingMuHeight = mainEdit.PointToScreen(new Point(0, 0));
                int GridCtrolY = this.Controls[2].Location.Y;
                if ((this.Height + PingMuHeight.Y + 60) > ScreenArea.Height)
                {
                    y = 3;
                }
                this.Controls[3].Location = new Point { X = 30, Y = y };
               
            }
        }

        public override void ShowPopupForm()
        {          
            if (mainEdit.Properties.PopformWidth > 200)
            {
                this.Size = new Size { Width = mainEdit.Properties.PopformWidth, Height = this.Size.Height };
            }
            if (mainEdit.Text == "")
            {
                return;
            }
            base.ShowPopupForm();
            if (this.Controls.Find("lab_TiShi", true).Length > 0)
            {
                string mingcheng = "";
                if (this.Width > 250)
                {
                    mingcheng = "--【" + mainEdit.Properties.XiangMuMing + "】";
                    ((Label)this.Controls.Find("lab_TiShi", true)[0]).Width = 230;
                }
                ((Label)this.Controls.Find("lab_TiShi", true)[0]).Text = "共有" + mainEdit.Properties.FilterRowCount + "条记录" +mingcheng; 
            }
        }

    }
    #endregion

}
