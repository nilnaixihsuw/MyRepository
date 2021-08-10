using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediPanelButtonQX : MediForm
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string QuanXianID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string QuanXianMC { get; set; }

        /// <summary>
        /// 获取职工信息数据
        /// </summary>
        private JCJGZhiGongService GYZhiGongService { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 窗体名称
        /// </summary>
        public string ThisFormName { get; set; }

        /// <summary>
        /// 控件集合
        /// </summary>
        public List<Control> ControlList { get; set; }

        /// <summary>
        /// 功能id
        /// </summary>
        public string GongNengID { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 权限表数据获取服务
        /// </summary>
        public JCJGQuanXian2NService GYquanXian2nService { get; set; }

        /// <summary>
        /// 删除的行
        /// </summary>
        public List<E_GY_JUESECKQX_NEW> deleteRows { get; set; }
        /// <summary>
        /// 当前权限下的角色
        /// </summary>
        /// <param name="zhigongUserRootList"></param>
        public MediPanelButtonQX(string nameSpace, string fromName, List<Control> controlList, string gongNengId, string quanxianid, string quanxianMC, string controlName)
        {
            InitializeComponent();
            this.NameSpace = nameSpace;
            this.ThisFormName = fromName;
            this.ControlList = controlList;
            this.GongNengID = gongNengId;
            GYZhiGongService = new JCJGZhiGongService();
            this.GYquanXian2nService = new JCJGQuanXian2NService();
            this.QuanXianID = HISClientHelper.YINGYONGID + "." + GongNengID + "." + quanxianid;
            this.QuanXianMC = quanxianMC;
            this.ControlName = controlName;

            deleteRows = new List<E_GY_JUESECKQX_NEW>();
        }

        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnRightMove_Click(object sender, EventArgs e)
        {
            //权限管理
            MediTraceList<E_GY_JUESECKQX_NEW> E_GY_ZHIGONGYHQX1 = (MediTraceList<E_GY_JUESECKQX_NEW>)mediGridControl1.DataSource;

            if (E_GY_ZHIGONGYHQX1.Count > 0)
            {
                //用户权限管理
                MediTraceList<E_GY_JUESE> E_GY_ZHIGONGYHQX2 = (MediTraceList<E_GY_JUESE>)mediGridControl2.DataSource;
                if (this.mediGridView1.FocusedRowHandle > -1)
                {
                    string jueseid = (string)this.mediGridView1.GetFocusedRowCellValue("JUESEID");
                    E_GY_ZHIGONGYHQX1.ToList().ForEach(o =>
                    {
                        if (o.JUESEID == jueseid)
                        {
                            o.SetState(DTOState.Delete);
                            deleteRows.Add(o);
                        }
                    });

                    //E_GY_ZHIGONGYHQX1.Remove(E_GY_ZHIGONGYHQX1.Where(o => o.JUESEID == jueseid).FirstOrDefault());
                    int selectRow = this.mediGridView1.FocusedRowHandle;
                    //this.mediGridView1.DeleteSelectedRows();
                    this.mediGridView1.DeleteRow(selectRow);
                    mediGridControl1.DataSource = new MediTraceList<E_GY_JUESECKQX_NEW>(E_GY_ZHIGONGYHQX1);

                    this.mediGridView2.Columns["JUESEID"].BestFit();
                    this.mediGridView2.Columns["JUESEMC"].BestFit();

                    this.mediGridControl1.RefreshDataSource();
                    this.mediGridControl2.RefreshDataSource();
                }
            }
        }

        /// <summary>
        /// 全部右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnRightAllMove_Click(object sender, EventArgs e)
        {
            //已授权集合
            MediTraceList<E_GY_JUESECKQX_NEW> E_GY_ZHIGONGYHQX1 = (MediTraceList<E_GY_JUESECKQX_NEW>)mediGridControl1.DataSource;

            if (E_GY_ZHIGONGYHQX1.Count > 0)
            {
                //未授权集合
                MediTraceList<E_GY_JUESE> E_GY_ZHIGONGYHQX2 = (MediTraceList<E_GY_JUESE>)mediGridControl2.DataSource;
                if (this.mediGridView1.FocusedRowHandle > -1)
                {
                    E_GY_ZHIGONGYHQX1.ToList().ForEach(o =>
                    {
                        o.SetState(DTOState.Delete);
                        deleteRows.Add(o);
                    });

                    this.mediGridView1.SelectAll();
                    this.mediGridView1.DeleteSelectedRows();
                    mediGridControl1.DataSource = E_GY_ZHIGONGYHQX1;

                    this.mediGridView2.Columns["JUESEID"].BestFit();
                    this.mediGridView2.Columns["JUESEMC"].BestFit();

                    this.mediGridControl1.RefreshDataSource();
                    this.mediGridControl2.RefreshDataSource();
                }
            }

        }

        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediBtnLeftMove_Click(object sender, EventArgs e)
        {
            //未授权集合
            MediTraceList<E_GY_JUESE> E_GY_ZHIGONGYHQX2 = (MediTraceList<E_GY_JUESE>)mediGridControl2.DataSource;
            if (E_GY_ZHIGONGYHQX2 != null)
                if (E_GY_ZHIGONGYHQX2.Count > 0)
                {
                    //已授权集合
                    MediTraceList<E_GY_JUESECKQX_NEW> E_GY_ZHIGONGYHQX1 = (MediTraceList<E_GY_JUESECKQX_NEW>)mediGridControl1.DataSource;

                    if (this.mediGridView2.FocusedRowHandle > -1)
                    {
                        string jueseid = (string)this.mediGridView2.GetFocusedRowCellValue("JUESEID");

                        List<E_GY_JUESE> selectgyzhigongyhqx = E_GY_ZHIGONGYHQX2.Where(o => o.JUESEID == jueseid).ToList();
                        foreach (var item in selectgyzhigongyhqx)
                        {
                            var egyquanxian2new = new E_GY_JUESECKQX_NEW()
                            {
                                JUESEID = item.JUESEID,
                                JUESEMC = item.JUESEMC,
                                QUANXIANID = QuanXianID,
                                QUANXIANMC = QuanXianMC,
                                CONTROLNAME = ControlName,
                                GONGNENGID = GongNengID,
                                YINGYONGID = HISClientHelper.YINGYONGID
                            };
                            egyquanxian2new.SetTraceChange(true);
                            if (E_GY_ZHIGONGYHQX1.Where(o => o.JUESEID == item.JUESEID).ToList().Count > 0)
                            {
                                MediMsgBox.Show("已存在该角色！", "提示");
                                return;
                            }
                            E_GY_ZHIGONGYHQX1.Add(egyquanxian2new);
                        }

                        mediGridControl1.DataSource = E_GY_ZHIGONGYHQX1;

                        this.mediGridView2.Columns["JUESEID"].BestFit();
                        this.mediGridView2.Columns["JUESEMC"].BestFit();

                        this.mediGridControl1.RefreshDataSource();
                        this.mediGridControl2.RefreshDataSource();
                    }
                }

        }

        /// <summary>
        /// 全部左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediBtnLeftAllMove_Click(object sender, EventArgs e)
        {
            MediTraceList<E_GY_JUESE> E_GY_ZHIGONGYHQX2 = (MediTraceList<E_GY_JUESE>)mediGridControl2.DataSource;
            if (E_GY_ZHIGONGYHQX2 != null)
                if (E_GY_ZHIGONGYHQX2.Count > 0)
                {
                    //已授权集合
                    MediTraceList<E_GY_JUESECKQX_NEW> E_GY_ZHIGONGYHQX1 = (MediTraceList<E_GY_JUESECKQX_NEW>)mediGridControl1.DataSource;

                    foreach (var item in E_GY_ZHIGONGYHQX2)
                    {
                        var egyquanxian2new = new E_GY_JUESECKQX_NEW()
                        {
                            JUESEID = item.JUESEID,
                            JUESEMC = item.JUESEMC,
                            QUANXIANID = QuanXianID,
                            QUANXIANMC = QuanXianMC,
                            CONTROLNAME = ControlName,
                            YINGYONGID = HISClientHelper.YINGYONGID
                        };
                        egyquanxian2new.SetTraceChange(true);
                        if (E_GY_ZHIGONGYHQX1.Where(o => o.JUESEID == item.JUESEID).ToList().Count > 0)
                        {
                            MediMsgBox.Show("已存在该角色！", "提示");
                            continue;
                        }
                        E_GY_ZHIGONGYHQX1.Add(egyquanxian2new);
                    }
                    mediGridControl1.DataSource = E_GY_ZHIGONGYHQX1;

                    this.mediGridControl1.RefreshDataSource();
                }
        }

        /// <summary>
        /// 窗体加载后获取所有职工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondPowerAdSetForm_Load(object sender, EventArgs e)
        {
            MediTraceList<E_GY_JUESE> jueseqxs = new MediTraceList<E_GY_JUESE>();
            var yonghuqxxx = GYZhiGongService.GetYongHuCKQXByIDNEW(QuanXianID);
            if (yonghuqxxx.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                this.mediGridControl1.DataSource = new MediTraceList<E_GY_JUESECKQX_NEW>(yonghuqxxx.Return);
                this.mediGridView1.BestFitColumns();
            }

            var juesexx = GYZhiGongService.GetJueSe();
            if (juesexx.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                List<E_GY_JUESE> alljusexx = juesexx.Return.Where(o => o.ZUOFEIBZ == 0).ToList();
                this.mediGridControl2.DataSource = new MediTraceList<E_GY_JUESE>(alljusexx);
                this.mediGridView2.BestFitColumns();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnSave_Click(object sender, EventArgs e)
        {
            //如果当前集合为空则删除该权限下的所有用户,否则更新当前用户表信息

            MediTraceList<E_GY_JUESECKQX_NEW> juseqxList = (MediTraceList<E_GY_JUESECKQX_NEW>)mediGridControl1.DataSource;

            List<E_GY_JUESECKQX_NEW> changes = new List<E_GY_JUESECKQX_NEW>();
            //this.Tag = juseqxList.GetChanges();

            if (deleteRows.Count > 0)
            {
                changes = deleteRows;
            }
            if (juseqxList!=null&& juseqxList.Count>0)
            {
                changes.AddRange(juseqxList.GetChanges());
            }
            this.Tag = changes;

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnCancel_Click(object sender, EventArgs e)
        {
            MediTraceList<E_GY_JUESECKQX_NEW> juseqxList = (MediTraceList<E_GY_JUESECKQX_NEW>)mediGridControl1.DataSource;
            if (juseqxList!=null&& juseqxList.Count>0)
            {
                this.Tag = juseqxList.GetChanges();
            }
            this.Close();
            this.Dispose();
        }

        private void MediErJiQXGJForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string zhigongids = string.Empty;
            //for (int i = 0; i < mediGridView1.DataRowCount; i++)
            //{GetRowCellValue(i, this.mediGridView1.Columns["ZHIGONGID"])+",";
            //}
            //if (zhigongids.Length > 0)
            //    zhigongids += this.mediGridView1.
            //{
            //    zhigongids.Remove(zhigongids.Length - 1);
            //}
            //this.Tag = zhigongids;

            this.DialogResult = DialogResult.OK;
        }

        //private void jueselookupedit_EditValueChanged(object sender, EventArgs e)
        //{
        //    var xtyonghu = GYZhiGongService.GetYongHuYYByYingYongID(HISClientHelper.YINGYONGID);
        //    if (xtyonghu.ReturnCode == Enterprise.ReturnCode.SUCCESS)
        //    {
        //        List<E_GY_YONGHUYY> yonghulist = new List<E_GY_YONGHUYY>(xtyonghu.Return);
        //        var juserenyuan = GYZhiGongService.GetJueSeYHEXByID(jueselookupedit.EditValue.ToString());
        //        if (juserenyuan.ReturnCode == Enterprise.ReturnCode.SUCCESS)
        //        {
        //            List<E_GY_JUESEYH_EX> jueserenyuanlist = new List<E_GY_JUESEYH_EX>(juserenyuan.Return);
        //            MediTraceList<E_GY_JUESEYH_EX> jueserenyuans = new MediTraceList<E_GY_JUESEYH_EX>();
        //            foreach (string item in yonghulist.Select(o => o.YONGHUID))
        //            {

        //                jueserenyuanlist.ToList().ForEach(o =>
        //                {
        //                    if (o.YONGHUID == item)
        //                        jueserenyuans.Add(o);
        //                });
        //            }
        //            mediGridControl2.DataSource = jueserenyuans;
        //            this.mediGridView2.Columns["JUESEID"].BestFit();
        //            this.mediGridView2.Columns["JUESEMC"].BestFit();
        //        }
        //    }

        //}
    }
}