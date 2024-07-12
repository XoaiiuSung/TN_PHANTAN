using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
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
    public partial class formThi : DevExpress.XtraEditors.XtraForm
    {
        int thoigianthi = 0;
        private int soCauThi = 0;
        public static Boolean checkThi = false;
        public static CauHoiItem[] listCauHoi;
        public static ListViewItem baiThi;
        private float diem = -1;

        public formThi()
        {
            InitializeComponent();
        }

        private void formThi_Load(object sender, EventArgs e)
        {
            
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.BAITHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.BAITHITableAdapter.Fill(this.DS_TN_CSDLPT.BAITHI);
            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
            cmbMon.DataSource = this.DS_TN_CSDLPT.MONHOC;
            cmbMon.DisplayMember = "TENMH";
            cmbMon.ValueMember = "MAMH";

            this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);

            cmbLan.Items.Add(new Decimal(1.0));
            cmbLan.Items.Add(new Decimal(2.0));

            deNgayThi.EditValue = DateTime.Now.Date;



            string strlenh = "EXEC SP_THI_ThongTinSinhVien '" + Program.username + "'";
            Program.myReader = Program.ExecSqlDataReader(strlenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();

            //Program.username = Program.myReader.GetString(0);
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập cơ sở dữ liệu\n Bạn xem lại username, password");
                return;
            }
            txtMALOP.Text = Program.myReader.GetString(0);
            txtTENLOP.Text = Program.myReader.GetString(1);
            txtHOTENSV.Text = Program.myReader.GetString(2);

            Program.myReader.Close();
            Program.conn.Close();




        }

        private void gIAOVIEN_DANGKYBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsGVDK.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

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

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = "";
            int kq = 0;
            sql = "EXEC SP_THI_SV_DATHI '" + Program.username + "','" + txtMAMH.Text.Trim() + "'," + cmbLan.Text.Trim();
            kq = Program.ExecSqlNonQuery(sql);

            if (kq == 1)
            {
                cmbMon.Focus();
                return;
            }

            cmbMon.Enabled = false;
            cmbLan.Enabled = false;
            deNgayThi.Enabled = false;
            string ngay = deNgayThi.DateTime.ToString("dd/MM/yyyy");

            


            string strlenh = "EXEC SP_THI_ThongTinBaiThi N'" + txtMAMH.Text.Trim() + "', N'" + txtMALOP.Text.Trim() + "', N'" + ngay + "', " + cmbLan.Text.Trim();

            //MessageBox.Show(strlenh);
            Program.myReader = Program.ExecSqlDataReader(strlenh);
            if (Program.myReader == null)
            {
                cmbMon.Enabled = true;
                cmbLan.Enabled = true;
                deNgayThi.Enabled = true;
                return;
            }
            Program.myReader.Read();

            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập cơ sở dữ liệu\n Bạn xem lại username, password");
                return;
            }
            txtSoCauThi.Text = Program.myReader[0].ToString();
            txtTrinhDo.Text = Program.myReader[1].ToString();
            txtThoiGian.Text = Program.myReader[2].ToString();

            Program.myReader.Close();
            Program.conn.Close();

            btnBatDau.Visible = true;
            btnXacNhan.Visible = false;
            btnNhapLai.Visible = true;

        }

        private int s = 1;



        private void timer1_Tick(object sender, EventArgs e)
        {
            s--;
            if (s == 0)
            {
                if (thoigianthi != 0)
                {
                    thoigianthi--;
                    s = 59;
                }
            }
            lbTime.Text = thoigianthi.ToString() + " : " + s.ToString();
            if (thoigianthi == 0 && s == 0)
            {
                timer1.Stop();
                ///cbbTenLop.Enabled = cbbTenMon.Enabled = cbbLanThi.Enabled = true;
                checkThi = false;
                MessageBox.Show("Đã hết thời gian thi!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tinhdiem();
                
                insertdiemsv();
                
                btnNopBai.Enabled = false;
            }
        }

        public void capNhapDaChon(int cauSo, String daChon)
        {
            String[] arr = new string[2];
            arr[0] = (cauSo).ToString();
            arr[1] = daChon;
            ListViewItem baiThi = new ListViewItem(arr);
            summarylistview.Items[cauSo - 1] = baiThi;
        }

        public void loadCauHoi()
        {

            String sql = "";

            sql = "exec SP_BAITHI '"
                + txtMALOP.Text.Trim() + "','"
                + Program.username + "','"
                + txtMAMH.Text.Trim() + "', "
                + cmbLan.Text.Trim();

            //MessageBox.Show(sql);

            DataTable dt = Program.ExecSqlDataTable(sql);

            //MessageBox.Show(dt.Rows.Count.ToString());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không thể lấy được đề thi, thiếu đề", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            // bắt đầu thi khi đã có dữ liệu
            lbTG.Visible = lbTime.Visible = true;
            btnBatDau.Visible = false;
            btnNopBai.Visible = true;
            timer1.Start();
            bdsBaiThi.DataSource = dt;
            listCauHoi = new CauHoiItem[soCauThi];
            checkThi = true;
            for (int i = 0; i < listCauHoi.Length; i++)
            {
                listCauHoi[i] = new CauHoiItem();
                listCauHoi[i].Width = scrollCauHoi.Width;

                listCauHoi[i].CauSo = i + 1;
                listCauHoi[i].IDBaiThi = (int)((DataRowView)bdsBaiThi[i])["CauHoi"];
                Console.WriteLine("id cau hoi: " + listCauHoi[i].IDBaiThi);
                listCauHoi[i].IDDe = (int)((DataRowView)bdsBaiThi[i])["CauSo"];
                listCauHoi[i].NDCauHoi = ((DataRowView)bdsBaiThi[i])["NoiDung"].ToString();
                listCauHoi[i].CauA = ((DataRowView)bdsBaiThi[i])["A"].ToString();
                listCauHoi[i].CauB = ((DataRowView)bdsBaiThi[i])["B"].ToString();
                listCauHoi[i].CauC = ((DataRowView)bdsBaiThi[i])["C"].ToString();
                listCauHoi[i].CauD = ((DataRowView)bdsBaiThi[i])["D"].ToString();
                listCauHoi[i].CauDapAn = ((DataRowView)bdsBaiThi[i])["DapAn"].ToString();
                listCauHoi[i].MaBangDiem = (int)((DataRowView)bdsBaiThi[i])["MaBD"];
                listCauHoi[i].CauDaChon = "";

                String[] arr = new string[2];
                arr[0] = (i + 1).ToString();
                arr[1] = listCauHoi[i].CauDaChon;

                baiThi = new ListViewItem(arr);
                Console.WriteLine("cau: " + (i + 1) + ":" + listCauHoi[i].CauDapAn);
                this.summarylistview.Items.Add(baiThi);


                if (scrollCauHoi.Controls.Count < 0)
                {
                    scrollCauHoi.Controls.Clear();
                }
                else
                    scrollCauHoi.Controls.Add(listCauHoi[i]);
            }
        }

        public void capNhatDaChon(int cauSo, String daChon)
        {
            String[] arr = new string[2];
            arr[0] = (cauSo).ToString();
            arr[1] = daChon;
            ListViewItem baiThi = new ListViewItem(arr);
            summarylistview.Items[cauSo - 1] = baiThi;
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            btnNhapLai.Visible = btnXacNhan.Visible = false;
            summarylistview.Visible = true;

            thoigianthi = int.Parse(txtThoiGian.Text);
            soCauThi = int.Parse(txtSoCauThi.Text);

            loadCauHoi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (checkThi == true)
            {
                if (MessageBox.Show("Bạn chưa nộp bài thi, nhấn OK để nộp bài", "Thông báo", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    btnNopBai_Click(sender, e);
                    if (checkThi == false)
                    {
                        Program.formSV.rbSV_Main.Enabled = true;
                        this.Close();

                    }
                    else
                        return;
                }
            }
            else
            {
                Program.formSV.rbSV_Main.Enabled = true;

                this.Close();
            }
        }

        private void tinhdiem()
        {
            int caudung = 0;
            for (int i = 0; i < listCauHoi.Length; i++)
            {
                if (listCauHoi[i].CauDaChon.Trim().CompareTo(listCauHoi[i].CauDapAn.Trim()) == 0)
                    caudung++;

            }

            if (caudung == 0) diem = 0;

            else diem = (float)Math.Round((double)(10 * caudung) / soCauThi, 2);
            MessageBox.Show("Số câu đúng: " + caudung + "/" + soCauThi + "\nĐiểm: " + diem, "Kết Quả", MessageBoxButtons.OK);
            btnNopBai.Visible = false;
            btnBatDau.Visible = true;
        }

        private void btnNopBai_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn nộp bài", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                timer1.Stop();
                checkThi = false;
                tinhdiem();

               
                insertdiemsv();

                

                
                //cbbTenLop.Enabled = cbbLanThi.Enabled = cbbTenMon.Enabled = true;
            }
        }

        private void insertdiemsv()
        {
            String sql = "UPDATE dbo.BANGDIEM SET DIEM = " + diem +
                "WHERE MASV = '" + Program.username
                + "' AND MAMH = '" + txtMAMH.Text.Trim()
                + "' AND LAN = " + cmbLan.Text.Trim();

            try
            {
                int kq = Program.ExecSqlNonQuery(sql);
                Program.conn.Close();
                ghiDapAn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi điểm thi " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }
        private void ghiDapAn()
        {
            string sqlUpdate = "";
            for (int i = 0; i < listCauHoi.Length; i++)
            {
                sqlUpdate += " UPDATE dbo.BAITHI SET DaChon = '"
                   + listCauHoi[i].CauDaChon
                   + "' WHERE CauHoi = " + listCauHoi[i].IDBaiThi + "  ";
                Console.WriteLine("id cau hoi ghi:  " + listCauHoi[i].IDBaiThi + " da chon:" + listCauHoi[i].CauDaChon);
            }

            Console.WriteLine("câu lệnh: " + sqlUpdate);

            try
            {
                int kq = Program.ExecSqlNonQuery(sqlUpdate);
                Program.conn.Close();
                MessageBox.Show("Đã thi xong!", "", MessageBoxButtons.OK);

                btnNopBai.Visible = false;
                btnBatDau.Visible = btnBatDau.Enabled = true;
                summarylistview.Items.Clear();
                scrollCauHoi.Controls.Clear();

                Program.formSV.rbSV_Main.Enabled = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi đáp án thi " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            cmbMon.Enabled = deNgayThi.Enabled = cmbLan.Enabled = true;
            btnXacNhan.Visible = true;
            btnNhapLai.Visible = false;
            btnBatDau.Visible = false;
        }
    }
}