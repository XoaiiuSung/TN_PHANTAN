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
    public partial class formSV_DoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        public formSV_DoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string strlenh = "EXEC SP_THI_KTMK_SV '" + Program.username + "'";
            Program.myReader = Program.ExecSqlDataReader(strlenh);
            if (Program.myReader == null)
            {
                return;
            }
            Program.myReader.Read();

            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập cơ sở dữ liệu\n Bạn xem lại username, password");
                return;
            }
            if(txtMKcu.Text.Trim() != Program.myReader[0].ToString().Trim())
            {
                Program.myReader.Close();
                Program.conn.Close();
                MessageBox.Show("SAI MẬT KHẨU CŨ!", "", MessageBoxButtons.OK);
                txtMKcu.Focus();
                return;
            }
            Program.myReader.Close();
            Program.conn.Close();
            if (txtMKmoi.Text != txtXACNHANmk.Text)
            {
                MessageBox.Show("MẬT KHẨU XÁC NHẬN KHÔNG TRÙNG VỚI MẬT KHẨU MỚI!", "", MessageBoxButtons.OK);
                txtXACNHANmk.Focus();
                return;
            }
            string matkhaumoi = "UPDATE DBO.TAIKHOANSV " +
                                            "SET " +
                                            "MATKHAU = '" + txtMKmoi.Text.Trim() + "' " +
                                            "WHERE MASV = '" + Program.username + "'";
            int n = Program.ExceSqlNoneQuery(matkhaumoi);

            //MessageBox.Show(matkhaumoi, "", MessageBoxButtons.OK);


            MessageBox.Show("ĐỔI MẬT KHẨU THÀNH CÔNG! BẠN SẼ ĐƯỢC ĐƯA VỀ TRANG ĐĂNG NHẬP", "", MessageBoxButtons.OK);

            Program.mlogin = "";
            Program.password = "";

            Program.formSV.Hide();
            Program.formSV.Close();

            Program.frmDangNhap = new formDangNhap();
            this.Hide();
            Program.frmDangNhap.ShowDialog();
            this.Close();
            

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("BẠN CÓ CHẮC MUỐN THOÁT! ", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Program.formSV.rbSV_Main.Enabled = true;
                this.Close();
            }
        }
    }
}