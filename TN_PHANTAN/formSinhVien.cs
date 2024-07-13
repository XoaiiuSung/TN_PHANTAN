using DevExpress.XtraEditors;
using System;
using System.Collections;
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

        Stack undoList = new Stack();
        private Boolean dangthem = false;
        private Boolean dangsua = false;
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
            if (bdsSinhVien.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
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

        private Boolean KiemTraLoiInput()
        {
            if (txtMASV.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txtMASV.Focus();
                return false;
            }
            if (txtHo.Text.Trim() == "")
            {
                MessageBox.Show("Họ sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtNgaySinh.DateTime > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại!", "", MessageBoxButtons.OK);
                txtNgaySinh.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtMALOP.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                btnChonLop.Focus();
                return false;
            }
            return true;
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
            dangthem = true;
            
            txtNgaySinh.EditValue = DateTime.Now;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsSinhVien.Position;
            pcSinhVien.Enabled = true;
            gcSinhVien.Enabled = false;
            txtMASV.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            dangsua = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!KiemTraLoiInput())
            {
                return;
            }

            DataRowView sv = (DataRowView)bdsSinhVien[bdsSinhVien.Position];
            string masinhvien = "";
            string ho = "";
            string ten = "";
            DateTime ngaysinh;
            string diachi = "";
            string malop = "";

            String sql = "";
            int kq = 0;

            if (dangthem)
            {
                sql = "EXEC SP_KT_SINHVIEN_TONTAI '" + txtMASV.Text.Trim() + "'";
                kq = Program.ExecSqlNonQuery(sql);
            }
            if (kq == 1)
            {
                txtMASV.Focus();
                return;
            }
            if (dangthem || dangsua)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn GHI dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    string CauTruyVanHoanTac = "";
                    try
                    {
                        if (dangthem)
                        {
                            CauTruyVanHoanTac = "" +
                                "DELETE DBO.SINHVIEN " +
                                "WHERE MASV = '" + txtMASV.Text.Trim() + "'";
                        }
                        if (dangsua)
                        {
                            masinhvien = sv["MASV"].ToString().Trim();
                            ho = sv["HO"].ToString();
                            ten = sv["TEN"].ToString();
                            ngaysinh = Convert.ToDateTime(sv["NGAYSINH"]);
                            string ngaysinhFormatted = ngaysinh.ToString("yyyy-MM-dd");
                            diachi = sv["DIACHI"].ToString();
                            malop = sv["MALOP"].ToString().Trim();

                            CauTruyVanHoanTac =
                                            "UPDATE DBO.SINHVIEN " +
                                            "SET " +
                                            "HO = N'" + ho + "'," +
                                            "TEN = N'" + ten + "'," +
                                            "NGAYSINH = '" + ngaysinhFormatted +"'," +
                                            "DIACHI = N'" + diachi + "'," +
                                            "MALOP = '" + malop + "'" +
                                            "WHERE MASV = '" + masinhvien + "'";

                        }
                        undoList.Push(CauTruyVanHoanTac);
                        dangthem = false;
                        dangsua = false;

                        bdsSinhVien.EndEdit();
                        bdsSinhVien.ResetCurrentItem();

                        this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.SINHVIENTableAdapter.Update(this.DS_TN_CSDLPT.SINHVIEN);

                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                        btnGhi.Enabled = false;
                        pcSinhVien.Enabled = false;
                        gcSinhVien.Enabled = true;
                        txtMASV.Enabled = true; // bật lại để còn xài thêm
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi sinh viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string masinhvien = txtMASV.Text.Trim();
            string ho = txtHo.Text;
            string ten = txtTen.Text;
            string ngaysinh = (txtNgaySinh.DateTime).ToString("yyyy-MM-dd");
            string diachi = txtDiaChi.Text;
            string malop = txtMALOP.Text.Trim();

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

                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    string cauTruyVanHoanTac =
                        "INSERT INTO DBO.SINHVIEN( MASV,HO,TEN,NGAYSINH,DIACHI,MALOP) " +
                        " VALUES( '" + masinhvien + "',N'" +
                        ho + "', N'" +
                        ten + "', '" +
                        ngaysinh + "', N'" +
                        diachi + "', '" +
                        malop + "' ) ";
                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sinh viên. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
                    bdsSinhVien.Position = bdsSinhVien.Find("MASV", masv);
                    return;
                }
            }
            btnPhucHoi.Enabled = true;
            if (bdsSinhVien.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangthem || dangsua)
            {
                bdsSinhVien.CancelEdit();
                if (btnThem.Enabled == false) bdsSinhVien.Position = vitri;

                gcSinhVien.Enabled = true;
                pcSinhVien.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = false;
                if (undoList.Count > 0) btnPhucHoi.Enabled = true;
                else btnPhucHoi.Enabled = false;
                txtMASV.Enabled = true; // bật lên còn xài 

                dangthem = false;
                dangsua = false;
                if (bdsSinhVien.Count == 0)
                {
                    btnXoa.Enabled = false;
                    btnHieuChinh.Enabled = false;
                }

                return;
            }
            String cauTruyVanHoanTac = undoList.Pop().ToString();
            int n = Program.ExceSqlNoneQuery(cauTruyVanHoanTac);
            this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
            bdsSinhVien.Position = vitri;
            if (undoList.Count > 0) btnPhucHoi.Enabled = true;
            else btnPhucHoi.Enabled = false;
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
                Program.formChinh.rbMain.Enabled = true;
                this.Dispose();
            }
        }

        private void btnChonLop_Click(object sender, EventArgs e)
        {
            sformChonLop frm = new sformChonLop();
            frm.ShowDialog();
            this.txtMALOP.Text = Program.malopduocchon;
        }
    }
}