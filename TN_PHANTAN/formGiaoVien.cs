using DevExpress.XtraEditors;
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
    public partial class formGiaoVien : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        int magv = 0;
        string macoso = "";
        public formGiaoVien()
        {
            InitializeComponent();
        }

        private void gIAOVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsGIAOVIEN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formGiaoVien_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;

            this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);

            this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);

            this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);

            pcGiaoVien.Enabled = false;

            try
            {

                // Lấy kết danh sách phân mảnh đổ vào combobox
                cmbCOSO.DataSource = Program.bds_dspm;
                cmbCOSO.DisplayMember = "TENCN";
                cmbCOSO.ValueMember = "TENSERVER";
                cmbCOSO.SelectedIndex = Program.mCoso;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu cơ sở" + ex.Message, "", MessageBoxButtons.OK);
            }
            if (Program.mGroup == "TRUONG")
            {
                cmbCOSO.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                cmbCOSO.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = true;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsGIAOVIEN.Position;
            pcGiaoVien.Enabled = true;
            bdsGIAOVIEN.AddNew();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnInDS.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcGiaoVien.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsGIAOVIEN.CancelEdit();
            if(btnThem.Enabled == false) bdsGIAOVIEN.Position = vitri;
            gcGiaoVien.Enabled = true;
            pcGiaoVien.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnInDS.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsGIAOVIEN.Position;
            pcGiaoVien.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnInDS.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            gcGiaoVien .Enabled = false;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try 
            {
                this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(bdsGIAOVIEN_DANGKY.Count > 0)
            {
                MessageBox.Show("Không thể xóa giáo viên này vì đã đăng ký", "", MessageBoxButtons.OK);
                return;
            }
            if(bdsBODE.Count > 0)
            {
                MessageBox.Show("Không thể xóa giáo viên này vì có bộ đề", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thât sự muốn xóa giáo viên này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    magv = int.Parse(((DataRowView)bdsGIAOVIEN[bdsGIAOVIEN.Position])["MAGV"].ToString());
                    bdsGIAOVIEN.RemoveCurrent();
                    this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.GIAOVIENTableAdapter.Update(DS_TN_CSDLPT.GIAOVIEN);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
                    bdsGIAOVIEN.Position = bdsGIAOVIEN.Find("MAGV", magv);
                    return;
                }
            }
            if(bdsGIAOVIEN.Count == 0) btnXoa.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(txtMAGV.Text.Trim() == "")
            {
                MessageBox.Show("Mã giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtMAGV.Focus();
                return;
            }
            if(txtHO.Text.Trim() == "")
            {
                MessageBox.Show("Họ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHO.Focus();
                return;
            }
            if(txtTEN.Text.Trim() == "")
            {
                MessageBox.Show("Tên giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtTEN.Focus();
                return;
            }
            if(txtDIACHI.Text.Trim() == "")
            {
                MessageBox.Show("Dịa chỉ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtDIACHI.Focus();
                return;
            }
            try
            {
                bdsGIAOVIEN.EndEdit();
                bdsGIAOVIEN.ResetCurrentItem();
                this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIENTableAdapter.Update(this.DS_TN_CSDLPT.GIAOVIEN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi giáo viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            gcGiaoVien.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnInDS.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            pcGiaoVien.Enabled = false;

        }

        private void cmbCOSO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCOSO.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cmbCOSO.SelectedValue.ToString();

            if (cmbCOSO.SelectedIndex != Program.mCoso)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "", MessageBoxButtons.OK);
            else
            {
                this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);

                this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);

                this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}