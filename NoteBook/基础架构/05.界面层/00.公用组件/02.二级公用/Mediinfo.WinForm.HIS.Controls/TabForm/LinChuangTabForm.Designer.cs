using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Mediinfo.WinForm.HIS.Controls.TabForm
{
    partial class LinChuangTabForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mediBRTabControl = new MediLCTabControl();
            this.lcTabControlRightMenu = new MediPopupMenu();
            this.barButtonItem1 = new BarButtonItem();
            this.barButtonItem2 = new BarButtonItem();
            this.barButtonItem3 = new BarButtonItem();
            this.mediLCTabControlBM = new BarManager();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            ((ISupportInitialize)(this.mediBRTabControl)).BeginInit();
            ((ISupportInitialize)(this.lcTabControlRightMenu)).BeginInit();
            ((ISupportInitialize)(this.mediLCTabControlBM)).BeginInit();
            this.SuspendLayout();
            // 
            // mediBRTabControl
            // 
            this.mediBRTabControl.Appearance.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.mediBRTabControl.Appearance.Options.UseBackColor = true;
            this.mediBRTabControl.BorderStyle = BorderStyles.NoBorder;
            this.mediBRTabControl.BorderStylePage = BorderStyles.NoBorder;
            this.mediBRTabControl.ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;
            this.mediBRTabControl.developerHelper = null;
            this.mediBRTabControl.Dock = DockStyle.Fill;
            this.mediBRTabControl.IsAdsSide = false;
            this.mediBRTabControl.Location = new Point(0, 0);
            this.mediBRTabControl.MediTabControlTheme = MediTabControlStyle.TabHeaderBlueButton;
            this.mediBRTabControl.Name = "mediBRTabControl";
            this.mediBRTabControl.ParentForm = null;
            this.mediBRTabControl.Size = new Size(890, 535);
            this.mediBRTabControl.TabIndex = 0;
            this.mediBRTabControl.SelectedPageChanged += new TabPageChangedEventHandler(this.mediBRTabControl_SelectedPageChanged);
            this.mediBRTabControl.SelectedPageChanging += new TabPageChangingEventHandler(this.mediBRTabControl_SelectedPageChanging);
            this.mediBRTabControl.CloseButtonClick += new EventHandler(this.mediTabControl1_CloseButtonClick);
            this.mediBRTabControl.MouseUp += new MouseEventHandler(this.mediBRTabControl_MouseUp);
            // 
            // lcTabControlRightMenu
            // 
            this.lcTabControlRightMenu.developerHelper = null;
            this.lcTabControlRightMenu.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
            new LinkPersistInfo(this.barButtonItem1),
            new LinkPersistInfo(this.barButtonItem2),
            new LinkPersistInfo(this.barButtonItem3)});
            this.lcTabControlRightMenu.Manager = this.mediLCTabControlBM;
            this.lcTabControlRightMenu.Name = "lcTabControlRightMenu";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "添加到工具栏";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "关闭左边";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "关闭右边";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // mediLCTabControlBM
            // 
            this.mediLCTabControlBM.DockControls.Add(this.barDockControlTop);
            this.mediLCTabControlBM.DockControls.Add(this.barDockControlBottom);
            this.mediLCTabControlBM.DockControls.Add(this.barDockControlLeft);
            this.mediLCTabControlBM.DockControls.Add(this.barDockControlRight);
            this.mediLCTabControlBM.Form = this;
            this.mediLCTabControlBM.Items.AddRange(new BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3});
            this.mediLCTabControlBM.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = DockStyle.Top;
            this.barDockControlTop.Location = new Point(0, 0);
            this.barDockControlTop.Manager = this.mediLCTabControlBM;
            this.barDockControlTop.Size = new Size(890, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = DockStyle.Bottom;
            this.barDockControlBottom.Location = new Point(0, 535);
            this.barDockControlBottom.Manager = this.mediLCTabControlBM;
            this.barDockControlBottom.Size = new Size(890, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = DockStyle.Left;
            this.barDockControlLeft.Location = new Point(0, 0);
            this.barDockControlLeft.Manager = this.mediLCTabControlBM;
            this.barDockControlLeft.Size = new Size(0, 535);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = DockStyle.Right;
            this.barDockControlRight.Location = new Point(890, 0);
            this.barDockControlRight.Manager = this.mediLCTabControlBM;
            this.barDockControlRight.Size = new Size(0, 535);
            // 
            // LinChuangTabForm
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(890, 535);
            this.Controls.Add(this.mediBRTabControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "LinChuangTabForm";
            this.Text = "LinChuangTabForm";
            this.FormClosed += new FormClosedEventHandler(this.LinChuangTabForm_FormClosed);
            ((ISupportInitialize)(this.mediBRTabControl)).EndInit();
            ((ISupportInitialize)(this.lcTabControlRightMenu)).EndInit();
            ((ISupportInitialize)(this.mediLCTabControlBM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MediLCTabControl mediBRTabControl;
        private MediPopupMenu lcTabControlRightMenu;
        private BarManager mediLCTabControlBM;
        private BarButtonItem barButtonItem1;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem barButtonItem2;
        private BarButtonItem barButtonItem3;
    }
}