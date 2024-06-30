using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace TN_PHANTAN
{
    public partial class Xrpt_BangDiemMonHoc : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_BangDiemMonHoc()
        {
            
        }
        public Xrpt_BangDiemMonHoc(string malop, string mamonhoc, int lanthi)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = malop;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = mamonhoc;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = lanthi;
            this.sqlDataSource1.Fill();
        }

    }
}
