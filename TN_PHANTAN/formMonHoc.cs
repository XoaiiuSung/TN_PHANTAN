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

namespace TN_PHANTAN
{
    public partial class formMonHoc : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private string mamh = "";
        Stack undoList = new Stack();
        private Boolean dangthem = false;
        private Boolean dangsua = false;

        public formMonHoc()
        {
            InitializeComponent();
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsMonHoc.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formMonHoc_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;

            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
            this.BANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
            this.BANGDIEMTableAdapter.Fill(this.DS_TN_CSDLPT.BANGDIEM);
            this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
            this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
            this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);

            pcMonHoc.Enabled = false;

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
            if (bdsMonHoc.Count == 0) btnXoa.Enabled = false;
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
                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                this.BANGDIEMTableAdapter.Connection.ConnectionString = Program.connstr;
                this.BANGDIEMTableAdapter.Fill(this.DS_TN_CSDLPT.BANGDIEM);
                this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
                this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            }
        }

        private Boolean KiemTraLoiInput()
        {
            if (txtMAMH.Text.Trim() == "")
            {
                MessageBox.Show("Mã môn học không được thiếu!", "", MessageBoxButtons.OK);
                txtMAMH.Focus();
                return false;
            }
            if (txtTENMH.Text.Trim() == "")
            {
                MessageBox.Show("Tên môn học không được thiếu!", "", MessageBoxButtons.OK);
                txtTENMH.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsMonHoc.Position;
            pcMonHoc.Enabled = true;
            bdsMonHoc.AddNew();
            txtMAMH.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcMonHoc.Enabled = false;
            dangthem = true;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsMonHoc.Position;
            pcMonHoc.Enabled = true;
            gcMonHoc.Enabled = false;
            txtMAMH.Enabled = false;

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

            DataRowView mh = (DataRowView)bdsMonHoc[bdsMonHoc.Position];
            string mamonhoc = "";
            string tenmonhoc = "";

            String sql = "";
            int kq = 0;

            if (dangthem)
            {
                sql = "EXEC SP_KT_MONHOC_TONTAI '" + txtMAMH.Text.Trim() + "', N'" + txtTENMH.Text.Trim() + "'";
                kq = Program.ExecSqlNonQuery(sql);
            }
            if (kq == 1)
            {
                txtMAMH.Focus();
                return;
            }
            if (kq == 2)
            {
                txtTENMH.Focus();
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
                                "DELETE DBO.MONHOC " +
                                "WHERE MAMH = '" + txtMAMH.Text.Trim() + "'";
                        }
                        if (dangsua)
                        {
                            mamonhoc = mh["MAMH"].ToString().Trim();
                            tenmonhoc = mh["TENMH"].ToString();

                            CauTruyVanHoanTac =
                                            "UPDATE DBO.MONHOC " +
                                            "SET " +
                                            "TENMH = N'" + tenmonhoc + "'" +
                                            "WHERE MAMH = '" + mamonhoc + "'";
                        }
                        undoList.Push(CauTruyVanHoanTac);
                        dangthem = false;
                        dangsua = false;

                        bdsMonHoc.EndEdit();
                        bdsMonHoc.ResetCurrentItem();
                        this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.MONHOCTableAdapter.Update(this.DS_TN_CSDLPT.MONHOC);

                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                        btnGhi.Enabled = false;
                        pcMonHoc.Enabled = false;
                        gcMonHoc.Enabled = true;
                        txtMAMH.Enabled = true; // bật lại để còn xài thêm
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi môn học! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string mamonhoc = txtMAMH.Text.Trim();
            string tenmonhoc = txtTENMH.Text;
            if (bdsBangDiem.Count > 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì đã có bảng điểm!", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsBODE.Count > 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì đã có bộ đề", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsGVDK.Count > 0)
            {
                MessageBox.Show("Không thể xóa môn học này vì đã có giáo viên đăng ký", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thât sự muốn xóa môn học này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    mamh = ((DataRowView)bdsMonHoc[bdsMonHoc.Position])["MAMH"].ToString();
                    bdsMonHoc.RemoveCurrent();
                    this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.MONHOCTableAdapter.Update(DS_TN_CSDLPT.MONHOC);

                    MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK);
                    string cauTruyVanHoanTac =
                        "INSERT INTO DBO.MONHOC( MAMH,TENMH) " +
                        " VALUES( '" + mamonhoc + "',N'" +
                        tenmonhoc + "' ) ";
                    undoList.Push(cauTruyVanHoanTac);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa môn học. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                    bdsMonHoc.Position = bdsMonHoc.Find("MAMH", mamh);
                    return;
                }
            }
            btnPhucHoi.Enabled = true;
            if (bdsMonHoc.Count == 0) btnXoa.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(dangthem || dangsua)
            {
                bdsMonHoc.CancelEdit();
                if (btnThem.Enabled == false) bdsMonHoc.Position = vitri;

                gcMonHoc.Enabled = true;
                pcMonHoc.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = false;
                if (undoList.Count > 0) btnPhucHoi.Enabled = true;
                else btnPhucHoi.Enabled = false;
                txtMAMH.Enabled = true; // bật lên còn xài 

                dangthem = false;
                dangsua = false;

                if (bdsMonHoc.Count == 0) btnXoa.Enabled = false;
                return;
            }
            String cauTruyVanHoanTac = undoList.Pop().ToString();
            int n = Program.ExceSqlNoneQuery(cauTruyVanHoanTac);
            this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
            bdsMonHoc.Position = vitri;
            if (undoList.Count > 0) btnPhucHoi.Enabled = true;
            else btnPhucHoi.Enabled = false;

        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
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

        
    }
}