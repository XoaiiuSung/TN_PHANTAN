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
    public partial class sformChonKhoa : DevExpress.XtraEditors.XtraForm
    {
        public sformChonKhoa()
        {
            InitializeComponent();
        }

        private void kHOABindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKhoa.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS_TN_CSDLPT);

        }

        private void sformChonKhoa_Load(object sender, EventArgs e)
        {
            DS_TN_CSDLPT.EnforceConstraints = false;
            this.KHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.KHOATableAdapter.Fill(this.DS_TN_CSDLPT.KHOA);

        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            string makhoa = ((DataRowView)bdsKhoa.Current)["MAKH"].ToString();


            Program.makhoaduocchon = makhoa;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}