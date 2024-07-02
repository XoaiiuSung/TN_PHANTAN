namespace TN_PHANTAN.subform
{
    partial class fomchonlop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dS_SinhVien = new TN_PHANTAN.DS_SinhVien();
            this.dSLOPBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSLOPTableAdapter = new TN_PHANTAN.DS_SinhVienTableAdapters.DSLOPTableAdapter();
            this.tableAdapterManager = new TN_PHANTAN.DS_SinhVienTableAdapters.TableAdapterManager();
            this.dSLOPGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.colTENLOP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMALOP = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dS_SinhVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSLOPBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSLOPGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dS_SinhVien
            // 
            this.dS_SinhVien.DataSetName = "DS_SinhVien";
            this.dS_SinhVien.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSLOPBindingSource
            // 
            this.dSLOPBindingSource.DataMember = "DSLOP";
            this.dSLOPBindingSource.DataSource = this.dS_SinhVien;
            // 
            // dSLOPTableAdapter
            // 
            this.dSLOPTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BANGDIEMTableAdapter = null;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.SINHVIENTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = TN_PHANTAN.DS_SinhVienTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // dSLOPGridControl
            // 
            this.dSLOPGridControl.DataSource = this.dSLOPBindingSource;
            this.dSLOPGridControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.dSLOPGridControl.Location = new System.Drawing.Point(0, 0);
            this.dSLOPGridControl.MainView = this.gridView1;
            this.dSLOPGridControl.Name = "dSLOPGridControl";
            this.dSLOPGridControl.Size = new System.Drawing.Size(778, 220);
            this.dSLOPGridControl.TabIndex = 1;
            this.dSLOPGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTENLOP,
            this.colMALOP});
            this.gridView1.GridControl = this.dSLOPGridControl;
            this.gridView1.Name = "gridView1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(420, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "chon";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(574, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "huy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // colTENLOP
            // 
            this.colTENLOP.FieldName = "TENLOP";
            this.colTENLOP.MinWidth = 25;
            this.colTENLOP.Name = "colTENLOP";
            this.colTENLOP.Visible = true;
            this.colTENLOP.VisibleIndex = 0;
            this.colTENLOP.Width = 94;
            // 
            // colMALOP
            // 
            this.colMALOP.FieldName = "MALOP";
            this.colMALOP.MinWidth = 25;
            this.colMALOP.Name = "colMALOP";
            this.colMALOP.Visible = true;
            this.colMALOP.VisibleIndex = 1;
            this.colMALOP.Width = 94;
            // 
            // fomchonlop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 380);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dSLOPGridControl);
            this.Name = "fomchonlop";
            this.Text = "fomchonlop";
            this.Load += new System.EventHandler(this.fomchonlop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dS_SinhVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSLOPBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSLOPGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DS_SinhVien dS_SinhVien;
        private System.Windows.Forms.BindingSource dSLOPBindingSource;
        private DS_SinhVienTableAdapters.DSLOPTableAdapter dSLOPTableAdapter;
        private DS_SinhVienTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraGrid.GridControl dSLOPGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraGrid.Columns.GridColumn colTENLOP;
        private DevExpress.XtraGrid.Columns.GridColumn colMALOP;
    }
}