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
using System.Data.SqlClient;
using System.Reflection.Emit;


namespace TN_PHANTAN
{
    public partial class formDangNhap : DevExpress.XtraEditors.XtraForm
    {
        private SqlConnection conn_publisher = new SqlConnection();
        private Boolean giaovien = false;
        private Boolean sinhvien = false;
        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.bds_dspm.DataSource = dt;

            cmbCoSo.DataSource = Program.bds_dspm;
            cmbCoSo.DisplayMember = "TENCN";
            cmbCoSo.ValueMember = "TENSERVER";
        }
        public formDangNhap()
        {
            InitializeComponent();
        }

        private int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = Program.connstr_publisher;
                conn_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối về cơ sở dữ liệu gốc.\nBạn xem lại tên Server");
                return 0;
            }
        }
        private void formDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0) return;
            LayDSPM("SELECT * FROM V_DS_PHANMANH");
            cmbCoSo.SelectedIndex = 1; cmbCoSo.SelectedIndex = 0;
            giaovien = true; sinhvien = false;
        }

        /*bị thừa*/
        private void cmbChinhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cmbCoSo.SelectedValue.ToString();
            }
            catch (Exception) { }
        }

        private void bntDangNhap_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Login name và mật mã không được trống", "", MessageBoxButtons.OK);
                return;
            }

            if (giaovien)
            {
                Program.mlogin = txtLogin.Text; Program.password = txtPass.Text;
                if (Program.KetNoi() == 0) return;

                Program.mCoso = cmbCoSo.SelectedIndex;
                Program.mloginDN = Program.mlogin;
                Program.passwordDN = Program.password;
                string strlenh = "EXEC SP_LayThongTinGiaoVien '" + Program.mlogin + "'";

                Program.myReader = Program.ExecSqlDataReader(strlenh);
                if (Program.myReader == null) return;
                Program.myReader.Read();

                Program.username = Program.myReader.GetString(0);
                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login bạn nhập không có quyền truy cập cơ sở dữ liệu\n Bạn xem lại username, password");
                    return;
                }
                Program.mHoten = Program.myReader.GetString(1);
                Program.mGroup = Program.myReader.GetString(2);
                Program.myReader.Close();
                Program.conn.Close();

                Program.formChinh = new formMain();
                Program.formChinh.MAGV.Text = "Mã giáo viên: " + Program.username;
                Program.formChinh.HOTEN.Text = "Họ tên: " + Program.mHoten;
                Program.formChinh.NHOM.Text = "Nhóm: " + Program.mGroup;
                giaovien = sinhvien = false;
                this.Hide();
                Program.formChinh.ShowDialog();
                this.Close();
            }
            if (sinhvien)
            {
                Program.mlogin = "SINHVIENSA"; Program.password = "9999";

                if (Program.KetNoi() == 0) return;

                Program.mCoso = cmbCoSo.SelectedIndex;
                Program.mloginDN = Program.mlogin;
                Program.passwordDN = Program.password;

                String sql = "";
                int kq = 0;
                sql = "EXEC SP_KT_SINHVIEN_DANGNHAP '" + txtLogin.Text.Trim() + "','" + txtPass.Text.Trim() + "'";
                kq = Program.ExecSqlNonQuery(sql);
                if (kq == 1)
                {
                    txtLogin.Focus();
                    return;
                }

                string strlenh = "EXEC SP_LayThongTinSinhVien '" + txtLogin.Text.Trim() + "','" + txtPass.Text.Trim() + "'";
                Program.myReader = Program.ExecSqlDataReader(strlenh);
                if (Program.myReader == null) return;
                Program.myReader.Read();

                Program.username = Program.myReader.GetString(0);
                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login bạn nhập không có quyền truy cập cơ sở dữ liệu\n Bạn xem lại username, password");
                    return;
                }
                Program.mHoten = Program.myReader.GetString(1);
                Program.mGroup = Program.myReader.GetString(2);
                Program.myReader.Close();
                Program.conn.Close();






                
                ////Program.conn.Close();
                Program.formSV = new formSV_Main();
                Program.formSV.MASV.Text = "Mã sinh viên: " + Program.username;
                Program.formSV.HOTEN.Text = "Họ tên: " +Program.mHoten;
                Program.formSV.NHOM.Text = "Nhóm: " + Program.mGroup;

                giaovien = sinhvien = false;
                this.Hide();
                Program.formSV.ShowDialog();
                this.Close();

            }



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Tài Khoản";
            giaovien = true;
            sinhvien = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "MSSV";
            giaovien = false;
            sinhvien = true;
        }
        /*Thêm ở đay mới đúng*/
        private void cmbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.servername = cmbCoSo.SelectedValue.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát chương trình", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
        }
    }
}