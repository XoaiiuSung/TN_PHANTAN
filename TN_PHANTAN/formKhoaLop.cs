using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
    public partial class formKhoaLop : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        private string lastClickedTable = string.Empty;

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

                Program.MACS_now = ((DataRowView)bdsCoSo.Current)["MACS"].ToString();
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

                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);

                this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
                this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);

                Program.MACS_now = ((DataRowView)bdsCoSo.Current)["MACS"].ToString();
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lastClickedTable == "KHOA")
            {
                bdsKhoa.CancelEdit();
                if (btnThem.Enabled == false) bdsKhoa.Position = vitri;

                gcKhoa.Enabled = true;
                gcLop.Enabled = true;

                pcKhoa.Enabled = false;
                pcLop.Enabled = false;


                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
            }
            if (lastClickedTable == "LOP")
            {
                bdsLop.CancelEdit();
                if (btnThem.Enabled == false) bdsLop.Position = vitri;

                gcKhoa.Enabled = true;
                gcLop.Enabled = true;

                pcKhoa.Enabled = false;
                pcLop.Enabled = false;


                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(lastClickedTable == "KHOA")
            {
                vitri = bdsKhoa.Position;
                
                txtTenKhoa.Focus();

                pcKhoa.Enabled = true;
                pcLop.Enabled = false;
                bdsKhoa.AddNew();

                txtMaCS.Text = Program.MACS_now;
            }
            if(lastClickedTable == "LOP")
            {
                
                txtTenLop.Focus();

                pcKhoa.Enabled = false;
                pcLop.Enabled = true;
                bdsLop.AddNew();

                txtMAKH_LOP.Text = ((DataRowView)bdsKhoa.Current)["MAKH"].ToString();
            }

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcKhoa.Enabled = false;
            gcLop.Enabled = false;
        }

        private void gcKhoa_Click(object sender, EventArgs e)
        {
            lastClickedTable = "KHOA";
        }

        private void gcLop_Click(object sender, EventArgs e)
        {
            lastClickedTable = "LOP";
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(lastClickedTable == "KHOA")
            {
                try
                {

                    bdsKhoa.EndEdit();
                    bdsKhoa.ResetCurrentItem();
                    this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
                    //DS_TN_CSDLPT.KHOA.AcceptChanges();
                    this.KHOATableAdapter.Update(this.DS_TN_CSDLPT.KHOA);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi giáo viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            if(lastClickedTable == "LOP")
            {
                try
                {

                    bdsLop.EndEdit();
                    bdsLop.ResetCurrentItem();
                    this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTableAdapter.Update(this.DS_TN_CSDLPT.LOP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi giáo viên! \n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            
            
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }
    }
}