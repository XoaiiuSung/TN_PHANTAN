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

namespace TN_PHANTAN.subform
{
    public partial class fomchonlop : DevExpress.XtraEditors.XtraForm
    {
        public fomchonlop()
        {
            InitializeComponent();
        }

        private void fomchonlop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS_SinhVien.DSLOP' table. You can move, or remove it, as needed.
            this.dSLOPTableAdapter.Fill(this.dS_SinhVien.DSLOP);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string malop = ((DataRowView)dSLOPBindingSource.Current)["MALOP"].ToString();

            /*Cach nay phai tuy bien ban moi chay duoc*/
            //Program.formDonDatHang.txtMaKho.Text = maKhoHang;


            Program.malopduocchon = malop;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}