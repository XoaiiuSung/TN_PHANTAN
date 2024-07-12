namespace TN_PHANTAN
{
    partial class frpt_XemKetQua
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label tENMHLabel;
            this.DS_TN_CSDLPT = new TN_PHANTAN.TN_CSDLPTDataSet();
            this.bdsMonHoc = new System.Windows.Forms.BindingSource(this.components);
            this.MONHOCTableAdapter = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.MONHOCTableAdapter();
            this.tableAdapterManager = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.TableAdapterManager();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtMAMH = new System.Windows.Forms.TextBox();
            this.cmbMon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPreView = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.cmbLan = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            tENMHLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMonHoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DS_TN_CSDLPT
            // 
            this.DS_TN_CSDLPT.DataSetName = "TN_CSDLPTDataSet";
            this.DS_TN_CSDLPT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bdsMonHoc
            // 
            this.bdsMonHoc.DataMember = "MONHOC";
            this.bdsMonHoc.DataSource = this.DS_TN_CSDLPT;
            // 
            // MONHOCTableAdapter
            // 
            this.MONHOCTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BAITHITableAdapter = null;
            this.tableAdapterManager.BANGDIEMTableAdapter = null;
            this.tableAdapterManager.BODETableAdapter = null;
            this.tableAdapterManager.COSOTableAdapter = null;
            this.tableAdapterManager.DSDK_THITN_SONGSONGTableAdapter = null;
            this.tableAdapterManager.GIAOVIEN_DANGKYTableAdapter = null;
            this.tableAdapterManager.GIAOVIENTableAdapter = null;
            this.tableAdapterManager.GV_HOTENTableAdapter = null;
            this.tableAdapterManager.KHOATableAdapter = null;
            this.tableAdapterManager.LOPTableAdapter = null;
            this.tableAdapterManager.MONHOCTableAdapter = this.MONHOCTableAdapter;
            this.tableAdapterManager.SINHVIENTableAdapter = null;
            this.tableAdapterManager.TBTRINHDOTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbLan);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.btnPreView);
            this.panelControl1.Controls.Add(label2);
            this.panelControl1.Controls.Add(this.txtMAMH);
            this.panelControl1.Controls.Add(tENMHLabel);
            this.panelControl1.Controls.Add(this.cmbMon);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(915, 477);
            this.panelControl1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(237, 215);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(34, 16);
            label2.TabIndex = 12;
            label2.Text = "LẦN:";
            // 
            // txtMAMH
            // 
            this.txtMAMH.Enabled = false;
            this.txtMAMH.Location = new System.Drawing.Point(600, 135);
            this.txtMAMH.Name = "txtMAMH";
            this.txtMAMH.Size = new System.Drawing.Size(100, 23);
            this.txtMAMH.TabIndex = 10;
            // 
            // tENMHLabel
            // 
            tENMHLabel.AutoSize = true;
            tENMHLabel.Location = new System.Drawing.Point(176, 138);
            tENMHLabel.Name = "tENMHLabel";
            tENMHLabel.Size = new System.Drawing.Size(95, 16);
            tENMHLabel.TabIndex = 8;
            tENMHLabel.Text = "TÊN MÔN HỌC:";
            // 
            // cmbMon
            // 
            this.cmbMon.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsMonHoc, "TENMH", true));
            this.cmbMon.DataSource = this.bdsMonHoc;
            this.cmbMon.DisplayMember = "TENMH";
            this.cmbMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMon.FormattingEnabled = true;
            this.cmbMon.Location = new System.Drawing.Point(304, 135);
            this.cmbMon.Name = "cmbMon";
            this.cmbMon.Size = new System.Drawing.Size(290, 24);
            this.cmbMon.TabIndex = 9;
            this.cmbMon.ValueMember = "MAMH";
            this.cmbMon.SelectedIndexChanged += new System.EventHandler(this.cmbMon_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(296, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 39);
            this.label1.TabIndex = 7;
            this.label1.Text = "XEM KẾT QUẢ";
            // 
            // btnPreView
            // 
            this.btnPreView.Location = new System.Drawing.Point(473, 279);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(86, 55);
            this.btnPreView.TabIndex = 13;
            this.btnPreView.Text = "Preview";
            this.btnPreView.UseVisualStyleBackColor = true;
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(614, 279);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(86, 55);
            this.btnThoat.TabIndex = 14;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // cmbLan
            // 
            this.cmbLan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLan.FormattingEnabled = true;
            this.cmbLan.Location = new System.Drawing.Point(304, 212);
            this.cmbLan.Name = "cmbLan";
            this.cmbLan.Size = new System.Drawing.Size(121, 24);
            this.cmbLan.TabIndex = 15;
            // 
            // frpt_XemKetQua
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 477);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frpt_XemKetQua";
            this.Text = "frpt_XemKetQua";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frpt_XemKetQua_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMonHoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TN_CSDLPTDataSet DS_TN_CSDLPT;
        private System.Windows.Forms.BindingSource bdsMonHoc;
        private TN_CSDLPTDataSetTableAdapters.MONHOCTableAdapter MONHOCTableAdapter;
        private TN_CSDLPTDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txtMAMH;
        private System.Windows.Forms.ComboBox cmbMon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnPreView;
        private System.Windows.Forms.ComboBox cmbLan;
    }
}