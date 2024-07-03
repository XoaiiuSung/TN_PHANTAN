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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TN_PHANTAN
{
    public partial class formSinhVien : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private string masv = "";
        private Boolean checkThem = false;
        public formSinhVien()
        {
            InitializeComponent();
        }

        private void sINHVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsSinhVien.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formSinhVien_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
            this.BANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
            this.BANGDIEMTableAdapter.Fill(this.DS_TN_CSDLPT.BANGDIEM);
            
            pcSinhVien.Enabled = false;
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
            if (bdsSinhVien.Count == 0) btnXoa.Enabled = false;
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
                this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
                this.BANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
                this.BANGDIEMTableAdapter.Fill(this.DS_TN_CSDLPT.BANGDIEM);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsSinhVien.Position;
            pcSinhVien.Enabled = true;
            bdsSinhVien.AddNew();
            txtMASV.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcSinhVien.Enabled = false;
            checkThem = true;
            
            txtNgaySinh.EditValue = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsSinhVien.Position;
            pcSinhVien.Enabled = true;
            gcSinhVien.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (checkThem == true)
            {
                if (txtMASV.Text.Trim() == "")
                {
                    MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtMASV.Focus();
                    return;
                }
                if (txtHo.Text.Trim() == "")
                {
                    MessageBox.Show("Họ sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtTen.Text.Trim() == "")
                {
                    MessageBox.Show("Tên sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if(txtNgaySinh.DateTime > DateTime.Now)
                {
                    MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại!", "", MessageBoxButtons.OK);
                    txtNgaySinh.Focus();
                    return;
                }
                if (txtDIaChi.Text.Trim() == "")
                {
                    MessageBox.Show("Địa chỉ sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtMALOP.Text.Trim() == "")
                {
                    MessageBox.Show("Mã lớp sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    btnChonLop.Focus();
                    return;
                }
                String sql = "EXEC SP_KT_SINHVIEN_TONTAI '" + txtMASV.Text.Trim() + "'";

                int kq = Program.ExecSqlNonQuery(sql);
                if (kq == 1)
                {
                    txtMASV.Focus();
                    return;
                }
                else
                {
                    try
                    {

                        bdsSinhVien.EndEdit();
                        bdsSinhVien.ResetCurrentItem();
                        this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.SINHVIENTableAdapter.Update(this.DS_TN_CSDLPT.SINHVIEN);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi môn học! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
                checkThem = false;
            }
            else
            {
                if (txtMASV.Text.Trim() == "")
                {
                    MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtMASV.Focus();
                    return;
                }
                if (txtHo.Text.Trim() == "")
                {
                    MessageBox.Show("Họ sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtTen.Text.Trim() == "")
                {
                    MessageBox.Show("Tên sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                if (txtNgaySinh.DateTime > DateTime.Now)
                {
                    MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại!", "", MessageBoxButtons.OK);
                    txtNgaySinh.Focus();
                    return;
                }
                if (txtDIaChi.Text.Trim() == "")
                {
                    MessageBox.Show("Địa chỉ sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                    txtHo.Focus();
                    return;
                }
                else
                {
                    try
                    {

                        bdsSinhVien.EndEdit();
                        bdsSinhVien.ResetCurrentItem();
                        this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.SINHVIENTableAdapter.Update(this.DS_TN_CSDLPT.SINHVIEN);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi môn học! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            pcSinhVien.Enabled = false;
            gcSinhVien.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsBangDiem.Count > 0)
            {
                MessageBox.Show("Không thể xóa sinh viên này vì đã có bảng điểm!", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thât sự muốn xóa sinh viên này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    masv = ((DataRowView)bdsSinhVien[bdsSinhVien.Position])["MASV"].ToString();
                    bdsSinhVien.RemoveCurrent();
                    this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.SINHVIENTableAdapter.Update(DS_TN_CSDLPT.SINHVIEN);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sinh viên. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
                    bdsSinhVien.Position = bdsSinhVien.Find("MASV", masv);
                    return;
                }
            }
            if (bdsSinhVien.Count == 0) btnXoa.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsSinhVien.CancelEdit();
            if (btnThem.Enabled == false) bdsSinhVien.Position = vitri;

            gcSinhVien.Enabled = true;
            pcSinhVien.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            if (bdsSinhVien.Count == 0) btnXoa.Enabled = false;
            checkThem = false;
            if (bdsSinhVien.Count == 0) btnXoa.Enabled = false;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
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

        private void btnChonLop_Click(object sender, EventArgs e)
        {
            fomchonlop frm = new fomchonlop();
            frm.ShowDialog();
            this.txtMALOP.Text = Program.malopduocchon;
        }
    }
}