using DevExpress.DataAccess.Wizard.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
    public partial class formKhoaLop : DevExpress.XtraEditors.XtraForm
    {
        private string makhoa = "";
        private string malop = "";
        private int vitri = 0;
        private string lastClickedTable = string.Empty;

        Stack undoList = new Stack();
        private Boolean dangthem = false;
        private Boolean dangsua = false;

        public formKhoaLop()
        {
            InitializeComponent();
        }

       


        private void kHOABindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKhoa.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formKhoaLop_Load(object sender, EventArgs e)
        {

            DS_TN_CSDLPT.EnforceConstraints = false;

            this.COSOTableAdapter.Connection.ConnectionString = Program.connstr;
            this.COSOTableAdapter.Fill(this.DS_TN_CSDLPT.COSO);
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
            this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);

            this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);

            this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString= Program.connstr;
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);

            pcLop.Enabled = false;
            pcKhoa.Enabled = false;

            lastClickedTable = "LOP";
            

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
            if (bdsLop.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
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
                this.COSOTableAdapter.Connection.ConnectionString = Program.connstr;
                this.COSOTableAdapter.Fill(this.DS_TN_CSDLPT.COSO);

                this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
                this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);

                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
               
                this.GIAOVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIENTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN);

                this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
                this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.SINHVIENTableAdapter.Fill(this.DS_TN_CSDLPT.SINHVIEN);
            }
        }

        private Boolean KiemTraLoiInputKhoa()
        {
            if (txtTenKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Tên khoa không được thiếu!", "", MessageBoxButtons.OK);
                txtTenKhoa.Focus();
                return false;
            }
            if (txtMaKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Mã khoa không được thiếu!", "", MessageBoxButtons.OK);
                txtMaKhoa.Focus();
                return false;
            }
            return true;
        }

        private Boolean KiemTraLoiInputLop()
        {
            if (txtTenLop.Text.Trim() == "")
            {
                MessageBox.Show("Tên lớp không được thiếu!", "", MessageBoxButtons.OK);
                txtTenLop.Focus();
                return false;
            }
            if (txtMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Mã Lớp không được thiếu!", "", MessageBoxButtons.OK);
                txtMaLop.Focus();
                return false;
            }
            return true;
        }

        private void gcKhoa_Click(object sender, EventArgs e)
        {
            lastClickedTable = "KHOA";
            if (bdsKhoa.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
            else btnXoa.Enabled = true;
        }

        private void gcLop_Click(object sender, EventArgs e)
        {
            lastClickedTable = "LOP";
            if (bdsLop.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
            else btnXoa.Enabled = true;
        }
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(lastClickedTable == "KHOA")
            {
                vitri = bdsKhoa.Position;
                pcKhoa.Enabled = true;
                pcLop.Enabled = false;
                bdsKhoa.AddNew();
                txtTenKhoa.Focus();

                txtMaCS.Text = ((DataRowView)bdsCoSo.Current)["MACS"].ToString();
            }
            if(lastClickedTable == "LOP")
            {
                vitri = bdsLop.Position;                
                pcKhoa.Enabled = false;
                pcLop.Enabled = true;
                bdsLop.AddNew();
                txtTenLop.Focus();

                txtMAKH_LOP.Text = ((DataRowView)bdsKhoa.Current)["MAKH"].ToString();
            }

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcKhoa.Enabled = false;
            gcLop.Enabled = false;
            dangthem = true;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(lastClickedTable == "KHOA")
            {
                vitri = bdsKhoa.Position;
                pcKhoa.Enabled = true;
                gcKhoa.Enabled = false;
                gcLop.Enabled = false;
                txtMaKhoa.Enabled=false;
            }
            if (lastClickedTable == "LOP")
            {
                vitri = bdsLop.Position;
                pcLop.Enabled = true;
                gcKhoa.Enabled = false;
                gcLop.Enabled = false;
                txtMaLop.Enabled = false;
            }

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            dangsua = true;

        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lastClickedTable == "KHOA")
            {
                if (!KiemTraLoiInputKhoa()) return;
                DataRowView kh = (DataRowView)bdsKhoa[bdsKhoa.Position];
                string makh = "";
                string tenkh = "";
                String macs = "";

                String sql = "";
                int kq = 0;
                if (dangthem)
                {
                    sql = "EXEC SP_KT_KHOA_TONTAI '" + txtMaKhoa.Text.Trim() + "', N'" + txtTenKhoa.Text.Trim() + "'";
                    kq = Program.ExecSqlNonQuery(sql);
                }
                if (kq == 1)
                {
                    txtTenKhoa.Focus();
                    return;
                }
                if (kq == 2)
                {
                    txtMaKhoa.Focus();
                    return;
                }
                if(dangthem || dangsua)
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
                                    "DELETE DBO.KHOA " +
                                    "WHERE MAKH = '" + txtMaKhoa.Text.Trim() + "'";
                            }
                            if (dangsua)
                            {
                                makh = kh["MAKH"].ToString().Trim();
                                tenkh = kh["TENKH"].ToString();
                                macs = kh["MACS"].ToString().Trim();

                                CauTruyVanHoanTac =
                                                "UPDATE DBO.KHOA " +
                                                "SET " +
                                                "TENKH = N'" + tenkh + "'," +
                                                "MACS = '" + macs + "'" +
                                                "WHERE MAKH = '" + makh + "'";

                            }
                            undoList.Push(CauTruyVanHoanTac);
                            dangthem = false;
                            dangsua = false;
                            bdsKhoa.EndEdit();
                            bdsKhoa.ResetCurrentItem();
                            this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
                            this.KHOATableAdapter.Update(this.DS_TN_CSDLPT.KHOA);
                            txtMaKhoa.Enabled = true;

                            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                            btnGhi.Enabled  = false;

                            pcKhoa.Enabled = false;
                            pcLop.Enabled = false;

                            gcKhoa.Enabled = true;
                            gcLop.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi ghi khoa! \n" + ex.Message, "", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            if(lastClickedTable == "LOP")
            {
                if (!KiemTraLoiInputLop()) return;
                DataRowView lop = (DataRowView)bdsLop[bdsLop.Position];
                string mal = "";
                string tenl = "";
                String makhl = "";

                String sql = "";
                int kq = 0;
                if (dangthem)
                {
                    sql = "EXEC SP_KT_LOP_TONTAI '" + txtMaLop.Text.Trim() + "', N'" + txtTenLop.Text.Trim() + "'";
                    kq = Program.ExecSqlNonQuery(sql);
                }
                if (kq == 1)
                {
                    txtTenLop.Focus();
                    return;
                }
                if (kq == 2)
                {
                    txtMaLop.Focus();
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
                                    "DELETE DBO.LOP " +
                                    "WHERE MALOP = '" + txtMaLop.Text.Trim() + "'";
                            }
                            if (dangsua)
                            {
                                mal = lop["MALOP"].ToString().Trim();
                                tenl = lop["TENLOP"].ToString();
                                makhl = lop["MAKH"].ToString().Trim();

                                CauTruyVanHoanTac =
                                                "UPDATE DBO.LOP " +
                                                "SET " +
                                                "TENLOP = N'" + tenl + "'," +
                                                "MAKH = '" + makhl + "'" +
                                                "WHERE MALOP = '" + mal + "'";

                            }
                            undoList.Push(CauTruyVanHoanTac);
                            dangthem = false;
                            dangsua = false;
                            bdsLop.EndEdit();
                            bdsLop.ResetCurrentItem();
                            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                            this.LOPTableAdapter.Update(this.DS_TN_CSDLPT.LOP);
                            txtMaLop.Enabled = true;

                            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                            btnGhi.Enabled  = false;

                            pcKhoa.Enabled = false;
                            pcLop.Enabled = false;

                            gcKhoa.Enabled = true;
                            gcLop.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi ghi lớp! \n" + ex.Message, "", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(lastClickedTable == "KHOA")
            {
                string makhoaa = txtMaKhoa.Text.Trim();
                string tenkhoa = txtTenKhoa.Text;
                string macoso = txtMaCS.Text.Trim();
                if (bdsLop.Count > 0)
                {
                    MessageBox.Show("Không thể xóa khoa này vì đã có lớp!", "", MessageBoxButtons.OK);
                    return;
                }
                if (bdsGiaoVien.Count > 0)
                {
                    MessageBox.Show("Không thể xóa khoa này vì đã có giáo viên", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thât sự muốn xóa khoa này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        makhoa = ((DataRowView)bdsKhoa[bdsKhoa.Position])["MAKH"].ToString();
                        bdsKhoa.RemoveCurrent();
                        this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
                        this.KHOATableAdapter.Update(DS_TN_CSDLPT.KHOA);

                        MessageBox.Show("Xóa khoa thành công!", "Thông báo", MessageBoxButtons.OK);
                        string cauTruyVanHoanTac =
                            "INSERT INTO DBO.KHOA( MAKH,TENKH,MACS) " +
                            " VALUES( '" + makhoaa + "',N'" +
                            tenkhoa + "', '" +
                            macoso + "' ) ";
                        undoList.Push(cauTruyVanHoanTac);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa khoa. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                        this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);
                        bdsKhoa.Position = bdsKhoa.Find("MAKH", makhoa);
                        return;
                    }
                }
                if (bdsKhoa.Count == 0) btnXoa.Enabled = false;
            }
            if (lastClickedTable == "LOP")
            {
                string mal = txtMaLop.Text.Trim();
                string tenl = txtTenLop.Text;
                string makhoal = txtMAKH_LOP.Text.Trim();
                if (bdsSinhVien.Count > 0)
                {
                    MessageBox.Show("Không thể xóa lớp này vì đã có sinh viên!", "", MessageBoxButtons.OK);
                    return;
                }
                if (bds_GVDK.Count > 0)
                {
                    MessageBox.Show("Không thể xóa lớp này vì đã có giáo viên đăng ký", "", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("Bạn có thât sự muốn xóa lớp này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        malop = ((DataRowView)bdsLop[bdsLop.Position])["MALOP"].ToString();
                        bdsLop.RemoveCurrent();
                        this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.LOPTableAdapter.Update(DS_TN_CSDLPT.LOP);

                        MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK);
                        string cauTruyVanHoanTac =
                            "INSERT INTO DBO.LOP( MALOP,TENLOP,MAKH) " +
                            " VALUES( '" + mal + "',N'" +
                            tenl + "', '" +
                            makhoal + "' ) ";
                        undoList.Push(cauTruyVanHoanTac);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa lớp. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                        this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
                        bdsKhoa.Position = bdsKhoa.Find("MALOP", malop);
                        return;
                    }
                }
                btnPhucHoi.Enabled = true;
                if (bdsLop.Count == 0) btnXoa.Enabled = false;
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(dangthem || dangsua)
            {
                if (lastClickedTable == "KHOA")
                {
                    bdsKhoa.CancelEdit();
                    if (btnThem.Enabled == false) bdsKhoa.Position = vitri;
                    if (bdsKhoa.Count == 0) btnXoa.Enabled = false;
                    bdsKhoa.Position = vitri;
                    txtMaKhoa.Enabled = true; // bật lên còn xài 


                }
                if (lastClickedTable == "LOP")
                {
                    bdsLop.CancelEdit();
                    if (btnThem.Enabled == false) bdsLop.Position = vitri;
                    if (bdsLop.Count == 0) btnXoa.Enabled = false;
                    bdsLop.Position = vitri;
                    txtMaLop.Enabled = true; // bật lên còn xài 

                }
                gcKhoa.Enabled = true;
                gcLop.Enabled = true;

                pcKhoa.Enabled = false;
                pcLop.Enabled = false;


                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = false;
                if (undoList.Count > 0) btnPhucHoi.Enabled = true;
                else btnPhucHoi.Enabled = false;
                dangthem = false;
                dangsua = false;

                return;
            }

            String cauTruyVanHoanTac = undoList.Pop().ToString();
            int n = Program.ExceSqlNoneQuery(cauTruyVanHoanTac);
            this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);

            if (undoList.Count > 0) btnPhucHoi.Enabled = true;
            else btnPhucHoi.Enabled = false;
        }
        
        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);
                this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
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



    }
}