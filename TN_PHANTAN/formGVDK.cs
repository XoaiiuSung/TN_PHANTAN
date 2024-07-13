using DevExpress.CodeParser;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TN_PHANTAN
{
    public partial class formGVDK : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        //private string magv = "";

        private Stack undoList = new Stack();
        private Boolean dangthem = false;
        private Boolean dangsua = false;

        string magv = "";
        string mamh = "";
        string malop = "";
        string trinhdo = "";
        string ngaythi = "";
        string lan = "";
        string socauthi = "";
        string thoigian = "";
        public formGVDK()
        {
            InitializeComponent();
        }

        private void gIAOVIEN_DANGKYBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bds_GVDK.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formGVDK_Load(object sender, EventArgs e)
        {
            
            DS_TN_CSDLPT.EnforceConstraints = false;

            this.TBTRINHDOTableAdapter.Connection.ConnectionString = Program.connstr;
            this.TBTRINHDOTableAdapter.Fill(this.DS_TN_CSDLPT.TBTRINHDO);
            cmbTrinhDo.DataSource = this.DS_TN_CSDLPT.TBTRINHDO;
            cmbTrinhDo.DisplayMember = "TENTRINHDO";
            cmbTrinhDo.ValueMember = "TRINHDO";

            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
            cmbTenLop.DataSource = this.DS_TN_CSDLPT.LOP;
            cmbTenLop.DisplayMember = "TENLOP";
            cmbTenLop.ValueMember = "MALOP";

            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
            cmbTenMH.DataSource = this.DS_TN_CSDLPT.MONHOC;
            cmbTenMH.DisplayMember = "TENMH";
            cmbTenMH.ValueMember = "MAMH";

            this.GV_HOTENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GV_HOTENTableAdapter.Fill(this.DS_TN_CSDLPT.GV_HOTEN);
            cmbTenGV.DataSource = this.DS_TN_CSDLPT.GV_HOTEN;
            cmbTenGV.DisplayMember = "TEN";
            cmbTenGV.ValueMember = "MAGV";

            this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);


            pcGVDK.Enabled = false;
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
            if (Program.mGroup == "TRUONG")
            {
                cmbCOSO.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                cmbCOSO.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = true;
            }
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            if (bds_GVDK.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }

            

            cmbLanThi.Items.Add(new Decimal(1.0));
            cmbLanThi.Items.Add(new Decimal(2.0));

            if(bds_GVDK.Count > 0) cmbLanThi.Text = ((DataRowView)this.bds_GVDK.Current).Row["LAN"].ToString();

            //txtThoiGian.Text = ((DataRowView)this.bds_GVDK.Current).Row["THOIGIAN"].ToString();

        }

        private void cmbCOSO_SelectedIndexChanged(object sender, EventArgs e)
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
                this.TBTRINHDOTableAdapter.Connection.ConnectionString = Program.connstr;
                this.TBTRINHDOTableAdapter.Fill(this.DS_TN_CSDLPT.TBTRINHDO);
                cmbTrinhDo.DataSource = this.DS_TN_CSDLPT.TBTRINHDO;
                cmbTrinhDo.DisplayMember = "TENTRINHDO";
                cmbTrinhDo.ValueMember = "TRINHDO";

                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
                cmbTenLop.DataSource = this.DS_TN_CSDLPT.LOP;
                cmbTenLop.DisplayMember = "TENLOP";
                cmbTenLop.ValueMember = "MALOP";

                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                cmbTenMH.DataSource = this.DS_TN_CSDLPT.MONHOC;
                cmbTenMH.DisplayMember = "TENMH";
                cmbTenMH.ValueMember = "MAMH";

                this.GV_HOTENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GV_HOTENTableAdapter.Fill(this.DS_TN_CSDLPT.GV_HOTEN);
                cmbTenGV.DataSource = this.DS_TN_CSDLPT.GV_HOTEN;
                cmbTenGV.DisplayMember = "TEN";
                cmbTenGV.ValueMember = "MAGV";

                this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            }
        }

        private Boolean KiemTraLoiInput()
        {
            if (txtNgayThi.DateTime < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày thi phải lớn hơn hoặc bằng ngày hiện tại!", "", MessageBoxButtons.OK);
                txtNgayThi.Focus();
                return false;
            }
            if(int.Parse(txtSoCauThi.Text) < 10 || int.Parse(txtSoCauThi.Text) > 100)
            {
                MessageBox.Show("Số câu thi phải nằm trong khoảng 10-100!", "", MessageBoxButtons.OK);
                txtSoCauThi.Focus();
                return false;
            }
            if(int.Parse(txtThoiGian.Text) < 2 || int.Parse(txtThoiGian.Text) > 60)
            {
                MessageBox.Show("Thời gian thi phải nằm trong khoảng 2-60 phút!", "", MessageBoxButtons.OK);
                txtThoiGian.Focus();
                return false;
            }
            String mamh = cmbTenMH.SelectedValue.ToString();
            String ktsocau = "select count (CAUHOI) from BODE where MAMH = '" + mamh + "'";
            if (Program.myReader.IsClosed == false)
            {
                Program.myReader.Close();
            }
            Program.myReader = Program.ExecSqlDataReader(ktsocau);
            if (Program.myReader == null)
            {
                return false;
            }
            Program.myReader.Read();
            if (Program.myReader.GetInt32(0) < int.Parse(txtSoCauThi.Text))
            {
                MessageBox.Show("Không đủ câu hỏi trong bộ đề, chỉ còn "
                        + Program.myReader.GetInt32(0) + " câu của môn học này");
                txtSoCauThi.Focus();
                Program.myReader.Close();
                return false;
            }
            Program.myReader.Close();

            return true;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bds_GVDK.Position;
            pcGVDK.Enabled = true;
            bds_GVDK.AddNew();
            cmbTenGV.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcGVDK.Enabled = false;
            dangthem = true;


            cmbTenGV.SelectedIndex = 1; cmbTenGV.SelectedIndex = 0;
            cmbTenMH.SelectedIndex = 1; cmbTenMH.SelectedIndex = 0;
            cmbTenLop.SelectedIndex = 1; cmbTenLop.SelectedIndex = 0;
            cmbTrinhDo.SelectedIndex = 1;cmbTrinhDo.SelectedIndex = 0;
            cmbLanThi.SelectedIndex = 1; cmbLanThi.SelectedIndex = 0;

            txtNgayThi.EditValue = DateTime.Now.Date;

        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangsua = true;
            vitri = bds_GVDK.Position;
            pcGVDK.Enabled = true;
            gcGVDK.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            cmbTenMH.Enabled = false;
            cmbTenLop.Enabled = false;
            cmbLanThi.Enabled = false;

            DataRowView gvdk = (DataRowView)bds_GVDK[bds_GVDK.Position];
            magv =      gvdk["MAGV"].ToString().Trim();
            mamh =      gvdk["MAMH"].ToString().Trim();
            malop =     gvdk["MALOP"].ToString().Trim();
            trinhdo =   gvdk["TRINHDO"].ToString().Trim();
            ngaythi =   (Convert.ToDateTime(gvdk["NGAYTHI"])).ToString("yyyy-MM-dd");
            lan =       gvdk["LAN"].ToString();
            socauthi =  gvdk["SOCAUTHI"].ToString();
            thoigian =  gvdk["THOIGIAN"].ToString();
        }
        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!KiemTraLoiInput())
            {
                this.TBTRINHDOTableAdapter.Connection.ConnectionString = Program.connstr;
                this.TBTRINHDOTableAdapter.Fill(this.DS_TN_CSDLPT.TBTRINHDO);
                cmbTrinhDo.DataSource = this.DS_TN_CSDLPT.TBTRINHDO;
                cmbTrinhDo.DisplayMember = "TENTRINHDO";
                cmbTrinhDo.ValueMember = "TRINHDO";

                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.DS_TN_CSDLPT.LOP);
                cmbTenLop.DataSource = this.DS_TN_CSDLPT.LOP;
                cmbTenLop.DisplayMember = "TENLOP";
                cmbTenLop.ValueMember = "MALOP";

                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                cmbTenMH.DataSource = this.DS_TN_CSDLPT.MONHOC;
                cmbTenMH.DisplayMember = "TENMH";
                cmbTenMH.ValueMember = "MAMH";

                this.GV_HOTENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GV_HOTENTableAdapter.Fill(this.DS_TN_CSDLPT.GV_HOTEN);
                cmbTenGV.DataSource = this.DS_TN_CSDLPT.GV_HOTEN;
                cmbTenGV.DisplayMember = "TEN";
                cmbTenGV.ValueMember = "MAGV";

                cmbTenGV.SelectedIndex = 1; cmbTenGV.SelectedIndex = 0;
                cmbTenMH.SelectedIndex = 1; cmbTenMH.SelectedIndex = 0;
                cmbTenLop.SelectedIndex = 1; cmbTenLop.SelectedIndex = 0;
                cmbTrinhDo.SelectedIndex = 1; cmbTrinhDo.SelectedIndex = 0;
                cmbLanThi.SelectedIndex = 1; cmbLanThi.SelectedIndex = 0;

                txtNgayThi.EditValue = DateTime.Now.Date;
                return;
            }
            
            String sql = "";
            int kq = 0;
            if (dangthem)
            {
                //Program.myReader.Close();
                //MessageBox.Show("đang kiểm lỗi db!" + txtMALOP.Text.Trim() + txtMAMH.Text.Trim() + cmbLanThi.SelectedItem.ToString(), "Thông báo", MessageBoxButtons.OK);


                sql = "EXEC SP_KT_GVDK '" + txtMAMH.Text.Trim()
                + "', '" + txtMALOP.Text.Trim()
                + "',  " + cmbLanThi.SelectedItem.ToString().Trim();
                kq = Program.ExecSqlNonQuery(sql);

               // MessageBox.Show("đang kiểm lỗi db!" + sql, "Thông báo", MessageBoxButtons.OK);

            }
            if (kq == 1)
            {
                cmbTenMH.Focus();
                return;
            }

            if (dangthem || dangsua)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn GHI dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    string CauTruyVanHoanTac = "";
                    try
                    {
                        if (dangthem)
                        {
                            CauTruyVanHoanTac = "" +
                                "DELETE DBO.GIAOVIEN_DANGKY " +
                                "WHERE MAMH = '" + txtMAMH.Text.Trim() + "' AND " +
                                "MALOP = '" + txtMALOP.Text.Trim() + "' AND " +
                                "LAN = '" + cmbLanThi.SelectedItem.ToString() + "'";
                        }
                        if (dangsua)
                        {
                            CauTruyVanHoanTac =
                                            "UPDATE DBO.GIAOVIEN_DANGKY " +
                                            "SET " +
                                            "MAGV = '" + magv + "', " +
                                            "TRINHDO = '" + trinhdo + "', " +
                                            "NGAYTHI = '" + ngaythi + "', " +
                                            "SOCAUTHI = '" + socauthi + "', " +
                                            "THOIGIAN = '" + thoigian + "' " +
                                            "WHERE MAMH = '" + mamh + "' AND " +
                                                  "MALOP = '" + malop + "' AND " +
                                                  "LAN = '" + lan + "'";

                        }
                        undoList.Push(CauTruyVanHoanTac);
                        dangthem = false;
                        dangsua = false;

                        bds_GVDK.EndEdit();
                        bds_GVDK.ResetCurrentItem();
                        this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.GIAOVIEN_DANGKYTableAdapter.Update(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);

                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                        btnGhi.Enabled = false;
                        pcGVDK.Enabled = false;
                        gcGVDK.Enabled = true;

                        cmbTenMH.Enabled = true;
                        cmbTenLop.Enabled = true;
                        cmbLanThi.Enabled = true;

                        magv =     "";
                        mamh =     "";
                        malop =    "";
                        trinhdo =  "";
                        ngaythi =  "";
                        lan =      "";
                        socauthi = "";
                        thoigian = "";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("cautv:" + trinhdo, "", MessageBoxButtons.OK);

                        MessageBox.Show("Lỗi ghi giáo viên đăng ký! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            magv = txtMAGV.Text.Trim();
            mamh = txtMAMH.Text.Trim();
            malop = txtMALOP.Text.Trim();  
            trinhdo = txtTrinhDo.Text.Trim();
            ngaythi = (txtNgayThi.DateTime).ToString("yyyy-MM-dd");
            lan = cmbLanThi.Text;
            socauthi = txtSoCauThi.Text;
            thoigian = txtThoiGian.Text;


            if (MessageBox.Show("Bạn có thât sự muốn xóa giáo viên đăng ký này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa giáo viên đăng ký của môn "
                        + ((DataRowView)this.bds_GVDK.Current).Row["MAMH"].ToString() + " lớp "
                        + ((DataRowView)this.bds_GVDK.Current).Row["MALOP"].ToString() + " lần "
                        + ((DataRowView)this.bds_GVDK.Current).Row["LAN"].ToString()
                        + "?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes) ;


                    string  xoagvdk = "" +
                            "DELETE DBO.GIAOVIEN_DANGKY " +
                            "WHERE MAMH = '" + mamh + "' AND " +
                            "MALOP = '" + malop + "' AND " +
                            "LAN = '" + lan + "'";
                    Program.ExceSqlNoneQuery(xoagvdk);
                    
                    this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.GIAOVIEN_DANGKYTableAdapter.Update(DS_TN_CSDLPT.GIAOVIEN_DANGKY);
                   

                    MessageBox.Show("Xóa giáo viên đăng ký thành công!", "Thông báo", MessageBoxButtons.OK);

                    this.GIAOVIEN_DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.GIAOVIEN_DANGKYTableAdapter.Fill(DS_TN_CSDLPT.GIAOVIEN_DANGKY);

                    string cauTruyVanHoanTac =
                        "INSERT INTO DBO.GIAOVIEN_DANGKY( MAGV,MAMH,MALOP,TRINHDO,NGAYTHI,LAN,SOCAUTHI,THOIGIAN) " +
                        " VALUES( '" + magv + "','" +
                        mamh + "', '" +
                        malop + "', '" +
                        trinhdo + "', '" +
                        ngaythi + "', '" +
                        lan + "', '" +
                        socauthi + "', '" +
                        thoigian + "') ";
                    undoList.Push(cauTruyVanHoanTac);


                    magv = "";
                    mamh = "";
                    malop = "";
                    trinhdo = "";
                    ngaythi = "";
                    lan = "";
                    socauthi = "";
                    thoigian = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa giáo viên đăng ký. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
                    return;
                }
            }
            btnPhucHoi.Enabled = true;

            if (bds_GVDK.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangthem || dangsua)
            {
                bds_GVDK.CancelEdit();
                if (btnThem.Enabled == false) bds_GVDK.Position = vitri;

                gcGVDK.Enabled = true;
                pcGVDK.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = false;
                if (undoList.Count > 0) btnPhucHoi.Enabled = true;
                else btnPhucHoi.Enabled = false;

                cmbTenMH.Enabled = true;
                cmbTenLop.Enabled = true;
                cmbLanThi.Enabled = true;

                dangthem = false;
                dangsua = false;
                if (bds_GVDK.Count == 0)
                {
                    btnXoa.Enabled = false;
                    btnHieuChinh.Enabled = false;
                }

                return;
            }

            String cauTruyVanHoanTac = undoList.Pop().ToString();
            //MessageBox.Show("cautv:" + cauTruyVanHoanTac, "", MessageBoxButtons.OK);

            int n = Program.ExceSqlNoneQuery(cauTruyVanHoanTac);
            //MessageBox.Show("cautv:" + cauTruyVanHoanTac, "", MessageBoxButtons.OK);
            this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            bds_GVDK.Position = vitri;
            if (undoList.Count > 0) btnPhucHoi.Enabled = true;
            else btnPhucHoi.Enabled = false;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.GIAOVIEN_DANGKYTableAdapter.Fill(this.DS_TN_CSDLPT.GIAOVIEN_DANGKY);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn thoát", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Program.formChinh.rbMain.Enabled = true;
                this.Dispose();
            }
        }

        private void cmbTenGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMAGV.Text = cmbTenGV.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbTenMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMAMH.Text = cmbTenMH.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbTenLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMALOP.Text = cmbTenLop.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbTrinhDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtTrinhDo.Text = cmbTrinhDo.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}