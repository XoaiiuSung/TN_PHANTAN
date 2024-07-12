namespace TN_PHANTAN
{
    partial class formTaoTKSV
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
            this.bdsSVchuaTK = new System.Windows.Forms.BindingSource(this.components);
            this.SP_MASV_CHUA_TAO_TKTableAdapter = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.SP_MASV_CHUA_TAO_TKTableAdapter();
            this.tableAdapterManager = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.TableAdapterManager();
            this.cmbHoTen = new System.Windows.Forms.ComboBox();
            this.txtMASV = new System.Windows.Forms.TextBox();
            this.btnTao = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            hOTENLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSVchuaTK)).BeginInit();
            this.SuspendLayout();
            // 
            // hOTENLabel
            // 
            hOTENLabel.AutoSize = true;
            hOTENLabel.Location = new System.Drawing.Point(162, 182);
            hOTENLabel.Name = "hOTENLabel";
            hOTENLabel.Size = new System.Drawing.Size(73, 19);
            hOTENLabel.TabIndex = 2;
            hOTENLabel.Text = "HỌ TÊN:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(310, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(443, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "TẠO TÀI KHOẢN SINH VIÊN";
            // 
            // DS_TN_CSDLPT
            // 
            this.DS_TN_CSDLPT.DataSetName = "TN_CSDLPTDataSet";
            this.DS_TN_CSDLPT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bdsSVchuaTK
            // 
            this.bdsSVchuaTK.DataMember = "SP_MASV_CHUA_TAO_TK";
            this.bdsSVchuaTK.DataSource = this.DS_TN_CSDLPT;
            // 
            // SP_MASV_CHUA_TAO_TKTableAdapter
            // 
            this.SP_MASV_CHUA_TAO_TKTableAdapter.ClearBeforeFill = true;
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
            this.cmbHoTen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsSVchuaTK, "HOTEN", true));
            this.cmbHoTen.DataSource = this.bdsSVchuaTK;
            this.cmbHoTen.DisplayMember = "HOTEN";
            this.cmbHoTen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoTen.FormattingEnabled = true;
            this.cmbHoTen.Location = new System.Drawing.Point(317, 179);
            this.cmbHoTen.Name = "cmbHoTen";
            this.cmbHoTen.Size = new System.Drawing.Size(385, 27);
            this.cmbHoTen.TabIndex = 3;
            this.cmbHoTen.ValueMember = "MASV";
            // 
            // txtMASV
            // 
            this.txtMASV.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsSVchuaTK, "MASV", true));
            this.txtMASV.Enabled = false;
            this.txtMASV.Location = new System.Drawing.Point(725, 179);
            this.txtMASV.Name = "txtMASV";
            this.txtMASV.Size = new System.Drawing.Size(100, 27);
            this.txtMASV.TabIndex = 4;
            // 
            // btnTao
            // 
            this.btnTao.Location = new System.Drawing.Point(554, 283);
            this.btnTao.Name = "btnTao";
            this.btnTao.Size = new System.Drawing.Size(100, 63);
            this.btnTao.TabIndex = 5;
            this.btnTao.Text = "Tạo";
            this.btnTao.UseVisualStyleBackColor = true;
            this.btnTao.Click += new System.EventHandler(this.btnTao_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(725, 283);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 63);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // formTaoTKSV
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 425);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTao);
            this.Controls.Add(this.txtMASV);
            this.Controls.Add(hOTENLabel);
            this.Controls.Add(this.cmbHoTen);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formTaoTKSV";
            this.Text = "formTaoTKSV";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formTaoTKSV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSVchuaTK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private TN_CSDLPTDataSet DS_TN_CSDLPT;
        private System.Windows.Forms.BindingSource bdsSVchuaTK;
        private TN_CSDLPTDataSetTableAdapters.SP_MASV_CHUA_TAO_TKTableAdapter SP_MASV_CHUA_TAO_TKTableAdapter;
        private TN_CSDLPTDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ComboBox cmbHoTen;
        private System.Windows.Forms.TextBox txtMASV;
        private System.Windows.Forms.Button btnTao;
        private System.Windows.Forms.Button btnThoat;
    }
}