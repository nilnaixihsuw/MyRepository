using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraToolbox;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Utility.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 左右分割表 
    /// </summary>
    [ToolboxItem(true)]
    public class MediGridSplitContainer : GridSplitContainer
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediGridSplitContainer()
        {

        }

        /// <summary>
        /// //通知其他控件清除焦点 add by zhukp 2019-06-26
        /// </summary>
        public Action<MediGridView> FocuseRow;

        /// <summary>
        /// 当前聚焦的表格
        /// </summary>
        [Browsable(false)]
        public MediGridView FocusedMediGridView { get; set; }

        /// <summary>
        /// 获取当前焦点的表 add by songxl on 2019-4-2
        /// </summary>
        /// <returns></returns>
        public MediGridView GetFocusMediGridView()
        {
            switch (MediFocusedRowHandle % 2)
            {
                case 0:
                    return this.View as MediGridView;
                case 1:
                    return this.SplitChildGrid.MainView as MediGridView;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        protected override void CreateSplitView(BaseView view)
        {
            SplitChildGrid.ViewCollection.Clear();
            BaseView child = new MediGridView();

            SplitChildGrid.ViewCollection.Add(child);
            SplitChildGrid.MainView = child;
        }

        /// <summary>
        /// 创建Gridcontrol
        /// </summary>
        /// <returns></returns>
        protected override GridControl CreateGridControl()
        {
            return new MediGridControl();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (!SkinCat.Instance.IsDesignMode)
            {
                this.Grid.DataSourceChanged += Grid_DataSourceChanged;
                this.Grid.Load += Grid_Load;
                this.SplitViewCreated += MediGridSplitContainer_SplitViewCreated;
            }
        }

        private void Grid_Load(object sender, EventArgs e)
        {
            this.SynchronizeViews = DefaultBoolean.False;
            this.ShowSplitView();
            if (this.SplitChildGrid.MainView is MediGridView childView)
            {
                childView.BorderStyle = BorderStyles.Default;
                if (this.View is MediGridView mainView)
                {
                    mainView.BorderStyle = BorderStyles.Default;
                    mainView.EnabledFocusedRow();
                }

                childView.ClearFocuseRow();
                childView.IsShowLine = childView.IsShowLine;//因为子视图不会走MediGridView中GridControl_Load方法，导致视图的网格线显示不正常
            }

            if (selectAll)
            {
                SelectAllRow();
            }
            if (autoSelectGrid)
            {
                AutoSelectView();
            }
        }

        /// <summary>
        /// 自动选中视图
        /// </summary>
        public void AutoSelectView()
        {
            if (View is MediGridView mainView)
            {
                mainView.AutoSelect = true;
            }

            if (SplitChildGrid != null)
            {
                if (SplitChildGrid.MainView is MediGridView childView)
                {
                    childView.AutoSelect = true;
                }
            }
        }

        /// <summary>
        /// 注册数据源新增数据事件 add by songxl on 2019-4-17
        /// </summary>
        public void RegisterDatasourceAddingNewEvent()
        {
            ((BindingSource)this.Grid.DataSource).AddingNew += MediGridSplitContainer_AddingNew;
        }

        private int mediFocusedRowHandle = -1;

        private bool doubleClick = false;
        private bool selectAll = false;
        private bool autoSelectGrid = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]    // add by songxl 阻止这段代码在设计器文件中序列化
        public int MediFocusedRowHandle
        {
            get
            {
                if (SkinCat.Instance.IsDesignMode) return mediFocusedRowHandle;
                mediFocusedRowHandle = ReturnFocusedRowHandle();
                return mediFocusedRowHandle;
            }
            set
            {
                mediFocusedRowHandle = value;
                if (SkinCat.Instance.IsDesignMode) return;
                if ((value + 1) % 2 != 0)
                {
                    MediGridView mainView = this.View as MediGridView;
                    var childRowCount = 0;
                    if (SplitChildGrid?.MainView is MediGridView childView)
                    {
                        childView.ClearFocuseRow();
                        childRowCount = childView.DataRowCount;
                    }

                    if (mainView != null && mainView.DataRowCount + childRowCount < value + 1)
                    {
                        return;
                    }

                    if (mainView == null) return;
                    mainView.EnabledFocusedRow();
                    mainView.Focus();
                    mainView.SelectRow(((value + 2) / 2) - 1);
                    mainView.FocusedRowHandle = ((value + 2) / 2 - 1);
                }
                else
                {
                    MediGridView mainView = this.View as MediGridView;
                    MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
                    if (childView != null && (mainView != null && mainView.DataRowCount + childView.DataRowCount < value + 1))
                    {
                        return;
                    }

                    if (childView == null) return;

                    childView.EnabledFocusedRow();
                    mainView?.ClearFocuseRow();
                    childView.Focus();
                    childView.SelectRow((value + 1) / 2 - 1);
                    childView.FocusedRowHandle = ((value + 1) / 2) - 1;
                }
            }
        }

        public void DisableContainer(bool disable)
        {
            if (this.View is MediGridView mainView) mainView.EditableState = disable;

            if (SplitChildGrid?.MainView is MediGridView childView) childView.EditableState = disable;
        }
        public int ReturnFocusedRowHandle()
        {
            try
            {
                if (this.SplitChildGrid?.MainView is MediGridView childView && childView.FocusRectStyle == DrawFocusRectStyle.RowFocus)
                {
                    return (childView.FocusedRowHandle * 2) + 1;
                }

                if (this.View is MediGridView mainView && mainView.FocusRectStyle == DrawFocusRectStyle.RowFocus)
                {
                    return (mainView.FocusedRowHandle * 2);
                }
                return mediFocusedRowHandle;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private Timer timer;//定时器，用于检查鼠标是否在子表区域内
        private GridColumn colActionMarkColumn;//按钮列
        RepositoryItemMediButtonEdit rpiaddpicmark;
        RepositoryItemMediButtonEdit emptyItemButtonEdit;

        private void MediGridSplitContainer_SplitViewCreated(object sender, EventArgs e)
        {
            GridColumn childgridColumn = new GridColumn();

            childgridColumn.Caption = "even";
            childgridColumn.Name = "even";
            childgridColumn.FieldName = "even";
            childgridColumn.Visible = false;
            childgridColumn.VisibleIndex = -1;
            childgridColumn.UnboundType = UnboundColumnType.Integer;


            GridColumn maingridColumn = new GridColumn();

            maingridColumn.Caption = "odd";
            maingridColumn.Name = "odd";
            maingridColumn.FieldName = "odd";
            maingridColumn.Visible = false;
            maingridColumn.VisibleIndex = -1;
            maingridColumn.UnboundType = UnboundColumnType.Integer;

            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            childView.IsShowLineNumber = false;
            childView.IsShowIndexNumber = false;
            childView.CustomUnboundColumnData += ChildView_CustomUnboundColumnData;
            childView.KeyDown += ChildView_KeyDown;
            childView.RowCountChanged += ChildView_RowCountChanged;
            childView.RowClick += ChildView_RowClick;
            childView.RowDeleted += ChildView_RowDeleted;
            childView.RowCellClick += ChildView_RowCellClick;
            childView.FocusedColumnChanged += ChildView_FocusedColumnChanged;
            childView.MouseDown += ChildView_MouseDown;
            childView.DoubleClick += ChildView_DoubleClick;
            childView.GotFocus += ChildView_GotFocus;
            childView.CustomDrawCell += ChildView_CustomDrawCell;
            childView.Disposed += ChildView_Disposed;
            childView.CustomRowCellEdit += ChildView_CustomRowCellEdit;
            childView.ShowingEditor += ChildView_ShowingEditor;
            if (!childView.Columns.Contains(childgridColumn))
                childView.Columns.Add(childgridColumn);
            childView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            childView.ActiveFilterString = "[even] = 1";

            MediGridView mainView = this.View as MediGridView;
            mainView.IsShowLineNumber = false;

            mainView.KeyDown += MainView_KeyDown;
            mainView.RowCountChanged += MainView_RowCountChanged;
            mainView.RowClick += MainView_RowClick;
            mainView.RowDeleted += MainView_RowDeleted;
            mainView.RowCellClick += MainView_RowCellClick;
            mainView.FocusedColumnChanged += MainView_FocusedColumnChanged;
            mainView.MouseDown += MainView_MouseDown;
            mainView.DoubleClick += MainView_DoubleClick;
            mainView.GotFocus += MainView_GotFocus;
            mainView.CustomDrawCell += MainView_CustomDrawCell;
            mainView.ShowingEditor += MainView_ShowingEditor;
            mainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;
            if (!mainView.Columns.Contains(maingridColumn))
                mainView.Columns.Add(maingridColumn);
            mainView.ActiveFilterString = "[odd] = 2";
            mainView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            if (mainView.IsShowAddMark)
                childView.IsShowAddMark = true;
            if (mainView.IsShowMinusMark)
                childView.IsShowMinusMark = true;
            if (mainView.IsShowGengDuoMark)
                childView.IsShowGengDuoMark = true;
            if (mainView.IsShowQuXiaoTHMark)
                childView.IsShowQuXiaoTHMark = true;
            if (mainView.IsShowTiHuanMark)
                childView.IsShowTiHuanMark = true;
            if (mainView.IsShowUPMark)
                childView.IsShowUPMark = true;
            if (mainView.IsShowDOWNMark)
                childView.IsShowDOWNMark = true;
            if (mainView.IsShowBianJiMark)
                childView.IsShowBianJiMark = true;
            if (mainView.IsShowZZDMark)
                childView.IsShowZZDMark = true;
            if (mainView.IsShowZanTingMark)
                childView.IsShowZanTingMark = true;
            if (mainView.IsShowBaoBiaoMark)
                childView.IsShowBaoBiaoMark = true;
            if (mainView.IsShowIndexNumber)
                childView.IsShowIndexNumber = true;

            SetSerialNo(mainView);
            SetSerialNo(childView);

            #region 双表场景，子表按钮列需单独写相关逻辑，否则子表按钮无法按需显示

            //获取子表按钮列
            colActionMarkColumn = childView.Columns.FirstOrDefault(c => c.FieldName == "ActionMark");
            if (colActionMarkColumn != null)
            {
                rpiaddpicmark = colActionMarkColumn.ColumnEdit as RepositoryItemMediButtonEdit;
                if (rpiaddpicmark != null)
                {
                    emptyItemButtonEdit = new RepositoryItemMediButtonEdit();
                    emptyItemButtonEdit.Buttons.Clear();
                    emptyItemButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;

                    //rpiaddpicmark.ButtonClick += (s, ea) =>
                    //{
                    //    childView.CloseEditor();
                    //};

                    timer = new Timer();
                    timer.Interval = 1;
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }
            }

            #endregion

        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (autoSelectGrid)
            {
                MediGridView mainView = this.View as MediGridView;
                if (mainView.InButtonAction) return;
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;

                var oEdit = (TextEdit)this.SplitChildGrid.MainView.ActiveEditor;
                if (null != oEdit)
                {
                    oEdit.SelectionStart = 0;
                    oEdit.SelectionLength = oEdit.Text.Length;
                }
            }
        }

        private void ChildView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (autoSelectGrid)
            {

                MediGridView mainView = this.View as MediGridView;


                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
                var oEdit = (TextEdit)this.SplitChildGrid.MainView.ActiveEditor;
                if (null != oEdit)
                {
                    oEdit.SelectionStart = 0;
                    oEdit.SelectionLength = oEdit.Text.Length;
                }
            }
        }

        #region 子表按钮显示(add by 余佳平)

        private void ChildView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (colActionMarkColumn != null)
            {
                #region 鼠标移至按钮单元格显示按钮

                //if (e.Column.Name == colActionMarkColumn.Name && e.RepositoryItem is RepositoryItemButtonEdit repItem)
                //{
                //    buttonVisible = e.RowHandle == mouseHoverRowHandle && mouseHoverColumnIndex == e.Column.VisibleIndex;
                //    foreach (EditorButton btn in repItem.Buttons)
                //    {
                //        btn.Visible = buttonVisible;
                //    }
                //}

                #endregion

                #region 鼠标悬浮显示对应行按钮

                if (e.Column.Name == colActionMarkColumn.Name)
                {
                    if (e.RowHandle == mouseHoverRowHandle)
                        e.RepositoryItem = rpiaddpicmark;
                    else
                        e.RepositoryItem = emptyItemButtonEdit;
                }

                #endregion
            }
        }

        private void ChildView_Disposed(object sender, EventArgs e)
        {
            //释放定时器
            if (timer != null)
            {
                timer.Dispose();
            }
        }

        int mouseHoverRowHandle = -1;//鼠标悬浮所在行索引
        int mouseHoverColumnIndex = -1;//鼠标悬浮所在列索引
        bool buttonVisible;//按钮是否可见

        /// <summary>
        /// 根据鼠标位置决定是否显示子表按钮
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            #region 鼠标移至按钮单元格显示按钮

            //MediGridView mainView = this.View as MediGridView;
            //MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            //Point pt = this.SplitChildGrid.PointToClient(MousePosition);
            //GridHitInfo hi = childView.CalcHitInfo(pt);
            //if (!mainView.InButtonAction && (childView.ViewRect.Contains(pt) || buttonVisible))
            //{
            //    if (mouseHoverRowHandle != hi.RowHandle)
            //    {
            //        //当按钮被点击之后，鼠标移出该行需手动关闭编辑器，否则会出现显示两行按钮的情况
            //        if (childView.FocusedColumn?.Name == colActionMarkColumn.Name && childView.IsEditorFocused && buttonVisible)
            //        {
            //            childView.CloseEditor();//此处调用该方法没有限制(可取消判断)，不会出现下面无法顺利触发按钮实时点击事件的情况
            //            buttonVisible = false;
            //        }

            //        //手动调用RefreshRowCell方法，从而触发CustomRowCellEdit事件，进行按钮的显示隐藏设置
            //        childView.RefreshRowCell(mouseHoverRowHandle, colActionMarkColumn);
            //        childView.RefreshRowCell(hi.RowHandle, colActionMarkColumn);
            //    }
            //    else
            //    {
            //        //当按钮被点击之后，鼠标移出按钮列需手动关闭编辑器，否则按钮会一直显示，无法达到鼠标移出按钮列单元格隐藏按钮的效果
            //        if (childView.FocusedColumn?.Name == colActionMarkColumn.Name && childView.IsEditorFocused && buttonVisible &&
            //            (mouseHoverColumnIndex != hi.Column?.VisibleIndex || !hi.InRowCell))
            //        {
            //            childView.CloseEditor();//此处，该方法必须按需调用，否则无法顺利触发按钮实时点击事件
            //            buttonVisible = false;
            //        }

            //        childView.RefreshRowCell(hi.RowHandle, colActionMarkColumn);
            //    }

            //    if (childView.FocusedColumn?.Name == colActionMarkColumn.Name && childView.IsEditorFocused)
            //        childView.ShowEditor();

            //}
            //mouseHoverRowHandle = hi.RowHandle;
            //mouseHoverColumnIndex = hi.Column == null ? -1 : hi.Column.VisibleIndex;

            #endregion

            #region 鼠标悬浮显示对应行按钮

            MediGridView mainView = this.View as MediGridView;
            if (mainView.InButtonAction) return;
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            Point pt = this.SplitChildGrid.PointToClient(MousePosition);
            GridHitInfo hi = childView.CalcHitInfo(pt);
            if (mouseHoverRowHandle != hi.RowHandle)
            {
                //当按钮被点击之后，鼠标移出该行需手动关闭编辑器，否则会出现显示两行按钮的情况
                if (childView.FocusedColumn?.Name == colActionMarkColumn.Name)
                {
                    childView.CloseEditor();
                }

                //手动调用RefreshRowCell方法，从而触发CustomRowCellEdit事件，进行按钮的显示隐藏设置
                childView.RefreshRowCell(mouseHoverRowHandle, colActionMarkColumn);
                childView.RefreshRowCell(hi.RowHandle, colActionMarkColumn);
            }
            mouseHoverRowHandle = hi.RowHandle;
            mouseHoverColumnIndex = hi.Column == null ? -1 : hi.Column.VisibleIndex;

            #endregion

        }

        #endregion

        /// <summary>
        /// 设置默认值后调用addingNew方法把默认值赋给数据源 add by songxl on 2019-4-17
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridSplitContainer_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = _DTOBase;
        }

        private void MainView_DoubleClick(object sender, EventArgs e)
        {
            doubleClick = true;
        }

        private void ChildView_DoubleClick(object sender, EventArgs e)
        {
            doubleClick = true;
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            MediGridView mainView = this.View as MediGridView;
            mainView.Focus();
            GridHitInfo info = mainView.CalcHitInfo(e.Location);
            if (info != null && info.InColumnPanel && info.Column != null && info.Column.Name == "DX$CheckboxSelectorColumn")
            {

                bool checkBoxSelectorMode = true;//标题头中的checkBox是否勾选标志
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;

                if (mainView.GetSelectedRows().Length != mainView.DataRowCount)
                {
                    checkBoxSelectorMode = false;
                }
                SelectAllRow(e, childView, checkBoxSelectorMode);
            }

        }

        private void ChildView_MouseDown(object sender, MouseEventArgs e)
        {
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;

            GridHitInfo info = childView.CalcHitInfo(e.Location);
            if (info != null && info.InColumnPanel && info.Column != null && info.Column.Name == "DX$CheckboxSelectorColumn")
            {
                bool checkBoxSelectorMode = true;//标题头中的checkBox是否勾选标志

                MediGridView mainView = this.View as MediGridView;
                if (childView.GetSelectedRows().Length != childView.DataRowCount)
                {
                    checkBoxSelectorMode = false;
                }
                SelectAllRow(e, mainView, checkBoxSelectorMode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="biaoZhi"></param>
        public void SetSelectAllBZ(bool biaoZhi = false)
        {
            selectAll = biaoZhi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="biaozhi"></param>
        public void SetAutoSelect(bool biaozhi = false)
        {
            autoSelectGrid = biaozhi;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SelectAllRow()
        {
            MediGridView mainView = this.View as MediGridView;
            mainView?.SelectAll();

            if (this.SplitChildGrid != null)
            {
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
                childView?.SelectAll();
            }
        }

        private void SelectAllRow(MouseEventArgs e, MediGridView view, bool check)
        {
            if (doubleClick)
            {
                doubleClick = false;
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            if (check)
            {
                view.ClearSelection();
            }
            else
            {
                view.SelectAll();

            }
        }

        /// <summary>
        /// 焦点列改变事件
        /// add by songxl on 2019-4-15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            MediGridView mainView = this.View as MediGridView;

            FocusedColumnChangedFunction(mainView);
        }

        /// <summary>
        /// 焦点列改变事件
        /// add by songxl on 2019-4-15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildView_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;

            FocusedColumnChangedFunction(childView);
        }

        /// <summary>
        /// 焦点列改变方法
        /// add by songxl on 2019-4-15
        /// </summary>
        private void FocusedColumnChangedFunction(MediGridView view)
        {
            if (view.FocusedColumn?.RealColumnEdit is RepositoryItemGridLookUpEdit)
            {
                this.ShowEditor();
                if (view.ActiveEditor != null)
                {
                    MediGridLookUpEdit edit = view.ActiveEditor as MediGridLookUpEdit;
                    if (edit.Properties.TextEditStyle == TextEditStyles.DisableTextEditor)
                    {
                        edit.ShowPopup();
                    }
                }
            }
        }

        /// <summary>
        /// 单元格点击事件
        /// add by songxl on 2019-4-15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            MediGridView mainView = this.View as MediGridView;

            RowCellClickFunction(e, mainView);
        }

        /// <summary>
        /// 单元格点击事件
        /// add by songxl on 2019-4-15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;

            RowCellClickFunction(e, childView);
        }

        /// <summary>
        /// 单元格点击方法
        /// add by songxl on 2019-4-15
        /// modify by songxl on 2019-05-06
        /// </summary>
        /// <param name="e"></param>
        /// <param name="view"></param>
        private void RowCellClickFunction(RowCellClickEventArgs e, MediGridView view)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                view.FocusedColumn = hitInfo.Column;
                view.FocusedRowHandle = hitInfo.RowHandle;
                this.ShowEditor();
                if (hitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit)
                {
                    if (view.ActiveEditor is CheckEdit edit)
                    {
                        edit.Toggle();
                    }
                }
                else if (hitInfo.Column.RealColumnEdit is RepositoryItemGridLookUpEdit)
                {
                    if (view.ActiveEditor is GridLookUpEdit edit)
                    {
                        if (edit.Properties.TextEditStyle != TextEditStyles.DisableTextEditor) return;
                        if (!edit.IsPopupOpen)
                        {
                            edit.ShowPopup();
                        }
                        else
                        {
                            edit.ClosePopup();
                        }
                    }
                }
                else if (hitInfo.Column.RealColumnEdit is RepositoryItemComboBox)
                {
                    if (view.ActiveEditor is ComboBoxEdit edit)
                    {
                        if (edit.Properties.TextEditStyle != TextEditStyles.DisableTextEditor) return;
                        if (!edit.IsPopupOpen)
                        {
                            edit.ShowPopup();
                        }
                        else
                        {
                            edit.ClosePopup();
                        }
                    }
                }
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }

            //add by zhukunpin  左右控件点击行自动勾选
            if (view.OptionsSelection.MultiSelect && view.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect)
            {
                //if (e.Column.FieldName != "DX$CheckboxSelectorColumn")
                //{
                //    var obj = view.GetRowCellValue(view.FocusedRowHandle, "DX$CheckboxSelectorColumn");
                //    if (bool.Parse(obj.ToStringEx()) == false)
                //        view.SelectRow(view.FocusedRowHandle);//光标选中              
                //    else
                //        view.InvertRowSelection(view.FocusedRowHandle);
                //}
            }
        }

        /// <summary>
        /// add by songxl on 2019-05-06
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_GotFocus(object sender, EventArgs e)
        {
            MediGridView mainView = this.View as MediGridView;
            FocuseRow?.Invoke(mainView);//通知其他控件清除焦点 add by zhukp 2019-06-26
            mainView.SetFocusedRow(mainView.FocusedRowHandle);
            if (this.SplitChildGrid != null)
            {
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
                childView.ClearFocuseRow();
            }

        }

        /// <summary>
        /// add by songxl on 2019-05-06
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildView_GotFocus(object sender, EventArgs e)
        {
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            FocuseRow?.Invoke(childView);//通知其他控件清除焦点 add by zhukp 2019-06-26
            childView.SetFocusedRow(childView.FocusedRowHandle);
            MediGridView mainView = this.View as MediGridView;
            mainView.ClearFocuseRow();

        }

        private void ChildView_RowDeleted(object sender, RowDeletedEventArgs e)
        {
            MediGridView childView = sender as MediGridView;
            MediGridView mainView = this.View as MediGridView;

            childView.ClearFocuseRow();
            childView.RefreshData();
            mainView.RefreshData();

        }

        private void MainView_RowDeleted(object sender, RowDeletedEventArgs e)
        {
            MediGridView mainView = sender as MediGridView;
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            mainView.ClearFocuseRow();
            childView.RefreshData();
            mainView.RefreshData();
        }


        /// <summary>
        /// 
        /// </summary>
        public int childcount = 0;
        private void ChildView_RowCountChanged(object sender, EventArgs e)
        {

            MediGridView mainView = this.View as MediGridView;
            MediGridView childView = sender as MediGridView;
            if (childcount < childView.DataRowCount)
            {
                childcount = childView.DataRowCount;
                childView.EnabledFocusedRow();
                mainView.ClearFocuseRow();
                mainView.ClearSelection();
                childView.RefreshData();
                FocusedMediGridView = childView;
                childView.Focus();
                //childView.ShowEditor();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public int maincount = 0;

        private void MainView_RowCountChanged(object sender, EventArgs e)
        {
            MediGridView mainView = sender as MediGridView;
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            if (maincount < mainView.DataRowCount)
            {
                maincount = mainView.DataRowCount;
                mainView.EnabledFocusedRow();
                childView.ClearFocuseRow();
                mainView.RefreshData();
                FocusedMediGridView = mainView;
                //mainView.ShowEditor();
            }
        }

        /// <summary>
        /// 获取主视图的数据行计数
        /// add by songxl on 2019-04-23
        /// </summary>
        /// <returns></returns>
        public int GetMainViewRowCount()
        {
            return this.View.DataRowCount;
        }

        /// <summary>
        /// 返回当前行计数
        /// add by songxl
        /// </summary>
        public int RowCount
        {
            get
            {
                if (this.SplitChildGrid != null)
                {
                    return this.View.RowCount + this.SplitChildGrid.MainView.RowCount;
                }

                return this.View.RowCount;
            }
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            MediGridView mainView = sender as MediGridView;
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            //mainView.EnabledFocusedRow();
            //childView.ClearFocuseRow();
        }

        private void ChildView_RowClick(object sender, RowClickEventArgs e)
        {
            MediGridView mainView = this.View as MediGridView;
            MediGridView childView = sender as MediGridView;
            //childView.EnabledFocusedRow();
            //mainView.ClearFocuseRow();
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        private void SetSerialNo(MediGridView mediGridView)
        {
            if (!mediGridView.IsShowIndexNumber) return;
            GridColumnCollection gridColumnCollection = mediGridView.Columns;

            bool exist = false;
            for (int i = 0; i < gridColumnCollection.Count; i++)
            {
                GridColumn gridColumn = gridColumnCollection[i];

                if (gridColumn.FieldName.IsNullOrWhiteSpace() && gridColumn.VisibleIndex == -1)
                {
                    exist = true;
                    break;
                }

                if (gridColumn.FieldName.ToUpper() == "SERIALNO")
                {
                    exist = true;

                    gridColumn.Visible = true;
                    break;
                }
            }

            if (exist) return;

            AddSerialNO(mediGridView);
        }

        /// <summary>
        /// 添加序列号 初始化一次
        /// </summary>
        private void AddSerialNO(MediGridView mediGridView)
        {
            GridColumn colSerialNum = new GridColumn();
            colSerialNum.FieldName = "SerialNo";//列绑定字段
            colSerialNum.Caption = "序号";//列名称
            colSerialNum.Name = "colSerialNo";
            colSerialNum.Visible = true;
            colSerialNum.VisibleIndex = 0;
            colSerialNum.UnboundType = UnboundColumnType.Integer;
            colSerialNum.AppearanceHeader.Options.UseTextOptions = true;
            colSerialNum.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colSerialNum.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            colSerialNum.AppearanceCell.Options.UseTextOptions = true;
            colSerialNum.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colSerialNum.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //列宽
            colSerialNum.Width = 40;
            //是否可以拖动列标题
            colSerialNum.OptionsColumn.AllowMove = false;
            //固定列头
            colSerialNum.Fixed = FixedStyle.None;
            //排序
            colSerialNum.OptionsColumn.AllowSort = DefaultBoolean.False;
            colSerialNum.OptionsColumn.AllowEdit = false;
            colSerialNum.OptionsColumn.AllowFocus = false;

            colSerialNum.OptionsFilter.AllowFilter = false;
            colSerialNum.OptionsColumn.AllowMerge = DefaultBoolean.False;
            mediGridView.Columns.Insert(0, colSerialNum);
        }

        /// <summary>
        /// 设置keyDown事件时是否允许跳到下一列 add by songxl on 2019-4-11
        /// </summary>
        public bool IsAllowedForKeyDownToNextColumn = true;
        /// <summary>
        /// 设置可以往下一行跳的列 add by songxl on 2019-4-11
        /// </summary>
        public string KeyDownToNextColumn = null;
        /// <summary>
        /// modify by songxl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            MediGridView mainview = sender as Mediinfo.WinForm.HIS.Controls.MediGridView;
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (sender is MediGridView)
                {
                    if (KeyDownToNextColumn != null && KeyDownToNextColumn.IndexOf(mainview.FocusedColumn.FieldName, StringComparison.OrdinalIgnoreCase) >= 0 && e.Handled == false)
                    {
                        mainview.CloseEditor();
                        childView.FocusedRowHandle = mainview.FocusedRowHandle;
                        if (mainview.FocusedRowHandle + 1 <= childView.DataRowCount)
                        {
                            childView.EnabledFocusedRow();
                            mainview.ClearFocuseRow();
                            childView.FocusedColumn = childView.VisibleColumns[1];
                            childView.ShowEditor();
                            e.Handled = true;
                        }
                    }
                    else if (IsAllowedForKeyDownToNextColumn)
                    {
                        mainview.FocusedColumn = mainview.VisibleColumns[mainview.FocusedColumn.VisibleIndex + 1];
                        mainview.ShowEditor();
                        e.Handled = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Left)
            {
                if (((GridView)sender).FocusedColumn.FieldName == "YAOPINMC" && ((GridView)sender).FocusedRowHandle >= 1)
                {
                    mainview.CloseEditor();
                    childView.EnabledFocusedRow();
                    mainview.ClearFocuseRow();
                    childView.FocusedRowHandle = mainview.FocusedRowHandle - 1;
                    childView.FocusedColumn = childView.VisibleColumns[1];
                    childView.ShowEditor();
                    e.Handled = true;
                }
            }

            if (e.KeyCode == Keys.Right)
            {
                if (((GridView)sender).FocusedColumn.FieldName == "GEIYAOFS")
                {
                    if (mainview != null)
                    {
                        mainview.CloseEditor();
                        if (childView != null)
                        {
                            childView.EnabledFocusedRow();
                            mainview.ClearFocuseRow();
                            childView.FocusedRowHandle = mainview.FocusedRowHandle;
                        }
                    }

                    if (childView != null)
                    {
                        childView.FocusedColumn = childView.VisibleColumns[1];
                        childView.ShowEditor();
                    }

                    e.Handled = true;
                }
            }
        }

        private void ChildView_KeyDown(object sender, KeyEventArgs e)
        {
            MediGridView childview = sender as MediGridView;
            MediGridView mainView = this.View as MediGridView;
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (sender is GridView)
                {
                    if (KeyDownToNextColumn != null && KeyDownToNextColumn.IndexOf(childview.FocusedColumn.FieldName, StringComparison.OrdinalIgnoreCase) >= 0 && e.Handled == false)
                    {
                        childview.CloseEditor();
                        mainView.EnabledFocusedRow();
                        childview.ClearFocuseRow();
                        mainView.FocusedRowHandle = childview.FocusedRowHandle + 1;
                        mainView.FocusedColumn = mainView.VisibleColumns[1];
                        mainView.ShowEditor();
                        e.Handled = true;
                    }
                    else if (IsAllowedForKeyDownToNextColumn)
                    {
                        childview.FocusedColumn = childview.VisibleColumns[childview.FocusedColumn.VisibleIndex + 1];
                        childview.ShowEditor();
                        e.Handled = true;
                    }

                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (((GridView)sender).FocusedColumn.FieldName == "YAOPINMC")
                {
                    childview.CloseEditor();
                    mainView.EnabledFocusedRow();
                    childview.ClearFocuseRow();
                    mainView.FocusedRowHandle = childview.FocusedRowHandle;
                    mainView.FocusedColumn = mainView.VisibleColumns[1];
                    mainView.ShowEditor();
                    e.Handled = true;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (((GridView)sender).FocusedColumn.FieldName == "GEIYAOFS")
                {
                    childview.CloseEditor();
                    mainView.EnabledFocusedRow();
                    childview.ClearFocuseRow();
                    mainView.FocusedRowHandle = childview.FocusedRowHandle + 1;
                    mainView.FocusedColumn = mainView.VisibleColumns[1];
                    mainView.ShowEditor();
                    e.Handled = true;
                }
            }
        }

        private void MainView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.Name.Equals("odd"))
            {
                if (e.IsGetData && e.ListSourceRowIndex % 2 == 0)
                    e.Value = 2;
            }
            if (e.Column.Name.Equals("colSerialNo"))
            {
                if (e.IsGetData)
                {
                    e.Value = e.ListSourceRowIndex + 1;
                }
            }
        }

        private void ChildView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.Name.Equals("even") && e.ListSourceRowIndex % 2 != 0)
            {
                if (e.IsGetData)
                    e.Value = 1;
            }
            if (e.Column.Name.Equals("colSerialNo"))
            {
                e.Value = e.ListSourceRowIndex + 1;
            }
        }

        private void Grid_DataSourceChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!SkinCat.Instance.IsDesignMode)
            {
                this.SplitterPosition = this.Width / 2;
                this.Horizontal = true;
            }
        }

        /// <summary>
        /// 根据行句柄删除行数据
        /// </summary>
        /// <param name="rowhandle"></param>
        public void DeleteRow(int rowhandle)
        {
            int mainRowHandle = -1;

            if (!(this.View is MediGridView mainView) || !(this.SplitChildGrid.MainView is MediGridView childView)) return;

            if (rowhandle % 2 == 0)//左边
            {
                mainRowHandle = rowhandle / 2;
                mainView.DeleteRow(mainRowHandle);
                //if (mainRowHandle < mainView.DataRowCount + childView.DataRowCount)
                //{
                //    this.mediFocusedRowHandle = mainRowHandle + 1;
                //}
                //else if (mainRowHandle == mainView.DataRowCount + childView.DataRowCount)
                //{
                //    this.mediFocusedRowHandle = mainRowHandle - 1;
                //}
            }
            else
            {
                var childRowHandle = (rowhandle - 1) / 2;

                childView.DeleteRow(childRowHandle);
                //if (mainRowHandle < mainView.DataRowCount + childView.DataRowCount)
                //{
                //    this.mediFocusedRowHandle = mainRowHandle + 1;
                //}
                //else if (mainRowHandle == mainView.DataRowCount + childView.DataRowCount)
                //{
                //    this.mediFocusedRowHandle = mainRowHandle - 1;
                //}
            }
            //this.MediFocusedRowHandle = rowhandle;//add by zhukunpin 删除后还是要聚焦到行，不然对中间插入会有影响
            //20200722 add by zhengcj for HB6-13961 begin
            if (rowhandle < this.RowCount)
            {
                this.MediFocusedRowHandle = rowhandle;
            }
            else
            {
                this.MediFocusedRowHandle = this.RowCount - 1;
            }
            //20200722 add by zhengcj for HB6-13961 end
        }

        /// <summary>
        /// focus current cell
        /// add by songxl on 2019-4-2
        /// </summary>
        public void ShowEditor()
        {
            GetFocusMediGridView()?.ShowEditor();
        }

        /// <summary>
        /// refresh row when datasource is changed
        /// add by songxl on 2019-4-2
        /// </summary>
        /// <param name="rowHandle"></param>
        public void RefreshRow(int rowHandle)
        {
            GetFocusMediGridView()?.RefreshRow(rowHandle / 2);
        }

        /// <summary>
        /// add by songxl on 2019-4-2
        /// </summary>
        public void RefreshData()
        {
            this.View?.RefreshData();
            this.SplitChildGrid?.MainView?.RefreshData();
        }

        /// <summary>
        /// get row value add by songxl on 2019-4-2
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public object GetRow(int rowHandle)
        {
            return GetFocusMediGridView()?.GetRow(rowHandle / 2);
        }

        /// <summary>
        /// get current value add by songxl on 2019-6-6
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public object GetRowByCurrentRow(int rowHandle)
        {
            return GetFocusMediGridView()?.GetRow(rowHandle);
        }

        /// <summary>
        /// 获取表格中当前正在编辑的值填入当前正在编辑的单元格中
        /// add by songxl on 2019-4-12
        /// </summary>
        public void GetCurrentViewEditingValueToCurrentCell()
        {
            var view = GetFocusMediGridView();
            var editingValue = view?.EditingValue;
            if (editingValue != null && editingValue.ToStringEx() != "System.Object")
            {
                view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, editingValue);
            }
        }

        /// <summary>
        /// get cell value  add by songxl on 2019-4-2
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="column"></param>
        public object GetRowCellValue(int rowHandle, string column)
        {
            return GetFocusMediGridView()?.GetRowCellValue(rowHandle / 2, column);
        }

        /// <summary>
        /// set cell value
        /// add by songxl on 2019-4-2
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetRowCellValue(int rowHandle, string column, object value)
        {
            GetFocusMediGridView()?.SetRowCellValue(rowHandle / 2, column, value);
        }

        /// <summary>
        /// add by songxl on 2019-4-2
        /// </summary>
        /// <returns></returns>
        public string GetFocusFieldName()
        {
            return GetFocusMediGridView()?.FocusedColumn.FieldName.ToStringEx();
        }

        /// <summary>
        /// add by songxl on 2019-4-2
        /// </summary>
        /// <param name="fieldName"></param>
        public void SetFocusColumn(string fieldName)
        {
            GetFocusMediGridView()?.SetFocusedColumn(fieldName);
        }

        /// <summary>
        /// 选中行
        /// add by songxl on 2019-4-2
        /// </summary>
        /// <param name="rowhandle"></param>
        public void SelectRow(int rowhandle)
        {
            GetFocusMediGridView()?.SelectRow(rowhandle / 2);
        }

        public void SelectNullRow(int row, string columns)
        {
            MediGridView mainView = this.View as MediGridView;
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            if (this.SplitChildGrid == null) return;
            switch (row % 2)
            {
                case 0:
                    mainView.SetFocusedRow(row / 2);
                    mainView.FocusedColumn = ((MediGridView)mainView).Columns[columns];
                    childView.ClearFocuseRow();
                    mainView.ShowEditor();
                    break;
                case 1:
                    childView.SetFocusedRow(row / 2);
                    childView.FocusedColumn = ((MediGridView)childView).Columns[columns];
                    mainView.ClearFocuseRow();
                    childView.ShowEditor();
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        public void SelectNewRow(string columns)
        {
            MediGridView mainView = this.View as MediGridView;
            if (this.SplitChildGrid != null)
            {
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;

                if (mainView.DataRowCount > childView.DataRowCount)
                {
                    mainView.SetFocusedRow(mainView.DataRowCount - 1);
                    mainView.FocusedColumn = ((MediGridView)mainView).Columns[columns];
                    childView.ClearFocuseRow();
                    mainView.ShowEditor();
                }
                else
                {
                    childView.SetFocusedRow(childView.DataRowCount - 1);
                    childView.FocusedColumn = ((MediGridView)childView).Columns[columns];
                    mainView.ClearFocuseRow();
                    childView.ShowEditor();
                }
            }
            else
            {
                mainView.FocusedColumn = mainView.VisibleColumns[1];
            }

        }

        /// <summary>
        /// add by songxl on 2019-4-2
        /// </summary>
        public void ValidateEditor()
        {
            GetFocusMediGridView()?.ValidateEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[] GetSelectedRows()
        {
            MediGridView mainView = this.View as MediGridView;
            if (this.SplitChildGrid == null)
            {
                return new int[0];
            }
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            int[] mainRows = mainView.GetSelectedRows();
            int[] childRows = childView.GetSelectedRows();
            for (int i = 0; i < mainRows.Length; i++)
            {
                mainRows[i] = mainRows[i] * 2;
            }
            for (int i = 0; i < childRows.Length; i++)
            {
                childRows[i] = childRows[i] * 2 + 1;
            }

            int[] rows = new int[mainRows.Length + childRows.Length];
            mainRows.CopyTo(rows, 0);
            childRows.CopyTo(rows, mainRows.Length);
            return rows;
        }

        /// <summary>
        /// 存储布局信息详情 add by songxl on 2019-4-17
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;

        /// <summary>
        /// 数据集 add by songxl on 2019-4-17
        /// </summary>
        private DTOBase _DTOBase;

        /// <summary>
        /// 设置默认值 add by songxl on 2019-4-17
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void SetFieldDefaultValue<T>(T t) where T : DTOBase
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0)
            {
                _DTOBase = t; return;
            }

            Type type = typeof(T);

            foreach (PropertyInfo p in type.GetProperties())
            {
                foreach (E_GY_DATALAYOUT2 e in _EDataLayout2)
                {
                    if (e.DEFAULTVALUE.IsNullOrEmpty()) continue;

                    if (e.FIELDNAME.ToUpper() == p.Name.ToUpper())
                    {
                        Type typeValue = p.PropertyType;
                        p.SetValue(t, ConvertTo(e.DEFAULTVALUE, typeValue), null);
                        break;
                    }
                }
            }
            _DTOBase = t;
        }

        /// <summary>  
        /// 转化类型
        /// add by songxl on 2019-4-17
        /// </summary> 
        /// <param name="convertibleValue"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private object ConvertTo(IConvertible convertibleValue, Type t)
        {
            if (string.IsNullOrEmpty(convertibleValue.ToString()))
            {
                return null;
            }
            if (!t.IsGenericType)
            {
                return Convert.ChangeType(convertibleValue, t);
            }
            else
            {
                Type genericTypeDefinition = t.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(t));
                }
            }
            return null;
        }

        private void MainView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            MediGridView mainView = this.View as MediGridView;
            // todo 分库：保密处方处理以后放到业务层
            //if (mainView?.GetRow(e.RowHandle) is E_MZ_CHUFANG2 mz && !mz.DAYINMC.IsNullOrWhiteSpace())
            //{
            //    e.Appearance.BackColor = Color.FromArgb(248, 188, 244);
            //}
            //add by zhukp 选择模式下，勾选后不用蓝底HB6-2132(475948)
            if (mainView.OptionsSelection.MultiSelect == true
                && mainView.OptionsSelection.MultiSelectMode == DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect)//多选模式下，不需要蓝底
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(245, 245, 245);
                }
            }

        }

        private void ChildView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
            if (this.SplitChildGrid != null)
            {
                // todo 分库：保密处方处理以后放到业务层
                //if (childView?.GetRow(e.RowHandle) is E_MZ_CHUFANG2 mz && !mz.DAYINMC.IsNullOrWhiteSpace())
                //{
                //    e.Appearance.BackColor = Color.FromArgb(248, 188, 244);
                //}
            }
            //add by zhukp 选择模式下，勾选后不用蓝底HB6-2132(475948)
            if (childView.OptionsSelection.MultiSelect == true &&
                 childView.OptionsSelection.MultiSelectMode == DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect)//多选模式下，不需要蓝底
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(245, 245, 245);
                }
            }

        }
        /// <summary>
        /// 清除所有焦点，add by zhukp  
        /// </summary>
        public void ClearFocuseRow()
        {
            MediGridView mainView = this.View as MediGridView;
            mainView.ClearFocuseRow();
            if (this.SplitChildGrid != null)
            {
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
                childView.ClearFocuseRow();
            }
        }

        /// <summary>
        /// 是否处于全选状态
        /// </summary>
        public bool IsSelectAll { get; set; }
        /// <summary>
        /// 是否显示复选框
        /// </summary>
        private bool _IsShowCheckBox = false;
        /// <summary>
        /// 复选框是否可以勾选
        /// </summary>
        private bool _IsEnableCheckBox = true;

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        [DefaultValue(false), Description("是否显示复选框")]
        public bool IsShowCheckBoX
        {
            get { return _IsShowCheckBox; }
            set
            {
                _IsShowCheckBox = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetCheckBox();
            }
        }

        /// <summary>
        /// 复选框是否可以勾选
        /// </summary>
        [DefaultValue(false), Description("是否显示复选框")]
        public bool IsEnableCheckBox
        {
            get => _IsEnableCheckBox;
            set => _IsEnableCheckBox = value;
        }



        /// <summary>
        /// 设置复选框
        /// </summary>
        private void SetCheckBox()
        {
            MediGridView mainView = this.View as MediGridView;
            AddCheckColumn(mainView);

            if (this.SplitChildGrid != null)
            {
                MediGridView childView = this.SplitChildGrid.MainView as MediGridView;
                AddCheckColumn(childView);
            }

        }

        private void AddCheckColumn(ColumnView view)
        {
            GridColumnCollection gridColumnCollection = view.Columns;
            bool exist = false;
            for (int i = 0; i < gridColumnCollection.Count; i++)
            {
                GridColumn gridColumn = gridColumnCollection[i];

                if (gridColumn.FieldName.ToUpper() == "IS_SELECT")
                {
                    exist = true;
                    gridColumn.Visible = _IsShowCheckBox;
                    break;
                }
            }

            if (exist) return;
            AddCheckBox(view);
        }

        /// <summary>
        /// 添加复选框
        /// </summary>
        private void AddCheckBox(ColumnView view)
        {
            if (!_IsShowCheckBox) return;

            GridColumn colCheckBox = new GridColumn
            {
                FieldName = "IS_SELECT",//列绑定字段
                Caption = @" ",//列名称
                Name = "colIS_SELECT",
                Visible = true,
                VisibleIndex = 0
            };
            colCheckBox.AppearanceHeader.Options.UseTextOptions = true;
            colCheckBox.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colCheckBox.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            colCheckBox.AppearanceCell.Options.UseTextOptions = true;
            colCheckBox.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colCheckBox.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //列宽
            colCheckBox.Width = 30;
            //是否可以拖动列标题
            colCheckBox.OptionsColumn.AllowMove = false;
            //固定列头
            colCheckBox.Fixed = FixedStyle.None;
            //排序
            colCheckBox.OptionsColumn.AllowSort = DefaultBoolean.False;
            colCheckBox.OptionsColumn.AllowEdit = true;
            colCheckBox.OptionsColumn.AllowFocus = true;
            colCheckBox.UnboundType = UnboundColumnType.Boolean;
            colCheckBox.OptionsFilter.AllowFilter = false;
            colCheckBox.OptionsColumn.AllowMerge = DefaultBoolean.False;
            view.Columns.Insert(0, colCheckBox);

            RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit
            {
                AutoHeight = false,
                CheckStyle = CheckStyles.Standard,
                ReadOnly = false,
                Name = "isselectcheckbox",
                Enabled = _IsEnableCheckBox
            };
            colCheckBox.ColumnEdit = repositoryItemCheckEdit;
            //colCheckBox.BestFit();
        }
    }
}
