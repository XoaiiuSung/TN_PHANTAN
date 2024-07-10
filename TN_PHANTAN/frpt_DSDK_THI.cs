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
    public partial class frpt_DSDK_THI : DevExpress.XtraEditors.XtraForm
    {
        public frpt_DSDK_THI()
        {
            InitializeComponent();
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ( txtTuNgay.DateTime > txtDenNgay.DateTime)
            {
                MessageBox.Show("ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "", MessageBoxButtons.OK);
                txtTuNgay.Focus();
                return ;
            }

            string tungay = txtTuNgay.DateTime.ToString("dd/MM/yyyy");
            string denngay = txtDenNgay.DateTime.ToString("dd/MM/yyyy");

            Xrpt_DSDK_THI rpt = new Xrpt_DSDK_THI(tungay, denngay);
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
    }
}