using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class frpt_BangDiemMonHoc : DevExpress.XtraEditors.XtraForm
    {
        string malop = "";
        string mamonhoc = "";
        public frpt_BangDiemMonHoc()
        {
            InitializeComponent();
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
                cmbTenMH.DataSource = this.DS_TN_CSDLPT.MONHOC;
                cmbTenMH.DisplayMember = "TENMH";
                cmbTenMH.ValueMember = "MAMH";

                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
                cmbLop.DataSource = this.DS_TN_CSDLPT.LOP;
                cmbLop.DisplayMember = "TENLOP";
                cmbLop.ValueMember = "MALOP";

                cmbLop.SelectedIndex = 0;
                malop = cmbLop.SelectedValue.ToString();

                cmbTenMH.SelectedIndex = 0; // Chọn phần tử đầu tiên
                mamonhoc = cmbTenMH.SelectedValue.ToString();
            }
            
               
            
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLop.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void frpt_BangDiemMonHoc_Load(object sender, EventArgs e)
        {
           

            DS_TN_CSDLPT.EnforceConstraints = false;

            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
            cmbTenMH.DataSource = this.DS_TN_CSDLPT.MONHOC;
            cmbTenMH.DisplayMember = "TENMH";
            cmbTenMH.ValueMember = "MAMH";

            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
            cmbLop.DataSource = this.DS_TN_CSDLPT.LOP;
            cmbLop.DisplayMember = "TENLOP";
            cmbLop.ValueMember = "MALOP";

            cmbCOSO.DataSource = Program.bds_dspm;
            cmbCOSO.DisplayMember = "TENCN";
            cmbCOSO.ValueMember = "TENSERVER";
            cmbCOSO.SelectedIndex = Program.mCoso;

            cmbLop.SelectedIndex = 0;
            malop = cmbLop.SelectedValue.ToString();

            cmbLanThi.SelectedIndex = 1; cmbLanThi.SelectedIndex = 0;

            cmbTenMH.SelectedIndex = 0; // Chọn phần tử đầu tiên
            mamonhoc = cmbTenMH.SelectedValue.ToString();


            if (Program.mGroup == "TRUONG")
            {
                cmbCOSO.Enabled = true;
            }
            else
            {
                cmbCOSO.Enabled = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Xrpt_BangDiemMonHoc rpt = new Xrpt_BangDiemMonHoc(malop, mamonhoc, int.Parse(cmbLanThi.Text));
            rpt.lbTieuDe.Text = "BẢNG ĐIỂM MÔN HỌC " + cmbTenMH.Text.ToUpper() + " LẦN THI " + cmbLanThi.Text;
            rpt.lbLop.Text = cmbLop.Text.ToUpper();

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void tENLOPComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                malop = cmbLop.SelectedValue.ToString();
            }
            catch (Exception ex) {
            }
        }

        private void tENMHComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                mamonhoc = cmbTenMH.SelectedValue.ToString();
            }
            catch (Exception ex) { }
        }

        private void btnThoat_Click(object sender, EventArgs e)
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