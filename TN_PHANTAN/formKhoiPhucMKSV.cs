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
    public partial class formKhoiPhucMKSV : DevExpress.XtraEditors.XtraForm
    {
        public formKhoiPhucMKSV()
        {
            InitializeComponent();
        }

        private void formKhoiPhucMKSV_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.SP_MASV_DA_TAO_TKTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SP_MASV_DA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MASV_DA_TAO_TK);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ten = cmbHoTen.Text;
            string matkhau = "123";
            string a = "UPDATE DBO.TAIKHOANSV " +
                                            "SET " +
                                            "MATKHAU = '" + matkhau + "'," +
                                            "MASV = '" + txtMASV.Text.Trim() + "'" +
                                            "WHERE MATAIKHOAN = '" + txtMASV.Text.Trim() + "'";


            int n = Program.ExceSqlNoneQuery(a);
            this.SP_MASV_DA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MASV_DA_TAO_TK);

            MessageBox.Show("đã khôi phục tài khoản cho " + ten, "", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
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