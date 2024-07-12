using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TN_PHANTAN
{
    public partial class formSV_Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public formSV_Main()
        {
            InitializeComponent();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private void btnThi_ItemClick(object sender, ItemClickEventArgs e)
        {
            Program.formSV.rbSV_Main.Enabled = false;
            Form frm = this.CheckExists(typeof(formThi));
            if (frm != null) frm.Activate();
            else
            {
                formThi f = new formThi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDoiMK_ItemClick(object sender, ItemClickEventArgs e)
        {
            Program.formSV.rbSV_Main.Enabled = false;
            Form frm = this.CheckExists(typeof(formSV_DoiMatKhau));
            if (frm != null) frm.Activate();
            else
            {
                formSV_DoiMatKhau f = new formSV_DoiMatKhau();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDangXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            Program.mlogin = "";
            Program.password = "";

            Program.frmDangNhap = new formDangNhap();
            this.Hide();
            Program.frmDangNhap.ShowDialog();
            this.Close();
        }

        private void btnThoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát chương trình", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}