using DevExpress.DirectX.Common.Direct2D;
using DevExpress.XtraTabbedMdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static DevExpress.Data.Helpers.FindSearchRichParser;

namespace TN_PHANTAN
{
    public partial class formMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static frpt_BangDiemMonHoc frptBangDiemMonHoc = null;
        public formMain()
        {
            InitializeComponent();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }




        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }



        private void formMain_Load(object sender, EventArgs e)
        {
            //Form frm = this.CheckExists(typeof(formDangNhap));
            //if (frm != null) frm.Activate();
            //else
            //{
            //    formDangNhap f = new formDangNhap();
            //    f.MdiParent = this;
            //    f.Show();
            //}
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formKhoaLop));
            if (frm != null) frm.Activate();
            else
            {
                formKhoaLop f = new formKhoaLop();
                f.MdiParent = this;
                f.Show();
            }
        }

        public void HienThiMenu()
        {
            MAGV.Text = "Mã GV : " + Program.username;
            HOTEN.Text = "Họ tên giáo viên : " + Program.mHoten;
            NHOM.Text = "Nhóm : " + Program.mGroup;

        }

        private void btnBangDiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(frpt_BangDiemMonHoc));
            if (frm != null) frm.Activate();
            else
            {
                frpt_BangDiemMonHoc f = new frpt_BangDiemMonHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnGiangVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formGiaoVien));
            if (frm != null) frm.Activate();
            else
            {
                formGiaoVien f = new formGiaoVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnSinhVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formSinhVien));
            if (frm != null) frm.Activate();
            else
            {
                formSinhVien f = new formSinhVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnMonHoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formMonHoc));
            if (frm != null) frm.Activate();
            else
            {
                formMonHoc f = new formMonHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            Program.mlogin = "";
            Program.password = "";
            
            Program.frmDangNhap = new formDangNhap();
            this.Hide();
            Program.frmDangNhap.ShowDialog();
            this.Close();
        }

        private void btnThoatMain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát chương trình", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTaoTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formTaoTaiKhoan));
            if (frm != null) frm.Activate();
            else
            {
                formTaoTaiKhoan f = new formTaoTaiKhoan();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDeThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formDeThi));
            if (frm != null) frm.Activate();
            else
            {
                formDeThi f = new formDeThi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnGiangVienDK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(formGVDK));
            if (frm != null) frm.Activate();
            else
            {
                formGVDK f = new formGVDK();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDanhSachDKT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbMain.Enabled = false;
            Form frm = this.CheckExists(typeof(frpt_DSDK_THI));
            if (frm != null) frm.Activate();
            else
            {
                frpt_DSDK_THI f = new frpt_DSDK_THI();
                f.MdiParent = this;
                f.Show();
            }
        }
    }
}
