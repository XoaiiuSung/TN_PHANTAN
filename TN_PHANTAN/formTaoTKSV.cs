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
    public partial class formTaoTKSV : DevExpress.XtraEditors.XtraForm
    {
        public formTaoTKSV()
        {
            InitializeComponent();
        }

        private void formTaoTKSV_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.SP_MASV_CHUA_TAO_TKTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SP_MASV_CHUA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MASV_CHUA_TAO_TK);

        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            string ten = cmbHoTen.Text;
            string matkhau = "123";
            string a = "INSERT INTO DBO.TAIKHOANSV( MATAIKHOAN,MATKHAU,MASV) " +
                        " VALUES( '" + txtMASV.Text.Trim() + "','" +
                        matkhau + "','" +
                        txtMASV.Text.Trim() + "' ) ";

            
            int n = Program.ExceSqlNoneQuery(a);
            this.SP_MASV_CHUA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MASV_CHUA_TAO_TK);

            MessageBox.Show("đã tạo tài khoản cho " + ten, "", MessageBoxButtons.OK);

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