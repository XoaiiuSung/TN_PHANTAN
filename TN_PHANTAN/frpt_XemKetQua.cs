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
    public partial class frpt_XemKetQua : DevExpress.XtraEditors.XtraForm
    {
        public frpt_XemKetQua()
        {
            InitializeComponent();
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsMonHoc.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void frpt_XemKetQua_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;

            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
            cmbMon.DataSource = this.DS_TN_CSDLPT.MONHOC;
            cmbMon.DisplayMember = "TENMH";
            cmbMon.ValueMember = "MAMH";

            cmbLan.Items.Add(new Decimal(1.0));
            cmbLan.Items.Add(new Decimal(2.0));

            cmbLan.SelectedIndex = 1; cmbLan.SelectedIndex = 0;
        }

        private void cmbMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMAMH.Text = cmbMon.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            String sql = "";
            int kq = 0;
            sql = "EXEC SP_TT_SV_DATHI '" + Program.username + "','" + txtMAMH.Text.Trim() + "'," + cmbLan.Text.Trim();
            kq = Program.ExecSqlNonQuery(sql);

            if (kq == 1)
            {
                cmbMon.Focus();
                return;
            }

            string monhoc = txtMAMH.Text.Trim();
            string sinhvien = Program.username;
            string lan = cmbLan.Text.Trim();

            Xrpt_XemKetQua rpt = new Xrpt_XemKetQua(monhoc,sinhvien,lan);

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
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