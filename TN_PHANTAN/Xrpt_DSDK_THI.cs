using DevExpress.XtraReports.UI;
using DevExpress.XtraWaitForm;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.DataAccess.Sql;

namespace TN_PHANTAN
{
    public partial class Xrpt_DSDK_THI : DevExpress.XtraReports.UI.XtraReport
    {
        
        public Xrpt_DSDK_THI()
        {
        }

        public Xrpt_DSDK_THI(string tungay, string denngay)
        {
            InitializeComponent();
            string connStr = Program.connstr;
            string spName = "SP_DSDK_THI"; // Tên stored procedure

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(spName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào stored procedure
                    cmd.Parameters.AddWithValue("@tungay", tungay);
                    cmd.Parameters.AddWithValue("@denngay", denngay);

                    // Sử dụng SqlDataAdapter và DataTable để lấy dữ liệu
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán dữ liệu cho SqlDataSource
                    sqlDataSource1 = new SqlDataSource();
                    sqlDataSource1.ConnectionName = "YourConnectionName"; // Tên kết nối đã cấu hình trong designer
                    sqlDataSource1.Name = "sqlDataSource1";
                    sqlDataSource1.Queries.Add(new DevExpress.DataAccess.Sql.CustomSqlQuery("CustomQuery", $"SELECT * FROM [{dt.TableName}]"));
                    sqlDataSource1.Fill();

                    // Gán SqlDataSource cho DataSource của report
                    this.DataSource = sqlDataSource1;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private int countCoSo1 = 0;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            int sttValue = GetCurrentColumnValue<int>("STT");

            if (sttValue == 1 && countCoSo1 == 0)
            {
                xrLabel1.Text = "Cơ sở 1";
                countCoSo1++;
            }
            else if (sttValue == 1 && countCoSo1 > 0)
            {
                xrLabel1.Text = "Cơ sở 2";
            }
        }
    }
}
