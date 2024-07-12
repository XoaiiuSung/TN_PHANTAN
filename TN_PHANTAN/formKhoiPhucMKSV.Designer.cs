namespace TN_PHANTAN
{
    partial class formKhoiPhucMKSV
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
            System.Windows.Forms.Label hOTENLabel;
            this.label1 = new System.Windows.Forms.Label();
            this.DS_TN_CSDLPT = new TN_PHANTAN.TN_CSDLPTDataSet();
            this.bdsSVdataoTK = new System.Windows.Forms.BindingSource(this.components);
            this.SP_MASV_DA_TAO_TKTableAdapter = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.SP_MASV_DA_TAO_TKTableAdapter();
            this.tableAdapterManager = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.TableAdapterManager();
            this.cmbHoTen = new System.Windows.Forms.ComboBox();
            this.txtMASV = new System.Windows.Forms.TextBox();
            this.btnKP = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            hOTENLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSVdataoTK)).BeginInit();
            this.SuspendLayout();
            // 
            // hOTENLabel
            // 
            hOTENLabel.AutoSize = true;
            hOTENLabel.Location = new System.Drawing.Point(177, 193);
            hOTENLabel.Name = "hOTENLabel";
            hOTENLabel.Size = new System.Drawing.Size(73, 19);
            hOTENLabel.TabIndex = 2;
            hOTENLabel.Text = "HỌ TÊN:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(245, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(545, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "KHÔI PHỤC MẬT KHẨU SINH VIÊN";
            // 
            // DS_TN_CSDLPT
            // 
            this.DS_TN_CSDLPT.DataSetName = "TN_CSDLPTDataSet";
            this.DS_TN_CSDLPT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bdsSVdataoTK
            // 
            this.bdsSVdataoTK.DataMember = "SP_MASV_DA_TAO_TK";
            this.bdsSVdataoTK.DataSource = this.DS_TN_CSDLPT;
            // 
            // SP_MASV_DA_TAO_TKTableAdapter
            // 
            this.SP_MASV_DA_TAO_TKTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.BAITHITableAdapter = null;
            this.tableAdapterManager.BANGDIEMTableAdapter = null;
            this.tableAdapterManager.BODETableAdapter = null;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.COSOTableAdapter = null;
            this.tableAdapterManager.DSDK_THITN_SONGSONGTableAdapter = null;
            this.tableAdapterManager.GIAOVIEN_DANGKYTableAdapter = null;
            this.tableAdapterManager.GIAOVIENTableAdapter = null;
            this.tableAdapterManager.GV_HOTENTableAdapter = null;
            this.tableAdapterManager.KHOATableAdapter = null;
            this.tableAdapterManager.LOPTableAdapter = null;
            this.tableAdapterManager.MONHOCTableAdapter = null;
            this.tableAdapterManager.SINHVIENTableAdapter = null;
            this.tableAdapterManager.TBTRINHDOTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // cmbHoTen
            // 
            this.cmbHoTen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsSVdataoTK, "HOTEN", true));
            this.cmbHoTen.DataSource = this.bdsSVdataoTK;
            this.cmbHoTen.DisplayMember = "HOTEN";
            this.cmbHoTen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoTen.FormattingEnabled = true;
            this.cmbHoTen.Location = new System.Drawing.Point(352, 190);
            this.cmbHoTen.Name = "cmbHoTen";
            this.cmbHoTen.Size = new System.Drawing.Size(358, 27);
            this.cmbHoTen.TabIndex = 3;
            this.cmbHoTen.ValueMember = "MASV";
            // 
            // txtMASV
            // 
            this.txtMASV.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsSVdataoTK, "MASV", true));
            this.txtMASV.Enabled = false;
            this.txtMASV.Location = new System.Drawing.Point(716, 190);
            this.txtMASV.Name = "txtMASV";
            this.txtMASV.Size = new System.Drawing.Size(100, 27);
            this.txtMASV.TabIndex = 4;
            // 
            // btnKP
            // 
            this.btnKP.Location = new System.Drawing.Point(461, 277);
            this.btnKP.Name = "btnKP";
            this.btnKP.Size = new System.Drawing.Size(182, 49);
            this.btnKP.TabIndex = 5;
            this.btnKP.Text = "KHÔI PHỤC";
            this.btnKP.UseVisualStyleBackColor = true;
            this.btnKP.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(680, 277);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(136, 49);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "THOÁT";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.button2_Click);
            // 
            // formKhoiPhucMKSV
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 401);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnKP);
            this.Controls.Add(this.txtMASV);
            this.Controls.Add(hOTENLabel);
            this.Controls.Add(this.cmbHoTen);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formKhoiPhucMKSV";
            this.Text = "formKhoiPhucMKSV";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formKhoiPhucMKSV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSVdataoTK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private TN_CSDLPTDataSet DS_TN_CSDLPT;
        private System.Windows.Forms.BindingSource bdsSVdataoTK;
        private TN_CSDLPTDataSetTableAdapters.SP_MASV_DA_TAO_TKTableAdapter SP_MASV_DA_TAO_TKTableAdapter;
        private TN_CSDLPTDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ComboBox cmbHoTen;
        private System.Windows.Forms.TextBox txtMASV;
        private System.Windows.Forms.Button btnKP;
        private System.Windows.Forms.Button btnThoat;
    }
}