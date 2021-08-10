using Mediinfo.HIS.Core;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Mediinfo.WinForm.HIS.Controls.FirstLevelFramework;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class MediMainForm : MainFormBase
    {
        public override string XiTongID
        {
            get
            {
                return "00";
            }
        }

        public MediMainForm()
        {
            InitializeComponent();
        }

        private void MediMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLockScreen)
            {
                e.Cancel = true;
                return;
            }

            if (IsCloseBaseForm)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                return;
            }

            string errorMsg = string.Empty;
            HISClientHelper.BatRunCmd("appshutdown.bat", AppDomain.CurrentDomain.BaseDirectory, out errorMsg);
            if (!string.IsNullOrWhiteSpace(errorMsg))
                throw new ApplicationException(errorMsg);
            List<string> yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();
            if (yingYongIdList != null)
            {
                if (yingYongIdList.Contains(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString()))
                {
                    MemoryMappedFileHelper.RemoveClipBoardData(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
                    yingYongIdList.Remove(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString());
                }
                if (yingYongIdList.Count < 1)
                    MemoryMappedFileHelper.ClearClipBoardData();
            }
        }

        private void MediMainForm_Shown(object sender, EventArgs e)
        {
            //add by niquan 2019/11/13
            if (HISClientHelper.YINGYONGID == "9801")
            {
                var path = Environment.CurrentDirectory;

                Control form = (Control)Assembly.Load("Mediinfo.WinForm.HIS.JFJK.JiFeiJK").CreateInstance("Mediinfo.WinForm.HIS.JFJK.JiFeiJK.W_JF_JiFeiJK");
                foreach (var control in this.Controls)
                {
                    if (control.GetType().ToString().Contains("MediPanelControl"))
                    {
                        ((MediPanelControl)control).Controls.Add(form);
                        form.Dock = DockStyle.Fill;
                    }
                }
            }
        }

        private void MediMainForm_SizeChanged(object sender, EventArgs e)
        {
            HISClientHelper.MainForm = this;
        }

        private void MediMainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                int screenLeft = (Screen.PrimaryScreen.Bounds.Width - HISClientHelper.MainForm.Width) / 2;
                int screenTop = (Screen.PrimaryScreen.Bounds.Height - HISClientHelper.MainForm.Height) / 2;
                HISClientHelper.MainForm.Top = screenTop;
                HISClientHelper.MainForm.Left = screenLeft;
            }
        }

        private void MediMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
            this.Dispose();
            Environment.Exit(0);
        }
    }
}
