using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeParser;
using System.Threading;

namespace TN_PHANTAN
{
    public partial class Xrpt_DSDK_SS : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_DSDK_SS()
        {
        }

        public Xrpt_DSDK_SS(string tungay, string denngay)
        {
            InitializeComponent();

            string connectionString = @"Data Source=DESKTOP-6NQMFP3\CLIENT1;Initial Catalog=TN_CSDLPT;Persist Security Info=True;User ID=sa;password=2710";

            //MessageBox.Show();

            // Tạo đối tượng SqlConnection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo đối tượng SqlCommand và thiết lập thuộc tính
                using (SqlCommand command = new SqlCommand("SP_DSDK_THI", connection))
                {
                    // Chỉ định rằng lệnh này là một Stored Procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số vào Stored Procedure
                    command.Parameters.AddWithValue("@tungay", tungay); // Giá trị từ biến tungay
                    command.Parameters.AddWithValue("@denngay", denngay); // Giá trị từ biến denngay

                    // Thực thi Stored Procedure và không có kết quả trả về
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"Số lượng bản ghi bị ảnh hưởng: {rowsAffected}");
                }
                connection.Close();
                //MessageBox.Show("ĐỢI 30S!");
                //Thread.Sleep(30000);
            }
            DataRowView currentRow = (DataRowView)Program.bds_dspm.Current;

            // Lấy giá trị của cột TENCN từ dòng hiện tại
            string currentCN = currentRow["TENCN"].ToString();
            

            //MessageBox.Show(currentCN);

            LbHienTai.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM " + currentCN;
            if(currentCN == "CƠ SỞ 1")
            {
                lbConLai.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM CƠ SỞ 2";
            }
            else
            {
                lbConLai.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM CƠ SỞ 1";

            }

            lbTuNgay1.Text = lbTuNgay2.Text = tungay;
            lbDenNgay1.Text = lbDenNgay2.Text = denngay;

            string strlenh = "SP_COUNT_DSDK";
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
            lbCount.Text = Program.myReader[0].ToString();

            Program.myReader.Close();
            Program.conn.Close();


            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Fill();

            Thread.Sleep(3000);
            


        }
    }
}
