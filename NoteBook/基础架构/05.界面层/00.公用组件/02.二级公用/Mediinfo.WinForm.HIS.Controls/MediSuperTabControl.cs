using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
using DevExpress.XtraTab.Buttons;
using DevExpress.XtraTab.Drawing;
using DevExpress.XtraTab.ViewInfo;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.Common;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Controls.FirstLevelFramework;
using Mediinfo.WinForm.HIS.Controls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    [ToolboxItem(false)]
    public class MediSuperTabControl : MediTabControl
    {
        public DevExpress.Utils.VisualEffects.Badge xiaoxibadge;
        public MediFlyoutPanel mediDoctorFlyoutPanel;
        public Form ParentForm { get; set; }
        public int LeftDistance { get; set; }
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public MediSuperTabControlSkinViewInfoRegistrator mediSkinViewInfoRegistrator;
        public MediSuperTabControl() : base()
        {
            this.ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;

            this.AppearancePage.Header.Font = new Font("微软雅黑", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            this.AppearancePage.Header.ForeColor = Color.FromArgb(255, 255, 255);
            this.AppearancePage.HeaderActive.ForeColor = Color.FromArgb(0, 115, 195);
            this.AppearancePage.Header.TextOptions.HAlignment = HorzAlignment.Center;
        }
        public override void EndInit()
        {
            if (!SkinCat.Instance.IsDesignMode)
                CreateTabHeaderControls();
            base.EndInit();
        }

        public BaseEdit ActiveEditor { get; set; }
        public object EditValue { get; set; }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CloseEditor();
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);


            if (ParentForm != null && e.Button == MouseButtons.Left)
            {
                MediSuperTabControlSkinTabHeaderViewInfo mediSkinTabHeaderViewInfo = mediSkinViewInfoRegistrator.TabHeaderViewInfo;

                if (mediSkinTabHeaderViewInfo != null && mediSkinTabHeaderViewInfo.TabPageButtonBoundDic.ContainsKey(this.SelectedTabPage.Name))
                {
                    if (!mediSkinTabHeaderViewInfo.TabPageButtonBoundDic[this.SelectedTabPage.Name].Contains(e.Location))
                    {
                        if (e.Clicks == 2 && e.Button == MouseButtons.Left)
                        {
                            if (ParentForm.WindowState == FormWindowState.Normal)
                            {
                                Screen currentScreen = Screen.FromControl(this);

                                ParentForm.WindowState = FormWindowState.Maximized;

                                if (Screen.AllScreens.Length > 1)
                                {
                                    ParentForm.MinimumSize = currentScreen.WorkingArea.Size;
                                    ParentForm.DesktopBounds = currentScreen.WorkingArea;
                                }
                                this.Invalidate(false);
                            }
                            else
                            {
                                ParentForm.WindowState = FormWindowState.Normal;
                                this.Invalidate(false);
                            }
                        }
                        if (ParentForm.WindowState == FormWindowState.Normal)
                        {
                            this.Capture = false;
                            SendMessage(ParentForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                        }
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (ParentForm != null && e.Button == MouseButtons.Left)
            {
                MediSuperTabControlSkinTabHeaderViewInfo mediSkinTabHeaderViewInfo = mediSkinViewInfoRegistrator.TabHeaderViewInfo;
                if (mediSkinTabHeaderViewInfo != null 
                    && mediSkinTabHeaderViewInfo.TabPageButtonBoundDic.ContainsKey(this.SelectedTabPage.Name) 
                    && !mediSkinTabHeaderViewInfo.TabPageButtonBoundDic[this.SelectedTabPage.Name].Contains(e.Location) 
                    && ParentForm.WindowState == FormWindowState.Normal 
                    && e.Clicks != 2)
                {
                    this.Capture = false;
                    SendMessage(ParentForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            CheckTabHeaderEditorsClick(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);
                EditorControlInfo editorControlInfo;
                foreach (var repositoryItem in afterrepositoryItemsdic)
                {
                    if (!repositoryItem.Value.Bound.Contains(e.Location))
                    {
                        editorControlInfo = repositoryItem.Value;
                        ParentForm = this.FindForm();
                        editorControlInfo.RaiseMouseLeave();

                        this.Invalidate(false);
                    }
                    if (repositoryItem.Value.Bound.Contains(e.Location))
                    {
                        editorControlInfo = repositoryItem.Value;
                        ParentForm = this.FindForm();
                        editorControlInfo.RaiseMouseHover();
                        this.Invalidate(false);
                    }
                }
            }
            catch (Exception exception)
            {
                  
            }
          
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
        }

        private void CheckTabHeaderEditorsClick(MouseEventArgs e)
        {
            if (FindEditorInfo(e, out var editorControlInfo))
            {
                ParentForm = this.FindForm();
                editorControlInfo.RaiseMouseDown(new EventArgs());
                this.Invalidate(false);
            }
        }

        public bool FindEditorInfo(MouseEventArgs e, out EditorControlInfo editorControlInfo)
        {
            foreach (var repositoryItem in afterrepositoryItemsdic)
            {
                if (repositoryItem.Value.Bound.Contains(e.Location))
                {
                    editorControlInfo = repositoryItem.Value;
                    return true;
                }
            }
            editorControlInfo = null;
            return false;
        }

        /// <summary>
        /// 创建tabheader控件
        /// </summary>
        public void CreateTabHeaderControls()
        {
            this.BeginUpdate();
            RepositoryItemPictureEdit riCloseEdit = new RepositoryItemPictureEdit {BorderStyle = BorderStyles.NoBorder};
            EditorControlInfo closeEditorControlInfo = new EditorControlInfo
            {
                RepositoryItem = riCloseEdit, Size = new Size(26, 26), Location = new Point(35, 25)
            };

            riCloseEdit.Appearance.BackColor = Color.Transparent;
            riCloseEdit.Name = "CLOSEBOX";
            closeEditorControlInfo.EditValue = Resources.header_guanbi;
            closeEditorControlInfo.HAlignment = HAlignment.Right;
            closeEditorControlInfo.MouseDown += CloseEditorControlInfo_MouseDown;
            closeEditorControlInfo.MouseHover += CloseEditorControlInfo_MouseHover;
            closeEditorControlInfo.MouseLeave += CloseEditorControlInfo_MouseLeave;
            beforerepositoryItemsdic.Add(riCloseEdit, closeEditorControlInfo);
            RepositoryItemPictureEdit riMaxEdit = new RepositoryItemPictureEdit();
            EditorControlInfo maxEditorControlInfo = new EditorControlInfo();
            maxEditorControlInfo.Name = "MAXBOX";
            maxEditorControlInfo.RepositoryItem = riMaxEdit;
            riMaxEdit.Name = "MAXBOX";
            riMaxEdit.BorderStyle = BorderStyles.NoBorder;
            maxEditorControlInfo.Size = new Size(26, 26);
            maxEditorControlInfo.Location = new Point(65, 25);

            riMaxEdit.Appearance.BackColor = Color.Transparent;
            maxEditorControlInfo.EditValue = Resources.header_chuangkou;
            maxEditorControlInfo.HAlignment = HAlignment.Right;
            maxEditorControlInfo.MouseDown += MaxEditorControlInfo_MouseDown;
            maxEditorControlInfo.MouseHover += MaxEditorControlInfo_MouseHover;
            maxEditorControlInfo.MouseLeave += MaxEditorControlInfo_MouseLeave;
            beforerepositoryItemsdic.Add(riMaxEdit, maxEditorControlInfo);
            RepositoryItemPictureEdit riMinEdit = new RepositoryItemPictureEdit();
            EditorControlInfo minEditorControlInfo = new EditorControlInfo();
            minEditorControlInfo.RepositoryItem = riMinEdit;
            riMinEdit.BorderStyle = BorderStyles.NoBorder;
            minEditorControlInfo.Size = new Size(26, 26);
            minEditorControlInfo.Location = new Point(95, 25);
            minEditorControlInfo.Name = "MINBOX";
            riMinEdit.Appearance.BackColor = Color.Transparent;
            minEditorControlInfo.EditValue = Resources.header_zuixiaohua;
            minEditorControlInfo.HAlignment = HAlignment.Right;
            minEditorControlInfo.MouseDown += MinEditorControlInfo_MouseDown;
            minEditorControlInfo.MouseHover += MinEditorControlInfo_MouseHover;
            minEditorControlInfo.MouseLeave += MinEditorControlInfo_MouseLeave;
            riMinEdit.Name = "MINBOX";
            beforerepositoryItemsdic.Add(riMinEdit, minEditorControlInfo);
            RepositoryItemButtonEdit riPersonLPEdit = new RepositoryItemButtonEdit();
            riPersonLPEdit.Name = "PERSONLP";
            riPersonLPEdit.ButtonsStyle = BorderStyles.NoBorder;

            riPersonLPEdit.AllowFocused = false;
            riPersonLPEdit.CustomDrawButton += RiPersonLPEdit_CustomDrawButton;
            riPersonLPEdit.MouseEnter += RiPersonLPEdit_MouseEnter;
            riPersonLPEdit.Buttons[0].Click += MediSuperTabControl_ButtonClick;
            riPersonLPEdit.Buttons[0].Kind = ButtonPredefines.Combo;
            riPersonLPEdit.ContextImageOptions.Image = Resources.header_doctor;
            riPersonLPEdit.NullText = string.Empty;
            EditorControlInfo personEditorControlInfo = new EditorControlInfo();
            personEditorControlInfo.RepositoryItem = riPersonLPEdit;
            personEditorControlInfo.Name = "PERSONLP";
            riPersonLPEdit.BorderStyle = BorderStyles.NoBorder;
            riPersonLPEdit.TextEditStyle = TextEditStyles.DisableTextEditor;

            riPersonLPEdit.Appearance.BorderColor = Color.FromArgb(0, 115, 195);
            riPersonLPEdit.Appearance.BackColor = Color.Transparent;
            riPersonLPEdit.Appearance.Font = new Font("微软雅黑", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            riPersonLPEdit.Appearance.ForeColor = Color.White;
            riPersonLPEdit.Click += RiPersonLPEdit_Click;

            BaseEdit personLPEdit = riPersonLPEdit.CreateEditor();
            Graphics persongh = personLPEdit.CreateGraphics();
            Size personSize = persongh.MeasureString(
               HISClientHelper.USERNAME + "(" + HISClientHelper.ZHIGONGGH + ")", 
               new Font("微软雅黑", 14, FontStyle.Regular, GraphicsUnit.Pixel)).ToSize();
            persongh.Dispose();
            personEditorControlInfo.Size = new Size(personSize.Width + 50, 26);

            personEditorControlInfo.Location = new Point(personEditorControlInfo.Size.Width + 120, 25);

            personEditorControlInfo.EditValue = HISClientHelper.USERNAME + "(" + HISClientHelper.ZHIGONGGH + ")";
            personEditorControlInfo.HAlignment = HAlignment.Right;
            personEditorControlInfo.MouseDown += PersonEditorControlInfo_MouseDown;
            personEditorControlInfo.MouseHover += PersonEditorControlInfo_MouseHover;
            personEditorControlInfo.MouseLeave += PersonEditorControlInfo_MouseLeave;

            beforerepositoryItemsdic.Add(riPersonLPEdit, personEditorControlInfo);

            RepositoryItemPictureEdit riBellEdit = new RepositoryItemPictureEdit { Name = "BELL" };

            EditorControlInfo bellEditorControlInfo = new EditorControlInfo();
            bellEditorControlInfo.Name = "BELL";
            bellEditorControlInfo.RepositoryItem = riBellEdit;
            riBellEdit.BorderStyle = BorderStyles.NoBorder;
            bellEditorControlInfo.Size = new Size(26, 26);
            bellEditorControlInfo.Location = new Point(personEditorControlInfo.Location.X + 31, 25);
            riBellEdit.Appearance.BackColor = Color.Transparent;
            bellEditorControlInfo.EditValue = Resources.header_xiaoxi;
            bellEditorControlInfo.HAlignment = HAlignment.Right;
            bellEditorControlInfo.MouseDown += BellEditorControlInfo_MouseDown;
            bellEditorControlInfo.MouseHover += BellEditorControlInfo_MouseHover;
            bellEditorControlInfo.MouseLeave += BellEditorControlInfo_MouseLeave;

            beforerepositoryItemsdic.Add(riBellEdit, bellEditorControlInfo);

            DevExpress.Utils.VisualEffects.AdornerUIManager adoBellInfo = new DevExpress.Utils.VisualEffects.AdornerUIManager(this.Container);
            xiaoxibadge = new DevExpress.Utils.VisualEffects.Badge();

            adoBellInfo.Elements.Add(xiaoxibadge);
            adoBellInfo.Owner = ParentForm;

            adoBellInfo.BadgeProperties.TextMargin = new System.Windows.Forms.Padding(0, 0, 0, 0);

            xiaoxibadge.Properties.BeginUpdate();

            xiaoxibadge.Click += Xiaoxibadge_Click;
            xiaoxibadge.Properties.Image = Resources.header_xiaoxiredbg;
            xiaoxibadge.Appearance.BackColor = Color.Red;
            xiaoxibadge.Appearance.Options.UseBackColor = true;
            xiaoxibadge.Properties.Location = ContentAlignment.TopRight;
            xiaoxibadge.Appearance.Font = new Font("微软雅黑", 9);
            xiaoxibadge.Properties.Offset = new Point(-(bellEditorControlInfo.Location.X - 20), 20);
            xiaoxibadge.Properties.PaintStyle = DevExpress.Utils.VisualEffects.BadgePaintStyle.Default;
            xiaoxibadge.TargetElement = this;
            xiaoxibadge.Visible = false;
            xiaoxibadge.Properties.EndUpdate();

            RepositoryItemTextEdit riSysNameEdit = new RepositoryItemTextEdit();
            EditorControlInfo sysNameEditorControlInfo = new EditorControlInfo();
            riSysNameEdit.BorderStyle = BorderStyles.NoBorder;
            riSysNameEdit.Name = "SYSNAME";
            riSysNameEdit.Appearance.ForeColor = Color.White;
            riSysNameEdit.Appearance.BackColor = Color.Transparent;
            riSysNameEdit.Appearance.Font = new Font("微软雅黑", 20, FontStyle.Regular, GraphicsUnit.Pixel);
            riSysNameEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            sysNameEditorControlInfo.Name = "SYSNAME";
            sysNameEditorControlInfo.Location = new Point(55, 30);
            sysNameEditorControlInfo.EditValue = HISClientHelper.YINGYONGMC;
            if (!string.IsNullOrWhiteSpace(HISClientHelper.YINGYONGMC))
            {
                // 计算LOGO处当前登录系统文字大小
                Graphics g = this.CreateGraphics();
                Size preferredSize = g.MeasureString(
                   HISClientHelper.YINGYONGMC.Trim(),
                   new Font("微软雅黑", 20, FontStyle.Regular, GraphicsUnit.Pixel)).ToSize();
                g.Dispose();
                sysNameEditorControlInfo.Size = new Size(preferredSize.Width, 40);
                this.LeftDistance = preferredSize.Width + 25;
            }

            sysNameEditorControlInfo.HAlignment = HAlignment.Left;
            beforerepositoryItemsdic.Add(riSysNameEdit, sysNameEditorControlInfo);
            RepositoryItemPictureEdit riSysIconEdit = new RepositoryItemPictureEdit();
            EditorControlInfo sysIcoEditorControlInfo = new EditorControlInfo();
            riSysIconEdit.BorderStyle = BorderStyles.NoBorder;
            riSysIconEdit.Appearance.BackColor = Color.Transparent;
            sysIcoEditorControlInfo.Size = new Size(35, 35);
            sysIcoEditorControlInfo.Location = new Point(19, 25);
            sysIcoEditorControlInfo.EditValue = Resources.header_mediinfologo;
            sysIcoEditorControlInfo.HAlignment = HAlignment.Left;
            riSysIconEdit.Name = "SYSICON";
            sysIcoEditorControlInfo.Name = "SYSICON";
            beforerepositoryItemsdic.Add(riSysIconEdit, sysIcoEditorControlInfo);

            this.EndUpdate();
            mediDoctorFlyoutPanel = CreateFlyOutControl();
        }
        /// <summary>
        /// 消息徽章事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xiaoxibadge_Click(object sender, EventArgs e)
        {
           if (ParentForm is MediUniversalMFBase form) form.SendCustomMessagForProcess();
        }
        /// <summary>
        /// 更新消息徽章
        /// </summary>
        internal void UpdateBageControl(bool isVisible,string numberStr="")
        {
            xiaoxibadge.Visible = isVisible;
            xiaoxibadge.Properties.Text = numberStr;
        }

        private void RiPersonLPEdit_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ButtonEdit)
            {
                (sender as ButtonEdit).Cursor = Cursors.Hand;
            }
        }

        private void RiPersonLPEdit_Click(object sender, EventArgs e)
        {
            ShowMyBeakForm();
        }

        private void MediSuperTabControl_ButtonClick(object sender, EventArgs e)
        {
            ShowMyBeakForm();
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string KeShiName { get; set; }

        private void ShowMyBeakForm()
        {
            mediDoctorFlyoutPanel.Size = new Size(this.ActiveEditor.Width, 135);
            mediDoctorFlyoutPanel.ParentForm = ParentForm;
            mediDoctorFlyoutPanel.OwnerControl = this.ActiveEditor;
            mediDoctorFlyoutPanel.ShowBeakForm();
        }

        /// <summary>
        /// 创建flyout控件
        /// </summary>
        /// <returns></returns>
        private MediFlyoutPanel CreateFlyOutControl()
        {
            MediFlyoutPanel mediFlyoutPanel1 = new MediFlyoutPanel();
            FlyoutPanelControl flyoutPanelControl1 = new FlyoutPanelControl();
            MediLayoutControl mediLayoutControl1 = new MediLayoutControl();

            SimpleButton btnQHXT = new SimpleButton();
            SimpleButton btnQHKS = new SimpleButton();
            SimpleButton btnSDPM = new SimpleButton();
            SimpleButton btnQHYH = new SimpleButton();
            SimpleButton btnSYSC = new SimpleButton();

            LayoutControlGroup layoutControlGroup1 = new LayoutControlGroup();
            LayoutControlItem layoutControlItem1 = new LayoutControlItem();
            LayoutControlItem layoutControlItem2 = new LayoutControlItem();
            LayoutControlItem layoutControlItem3 = new LayoutControlItem();
            LayoutControlItem layoutControlItem4 = new LayoutControlItem();
            LayoutControlItem layoutControlItem5 = new LayoutControlItem();

            ((ISupportInitialize)(mediFlyoutPanel1)).BeginInit();
            mediFlyoutPanel1.SuspendLayout();
            ((ISupportInitialize)(flyoutPanelControl1)).BeginInit();
            flyoutPanelControl1.SuspendLayout();
            ((ISupportInitialize)(mediLayoutControl1)).BeginInit();
            mediLayoutControl1.SuspendLayout();
            ((ISupportInitialize)(layoutControlGroup1)).BeginInit();
            ((ISupportInitialize)(layoutControlItem1)).BeginInit();
            ((ISupportInitialize)(layoutControlItem2)).BeginInit();
            ((ISupportInitialize)(layoutControlItem3)).BeginInit();
            ((ISupportInitialize)(layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // mediFlyoutPanel1
            // 
            mediFlyoutPanel1.Controls.Add(flyoutPanelControl1);
            mediFlyoutPanel1.Options.CloseOnOuterClick = true;
            mediFlyoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            mediFlyoutPanel1.Location = new Point(184, 141);
            mediFlyoutPanel1.Name = "mediFlyoutPanel1";
            mediFlyoutPanel1.Size = new Size(158, 130);
            mediFlyoutPanel1.TabIndex = 1;
            // 
            // flyoutPanelControl1
            // 
            flyoutPanelControl1.BorderStyle = BorderStyles.NoBorder;
            flyoutPanelControl1.Controls.Add(mediLayoutControl1);
            flyoutPanelControl1.Dock = DockStyle.Fill;
            flyoutPanelControl1.FlyoutPanel = mediFlyoutPanel1;
            flyoutPanelControl1.Location = new Point(0, 0);
            flyoutPanelControl1.Name = "flyoutPanelControl1";
            flyoutPanelControl1.Size = new Size(158, 130);
            flyoutPanelControl1.TabIndex = 0;
            // 
            // mediLayoutControl1
            // 
            mediLayoutControl1.Controls.Add(btnQHXT);
            mediLayoutControl1.Controls.Add(btnSDPM);
            mediLayoutControl1.Controls.Add(btnQHKS);
            mediLayoutControl1.Controls.Add(btnSYSC);
            mediLayoutControl1.Dock = DockStyle.Fill;
            mediLayoutControl1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            mediLayoutControl1.Location = new Point(0, 0);
            mediLayoutControl1.Name = "mediLayoutControl1";
            mediLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(590, 114, 650, 400);
            mediLayoutControl1.Root = layoutControlGroup1;
            mediLayoutControl1.Size = new Size(158, 130);
            mediLayoutControl1.TabIndex = 2;
            mediLayoutControl1.Text = "mediLayoutControl1";
            // 
            // 切换系统
            // 
            btnQHXT.Appearance.ForeColor = Color.Black;
            btnQHXT.AppearanceHovered.ForeColor = Color.FromArgb(28, 131, 206); ;
            btnQHXT.AppearanceHovered.Options.UseForeColor = true;
            btnQHXT.Location = new Point(12, 12);
            btnQHXT.Name = "btnQHXT";
            btnQHXT.ShowFocusRectangle = DefaultBoolean.False;
            btnQHXT.Size = new Size(134, 22);
            btnQHXT.StyleController = null;
            btnQHXT.BorderStyle = BorderStyles.NoBorder;
            btnQHXT.TabIndex = 4;
            btnQHXT.Text = "切换系统";
            btnQHXT.AllowFocus = false;
            btnQHXT.Click += BtnQHXT_Click;
            // 
            // 切换科室/切换病区
            // 
            btnQHKS.Appearance.ForeColor = Color.Black;
            btnQHKS.AppearanceHovered.ForeColor = Color.FromArgb(28, 131, 206);
            btnQHKS.AppearanceHovered.Options.UseForeColor = true;
            btnQHKS.Location = new Point(12, 64);
            btnQHKS.Name = "btnQHKS";
            btnQHKS.ShowFocusRectangle = DefaultBoolean.False;
            btnQHKS.Size = new Size(134, 22);
            btnQHKS.StyleController = null;
            btnQHKS.BorderStyle = BorderStyles.NoBorder;
            btnQHKS.TabIndex = 5;
            string keShiName = "切换科室";
            if (HISClientHelper.XITONGID == "12")
                keShiName = "切换病区";
            btnQHKS.Text = KeShiName ?? keShiName;
            btnQHKS.AllowFocus = false;
            btnQHKS.Click += BtnQHKS_Click;
            // 
            // 锁定屏幕
            // 
            btnSDPM.Appearance.ForeColor = Color.Black;
            btnSDPM.AppearanceHovered.ForeColor = Color.FromArgb(28, 131, 206);
            btnSDPM.AppearanceHovered.Options.UseForeColor = true;
            btnSDPM.Location = new Point(12, 38);
            btnSDPM.Name = "btnSDPM";
            btnSDPM.ShowFocusRectangle = DefaultBoolean.False;
            btnSDPM.Size = new Size(134, 22);
            btnSDPM.StyleController = null;
            btnSDPM.BorderStyle = BorderStyles.NoBorder;
            btnSDPM.TabIndex = 6;
            btnSDPM.Text = @"锁定屏幕";
            btnSDPM.AllowFocus = false;
            btnSDPM.Click += BtnSDPM_Click;
            // 
            // 切换用户
            // 
            btnQHYH.Appearance.ForeColor = Color.Black;
            btnQHYH.AppearanceHovered.ForeColor = Color.FromArgb(28, 131, 206);
            btnQHYH.AppearanceHovered.Options.UseForeColor = true;
            btnQHYH.Location = new Point(12, 38);
            btnQHYH.Name = "btnQHYH";
            btnQHYH.ShowFocusRectangle = DefaultBoolean.False;
            btnQHYH.Size = new Size(134, 22);
            btnQHYH.StyleController = null;
            btnQHYH.BorderStyle = BorderStyles.NoBorder;
            btnQHYH.TabIndex = 7;
            btnQHYH.Text = @"切换用户";
            btnQHYH.AllowFocus = false;
            btnQHYH.Click += BtnQHYH_Click;
            			   // 
            // 操作手册
            // 
            btnSYSC.Appearance.ForeColor = Color.Black;
            btnSYSC.AppearanceHovered.ForeColor = Color.FromArgb(28, 131, 206);
            btnSYSC.AppearanceHovered.Options.UseForeColor = true;
            btnSYSC.Location = new Point(12, 16);
            btnSYSC.Name = "btnSYSC";
            btnSYSC.ShowFocusRectangle = DefaultBoolean.False;
            btnSYSC.Size = new Size(134, 22);
            btnSYSC.StyleController = null;
            btnSYSC.BorderStyle = BorderStyles.NoBorder;
            btnSYSC.TabIndex = 8;
            //if (HISClientHelper.XITONGID == "12")
            //keShiName = "操作手册";
            btnSYSC.Text = @"操作手册";
            btnSYSC.AllowFocus = false;
            btnSYSC.Click += BtnSYSC_Click;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] {
            layoutControlItem1,
            layoutControlItem2,
            layoutControlItem3,
            layoutControlItem4,
            layoutControlItem5});
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new Size(158, 130);
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = btnQHXT;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(138, 26);
            layoutControlItem1.TextSize = new Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            //
            layoutControlItem2.Control = btnQHKS;
            layoutControlItem2.Location = new Point(0, 26);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(138, 26);
            layoutControlItem2.TextSize = new Size(0, 0);
            layoutControlItem2.TextVisible = false;
            // 院前准备中心、急诊分诊站 不显示“切换科室/病区”
            if (HISClientHelper.XITONGID == "77" || HISClientHelper.XITONGID == "36") layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnSDPM;
            layoutControlItem3.Location = new Point(0, 52);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(138, 38);
            layoutControlItem3.TextSize = new Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnQHYH;
            layoutControlItem4.Location = new Point(0, 52);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(138, 38);
            layoutControlItem4.TextSize = new Size(0, 0);
            layoutControlItem4.TextVisible = false;

            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = btnSYSC;
            layoutControlItem5.Location = new Point(0, 52);
            layoutControlItem5.Name = "layoutControlItem4";
            layoutControlItem5.Size = new Size(138, 38);
            layoutControlItem5.TextSize = new Size(0, 0);
            layoutControlItem5.TextVisible = false;

            ((ISupportInitialize)(mediFlyoutPanel1)).EndInit();
            mediFlyoutPanel1.ResumeLayout(false);
            ((ISupportInitialize)(flyoutPanelControl1)).EndInit();
            flyoutPanelControl1.ResumeLayout(false);
            ((ISupportInitialize)(mediLayoutControl1)).EndInit();
            mediLayoutControl1.ResumeLayout(false);
            ((ISupportInitialize)(layoutControlGroup1)).EndInit();
            ((ISupportInitialize)(layoutControlItem1)).EndInit();
            ((ISupportInitialize)(layoutControlItem2)).EndInit();
            ((ISupportInitialize)(layoutControlItem3)).EndInit();
            ((ISupportInitialize)(layoutControlItem4)).EndInit();
            ((ISupportInitialize)(layoutControlItem5)).EndInit();
            this.ResumeLayout(false);
            return mediFlyoutPanel1;
        }



        #region events

        /// <summary>
        /// 切换系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQHXT_Click(object sender, EventArgs e)
        {
            if (ParentForm != null && (ParentForm is LinChuangMainFormBase || ParentForm is ShouFeiMainFormBase))
            {
                // 判断是否有接诊病人
                if (ParentForm is LinChuangMainFormBase linChuangMainForm)
                {
                    if (linChuangMainForm.kuangjiadic.Count > 0)
                    {
                        MediMsgBox.Warn("当前有正在操作的病人，不可切换系统，请先关闭病人!");
                        return;
                    }
                }
                
                using (SwitchSystemForm switchSystemForm = new SwitchSystemForm())
                {
                    switchSystemForm.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 切换科室/切换病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQHKS_Click(object sender, EventArgs e)
        {
            if (ParentForm != null && (ParentForm is MediUniversalMFBase form))
            {
                var mainFormOpenEventArgs = new MainFormOpenEventArgs { ActionStateType = ActionType.ChangeKeShi };
                form.FireMainEvent(this, mainFormOpenEventArgs);
            }
        }

        /// <summary>
        /// 锁定屏幕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSDPM_Click(object sender, EventArgs e)
        {
            if (ParentForm != null && ParentForm is MediUniversalMFBase form)
            {
                var mainFormOpenEventArgs = new MainFormOpenEventArgs { ActionStateType = ActionType.LockScreen };
                form.FireMainEvent(this, mainFormOpenEventArgs);
            }
        }
        /// <summary>
        /// 操作手册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSYSC_Click(object sender, EventArgs e)
        {
            if (ParentForm != null && (ParentForm is MediUniversalMFBase form))
            {
                var mainFormOpenEventArgs = new MainFormOpenEventArgs { ActionStateType = ActionType.CaoZuoShouCe };
                form.FireMainEvent(this, mainFormOpenEventArgs);
            }
        }
        /// <summary>
        /// 切换用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQHYH_Click(object sender, EventArgs e)
        {
            if (ParentForm != null && ParentForm is MediUniversalMFBase form)
            {
                // 判断是否有接诊病人
                if (form is LinChuangMainFormBase linChuangMainForm && linChuangMainForm.kuangjiadic.Count > 0)
                {
                    MediMsgBox.Warn("当前有正在操作的病人，不可切换用户，请先关闭病人!");
                    return;
                }

                if (MediMsgBox.YesNo(this, "是否退出当前用户？", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    // 设置上次登录本地配置文件中的职工ID与工号为空
                    HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongID = "";     // 职工ID
                    HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH = "";     // 职工工号
                    HISClientHelper.ClientSetting.Save();

                    // 切换登录用户(启动一个新的进程)
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.FileName = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "Mediinfo.WinForm.HIS.Starter.exe");
                    processStartInfo.Arguments = "SwitchUser " + HISClientHelper.YINGYONGID;
                    processStartInfo.UseShellExecute = false;
                    processStartInfo.WorkingDirectory = Directory.GetParent(Application.StartupPath).FullName;
                    processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    // 处理XP系统报错问题
                    if (Environment.OSVersion.Version.Major >= 6)
                        Process.Start(processStartInfo);
                    else
                        WinApiHelper.ShellExecute(0,
                            "open",
                            processStartInfo.FileName,
                            null,
                            processStartInfo.WorkingDirectory,
                            11);

                    // 退出当前程序
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        #endregion

        private void RiPersonLPEdit_CustomDrawButton(object sender, CustomDrawButtonEventArgs e)
        {
            Point[] array = new Point[3];
            int xPos = e.Bounds.X + e.Bounds.Width - 8 - 5;
            int yPos = e.Bounds.Y + (e.Bounds.Height - 4) / 2;
            array[0].X = xPos;
            array[0].Y = yPos;
            array[1].X = xPos + 8;
            array[1].Y = yPos;
            array[2].X = xPos + 4;
            array[2].Y = yPos + 4;

            Color color = Color.White;
            if (e.State != ObjectState.Normal)
            {
                color = Color.FromArgb(171, 214, 255);
            }

            e.Graphics.FillPolygon(new SolidBrush(color), array);
            e.Handled = true;
        }

        private void MinEditorControlInfo_MouseLeave(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                ((EditorControlInfo)sender).EditValue = Resources.header_zuixiaohua;
            }
        }

        private void MinEditorControlInfo_MouseHover(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                ((EditorControlInfo)sender).EditValue = Resources.header_zuixiaohua_hover;
                //((EditorControlInfo)sender).RepositoryItem.Appearance.Image = ImageResource.header_zuixiaohua_hover;
            }
        }

        private void BellEditorControlInfo_MouseLeave(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                ((EditorControlInfo)sender).EditValue = Resources.header_xiaoxi;
            }
        }

        private void BellEditorControlInfo_MouseHover(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {

                (sender as EditorControlInfo).EditValue = Resources.header_xiaoxi_hover;

            }
        }

        private void PersonEditorControlInfo_MouseLeave(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                if (((EditorControlInfo)sender).RepositoryItem is RepositoryItemButtonEdit)
                {
                    ((RepositoryItemButtonEdit)(((EditorControlInfo)sender).RepositoryItem)).ContextImageOptions.Image = Resources.header_doctor;
                    ((RepositoryItemButtonEdit)(((EditorControlInfo)sender).RepositoryItem)).Appearance.ForeColor = Color.White;
                }
            }
        }

        private void PersonEditorControlInfo_MouseHover(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                ((RepositoryItemButtonEdit)(((EditorControlInfo)sender).RepositoryItem)).ContextImageOptions.Image = Resources.header_doctor_hover;
                ((RepositoryItemButtonEdit)(((EditorControlInfo)sender).RepositoryItem)).Appearance.ForeColor = Color.FromArgb(171, 214, 255);
            }
        }

        private void MaxEditorControlInfo_MouseLeave(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                if (ParentForm != null)
                {
                    if (ParentForm.WindowState == FormWindowState.Maximized)
                    {

                        ((EditorControlInfo)sender).EditValue = Resources.header_chuangkou;
                    }
                    else if (ParentForm.WindowState == FormWindowState.Normal)
                    {
                        ((EditorControlInfo)sender).EditValue = Resources.header_zuidahua;
                    }
                }

            }
        }

        private void MaxEditorControlInfo_MouseHover(object sender, EventArgs e)
        {
            if (ParentForm != null)
            {
                if (ParentForm.WindowState == FormWindowState.Maximized)
                {

                    ((EditorControlInfo)sender).EditValue = Resources.header_chuangkou_hover;
                }
                else if (ParentForm.WindowState == FormWindowState.Normal)
                {
                    ((EditorControlInfo)sender).EditValue = Resources.header_zuidahua_hover;
                }
            }
        }

        private void CloseEditorControlInfo_MouseLeave(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo info)
            {
                info.EditValue = Resources.header_guanbi;
            }

            if (ParentForm != null && ParentForm is MediUniversalMFBase form)
            {
                form.MouseInCloseButton = false;
            }
        }

        private void CloseEditorControlInfo_MouseHover(object sender, EventArgs e)
        {
            if (sender is EditorControlInfo)
            {
                ((EditorControlInfo)sender).EditValue = Resources.header_guanbi_hover;
            }

            if (ParentForm != null && ParentForm is MediUniversalMFBase)
            {
                ((MediUniversalMFBase)ParentForm).MouseInCloseButton = true;
            }
        }

        private void BellEditorControlInfo_MouseDown(object sender, EventArgs e)
        {
            Xiaoxibadge_Click(sender,e);
        }

        private void PersonEditorControlInfo_MouseDown(object sender, EventArgs e)
        {
            CloseEditor();
            Rectangle editorBounds;
            if (ClickInEditor(sender, out editorBounds))
            {
                ShowEditor(sender, editorBounds);
            }
        }

        private bool ClickInEditor(object sender, out Rectangle editorBounds)
        {
            editorBounds = Rectangle.Empty;

            if (sender is EditorControlInfo)
            {
                RepositoryItem Item = ((EditorControlInfo)sender).RepositoryItem;
                editorBounds = ((EditorControlInfo)sender).Bound;
            }

            return editorBounds.Contains(this.PointToClient(MousePosition));
        }

        /// <summary>
        /// GetEditorBounds
        /// </summary>
        /// <param name="totalBounds"></param>
        /// <param name="rightIdent"></param>
        /// <returns></returns>
        public static Rectangle GetEditorBounds(Rectangle totalBounds, int rightIdent)
        {
            Rectangle bounds = totalBounds;
            return bounds;
        }
        private void ShowEditor(object sender, Rectangle bounds)
        {
            if (sender is EditorControlInfo)
            {
                RepositoryItem item = ((EditorControlInfo)sender).RepositoryItem;
                // ActiveEditor.
                ActiveEditor = item.CreateEditor();
                ActiveEditor.Properties.LockEvents();
                ActiveEditor.Parent = this;
                ActiveEditor.Properties.Assign(item);
                ActiveEditor.Properties.AutoHeight = false;
                ActiveEditor.Location = bounds.Location;
                ActiveEditor.Size = bounds.Size;
                ActiveEditor.CreateControl();
                ActiveEditor.EditValue = EditValue;
                ActiveEditor.SendMouse(ActiveEditor.PointToClient(Control.MousePosition), Control.MouseButtons);
                ActiveEditor.Properties.UnLockEvents();
            }
        }

        private void CloseEditor()
        {
            if (ActiveEditor != null)
            {
                EditValue = ActiveEditor.EditValue;
                ActiveEditor.Dispose();
                ActiveEditor = null;
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinEditorControlInfo_MouseDown(object sender, EventArgs e)
        {
            ParentForm.WindowState = FormWindowState.Minimized;

        }
        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxEditorControlInfo_MouseDown(object sender, EventArgs e)
        {
            Screen currentScreen = Screen.FromControl(this);
            if (ParentForm.WindowState == FormWindowState.Normal)
            {
                ParentForm.WindowState = FormWindowState.Maximized;

                if (Screen.AllScreens.Length > 1)
                {
                    ParentForm.MinimumSize = currentScreen.WorkingArea.Size;
                    ParentForm.DesktopBounds = currentScreen.WorkingArea;
                }
            }
            else if (ParentForm.WindowState == FormWindowState.Maximized)
            {
                ParentForm.WindowState = FormWindowState.Normal;

                //ParentForm.MaximumSize = new Size(1366, 768);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseEditorControlInfo_MouseDown(object sender, EventArgs e)
        {
            try
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
            catch (Exception exception)
            {
                
            }
          
        }

        /// <summary>
        /// 控件集合
        /// </summary>
        public Dictionary<RepositoryItem, EditorControlInfo> beforerepositoryItemsdic = new Dictionary<RepositoryItem, EditorControlInfo>();
        public Dictionary<RepositoryItem, EditorControlInfo> afterrepositoryItemsdic = new Dictionary<RepositoryItem, EditorControlInfo>();

        protected override void CheckInfo()
        {
            FieldInfo fi = typeof(XtraTabControl).GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);
            mediSkinViewInfoRegistrator = new MediSuperTabControlSkinViewInfoRegistrator();
            fi.SetValue(this, mediSkinViewInfoRegistrator);
            CreateView();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // MediSuperTabControl
            // 
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Margin = new System.Windows.Forms.Padding(0);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }

    public class MediSuperTabControlSkinTabHeaderViewInfo : SkinTabHeaderViewInfo
    {
        public MediSuperTabControlSkinTabHeaderViewInfo(BaseTabControlViewInfo viewInfo) : base(viewInfo) { }

        protected override void CalcPageViewInfo(BaseTabRowViewInfo row, BaseTabPageViewInfo info, ref Point topLeft)
        {
            if (HeaderLocation == TabHeaderLocation.Bottom
                    || HeaderLocation == TabHeaderLocation.Top)

                if (info.Page.TabControl.GetTabPage(0).Equals(info.Page))
                {
                    if (this.TabControl is MediSuperTabControl)
                        topLeft = new Point(topLeft.X + ((MediSuperTabControl)this.TabControl).LeftDistance + 45, topLeft.Y + 5);
                }
                else
                {
                    topLeft = new Point(topLeft.X + 10, topLeft.Y);
                }
            else
                topLeft = new Point(topLeft.X, topLeft.Y + 150);

            base.CalcPageViewInfo(row, info, ref topLeft);
        }

        protected override Size UpdateHeaderBoundsSize(Size size)
        {
            if (IsSideLocation)
            {
                size.Width += 2;
            }
            else
            {
                size.Height += 2;
            }
            return size;

        }

        protected override EditorButtonPainter OnHeaderButtonGetPainter(TabButtonInfo button)
        {
            return new MediSuperSkinTabHeaderButtonPainter(ViewInfo as SkinTabControlViewInfo);
        }

        //public Rectangle TabPageButtonBound { get; set; }
        public Dictionary<string, Rectangle> TabPageButtonBoundDic = new Dictionary<string, Rectangle>();

        protected override void UpdatePageBounds(BaseTabPageViewInfo info)
        {
            SkinTabControlViewInfo vi = ViewInfo as SkinTabControlViewInfo;
            XtraTabPage tabpage = info.Page as XtraTabPage;
            Rectangle r = info.Bounds;
            if (!TabPageButtonBoundDic.ContainsKey(tabpage.Name))
                TabPageButtonBoundDic.Add(tabpage.Name, r);
            else
                TabPageButtonBoundDic[tabpage.Name] = r;

            Skin skin = vi.Skin;
            Rectangle controlBoxRect = info.ControlBox;
            int hgrow = skin.Properties.GetInteger(TabSkinProperties.SelectedHeaderHGrow);
            //int delta = skin.Properties.GetInteger(TabSkinProperties.SelectedHeaderUpGrow);
            int delta = 0;
            int contentIndent = skin.Properties.GetInteger(GetContentIndent());
            if ((info.PageState & ObjectState.Selected) == 0)
            {
                hgrow = 0;
                delta = 0;
            }
            if (IsSideLocation)
            {
                r.Height += hgrow * 2; r.Y -= hgrow;
                r.Width += delta + skin.Properties.GetInteger(GetHeaderDownGrow(info));
                if (HeaderLocation == TabHeaderLocation.Left)
                {
                    delta *= -1;
                    r.X += delta;
                }
                else
                {
                    r.X -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                }
                info.Text = info.OffsetRect(info.Text, delta + contentIndent, 0);
                info.Image = info.OffsetRect(info.Image, delta + contentIndent, 0);
                if (!controlBoxRect.IsEmpty) controlBoxRect = info.OffsetRect(controlBoxRect, delta + contentIndent, 0);
            }
            else
            {
                r.Width += hgrow * 2 + 5; r.X -= hgrow;
                if (info.IsActiveState)
                    r.Height = 46;
                else
                    r.Height = 44;

                //r.Height += delta + skin.Properties.GetInteger(GetHeaderDownGrow(info));
                if (HeaderLocation == TabHeaderLocation.Top)
                {
                    delta *= -1;
                    r.Y += delta - 1;
                }
                else
                {
                    r.Y -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                }
                info.Image = info.OffsetRect(info.Image, 0, delta / 2 + contentIndent);
                info.Text = info.OffsetRect(info.Text, 3, -4);

                if (CanShowCloseButtonForPage(info) || CanShowPinButtonForPage(info))
                {
                    controlBoxRect = info.OffsetRect(controlBoxRect, 0, delta / 2 + contentIndent - 3);
                }
            }
            if (!controlBoxRect.IsEmpty)
            {
                info.ButtonsPanel.ViewInfo.SetDirty();
                info.ButtonsPanel.ViewInfo.Calc(GraphicsInfo.Cache, controlBoxRect);

            }
            info.Bounds = r;
        }
        protected override Size CalcPageClientSize(BaseTabPageViewInfo info)
        {
            Size size = base.CalcPageClientSize(info);
            Size min = CalcMinPageSize();
            size.Width = Math.Max(size.Width, min.Width);
            size.Height = 40;
            size.Height = Math.Max(size.Height, min.Height);
            return size;
        }

        protected override void CalcRows(Rectangle rect, Size buttonsSize)
        {
            base.CalcRows(rect, buttonsSize);
        }
        protected override TabButtonsPanel CreateHeaderButtons()
        {
            return new MediSuperTabControlTabButtonsPanel(ViewInfo);
        }
        public override bool UpdatePageStates()
        {
            return base.UpdatePageStates();
        }
    }

    public class MediSuperSkinTabHeaderButtonPainter : SkinTabHeaderButtonPainter
    {
        public MediSuperSkinTabHeaderButtonPainter(ISkinProvider provider) : base(provider)
        {

        }

        protected override SkinElement GetSkinElement(EditorButtonObjectInfoArgs e, ButtonPredefines kind)
        {

            return TabSkins.GetSkin(Provider)["SuperTabHeaderButton"];
        }
    }

    public class MediSuperTabControlTabButtonsPanel : TabButtonsPanel
    {
        public MediSuperTabControlTabButtonsPanel(BaseTabControlViewInfo tabViewInfo)
            : base(tabViewInfo)
        {

        }

        protected override TabButtonInfo CreateUserButton(CustomHeaderButton button)
        {
            return new MediSuperTabControlUserTabButtonInfo(button);
        }
        protected override TabButtonInfo CreateButton(TabButtonType button)
        {
            if (!CanShowButton(button)) return null;
            ButtonPredefines predefines = ButtonPredefines.Glyph;
            switch (button)
            {
                case TabButtonType.Prev:
                    predefines = Orientation == TabOrientation.Horizontal ? ButtonPredefines.Left : ButtonPredefines.Up;
                    break;
                case TabButtonType.Next:
                    predefines = Orientation == TabOrientation.Horizontal ? ButtonPredefines.Right : ButtonPredefines.Down;
                    break;
                case TabButtonType.Close:
                    predefines = ButtonPredefines.Close;
                    break;
            }
            EditorButton editorButton = new EditorButton(predefines);
            return new MediSuperTabControlTabButtonInfo(editorButton, button);
        }
    }

    public class MediSuperTabControlUserTabButtonInfo : UserTabButtonInfo
    {
        public MediSuperTabControlUserTabButtonInfo(EditorButton button) : base(button) { }

        protected override void CreatePainter()
        {
            Painter = new MediSuperTabControlEditorButtonPainter(UserLookAndFeel.Default.Painter.Button);
        }
    }

    public class MediSuperTabControlTabButtonInfo : TabButtonInfo
    {
        public MediSuperTabControlTabButtonInfo(EditorButton button) : base(button) { }
        public MediSuperTabControlTabButtonInfo(EditorButton button, TabButtonType buttonType)
            : base(button, buttonType)
        {

        }

        protected override void CreatePainter()
        {
            if (Collection != null)
                base.CreatePainter();
            else
            {
                Painter = new MediSuperTabControlEditorButtonPainter(UserLookAndFeel.Default.Painter.Button);
            }
        }
    }

    public class MediSuperTabControlEditorButtonPainter : EditorButtonPainter
    {
        public MediSuperTabControlEditorButtonPainter(ObjectPainter buttonPainter)
            : base(buttonPainter)
        {

        }

        public override void DrawObject(ObjectInfoArgs e)
        {
            UpdateButtonInfo(e);
            if (IsTextOnlyDrawing)
            {
                DrawContent(e);
            }
            else
            {
                DrawButton(e);
                DrawContent(e);
                DrawFocusRectangle(e);
            }
        }
        protected override void UpdateButtonInfo(ObjectInfoArgs e)
        {
            e.Bounds = new Rectangle(e.Bounds.X, e.Bounds.Y, 35, 35);
        }

        protected override void DrawButton(ObjectInfoArgs e)
        {
            new RotateObjectPaintHelper().DrawRotated(e.Cache, GetButtonInfoArgs(e), ButtonPainter, GetRotationAngle(e));
        }
    }

    public class MediSuperTabControlSkinViewInfoRegistrator : MediSkinViewInfoRegistrator
    {
        public MediSuperTabControlSkinViewInfoRegistrator() : base() { }

        public MediSuperTabControlSkinTabHeaderViewInfo TabHeaderViewInfo { get; set; }
        public override BaseTabHeaderViewInfo CreateHeaderViewInfo(BaseTabControlViewInfo viewInfo)
        {
            TabHeaderViewInfo = new MediSuperTabControlSkinTabHeaderViewInfo(viewInfo);
            return TabHeaderViewInfo;
        }

        public override string ViewName
        {
            get
            {
                return "MediSuperTabControl";
            }
        }

        public MediSuperTabControlViewInfo MediViewInfo { get; set; }
        public override BaseTabControlViewInfo CreateViewInfo(IXtraTab tabControl)
        {
            return new MediSuperTabControlViewInfo(tabControl);
        }

        public override BaseTabPainter CreatePainter(IXtraTab tabControl)
        {
            return new MediSuperTabControlSkinTabPainter(tabControl);
        }

        public override BaseButtonsPanelPainter CreateControlBoxPainter(IXtraTab tabControl)
        {
            return new MediSuperTabButtonsPanelSkinPainter(tabControl.LookAndFeel);
        }
    }

    public class MediSuperTabButtonsPanelSkinPainter : TabButtonsPanelSkinPainter
    {
        public MediSuperTabButtonsPanelSkinPainter(ISkinProvider provider) : base(provider)
        {

        }

        public override DevExpress.XtraEditors.ButtonPanel.BaseButtonPainter GetButtonPainter()
        {
            return new MediSuperTabButtonSkinPainter(Provider);
        }
    }

    public class MediSuperTabButtonSkinPainter : TabButtonSkinPainter
    {
        public MediSuperTabButtonSkinPainter(ISkinProvider provider) : base(provider)
        {

        }

        protected override void DrawBackground(GraphicsCache cache, BaseButtonInfo info)
        {
            SkinElement skinElement = TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            if (((BaseTabPageViewInfo)info.ButtonPanelOwner).IsActiveState)
                skinElement = TabSkins.GetSkin(SkinProvider)["ActiveSuperTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            else
                skinElement = TabSkins.GetSkin(SkinProvider)["ActiveNormalTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            SkinElementInfo skinElementInfo = new SkinElementInfo(skinElement, info.Bounds);
            skinElementInfo.State = info.State;
            skinElementInfo.Cache = cache;
            skinElementInfo.ImageIndex = CalcImageIndexCore(info.State, skinElementInfo);
            skinElementInfo.GlyphIndex = CalcGlyphIndex(info.State, info);
            ObjectPainter.DrawObject(cache, SkinElementPainter.Default, skinElementInfo);
        }
    }
    public class MediSuperTabControlSkinTabPainter : SkinTabPainter
    {
        public IXtraTab MediSuperTabControl { get; set; }
        public MediSuperTabControlSkinTabPainter(IXtraTab tabControl)
            : base(tabControl)
        {
            MediSuperTabControl = tabControl;
        }

        protected override void DrawHeader(TabDrawArgs e)
        {
            base.DrawHeader(e);
            BaseTabHeaderViewInfo hInfo = e.ViewInfo.HeaderInfo;

            if (MediSuperTabControl is MediSuperTabControl)
            {
                MediSuperTabControl mediSuperTabControl = MediSuperTabControl as MediSuperTabControl;
                foreach (var repositoryItem in mediSuperTabControl.beforerepositoryItemsdic)
                {
                    Rectangle rectangle;
                    if (repositoryItem.Value.HAlignment == HAlignment.Left)
                    {
                        if (repositoryItem.Value.Name.Equals("SYSNAME"))
                        {
                            rectangle = new Rectangle(new Point(repositoryItem.Value.Location.X, hInfo.Bounds.Height - 43), repositoryItem.Value.Size);
                        }
                        else
                        {
                            rectangle = new Rectangle(new Point(repositoryItem.Value.Location.X, hInfo.Bounds.Height - 41), repositoryItem.Value.Size);
                        }
                        DrawTabHeaderEdit(e.Graphics, repositoryItem.Key, rectangle, repositoryItem.Value.EditValue);
                    }
                    else
                    {
                        rectangle = new Rectangle(new Point(hInfo.Bounds.Width - repositoryItem.Value.Location.X, hInfo.Bounds.Height - 39), repositoryItem.Value.Size);
                        DrawTabHeaderEdit(e.Graphics, repositoryItem.Key, rectangle, repositoryItem.Value.EditValue);

                    }
                    if (!mediSuperTabControl.afterrepositoryItemsdic.ContainsKey(repositoryItem.Key))
                    {
                        repositoryItem.Value.Bound = rectangle;
                        mediSuperTabControl.afterrepositoryItemsdic.Add(repositoryItem.Key, repositoryItem.Value);
                    }
                    else
                    {
                        repositoryItem.Value.Bound = rectangle;
                        mediSuperTabControl.afterrepositoryItemsdic[repositoryItem.Key] = repositoryItem.Value;
                    }
                }
            }
        }

        protected override void DrawHeaderBackground(TabDrawArgs e, BaseTabPageViewInfo pInfo, Rectangle bounds)
        {
            base.DrawHeaderBackground(e, pInfo, bounds);
            e.Graphics.DrawRectangle(new Pen(Color.Red), e.Bounds);
        }

        /// <summary>
        /// 画控件
        /// </summary>
        /// <param name="g">画笔</param>
        /// <param name="edit">控件</param>
        /// <param name="r">控件size</param>
        /// <param name="value">控件值</param>
        public static void DrawTabHeaderEdit(Graphics g, RepositoryItem edit, Rectangle r, object value)
        {
            // 判断是否是名称，如果是则画一条垂直线
            if (String.Compare(edit.Name, "SYSNAME", true) == 0)
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(102, 171, 219)))
                {
                    Pen pen = new Pen(brush, 1F);
                    g.DrawLine(pen, new Point(r.X + r.Width + 5, r.Y + 7), new Point(r.X + r.Width + 5, r.Y + 33));
                }
            }

            BaseEditViewInfo info = edit.CreateViewInfo();
            info.UpdatePaintAppearance();
            BaseEditPainter painter = edit.CreatePainter();

            info.EditValue = value;

            info.Bounds = r;
            info.CalcViewInfo(g);
            ControlGraphicsInfoArgs args = new ControlGraphicsInfoArgs(info, new GraphicsCache(g), r);
            painter.Draw(args);

            args.Cache.Dispose();
        }
    }

    public class MediSuperTabControlViewInfo : SkinTabControlViewInfo
    {
        public MediSuperTabControlViewInfo(IXtraTab tabControl) : base(tabControl) { }

        public override SkinElement SkinPane { get { return Skin["SuperTabPane"]; } }

        public override SkinElement SkinHeader { get { return Skin["SuperTabHeader"]; } }
    }

    public class EditorControlInfo
    {
        /// <summary>
        /// 鼠标点击状态
        /// </summary>
        public MouseState MouseStateByEdit { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public RepositoryItem RepositoryItem { get; set; }
        public Rectangle Bound { get; set; }
        public object EditValue { get; set; }
        public HAlignment HAlignment { get; set; }

        public event EventHandler MouseDown;
        public event EventHandler MouseHover;
        public event EventHandler MouseLeave;
        public event EventHandler MouseMove;

        public void RaiseMouseDown(EventArgs mouseDownEventArgs)
        {
            MouseDown?.Invoke(this, mouseDownEventArgs);
        }

        public void RaiseMouseHover()
        {
            MouseHover?.Invoke(this, new EventArgs());
        }

        public void RaiseMouseLeave()
        {
            MouseLeave?.Invoke(this, new EventArgs());
        }

        public void RaiseMouseMove()
        {
            MouseMove?.Invoke(this, new EventArgs());
        }
    }
    /// <summary>
    /// tabheader控件自定义控件参数
    /// </summary>
    public class TabHeaderCustomControlEventArgs:EventArgs
    {
        /// <summary>
        /// tabheader控件集合
        /// </summary>
        public Dictionary<RepositoryItem, EditorControlInfo> repositoryItemsdic { get; set; }
    }

    /// <summary>
    /// 控件位置
    /// </summary>
    public enum HAlignment
    {
        Left = 0,
        Right = 1
    }

    public enum MouseState
    {
        Normal = 0,
        Hover = 1,
        Click = 2,
        Leave = 3,
    }

    [ToolboxItem(false)]
    public partial class MediWorkShopTabControl : MediTabControl
    {
        public MediWorkShopTabControl()
        {

            if (!ControlCommonHelper.IsDesignMode())
            {
                this.AppearancePage.HeaderActive.ForeColor = Color.White;
                this.AppearancePage.Header.ForeColor = Color.FromArgb(1, 51, 51, 51);
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
        protected override void CheckInfo()
        {
            FieldInfo fi = typeof(XtraTabControl).GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);

            fi.SetValue(this, new MediWorkShopSkinViewInfoRegistrator());
            CreateView();
        }
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    public class MediWorkShopSkinViewInfoRegistrator : MediSkinViewInfoRegistrator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MediWorkShopSkinViewInfoRegistrator() : base() { }

        /// <summary>
        /// 创建头部视图
        /// </summary>
        /// <param name="viewInfo"></param>
        /// <returns></returns>
        public override BaseTabHeaderViewInfo CreateHeaderViewInfo(BaseTabControlViewInfo viewInfo)
        {

            return new MediWorkShopSkinTabHeaderViewInfo(viewInfo);
        }

        public override string ViewName
        {
            get
            {
                return "MediWorkShopTabControl";
            }
        }
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="tabControl"></param>
        /// <returns></returns>
        public override BaseTabControlViewInfo CreateViewInfo(IXtraTab tabControl)
        {



            return new MediWorkShopViewInfo(tabControl);
        }

        public override BaseButtonsPanelPainter CreateControlBoxPainter(IXtraTab tabControl)
        {
            return new MediWorkShopTabButtonsPanelSkinPainter(tabControl.LookAndFeel);
        }
    }

    public class MediWorkShopTabButtonsPanelSkinPainter : TabButtonsPanelSkinPainter
    {
        public MediWorkShopTabButtonsPanelSkinPainter(ISkinProvider provider) : base(provider)
        {

        }

        public override DevExpress.XtraEditors.ButtonPanel.BaseButtonPainter GetButtonPainter()
        {
            return new MediWorkShopTabButtonSkinPainter(Provider);
        }
    }

    public class MediWorkShopTabButtonSkinPainter : TabButtonSkinPainter
    {
        public MediWorkShopTabButtonSkinPainter(ISkinProvider provider) : base(provider)
        {

        }

        protected override void DrawBackground(GraphicsCache cache, BaseButtonInfo info)
        {
            SkinElement skinElement = TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            if (((BaseTabPageViewInfo)info.ButtonPanelOwner).IsActiveState)
                skinElement = TabSkins.GetSkin(SkinProvider)["ActiveNormalTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            else
                skinElement = TabSkins.GetSkin(SkinProvider)["InActiveTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            SkinElementInfo skinElementInfo = new SkinElementInfo(skinElement, info.Bounds);
            skinElementInfo.State = info.State;
            skinElementInfo.Cache = cache;
            skinElementInfo.ImageIndex = CalcImageIndexCore(info.State, skinElementInfo);
            skinElementInfo.GlyphIndex = CalcGlyphIndex(info.State, info);
            ObjectPainter.DrawObject(cache, SkinElementPainter.Default, skinElementInfo);
        }
    }

    public class MediWorkShopViewInfo : SkinTabControlViewInfo
    {
        public MediWorkShopViewInfo(IXtraTab tabControl) : base(tabControl)
        {

        }
    }

    public class MediWorkShopSkinTabHeaderViewInfo : MediSkinTabHeaderViewInfo
    {
        public MediWorkShopSkinTabHeaderViewInfo(BaseTabControlViewInfo viewInfo) : base(viewInfo) { }

        protected override void UpdatePageBounds(BaseTabPageViewInfo info)
        {
            if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
            {
                SkinTabControlViewInfo vi = ViewInfo as SkinTabControlViewInfo;
                XtraTabPage tabpage = info.Page as XtraTabPage;
                Rectangle r = info.Bounds;

                Skin skin = vi.Skin;
                Rectangle controlBoxRect = info.ControlBox;
                int hgrow = skin.Properties.GetInteger(TabSkinProperties.SelectedHeaderHGrow);

                int delta = 0;
                int contentIndent = skin.Properties.GetInteger(GetContentIndent());
                if ((info.PageState & ObjectState.Selected) == 0)
                {
                    hgrow = 0;
                    delta = 0;
                }
                if (IsSideLocation)
                {
                    r.Height += hgrow * 2; r.Y -= hgrow;
                    r.Width += delta + skin.Properties.GetInteger(GetHeaderDownGrow(info));
                    if (HeaderLocation == TabHeaderLocation.Left)
                    {
                        delta *= -1;
                        r.X += delta;
                    }
                    else
                    {
                        r.X -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                    }
                    info.Text = info.OffsetRect(info.Text, delta + contentIndent, 0);
                    info.Image = info.OffsetRect(info.Image, delta + contentIndent, 0);
                    if (!controlBoxRect.IsEmpty) controlBoxRect = info.OffsetRect(controlBoxRect, delta + contentIndent, 0);
                }
                else
                {
                    r.Width += hgrow * 2; r.X -= hgrow;
                    r.Height = 28;

                    if (HeaderLocation == TabHeaderLocation.Top)
                    {
                        delta *= -1;
                        r.Y += delta;
                    }
                    else
                    {
                        r.Y -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                    }
                    info.Image = info.OffsetRect(info.Image, 0, delta / 2 + contentIndent);
                    if (this.TabControl is MediTabControl mediTabControl)
                    {
                        info.Text = info.OffsetRect(info.Text, mediTabControl.IsAdsSide ? 41 : 3, -4);
                    }

                    if (CanShowCloseButtonForPage(info) || CanShowPinButtonForPage(info))
                    {
                        controlBoxRect = info.OffsetRect(controlBoxRect, 0, delta / 2 + contentIndent - 4);
                    }
                }
                if (!controlBoxRect.IsEmpty)
                {
                    info.ButtonsPanel.ViewInfo.SetDirty();
                    info.ButtonsPanel.ViewInfo.Calc(GraphicsInfo.Cache, controlBoxRect);

                }
                info.Bounds = r;
            }
            else
            {
                base.UpdatePageBounds(info);
            }
        }

        protected override Size UpdateHeaderBoundsSize(Size size)
        {
            if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
            {
                if (IsSideLocation)
                {
                    size.Width += 2;
                }
                else
                {
                    size.Height -= 6;
                }
            }
            return size;
        }
    }

    [ToolboxItem(false)]
    public partial class MediLCTabControl : MediTabControl
    {
        public Form ParentForm { get; set; }
        private int tabHeaderHeight = 2;
        [DefaultValue(2), Browsable(true)]
        public int TabHeaderHeight { get { return tabHeaderHeight; } set { tabHeaderHeight = value; } }
        public Dictionary<RepositoryItem, EditorControlInfo> beforerepositoryItemsdic = new Dictionary<RepositoryItem, EditorControlInfo>();
        public Dictionary<RepositoryItem, EditorControlInfo> afterrepositoryItemsdic = new Dictionary<RepositoryItem, EditorControlInfo>();
        public MediLCTabControl()
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                this.AppearancePage.HeaderActive.ForeColor = Color.White;
                this.AppearancePage.Header.ForeColor = Color.FromArgb(1, 51, 51, 51);
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
        protected override void CheckInfo()
        {
            FieldInfo fi = typeof(XtraTabControl).GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);

            fi.SetValue(this, new MediLCSkinViewInfoRegistrator());
            CreateView();
        }
        /// <summary>
        /// 创建控件集合
        /// </summary>
        /// <param name="controlCollection"></param>
        public void CreateTabHeaderControls(Mediinfo.WinForm.HIS.Controls.ControlCollection controlCollection)
        {
            beforerepositoryItemsdic.Clear();
            this.BeginUpdate();
            foreach (TabHeaderControl tabHeaderControl in controlCollection)
            {
                if (tabHeaderControl.RepositoryItem != null && tabHeaderControl.EditorControlInfo != null && tabHeaderControl.RepositoryItem is RepositoryItemPictureEdit)
                {
                    beforerepositoryItemsdic.Add(tabHeaderControl.RepositoryItem, tabHeaderControl.EditorControlInfo);
                }
                else if (tabHeaderControl.RepositoryItem != null && tabHeaderControl.EditorControlInfo != null && tabHeaderControl.RepositoryItem is RepositoryItemCheckEdit)
                {
                    beforerepositoryItemsdic.Add(tabHeaderControl.RepositoryItem, tabHeaderControl.EditorControlInfo);
                }
                else if (tabHeaderControl.RepositoryItem != null && tabHeaderControl.EditorControlInfo != null && tabHeaderControl.RepositoryItem is RepositoryItemTextEdit)
                {
                    beforerepositoryItemsdic.Add(tabHeaderControl.RepositoryItem, tabHeaderControl.EditorControlInfo);
                }
            }
            this.EndUpdate();

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CheckTabHeaderEditorsClick(e);
        }
        private void CheckTabHeaderEditorsClick(MouseEventArgs e)
        {
            if (FindEditorInfo(e, out var editorControlInfo))
            {
                ParentForm = this.FindForm();
                editorControlInfo.RaiseMouseDown(new TabHeaderCustomControlEventArgs() { repositoryItemsdic = afterrepositoryItemsdic });
                this.Invalidate(false);
            }
        }
        public bool FindEditorInfo(MouseEventArgs e, out EditorControlInfo editorControlInfo)
        {
            foreach (var repositoryItem in afterrepositoryItemsdic)
            {
                if (repositoryItem.Value.Bound.Contains(e.Location))
                {
                    editorControlInfo = repositoryItem.Value;
                    return true;
                }
            }
            editorControlInfo = null;
            return false;
        }
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            CheckTabHeaderEditorsMouseHover();
        }
        private void CheckTabHeaderEditorsMouseHover()
        {
            if (FindEditorInfoHover(out var editorControlInfo))
            {
                ParentForm = this.FindForm();
                editorControlInfo.RaiseMouseHover();
                this.Invalidate(false);
            }
        }
        public bool FindEditorInfoHover(out EditorControlInfo editorControlInfo)
        {
            foreach (var repositoryItem in afterrepositoryItemsdic)
            {
                if (repositoryItem.Value.Bound.Contains(this.PointToClient(MousePosition)))
                {
                    editorControlInfo = repositoryItem.Value;
                    return true;
                }
            }
            editorControlInfo = null;
            return false;
        }
        EditorControlInfo currentEditorControlInfo = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            CheckTabHeaderEditorsMouseMove(e);
        }
        private void CheckTabHeaderEditorsMouseMove(MouseEventArgs e)
        {
            if (afterrepositoryItemsdic == null)
                return;

            if (IsInAnotherEditorControlInfo(e, out var editorControlInfo))
            {
                editorControlInfo.RaiseMouseHover();
                editorControlInfo.RaiseMouseLeave();
                editorControlInfo.RaiseMouseMove();
            }
        }
        private bool IsInAnotherEditorControlInfo(MouseEventArgs e, out EditorControlInfo editorControlInfo)
        {
            bool isInAnotherEditorControlInfo = false;
            editorControlInfo = null;
            foreach (var repositoryItem in afterrepositoryItemsdic)
            {
                if (repositoryItem.Value.Bound.Contains(e.Location))
                {
                    editorControlInfo = repositoryItem.Value;
                    isInAnotherEditorControlInfo = currentEditorControlInfo != editorControlInfo;
                    currentEditorControlInfo = editorControlInfo;
                }
            }
            return isInAnotherEditorControlInfo;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            CheckTabHeaderEditorsMouseLeave();
        }
        private void CheckTabHeaderEditorsMouseLeave()
        {
            if (FindEditorInfoLeave(out var editorControlInfo))
            {
                ParentForm = this.FindForm();
                editorControlInfo.RaiseMouseLeave();
                this.Invalidate(false);
            }
        }
        public bool FindEditorInfoLeave(out EditorControlInfo editorControlInfo)
        {
            foreach (var repositoryItem in afterrepositoryItemsdic)
            {
                if (!repositoryItem.Value.Bound.Contains(this.PointToClient(MousePosition)))
                {
                    editorControlInfo = repositoryItem.Value;
                    return true;
                }
            }
            editorControlInfo = null;
            return false;
        }
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    public class MediLCSkinViewInfoRegistrator : MediSkinViewInfoRegistrator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MediLCSkinViewInfoRegistrator() : base() { }

        /// <summary>
        /// 创建头部视图
        /// </summary>
        /// <param name="viewInfo"></param>
        /// <returns></returns>
        public override BaseTabHeaderViewInfo CreateHeaderViewInfo(BaseTabControlViewInfo viewInfo)
        {

            return new MediLCSkinTabHeaderViewInfo(viewInfo);
        }

        public override string ViewName
        {
            get
            {
                return "MediLCTabControl";
            }
        }
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="tabControl"></param>
        /// <returns></returns>
        public override BaseTabControlViewInfo CreateViewInfo(IXtraTab tabControl)
        {
            return new MediLCViewInfo(tabControl);
        }

        public override BaseTabPainter CreatePainter(IXtraTab tabControl)
        {
            return new MediLCTabControlSkinTabPainter(tabControl);
        }

        public override BaseButtonsPanelPainter CreateControlBoxPainter(IXtraTab tabControl)
        {
            return new MediLCTabButtonsPanelSkinPainter(tabControl.LookAndFeel);
        }
    }
    public class MediLCTabButtonsPanelSkinPainter : TabButtonsPanelSkinPainter
    {
        public MediLCTabButtonsPanelSkinPainter(ISkinProvider provider) : base(provider)
        {

        }

        public override DevExpress.XtraEditors.ButtonPanel.BaseButtonPainter GetButtonPainter()
        {
            return new MediLCTabButtonSkinPainter(Provider);
        }
    }

    public class MediLCTabButtonSkinPainter : TabButtonSkinPainter
    {
        public MediLCTabButtonSkinPainter(ISkinProvider provider) : base(provider)
        {

        }

        protected override void DrawBackground(GraphicsCache cache, BaseButtonInfo info)
        {
            SkinElement skinElement;
            if (((BaseTabPageViewInfo)info.ButtonPanelOwner).IsActiveState)
                skinElement = TabSkins.GetSkin(SkinProvider)["ActiveNormalTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            else
                skinElement = TabSkins.GetSkin(SkinProvider)["InActiveTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            SkinElementInfo skinElementInfo = new SkinElementInfo(skinElement, info.Bounds)
            {
                State = info.State, Cache = cache
            };
            skinElementInfo.ImageIndex = CalcImageIndexCore(info.State, skinElementInfo);
            skinElementInfo.GlyphIndex = CalcGlyphIndex(info.State, info);
            ObjectPainter.DrawObject(cache, SkinElementPainter.Default, skinElementInfo);
        }
    }

    public class MediLCTabControlSkinTabPainter : SkinTabPainter
    {
        public IXtraTab MediLCTabControl { get; set; }
        public MediLCTabControlSkinTabPainter(IXtraTab tabControl)
            : base(tabControl)
        {
            MediLCTabControl = tabControl;
        }

        protected override void DrawHeader(TabDrawArgs e)
        {
            base.DrawHeader(e);
            BaseTabHeaderViewInfo hInfo = e.ViewInfo.HeaderInfo;


            if (MediLCTabControl is MediLCTabControl mediLcTabControl)
            {
                foreach (var repositoryItem in mediLcTabControl.beforerepositoryItemsdic)
                {
                    Rectangle rectangle;

                    if (!string.IsNullOrWhiteSpace(repositoryItem.Value.Name) && repositoryItem.Value.Name.Contains("&text"))
                    {
                        rectangle = new Rectangle(new Point(hInfo.Bounds.Width - repositoryItem.Value.Location.X, hInfo.Bounds.Height - 30), repositoryItem.Value.Size);
                    }
                    else
                    {
                        rectangle = new Rectangle(new Point(hInfo.Bounds.Width - repositoryItem.Value.Location.X, hInfo.Bounds.Height - 29), repositoryItem.Value.Size);
                    }

                    DrawTabHeaderEdit(e.Graphics, repositoryItem.Key, rectangle, repositoryItem.Value.EditValue);
                    if (!mediLcTabControl.afterrepositoryItemsdic.ContainsKey(repositoryItem.Key))
                    {
                        repositoryItem.Value.Bound = rectangle;
                        mediLcTabControl.afterrepositoryItemsdic.Add(repositoryItem.Key, repositoryItem.Value);
                    }
                    else
                    {
                        repositoryItem.Value.Bound = rectangle;
                        mediLcTabControl.afterrepositoryItemsdic[repositoryItem.Key] = repositoryItem.Value;
                    }
                }
            }
        }

        protected override void DrawHeaderBackground(TabDrawArgs e, BaseTabPageViewInfo pInfo, Rectangle bounds)
        {
            base.DrawHeaderBackground(e, pInfo, bounds);
            e.Graphics.DrawRectangle(new Pen(Color.Red), e.Bounds);
        }

        /// <summary>
        /// 画控件
        /// </summary>
        /// <param name="g">画笔</param>
        /// <param name="edit">控件</param>
        /// <param name="r">控件size</param>
        /// <param name="value">控件值</param>
        public static void DrawTabHeaderEdit(Graphics g, RepositoryItem edit, Rectangle r, object value)
        {

            BaseEditViewInfo info = edit.CreateViewInfo();

            info.UpdatePaintAppearance();
            BaseEditPainter painter = edit.CreatePainter();

            info.EditValue = value;

            info.Bounds = r;
            info.CalcViewInfo(g);
            ControlGraphicsInfoArgs args = new ControlGraphicsInfoArgs(info, new GraphicsCache(g), r);
            painter.Draw(args);

            args.Cache.Dispose();
        }
    }

    public class MediLCViewInfo : SkinTabControlViewInfo
    {
        public MediLCViewInfo(IXtraTab tabControl) : base(tabControl)
        {

        }
    }

    public class MediLCSkinTabHeaderViewInfo : MediSkinTabHeaderViewInfo
    {
        public MediLCSkinTabHeaderViewInfo(BaseTabControlViewInfo viewInfo) : base(viewInfo) { }

        protected override void UpdatePageBounds(BaseTabPageViewInfo info)
        {
            MediLCTabControl mediTabControl = (MediLCTabControl)TabControl;
            SkinTabControlViewInfo vi = ViewInfo as SkinTabControlViewInfo;
            XtraTabPage tabpage = info.Page as XtraTabPage;
            Rectangle r = info.Bounds;

            Skin skin = vi.Skin;
            Rectangle controlBoxRect = info.ControlBox;
            int hgrow = skin.Properties.GetInteger(TabSkinProperties.SelectedHeaderHGrow);

            int delta = 0;
            int contentIndent = skin.Properties.GetInteger(GetContentIndent());
            if ((info.PageState & ObjectState.Selected) == 0)
            {
                hgrow = 0;
                delta = 0;
            }
            if (IsSideLocation)
            {
                r.Height += hgrow * 2; r.Y -= hgrow;
                r.Width += delta + skin.Properties.GetInteger(GetHeaderDownGrow(info));
                if (HeaderLocation == TabHeaderLocation.Left)
                {
                    delta *= -1;
                    r.X += delta;
                }
                else
                {
                    r.X -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                }
                info.Text = info.OffsetRect(info.Text, delta + contentIndent, 0);
                info.Image = info.OffsetRect(info.Image, delta + contentIndent, 0);
                if (!controlBoxRect.IsEmpty) controlBoxRect = info.OffsetRect(controlBoxRect, delta + contentIndent, 0);
            }
            else
            {
                r.Width += hgrow * 2; r.X -= hgrow;
                r.Height = 28;

                if (HeaderLocation == TabHeaderLocation.Top)
                {
                    delta *= -1;
                    r.Y += delta + mediTabControl.TabHeaderHeight - 2;
                }
                else
                {
                    r.Y -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                }
                info.Image = info.OffsetRect(info.Image, 0, delta / 2 + contentIndent);
                info.Text = mediTabControl.IsAdsSide ? info.OffsetRect(info.Text, 41, -4) : info.OffsetRect(info.Text, 3, mediTabControl.TabHeaderHeight - 5);
                
                if (CanShowCloseButtonForPage(info) || CanShowPinButtonForPage(info))
                {
                    controlBoxRect = info.OffsetRect(controlBoxRect, 0, delta / 2 + contentIndent + (mediTabControl.TabHeaderHeight - 5));
                }
            }
            if (!controlBoxRect.IsEmpty)
            {
                info.ButtonsPanel.ViewInfo.SetDirty();
                info.ButtonsPanel.ViewInfo.Calc(GraphicsInfo.Cache, controlBoxRect);

            }
            info.Bounds = r;
        }

        protected override Size UpdateHeaderBoundsSize(Size size)
        {
            MediLCTabControl mediTabControl = (MediLCTabControl)TabControl;
            if (IsSideLocation)
            {
                size.Width += 2;
            }
            else
            {
                size.Height -= (8 - mediTabControl.TabHeaderHeight);
            }

            return size;
        }
    }
}
