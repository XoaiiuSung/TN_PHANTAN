using DevExpress.DataAccess.Wizard.Model;
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

namespace TN_PHANTAN
{
    public partial class formGiaoVien : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private string magv = "";

        Stack undoList = new Stack();
        private Boolean dangthem = false;
        private Boolean dangsua = false;
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
            if (bdsGiaoVien.Count == 0)
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
                this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
                this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
                this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            }
        }

        private Boolean KiemTraLoiInput()
        {
            if (txtMAGV.Text.Trim() == "")
            {
                MessageBox.Show("Mã giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtMAGV.Focus();
                return false;
            }
            if (txtHo.Text.Trim() == "")
            {
                MessageBox.Show("Họ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtMAKH.Text.Trim() == "")
            {
                MessageBox.Show("Mã khoa giáo viên không được thiếu!", "", MessageBoxButtons.OK);
                btnChonKhoa.Focus();
                return false;
            }
            return true;
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
            dangthem = true;


        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangsua = true;
            vitri = bdsGiaoVien.Position;
            pcGiaoVien.Enabled = true;
            gcGiaoVien.Enabled = false;
            txtMAGV.Enabled = false; // tắt ghi mã gv

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!KiemTraLoiInput())
            {
                return;
            }

            DataRowView gv = (DataRowView)bdsGiaoVien[bdsGiaoVien.Position];
            string magiaovien = "";
            string ho = "";
            string ten = "";
            string diachi = "";
            string makh = "";

            String sql = "";
            int kq = 0;
            if (dangthem)
            {
                sql = "EXEC SP_KT_GIAOVIEN_TONTAI '" + txtMAGV.Text.Trim() + "'";
                kq = Program.ExecSqlNonQuery(sql);
            }
            if (kq == 1)
            {
                txtMAGV.Focus();
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
                                "DELETE DBO.GIAOVIEN " +
                                "WHERE MAGV = '" + txtMAGV.Text.Trim() + "'";
                        }
                        if (dangsua)
                        {
                            magiaovien = gv["MAGV"].ToString().Trim();
                            ho = gv["HO"].ToString();
                            ten = gv["TEN"].ToString();
                            diachi = gv["DIACHI"].ToString();
                            makh = gv["MAKH"].ToString().Trim();

                            CauTruyVanHoanTac =
                                            "UPDATE DBO.GIAOVIEN " +
                                            "SET " +
                                            "HO = N'" + ho + "'," +
                                            "TEN = N'" + ten + "'," +
                                            "DIACHI = N'" + diachi + "'," +
                                            "MAKH = '" + makh + "'" +
                                            "WHERE MAGV = '" + magiaovien + "'";

                        }
                        undoList.Push(CauTruyVanHoanTac);
                        dangthem = false;
                        dangsua = false;

                        bdsGiaoVien.EndEdit();
                        bdsGiaoVien.ResetCurrentItem();
                        this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.GIAOVIENTableAdapter.Update(this.DS_TN_CSDLPT.GIAOVIEN);

                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                        btnGhi.Enabled = false;
                        pcGiaoVien.Enabled = false;
                        gcGiaoVien.Enabled = true;
                        txtMAGV.Enabled = true; // bật lại để còn xài thêm
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi giáo viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string magiaovien = txtMAGV.Text.Trim();
            string ho = txtHo.Text;
            string ten = txtTen.Text;
            string diachi = txtDiaChi.Text;
            string makh = txtMAKH.Text.Trim();

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

                    MessageBox.Show("Xóa giáo viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    string cauTruyVanHoanTac =
                        "INSERT INTO DBO.GIAOVIEN( MAGV,HO,TEN,DIACHI,MAKH) " +
                        " VALUES( '" + magiaovien + "','" +
                        ho + "', '" +
                        ten + "', '" +
                        diachi + "', '" +
                        makh + "' ) ";
                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa giáo viên. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
                    bdsGiaoVien.Position = bdsGiaoVien.Find("MAGV", magv);
                    return;
                }
            }
            btnPhucHoi.Enabled = true;

            if (bdsGiaoVien.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangthem || dangsua)
            {
                bdsGiaoVien.CancelEdit();
                if (btnThem.Enabled == false) bdsGiaoVien.Position = vitri;

                gcGiaoVien.Enabled = true;
                pcGiaoVien.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = false;
                if (undoList.Count > 0) btnPhucHoi.Enabled = true;
                else btnPhucHoi.Enabled = false;
                txtMAGV.Enabled = true; // bật lên còn xài 

                dangthem = false;
                dangsua = false;
                if (bdsGiaoVien.Count == 0)
                {
                    btnXoa.Enabled = false;
                    btnHieuChinh.Enabled = false;
                }

                return;
            }

            String cauTruyVanHoanTac = undoList.Pop().ToString();
            int n = Program.ExceSqlNoneQuery(cauTruyVanHoanTac);
            this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);
            bdsGiaoVien.Position = vitri;
            if (undoList.Count > 0) btnPhucHoi.Enabled = true;
            else btnPhucHoi.Enabled = false;
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