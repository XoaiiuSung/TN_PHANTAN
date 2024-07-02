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
using TN_PHANTAN.subform;

namespace TN_PHANTAN
{
    public partial class formSinhVien : DevExpress.XtraEditors.XtraForm
    {
        public formSinhVien()
        {
            InitializeComponent();
        }

        private void sINHVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.sINHVIENBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS_SinhVien);

        }

        private void formSinhVien_Load(object sender, EventArgs e)
        {
            //dataSet.EnforceConstraints = false;

            this.dSLOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSLOPTableAdapter.Fill(this.dS_SinhVien.DSLOP);


            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sINHVIENTableAdapter.Fill(this.dS_SinhVien.SINHVIEN);

        }


        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fomchonlop frm = new fomchonlop();
            frm.ShowDialog();
            this.mALOPTextBox.Text = Program.malopduocchon;
        }

        private void cmbCOSO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}