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
    public partial class formMonHoc : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private string mamh = "";
        private Boolean checkThem = false;
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

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsMonHoc.Position;
            pcMonHoc.Enabled = true;
            bdsMonHoc.AddNew();
            txtMAMH.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcMonHoc.Enabled = false;
            checkThem = true;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsMonHoc.Position;
            pcMonHoc.Enabled = true;
            gcMonHoc.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if( checkThem == true)
            {
                if (txtMAMH.Text.Trim() == "")
                {
                    MessageBox.Show("Mã môn học không được thiếu!", "", MessageBoxButtons.OK);
                    txtMAMH.Focus();
                    return;
                }
                if (txtTENMH.Text.Trim() == "")
                {
                    MessageBox.Show("Tên môn học không được thiếu!", "", MessageBoxButtons.OK);
                    txtTENMH.Focus();
                    return;
                }
                String sql = "EXEC SP_KT_MONHOC_TONTAI '" + txtMAMH.Text.Trim() + "', N'" + txtTENMH.Text.Trim() + "'";

                int kq = Program.ExecSqlNonQuery(sql);
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
                else
                {
                    try
                    {

                        bdsMonHoc.EndEdit();
                        bdsMonHoc.ResetCurrentItem();
                        this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.MONHOCTableAdapter.Update(this.DS_TN_CSDLPT.MONHOC);
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
                if (txtMAMH.Text.Trim() == "")
                {
                    MessageBox.Show("Mã môn học không được thiếu!", "", MessageBoxButtons.OK);
                    txtMAMH.Focus();
                    return;
                }
                if (txtTENMH.Text.Trim() == "")
                {
                    MessageBox.Show("Tên môn học không được thiếu!", "", MessageBoxButtons.OK);
                    txtTENMH.Focus();
                    return;
                }
                else
                {
                    try
                    {

                        bdsMonHoc.EndEdit();
                        bdsMonHoc.ResetCurrentItem();
                        this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.MONHOCTableAdapter.Update(this.DS_TN_CSDLPT.MONHOC);
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
            pcMonHoc.Enabled = false;
            gcMonHoc.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khoa. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                    bdsMonHoc.Position = bdsMonHoc.Find("MAMH", mamh);
                    return;
                }
            }
            if (bdsMonHoc.Count == 0) btnXoa.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsMonHoc.CancelEdit();
            if (btnThem.Enabled == false) bdsMonHoc.Position = vitri;

            gcMonHoc.Enabled = true;
            pcMonHoc.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            checkThem = false;
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