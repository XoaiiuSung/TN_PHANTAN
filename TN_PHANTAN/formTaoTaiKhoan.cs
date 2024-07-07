using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TN_PHANTAN
{
    public partial class formTaoTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public formTaoTaiKhoan()
        {
            InitializeComponent();
        }

        private void formTaoTaiKhoan_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MA_GV_CHUA_TAO_TK);
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
            //Phân quyền
            if (Program.mGroup == "COSO")
            {
                cmbCOSO.Enabled = false;
                cmbLoaiTK.Items.Add("COSO");
                cmbLoaiTK.Items.Add("GIANGVIEN");
            }
            else if (Program.mGroup == "TRUONG")
            {
                cmbCOSO.Enabled = true;
                cmbLoaiTK.Items.Add("TRUONG");
            }
            cmbLoaiTK.SelectedIndex = 0;
        }

        private void cmbCoSo_SelectedIndexChanged(object sender, EventArgs e)
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
                this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Connection.ConnectionString = Program.connstr;
                this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MA_GV_CHUA_TAO_TK);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text.Trim().Equals(""))
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập", "", MessageBoxButtons.OK);
                txtTenDangNhap.Focus();
                return;
            }
            if (txtMatKhau.Text.Trim().Equals(""))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "", MessageBoxButtons.OK);
                txtMatKhau.Focus();
                return;
            }
            SqlCommand sqlcmd;
            if (Program.mGroup.Equals("Coso"))
            {
                if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                sqlcmd = Program.conn.CreateCommand();
            }
            else
            {
                if (cmbCOSO.SelectedIndex != Program.mCoso)
                {
                    Program.servername = cmbCOSO.SelectedValue.ToString();

                    if (Program.KetNoi() == 0) return;
                    else
                    {
                        this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MA_GV_CHUA_TAO_TK);
                        sqlcmd = Program.conn.CreateCommand();
                    }
                }
                else
                {
                    if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                    sqlcmd = Program.conn.CreateCommand();
                }
            }


            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "sp_TaoTaiKhoan";
            sqlcmd.Parameters.Add(new SqlParameter("@LGNAME", txtTenDangNhap.Text.Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("@PASS", txtMatKhau.Text.Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("@USERNAME", cmbMAGV.SelectedValue.ToString().Trim()));
            sqlcmd.Parameters.Add(new SqlParameter("@ROLE", cmbLoaiTK.SelectedItem.ToString().Trim()));
            SqlParameter sqlParameter = new SqlParameter("@return", System.Data.SqlDbType.Int, sizeof(int));
            sqlParameter.Direction = System.Data.ParameterDirection.ReturnValue;
            sqlcmd.Parameters.Add(sqlParameter);
            int result = -1;
            try
            {
                sqlcmd.ExecuteNonQuery();
                result = (int)sqlcmd.Parameters["@return"].Value;
            }
            catch (Exception ex)
            {
                Program.conn.Close();
                MessageBox.Show("Lỗi tạo tài khoản " + ex.Message, "Lỗi", MessageBoxButtons.OK);
                return;
            }
            if (result != -1)
            {
                if (result == 1)
                {
                    Program.conn.Close();
                    MessageBox.Show("Tên đăng nhập đã tồn tại", "Lỗi", MessageBoxButtons.OK);
                    txtTenDangNhap.Focus();
                    return;
                }
                if (result == 2)
                {
                    Program.conn.Close();
                    MessageBox.Show("Mã giảng viên đã tồn tại", "Lỗi", MessageBoxButtons.OK);
                    cmbMAGV.Focus();
                    return;
                }
                //tao tk thành công
                if (result == 0)
                {
                    Program.conn.Close();
                    MessageBox.Show("Tạo tài khoản thành công", "Thành công", MessageBoxButtons.OK);
                    txtMatKhau.Text = "";
                    txtTenDangNhap.Text = "";
                    this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.SP_MA_GV_CHUA_TAO_TKTableAdapter.Fill(this.DS_TN_CSDLPT.SP_MA_GV_CHUA_TAO_TK);
                    return;
                }
                else
                {
                    Program.conn.Close();
                    MessageBox.Show("Tạo tài khoản thất bại", "Lỗi", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Tạo tài khoản thất bại", "Lỗi", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát tạo tài khoản", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
    }
}