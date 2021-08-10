using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.HIS.GongYong;
using Mediinfo.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Mediinfo.WinForm.HIS.Controls.NewFrmDataLayOutSet;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class New2FrmDataLayoutSet : MediDialog
    {
        public New2FrmDataLayoutSet()
        {
            InitializeComponent();
          
        }
        #region 属性

        /// <summary>
        /// 窗体名称
        /// </summary>
        private string _FormName;

        /// <summary>
        /// 控件名称
        /// </summary>
        private string _ControlName;

        /// <summary>
        /// 命名空间
        /// </summary>
        private string _NameSpace;

       

        /// <summary>
        /// 数据布局
        /// </summary>
        private MediDataLayoutControl _MediDataLayoutControl = null;

        /// <summary>
        /// 属性集合
        /// </summary>
        public List<NewItemLayoutAttribute> ItemLayoutAttributeList { get; set; }

        /// <summary>
        /// 存储布局信息
        /// </summary>
        private E_GY_DATALAYOUT1 _EDataLayout1 = null;

        /// <summary>
        /// 存储布局信息详情
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;

        /// <summary>
        /// 创建服务实例
        /// </summary>
        private GYDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// 数据源
        /// </summary>
        private GridColumnInfo gridColumnInfo = null;

        #endregion  
        /// <summary>
        /// 根据DataLayOutControl获取字段
        /// </summary>
        /// <param name="sFormName"></param>
        /// <param name="sControlName"></param>
        /// <param name="MediDataLayoutControl"></param>
        public New2FrmDataLayoutSet(string sFormName, string sControlName, string sNameSpace, MediDataLayoutControl control)
        {
            InitializeComponent();
            _FormName = sFormName;
            _ControlName = sControlName;
            _MediDataLayoutControl = control;
            _NameSpace = sNameSpace;
            this.mediDataLayOutTitleBar.LabelText = "名称:"+string.Format("{0}.{1}", _FormName, _ControlName);
            this.Text = "项属性";
            _GYDataLayoutService = new GYDataLayoutService();
        }

        /// <summary>
        /// 加载默认的页面布局
        /// </summary>
        /// <returns></returns>
        private bool LoadDefaultDataLayoutAttribute()
        {
          
            if (_MediDataLayoutControl != null)
            {
                //最外层的Group
                LayoutControlGroup layGroup1 = _MediDataLayoutControl.Root;

                RecursionLayoutControl(layGroup1);
            }

            return true;
        }


        private void InitialLookUpeditControlBindDataSource()
        {
           
            //列头对齐方式
            List<HIS.Controls.NewFrmDataLayOutSet.ColumnHeaderHAlignment> columnHeaderHAlignment = new List<HIS.Controls.NewFrmDataLayOutSet.ColumnHeaderHAlignment>();
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Default, ColumnHeaderHAlignmentCode = 0 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Near, ColumnHeaderHAlignmentCode = 1 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Center, ColumnHeaderHAlignmentCode = 2 });
            columnHeaderHAlignment.Add(new ColumnHeaderHAlignment() { ColumnHeaderHAlignmentName = HorzAlignment.Far, ColumnHeaderHAlignmentCode = 3 });
            this.rpiMediGridHAligentLookUpEdit.DataSource = columnHeaderHAlignment;
            rpiMediGridHAligentLookUpEdit.DisplayMember = "ColumnHeaderHAlignmentName";
            rpiMediGridHAligentLookUpEdit.ValueMember = "ColumnHeaderHAlignmentCode";
            rpiMediGridHAligentLookUpEdit.View.OptionsView.ShowIndicator = false;


        

            //输入法模式
            List<HIS.Controls.NewFrmDataLayOutSet.ImeMode> imeMode = new List<HIS.Controls.NewFrmDataLayOutSet.ImeMode>();
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Inherit, ImeModeCode = -1 });          
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.NoControl, ImeModeCode = 0 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.On, ImeModeCode = 1 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Off, ImeModeCode = 2 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Disable, ImeModeCode = 3 });        
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Hiragana, ImeModeCode = 4 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Katakana, ImeModeCode = 5 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.KatakanaHalf, ImeModeCode = 6 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.AlphaFull, ImeModeCode = 7 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Alpha, ImeModeCode = 8 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.HangulFull, ImeModeCode = 9 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Hangul, ImeModeCode = 10 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.Close, ImeModeCode = 11 });
            imeMode.Add(new HIS.Controls.NewFrmDataLayOutSet.ImeMode() { ImeModeName = System.Windows.Forms.ImeMode.OnHalf, ImeModeCode = 12 });
            this.rpiMediGridImodeLookUpEdit.DataSource = imeMode;
            rpiMediGridImodeLookUpEdit.DisplayMember = "ImeModeName";
            rpiMediGridImodeLookUpEdit.ValueMember = "ImeModeCode";
            rpiMediGridImodeLookUpEdit.View.OptionsView.ShowIndicator = false;






        }

        /// <summary>
        /// 从数据库加载dalayout页面布局属性
        /// </summary>
        /// <returns></returns>
        private bool LoadDBDataLayoutAttribute()
        {
            if (_EDataLayout1 == null) return false;

            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) return false;

            int index = 1;
            if (ItemLayoutAttributeList == null)
                ItemLayoutAttributeList = new List<NewItemLayoutAttribute>();
            else
                ItemLayoutAttributeList.Clear();

            _EDataLayout2.ToList().ForEach(o => {
                string feildName = o.FIELDNAME;
                if (!(ItemLayoutAttributeList.Where(c=>c.FIELDNAME==feildName).ToList().Count>0))
                {
                    NewItemLayoutAttribute itemLayoutAttribute = new NewItemLayoutAttribute();
                    itemLayoutAttribute.CAPTION = o.CAPTION;
                    itemLayoutAttribute.FIELDNAME = feildName;
                    itemLayoutAttribute.READONLY = o.READONLY.ToInt(0) == 0 ? false : true;
                    itemLayoutAttribute.DEFAULTVALUE = o.DEFAULTVALUE;
                    itemLayoutAttribute.HEADERFONTSIZE = o.HEADERFONTSIZE.ToInt(9);
                    itemLayoutAttribute.VISIBLE = o.VISIBLE.ToInt(1) == 0 ? false : true;
                    itemLayoutAttribute.VALIDATEEXPRISSION = o.VALIDATEEXPRISSION;
                    itemLayoutAttribute.VALIDATEDESCRIBE = o.VALIDATEDESCRIBE;
                    itemLayoutAttribute.HEADERHALIGNMENT = o.HEADERHALIGNMENT.ToInt(0);
                    itemLayoutAttribute.TABINDEX = o.TABINDEX.ToInt(index);
                    itemLayoutAttribute.IMEMODE = o.IMEMODE;         
                    itemLayoutAttribute.DatalayoutID = o.DATALAYOUTID;
                    itemLayoutAttribute.DatalayoutmxID = o.DATALAYOUTMXID;
                    ItemLayoutAttributeList.Add(itemLayoutAttribute);
                }
            });

            return true;
        }



        /// <summary>
        /// 遍历DataLayoutControl控件中的输入框
        /// </summary>
        /// <param name="layGroup"></param>
        /// <param name="details"></param>
        private void RecursionLayoutControl(LayoutControlGroup layGroup)
        {
            if (layGroup == null) return;

            if (layGroup.Items == null || layGroup.Items.Count == 0) return;

            LayoutGroupItemCollection layoutGroupColl = layGroup.Items;
            int i = 0;
            foreach (BaseLayoutItem item in layoutGroupColl)
            {
                if (item is LayoutControlGroup)
                {
                    RecursionLayoutControl(item as LayoutControlGroup);
                }
                else
                {
                    if (item is LayoutControlItem)
                    {
                        #region

                        //获取数据条
                        LayoutControlItem layItem = item as LayoutControlItem;

                        //获取ControlItem包含的控件
                        BaseEdit control = layItem.Control as BaseEdit;

                        //获取该控件绑定的数据字段
                        if (null == control || control.DataBindings.Count == 0)
                            continue;

                        string sFeildName = control.DataBindings[0].BindingMemberInfo.BindingField;

                        #region  初始化缓存
                        if (ItemLayoutAttributeList == null)
                            ItemLayoutAttributeList = new List<NewItemLayoutAttribute>();
                        if ((ItemLayoutAttributeList.Where(c => c.FIELDNAME == sFeildName).ToList().Count > 0)) continue;

                        NewItemLayoutAttribute itemLayout = new NewItemLayoutAttribute();

                        E_GY_DATALAYOUT2 layout2 = null;

                        //先从数据库中查找
                        if (_EDataLayout2 != null)
                            layout2 = this._EDataLayout2.Where(c => c.FIELDNAME == sFeildName).FirstOrDefault();

                       
                        itemLayout.DatalayoutID = (layout2 != null ? layout2.DATALAYOUTID : string.Empty);
                        itemLayout.DatalayoutmxID = (layout2 != null ? layout2.DATALAYOUTMXID : string.Empty);

                        itemLayout.CAPTION = (layout2 != null ? layout2.CAPTION : layItem.Text);
                        itemLayout.FIELDNAME = sFeildName;
                        itemLayout.READONLY = (layout2 != null ? layout2.READONLY == 1 ? true : false : control.ReadOnly);
                        itemLayout.DEFAULTVALUE = (layout2 != null ? layout2.DEFAULTVALUE : string.Empty);
                        itemLayout.HEADERFONTSIZE = (layout2 != null ? layout2.HEADERFONTSIZE.Value : layItem.AppearanceItemCaption.Font.Size);
                        itemLayout.VISIBLE = (layout2 != null ? layout2.VISIBLE.ToBool(control.Visible) : control.Visible);
                        itemLayout.VALIDATEEXPRISSION = (layout2 != null ? layout2.VALIDATEEXPRISSION : string.Empty);
                        itemLayout.VALIDATEDESCRIBE = (layout2 != null ? layout2.VALIDATEDESCRIBE : string.Empty);
                        itemLayout.HEADERHALIGNMENT = Convert.ToInt32(layItem.AppearanceItemCaption.TextOptions.HAlignment);
                        itemLayout.TABINDEX = (layout2 != null ? layout2.TABINDEX.ToInt(control.TabIndex) : control.TabIndex);
                        itemLayout.IMEMODE = (layout2 != null ? layout2.IMEMODE : Convert.ToInt32(control.ImeMode).ToString());
                        ItemLayoutAttributeList.Add(itemLayout);

                        #endregion

                        i++;
                        #endregion

                    }
                }
            }
        }

        private void New2FrmDataLayoutSet_Load(object sender, EventArgs e)
        {
            InitialLookUpeditControlBindDataSource();
            //初始化数据集
            GetDataLayoutForDB();
            //绑定属性值
          if (_MediDataLayoutControl != null)
            {
                if (!(LoadDBDataLayoutAttribute()))
                    DataLayoutAttributeSet();
                this.mediGridControl1.DataSource = ItemLayoutAttributeList;
            }
        }


        /// <summary>
        /// 获取数据布局信息从数据库
        /// </summary>
        private void GetDataLayoutForDB()
        {
            var ret = _GYDataLayoutService.GetDataLayoutInfo(_ControlName, _FormName, _NameSpace, HISClientHelper.YINGYONGID);

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                _EDataLayout1 = ret.Return.DataLayout1;
                _EDataLayout2 = ret.Return.DataLayout2;

                if (_EDataLayout1 == null || _EDataLayout1.YINGYONGID == "00" || string.IsNullOrWhiteSpace(_EDataLayout1.YINGYONGID))
                {
                    this.radioGroupLevel.EditValue = "2";
                }
                else if (_EDataLayout1.YINGYONGID.Length == 2)
                {
                    this.radioGroupLevel.EditValue = "1";
                }
                else
                {
                    this.radioGroupLevel.EditValue = "0";
                }
            }
        }


        /// <summary>
        /// 设置DataLayout属性
        /// </summary>
        private void DataLayoutAttributeSet()
        {
            RecursionLayoutControl(this._MediDataLayoutControl.Root);
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonReset_Click(object sender, EventArgs e)
        {
            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();
            List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            if (_MediDataLayoutControl != null)
            {
                GetDataLayoutAttribute(ref eDataLayout1, ref eDatalayout2List);
            }
            eDataLayout1.SetState(DTOState.Delete);

            eDatalayout2List.ForEach(o => {
                o.SetState(DTOState.Delete);
            });
            Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(eDataLayout1, eDatalayout2List);

            if (result.Return)
            {
                MediMsgBox.Success("重置成功!");
            }
            else
            {
                MediMsgBox.Failure("重置失败!");
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 获取DataLayou数据源
        /// </summary>
        /// <param name="eDataLayout1"></param>
        /// <param name="eDatalayout2List"></param>
        private void GetDataLayoutAttribute(ref E_GY_DATALAYOUT1 eDataLayout1, ref List<E_GY_DATALAYOUT2> eDatalayout2List)
        {
            if (ItemLayoutAttributeList == null || ItemLayoutAttributeList.Count == 0) return;

            #region 处理DataLayout1数据
            //E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();

            if (_EDataLayout1 == null)
            {
                //等于空
                eDataLayout1.SetState(DTOState.New);
            }
            else
            {
                //不等空
                eDataLayout1.DATALAYOUTID = _EDataLayout1.DATALAYOUTID;
                eDataLayout1.SetState(DTOState.Update);
            }

            eDataLayout1.YINGYONGID = (radioGroupLevel.SelectedIndex == 0 ? HISClientHelper.YINGYONGID : (radioGroupLevel.SelectedIndex == 1 ? HISClientHelper.XITONGID : "00"));

            eDataLayout1.FORMNAME = _FormName;
            eDataLayout1.CONTROLNAME = _ControlName;
            eDataLayout1.NAMESPACE = _NameSpace;
            eDataLayout1.ALLOWFILTER = 0;
            eDataLayout1.ALLOWSORT = 0;
            eDataLayout1.ENABLECOLUMNMENU = 0;
            eDataLayout1.ORDERBYFIELD = "";
            eDataLayout1.SHOWGROUPPANEL = 0;
            eDataLayout1.LINENUMBER = 0;
            eDataLayout1.ROWBACKCOLOREXPRESSION = "";
            eDataLayout1.ROWBACKCOLORDESCRIBE = "";
            eDataLayout1.ROWFONTSIZE = 9;
            #endregion

            #region 处理DataLayout2数据
         
            bool update = true;
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) update = false;

            foreach (NewItemLayoutAttribute ItemLayoutAttribute in ItemLayoutAttributeList)
            {
              
                E_GY_DATALAYOUT2 eDataLayout2 = null;
                if (_EDataLayout2 == null)
                {
                    eDataLayout2 = DataLayoutAttributeToE(ItemLayoutAttribute, false);
                }
                else
                {
                    var count = _EDataLayout2.Where(o => o.FIELDNAME.ToUpper() == ItemLayoutAttribute.FIELDNAME.ToUpper()).Count();

                    if (count > 0)
                    {
                        eDataLayout2 = DataLayoutAttributeToE(ItemLayoutAttribute, update);
                    }
                    else
                    {
                        eDataLayout2 = DataLayoutAttributeToE(ItemLayoutAttribute, false);
                    }
                }
                eDatalayout2List.Add(eDataLayout2);
            }


            #endregion

        }

        /// <summary>
        /// 属性信息转化为实体
        /// </summary>
        /// <param name="itemLayoutAttribute"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        private E_GY_DATALAYOUT2 DataLayoutAttributeToE(NewItemLayoutAttribute itemLayoutAttribute, bool update)
        {
            E_GY_DATALAYOUT2 eDataLayout2 = new E_GY_DATALAYOUT2();
            eDataLayout2.SetTraceChange(true);
            eDataLayout2.DATALAYOUTID = itemLayoutAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = itemLayoutAttribute.DatalayoutmxID;
            eDataLayout2.SORTNO = 0;

            if (update) eDataLayout2.SetState(DTOState.Update);
            else eDataLayout2.SetState(DTOState.New);

            eDataLayout2.BACKCOLORDESCRIBE = "";
            eDataLayout2.BACKCOLOREXPRISSION = "";
            eDataLayout2.CAPTION = itemLayoutAttribute.CAPTION;
            eDataLayout2.CELLFONTSIZE = 9;
            eDataLayout2.CELLFORECOLORDESCRIBE = "";
            eDataLayout2.CELLFORECOLOREXPRISSION = "";
            eDataLayout2.CELLHALIGNMENT = 0;
            eDataLayout2.DATALAYOUTID = itemLayoutAttribute.DatalayoutID;
            eDataLayout2.DATALAYOUTMXID = itemLayoutAttribute.DatalayoutmxID;
            eDataLayout2.DEFAULTVALUE = itemLayoutAttribute.DEFAULTVALUE;
            eDataLayout2.FIELDNAME = itemLayoutAttribute.FIELDNAME;
            eDataLayout2.FIXED = 0;
            eDataLayout2.FORMATSTRING = "";
            eDataLayout2.FORMATTYPE = 0;
            eDataLayout2.HEADERFONTSIZE = Convert.ToInt32(itemLayoutAttribute.HEADERFONTSIZE);
            eDataLayout2.HEADERHALIGNMENT = Convert.ToInt32(itemLayoutAttribute.HEADERHALIGNMENT);
            eDataLayout2.IMEMODE = Convert.ToInt32(itemLayoutAttribute.IMEMODE).ToString();
            //eDataLayout2.NONEMPTYEDESCRIBE = itemLayoutAttribute.非空表达式说明;
            //eDataLayout2.NONEMPTYEXPRESSION = itemLayoutAttribute.非空表达式;
            eDataLayout2.READONLY = itemLayoutAttribute.READONLY ? 1 : 0;


            eDataLayout2.SORTNO = 0;
            eDataLayout2.TABINDEX = itemLayoutAttribute.TABINDEX;
            eDataLayout2.VALIDATEDESCRIBE = itemLayoutAttribute.VALIDATEDESCRIBE;
            eDataLayout2.VALIDATEEXPRISSION = itemLayoutAttribute.VALIDATEEXPRISSION;
            eDataLayout2.VISIBLE = itemLayoutAttribute.VISIBLE ? 1 : 0;
            eDataLayout2.WIDTH = 99;


            return eDataLayout2;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();
            List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            if (_MediDataLayoutControl != null)
            {

                GetDataLayoutAttribute(ref eDataLayout1, ref eDatalayout2List);
            }
            Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(eDataLayout1, eDatalayout2List);

            if (result.Return)
            {

                MediMsgBox.Success(this, "保存成功！");
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
            }

           
        }
        /// <summary>
        /// 保存关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton1_Click(object sender, EventArgs e)
        {
            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();
            List<E_GY_DATALAYOUT2> eDatalayout2List = new List<E_GY_DATALAYOUT2>();

            if (_MediDataLayoutControl != null)
            {

                GetDataLayoutAttribute(ref eDataLayout1, ref eDatalayout2List);
            }
            Result<bool> result = _GYDataLayoutService.SaveDataLayoutInfo(eDataLayout1, eDatalayout2List);

            if (result.Return)
            {

                MediMsgBox.Success(this, "保存成功！");
            }
            else
            {
                MediMsgBox.Failure(this, "保存失败！", result.ReturnCode.ToString(), result.ReturnMessage);
            }
            this.Close();
            this.Dispose();
           
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 快捷键功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New2FrmDataLayoutSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control&&e.KeyCode == Keys.S)
            {
                mediButtonSave_Click(null, null);
            }
            else if(e.Control && e.KeyCode == Keys.R)
            {
                mediButtonReset_Click(null, null);
            }else if (e.Control && e.KeyCode == Keys.X)
            {
                mediButtonClose_Click(null, null);
            }
        }
    }


    /// <summary>
    /// DataLayoutControl项属性
    /// </summary>
    public class NewItemLayoutAttribute 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string DatalayoutmxID { get; set; }

        /// <summary>
        /// 外键ID
        /// </summary>    
        public string DatalayoutID { get; set; }
        /// <summary>
        /// 字段名字
        /// </summary>
        public string FIELDNAME { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CAPTION{ get; set; }
        /// <summary>
        /// 显示标志
        /// </summary>
        public bool VISIBLE
        { get; set; }

       /// <summary>
       ///字体大小
       /// </summary>
        public float HEADERFONTSIZE
        { get; set; }

      /// <summary>
      /// 标题对齐方式
      /// </summary>
        public int HEADERHALIGNMENT
        { get; set; }


       /// <summary>
       /// 跳转顺序
       /// </summary>
        public int TABINDEX
        { get; set; }
        /// <summary>
        /// 保护标志
        /// </summary>
        public bool READONLY
        { get; set; }

       /// <summary>
       /// 初始值
       /// </summary>
        public string DEFAULTVALUE
        { get; set; }


      /// <summary>
      /// 输入法模式
      /// </summary>
        public string IMEMODE
        { get; set; }
        

       /// <summary>
       /// 有效性检查表达式
       /// </summary>
        public string VALIDATEEXPRISSION
        { get; set; }

      /// <summary>
      /// 有效性检查描述
      /// </summary>
        public string VALIDATEDESCRIBE
        { get; set; }

       

       
    }
}
