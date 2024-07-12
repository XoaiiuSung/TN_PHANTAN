using DevExpress.XtraEditors;
using System;
using System.Collections;
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
    public partial class formDeThi : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        //private string magv = "";

        Stack undoList = new Stack();
        private Boolean dangthem = false;
        private Boolean dangsua = false;


        string cauhoi = "";
        string mamh = "";
        string trinhdo = "";
        string noidung = "";
        string caua = "";
        string caub = "";
        string cauc = "";
        string caud = "";
        string dapan = "";
        string magv = "";
        public formDeThi()
        {
            InitializeComponent();
        }

        private void bODEBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsBoDe.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void formDeThi_Load(object sender, EventArgs e)
        {

            if (Program.mGroup == "GIANGVIEN")
            {
                DS_TN_CSDLPT.EnforceConstraints = false;
                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.BODETableAdapter.FillByGV_MH(this.DS_TN_CSDLPT.BODE, Program.username, cmbMonHoc.SelectedValue.ToString().Trim());
            }
            else
            {
                DS_TN_CSDLPT.EnforceConstraints = false;
                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.DS_TN_CSDLPT.MONHOC);
                this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                this.BODETableAdapter.FillBy(this.DS_TN_CSDLPT.BODE, cmbMonHoc.SelectedValue.ToString().Trim());

                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
            }

            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            if (bdsBoDe.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }

            pcBoDe.Enabled = false;

            cmbMonHoc.DataSource = this.DS_TN_CSDLPT.MONHOC;
            cmbMonHoc.DisplayMember = "TENMH";
            cmbMonHoc.ValueMember = "MAMH";


            cmbTrinhDo.Items.Add("A");
            cmbTrinhDo.Items.Add("B");
            cmbTrinhDo.Items.Add("C");

            cmbDapAn.Items.Add("A");
            cmbDapAn.Items.Add("B");
            cmbDapAn.Items.Add("C");
            cmbDapAn.Items.Add("D");
        }

        private void cmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.mGroup == "GIANGVIEN")
            {
                try
                {


                    this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                    this.BODETableAdapter.FillByGV_MH(this.DS_TN_CSDLPT.BODE, Program.username, cmbMonHoc.SelectedValue.ToString().Trim());

                    txtMAMH.Text = cmbMonHoc.SelectedValue.ToString();

                    cmbMonHoc.DataSource = this.DS_TN_CSDLPT.MONHOC;
                    cmbMonHoc.DisplayMember = "TENMH";
                    cmbMonHoc.ValueMember = "MAMH";

                    if (bdsBoDe.Count != 0)
                    {
                        btnXoa.Enabled = true;
                        btnHieuChinh.Enabled = true;
                    }
                    if (bdsBoDe.Count == 0) btnHieuChinh.Enabled = false;

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                    this.BODETableAdapter.FillBy(this.DS_TN_CSDLPT.BODE, cmbMonHoc.SelectedValue.ToString().Trim());

                    txtMAMH.Text = cmbMonHoc.SelectedValue.ToString();

                    cmbMonHoc.DataSource = this.DS_TN_CSDLPT.MONHOC;
                    cmbMonHoc.DisplayMember = "TENMH";
                    cmbMonHoc.ValueMember = "MAMH";
                }
                catch (Exception ex)
                {
                }
            }

        }

        private Boolean KiemTraLoiInput()
        {
            if (txtCauHoi.Text.Trim() == "")
            {
                MessageBox.Show("Câu hỏi không được thiếu!", "", MessageBoxButtons.OK);
                txtCauHoi.Focus();
                return false;
            }
            if (txtNoiDung.Text.Trim() == "")
            {
                MessageBox.Show("Nội dung không được thiếu!", "", MessageBoxButtons.OK);
                txtNoiDung.Focus();
                return false;
            }
            if (txtA.Text.Trim() == "")
            {
                MessageBox.Show("Câu A không được thiếu!", "", MessageBoxButtons.OK);
                txtA.Focus();
                return false;
            }
            if (txtB.Text.Trim() == "")
            {
                MessageBox.Show("Câu B không được thiếu!", "", MessageBoxButtons.OK);
                txtB.Focus();
                return false;
            }
            if (txtC.Text.Trim() == "")
            {
                MessageBox.Show("Câu C không được thiếu!", "", MessageBoxButtons.OK);
                txtC.Focus();
                return false;
            }
            if (txtD.Text.Trim() == "")
            {
                MessageBox.Show("Câu D không được thiếu!", "", MessageBoxButtons.OK);
                txtD.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsBoDe.Position;
            pcBoDe.Enabled = true;
            bdsBoDe.AddNew();
            txtCauHoi.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnPhucHoi.Enabled = btnGhi.Enabled = true;
            gcBoDe.Enabled = false;
            dangthem = true;

            txtMAMH.Text = cmbMonHoc.SelectedValue.ToString();
            txtMAGV.Text = Program.username;
            cmbTrinhDo.SelectedIndex = 1;
            cmbTrinhDo.SelectedIndex = 0;

            cmbDapAn.SelectedIndex = 1;
            cmbDapAn.SelectedIndex = 0;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangsua = true;
            vitri = bdsBoDe.Position;
            pcBoDe.Enabled = true;
            gcBoDe.Enabled = false;
            txtCauHoi.Enabled = false; // tắt ghi mã gv

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            DataRowView bode = (DataRowView)bdsBoDe[bdsBoDe.Position];
            cauhoi = bode["CAUHOI"].ToString().Trim();
            mamh = bode["MAMH"].ToString().Trim();
            trinhdo = bode["TRINHDO"].ToString();
            noidung = bode["NOIDUNG"].ToString();
            caua = bode["A"].ToString();
            caub = bode["B"].ToString();
            cauc = bode["C"].ToString();
            caud = bode["D"].ToString();
            dapan = bode["DAP_AN"].ToString();
            magv = bode["MAGV"].ToString().Trim();
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!KiemTraLoiInput())
            {
                return;
            }



            String sql = "";
            int kq = 0;
            if (dangthem)
            {
                sql = "EXEC SP_KT_MA_BO_DE '" + txtCauHoi.Text.Trim() + "'";
                kq = Program.ExecSqlNonQuery(sql);
            }
            if (kq == 1)
            {
                txtCauHoi.Focus();
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
                                "DELETE DBO.BODE " +
                                "WHERE CAUHOI = '" + txtCauHoi.Text.Trim() + "'";
                        }
                        if (dangsua)
                        {


                            CauTruyVanHoanTac =
                                            "UPDATE DBO.BODE " +
                                            "SET " +
                                            "MAMH = '" + mamh + "', " +
                                            "TRINHDO = '" + trinhdo + "', " +
                                            "NOIDUNG = N'" + noidung + "', " +
                                            "A = N'" + caua + "', " +
                                            "B = N'" + caub + "', " +
                                            "C = N'" + cauc + "', " +
                                            "D = N'" + caud + "', " +
                                            "DAP_AN = '" + dapan + "', " +
                                            "MAGV = '" + magv + "' " +
                                            "WHERE CAUHOI = '" + cauhoi + "'";

                        }
                        undoList.Push(CauTruyVanHoanTac);
                        dangthem = false;
                        dangsua = false;

                        bdsBoDe.EndEdit();
                        bdsBoDe.ResetCurrentItem();
                        this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                        this.BODETableAdapter.Update(this.DS_TN_CSDLPT.BODE);

                        btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                        btnGhi.Enabled = false;
                        pcBoDe.Enabled = false;
                        gcBoDe.Enabled = true;
                        txtCauHoi.Enabled = true; // bật lại để còn xài thêm

                        cauhoi = "";
                        mamh = "";
                        trinhdo = "";
                        noidung = "";
                        caua = "";
                        caub = "";
                        cauc = "";
                        caud = "";
                        dapan = "";
                        magv = "";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi câu hỏi! \n" + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cauhoi = txtCauHoi.Text.Trim();
            mamh = txtMAMH.Text.Trim();
            trinhdo = cmbTrinhDo.Text;
            noidung = txtNoiDung.Text;
            caua = txtA.Text;
            caub = txtB.Text;
            cauc = txtC.Text;
            caud = txtD.Text;
            dapan = cmbDapAn.Text;
            magv = Program.username;


            if (MessageBox.Show("Bạn có thât sự muốn xóa câu hỏi này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    cauhoi = ((DataRowView)bdsBoDe[bdsBoDe.Position])["CAUHOI"].ToString();
                    bdsBoDe.RemoveCurrent();
                    this.BODETableAdapter.Connection.ConnectionString = Program.connstr;
                    this.BODETableAdapter.Update(DS_TN_CSDLPT.BODE);

                    MessageBox.Show("Xóa câu hỏi thành công!", "Thông báo", MessageBoxButtons.OK);
                    string cauTruyVanHoanTac =
                        "INSERT INTO DBO.BODE( CAUHOI,MAMH,TRINHDO,NOIDUNG,A,B,C,D,DAP_AN,MAGV) " +
                        " VALUES( '" + cauhoi + "','" +
                        mamh + "', '" +
                        trinhdo + "', N'" +
                        noidung + "', N'" +
                        caua + "', N'" +
                        caub + "', N'" +
                        cauc + "', N'" +
                        caud + "', '" +
                        dapan + "', '" +
                        magv + "' ) ";
                    undoList.Push(cauTruyVanHoanTac);


                        cauhoi = "";
                        mamh = "";
                        trinhdo = "";
                        noidung = "";
                        caua = "";
                        caub = "";
                        cauc = "";
                        caud = "";
                        dapan = "";
                        magv = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa câu hỏi. Bạn hãy xóa lại!" + ex.Message, "", MessageBoxButtons.OK);
                    this.BODETableAdapter.Fill(this.DS_TN_CSDLPT.BODE);
                    bdsBoDe.Position = bdsBoDe.Find("CAUHOI", cauhoi);
                    return;
                }
            }
            btnPhucHoi.Enabled = true;

            if (bdsBoDe.Count == 0)
            {
                btnXoa.Enabled = false;
                btnHieuChinh.Enabled = false;
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangthem || dangsua)
            {
                bdsBoDe.CancelEdit();
                if (btnThem.Enabled == false) bdsBoDe.Position = vitri;

                gcBoDe.Enabled = true;
                pcBoDe.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = false;
                if (undoList.Count > 0) btnPhucHoi.Enabled = true;
                else btnPhucHoi.Enabled = false;
                txtCauHoi.Enabled = true; // bật lên còn xài 

                dangthem = false;
                dangsua = false;
                if (bdsBoDe.Count == 0)
                {
                    btnXoa.Enabled = false;
                    btnHieuChinh.Enabled = false;
                }

                return;
            }

            String cauTruyVanHoanTac = undoList.Pop().ToString();
            int n = Program.ExceSqlNoneQuery(cauTruyVanHoanTac);
            //MessageBox.Show("cautv:" + cauTruyVanHoanTac, "", MessageBoxButtons.OK);
            this.BODETableAdapter.FillByGV_MH(this.DS_TN_CSDLPT.BODE, Program.username, cmbMonHoc.SelectedValue.ToString().Trim());
            bdsBoDe.Position = vitri;
            if (undoList.Count > 0) btnPhucHoi.Enabled = true;
            else btnPhucHoi.Enabled = false;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Program.mGroup == "GIANGVIEN")
            {
                try
                {
                    this.BODETableAdapter.FillByGV_MH(this.DS_TN_CSDLPT.BODE, Program.username, cmbMonHoc.SelectedValue.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Reload" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                try
                {
                    this.BODETableAdapter.FillBy(this.DS_TN_CSDLPT.BODE, cmbMonHoc.SelectedValue.ToString().Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Reload" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
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
    }
}