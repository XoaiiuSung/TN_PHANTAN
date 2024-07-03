using DevExpress.DataAccess.Wizard.Model;
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
using TN_PHANTAN.subform;

namespace TN_PHANTAN
{
    public partial class formGiaoVien : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private string magv = "";
        private Boolean checkThem = false;
        private Boolean checkSua = false;
        public formGiaoVien()
        {
            InitializeComponent();
        }

        private void gIAOVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsGiaoVien.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formGiaoVien_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
            this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
            this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);

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
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            if (bdsGiaoVien.Count == 0) btnXoa.Enabled = false;
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
                this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
                this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsGiaoVien.Position;
            pcGiaoVien.Enabled = true;
            bdsGiaoVien.AddNew();
            txtMAGV.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcGiaoVien.Enabled = false;
            checkThem = true;

            
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsGiaoVien.Position;
            pcGiaoVien.Enabled = true;
            gcGiaoVien.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (checkThem == true)
            {
                if (txtMAGV.Text.Trim() == "")
                {
                    MessageBox.Show("Mã giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtMAGV.Focus();
                    return;
                }
                if (txtHo.Text.Trim() == "")
                {
                    MessageBox.Show("Họ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtTen.Text.Trim() == "")
                {
                    MessageBox.Show("Tên giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtDiaChi.Text.Trim() == "")
                {
                    MessageBox.Show("Địa chỉ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtMAKH.Text.Trim() == "")
                {
                    MessageBox.Show("Mã khoa giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    btnChonKhoa.Focus();
                    return;
                }
                String sql = "EXEC SP_KT_GIAOVIEN_TONTAI '" + txtMAGV.Text.Trim() + "'";

                int kq = Program.ExecSqlNonQuery(sql);
                if (kq == 1)
                {
                    txtMAGV.Focus();
                    return;
                }
                else
                {
                    try
                    {

                        bdsGiaoVien.EndEdit();
                        bdsGiaoVien.ResetCurrentItem();
                        this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.GIAOVIENTableAdapter.Update(this.DS_TN_CSDLPT.GIAOVIEN);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi giáo viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
                checkThem = false;
            }
            else
            {
                if (txtMAGV.Text.Trim() == "")
                {
                    MessageBox.Show("Mã giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtMAGV.Focus();
                    return;
                }
                if (txtHo.Text.Trim() == "")
                {
                    MessageBox.Show("Họ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtTen.Text.Trim() == "")
                {
                    MessageBox.Show("Tên giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtDiaChi.Text.Trim() == "")
                {
                    MessageBox.Show("Địa chỉ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                else
                {
                    try
                    {

                        bdsGiaoVien.EndEdit();
                        bdsGiaoVien.ResetCurrentItem();
                        this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.GIAOVIENTableAdapter.Update(this.DS_TN_CSDLPT.GIAOVIEN);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi giáo viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            pcGiaoVien.Enabled = false;
            gcGiaoVien.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsBoDe.Count > 0)
            {
                MessageBox.Show("Không thể xóa giáo viên này vì đã có bộ đề!", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsGVDK.Count > 0)
            {
                MessageBox.Show("Không thể xóa giáo viên này vì đã có giáo viên đăng ký!", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thât sự muốn xóa giáo viên này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    magv = ((DataRowView)bdsGiaoVien[bdsGiaoVien.Position])["MAGV"].ToString();
                    bdsGiaoVien.RemoveCurrent();
                    this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.GIAOVIENTableAdapter.Update(DS_TN_CSDLPT.GIAOVIEN);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sinh viên. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
                    bdsGiaoVien.Position = bdsGiaoVien.Find("MAGV", magv);
                    return;
                }
            }
            if (bdsGiaoVien.Count == 0) btnXoa.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsGiaoVien.CancelEdit();
            if (btnThem.Enabled == false) bdsGiaoVien.Position = vitri;

            gcGiaoVien.Enabled = true;
            pcGiaoVien.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            checkThem = false;
            if (bdsGiaoVien.Count == 0) btnXoa.Enabled = false;
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

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void btnChonKhoa_Click(object sender, EventArgs e)
        {
            sformChonKhoa frm = new sformChonKhoa();
            frm.ShowDialog();
            this.txtMAKH.Text = Program.makhoaduocchon;
        }
    }
}