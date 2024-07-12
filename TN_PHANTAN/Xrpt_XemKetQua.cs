using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TN_PHANTAN
{
    public partial class Xrpt_XemKetQua : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_XemKetQua()
        {
        }
        public Xrpt_XemKetQua(string monhoc, string sinhvien, string lan)
        {
            InitializeComponent();
            string strlenh = "EXEC SP_TT_KETQUA '" + monhoc + "', '" + sinhvien + "', " + lan;

            //MessageBox.Show(strlenh);
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
            lbLop.Text = Program.myReader[0].ToString();
            lbMon.Text = Program.myReader[1].ToString();
            lbHoTen.Text = Program.myReader[2].ToString();
            lbNgay.Text = Convert.ToDateTime(Program.myReader[3]).ToString("dd/MM/yyyy");
            lbLan.Text = Program.myReader[4].ToString();
            lbDiem.Text = Program.myReader[6].ToString();

            string mabangdiem = Program.myReader[5].ToString();

            string connectionString = Program.connstr;

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_TT_BAITHI", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MABD", mabangdiem); 
                    

                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"Số lượng bản ghi bị ảnh hưởng: {rowsAffected}");
                }
                connection.Close();
            }

            Program.myReader.Close();
            Program.conn.Close();

            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Fill();

        }

    }
}
