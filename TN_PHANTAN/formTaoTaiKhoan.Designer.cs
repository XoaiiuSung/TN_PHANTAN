namespace TN_PHANTAN
{
    partial class formTaoTaiKhoan
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmbCOSO = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLoaiTK = new System.Windows.Forms.ComboBox();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.DS_TN_CSDLPT = new TN_PHANTAN.TN_CSDLPTDataSet();
            this.bds_GV_CHUA_TAO_TK = new System.Windows.Forms.BindingSource(this.components);
            this.SP_MA_GV_CHUA_TAO_TKTableAdapter = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.SP_MA_GV_CHUA_TAO_TKTableAdapter();
            this.tableAdapterManager = new TN_PHANTAN.TN_CSDLPTDataSetTableAdapters.TableAdapterManager();
            this.cmbMAGV = new System.Windows.Forms.ComboBox();
            this.txtMAGV = new System.Windows.Forms.TextBox();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds_GV_CHUA_TAO_TK)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Location = new System.Drawing.Point(730, 146);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(422, 57);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "TẠO TÀI KHOẢN";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbCOSO
            // 
            this.cmbCOSO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOSO.FormattingEnabled = true;
            this.cmbCOSO.Location = new System.Drawing.Point(866, 229);
            this.cmbCOSO.Name = "cmbCOSO";
            this.cmbCOSO.Size = new System.Drawing.Size(286, 24);
            this.cmbCOSO.TabIndex = 14;
            this.cmbCOSO.SelectedIndexChanged += new System.EventHandler(this.cmbCoSo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(727, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "CƠ SỞ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(727, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "LOẠI TÀI KHOẢN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(727, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "MÃ GIẢNG VIÊN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(727, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "TÊN ĐĂNG NHẬP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(727, 409);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "MẬT KHẨU";
            // 
            // cmbLoaiTK
            // 
            this.cmbLoaiTK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiTK.FormattingEnabled = true;
            this.cmbLoaiTK.Location = new System.Drawing.Point(866, 276);
            this.cmbLoaiTK.Name = "cmbLoaiTK";
            this.cmbLoaiTK.Size = new System.Drawing.Size(286, 24);
            this.cmbLoaiTK.TabIndex = 19;
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(866, 365);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(286, 23);
            this.txtTenDangNhap.TabIndex = 21;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(866, 402);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(286, 23);
            this.txtMatKhau.TabIndex = 22;
            // 
            // DS_TN_CSDLPT
            // 
            this.DS_TN_CSDLPT.DataSetName = "TN_CSDLPTDataSet";
            this.DS_TN_CSDLPT.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bds_GV_CHUA_TAO_TK
            // 
            this.bds_GV_CHUA_TAO_TK.DataMember = "SP_MA_GV_CHUA_TAO_TK";
            this.bds_GV_CHUA_TAO_TK.DataSource = this.DS_TN_CSDLPT;
            // 
            // SP_MA_GV_CHUA_TAO_TKTableAdapter
            // 
            this.SP_MA_GV_CHUA_TAO_TKTableAdapter.ClearBeforeFill = true;
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
            // cmbMAGV
            // 
            this.cmbMAGV.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bds_GV_CHUA_TAO_TK, "TEN", true));
            this.cmbMAGV.DataSource = this.bds_GV_CHUA_TAO_TK;
            this.cmbMAGV.DisplayMember = "TEN";
            this.cmbMAGV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMAGV.FormattingEnabled = true;
            this.cmbMAGV.Location = new System.Drawing.Point(866, 320);
            this.cmbMAGV.Name = "cmbMAGV";
            this.cmbMAGV.Size = new System.Drawing.Size(286, 24);
            this.cmbMAGV.TabIndex = 24;
            this.cmbMAGV.ValueMember = "MAGV";
            // 
            // txtMAGV
            // 
            this.txtMAGV.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bds_GV_CHUA_TAO_TK, "MAGV", true));
            this.txtMAGV.Location = new System.Drawing.Point(1178, 320);
            this.txtMAGV.Name = "txtMAGV";
            this.txtMAGV.Size = new System.Drawing.Size(100, 23);
            this.txtMAGV.TabIndex = 25;
            // 
            // btnDangKy
            // 
            this.btnDangKy.Location = new System.Drawing.Point(866, 465);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(135, 23);
            this.btnDangKy.TabIndex = 26;
            this.btnDangKy.Text = "ĐĂNG KÝ";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(1023, 465);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(129, 23);
            this.btnThoat.TabIndex = 27;
            this.btnThoat.Text = "THOÁT";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // formTaoTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1785, 772);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.txtMAGV);
            this.Controls.Add(this.cmbMAGV);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.cmbLoaiTK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCOSO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "formTaoTaiKhoan";
            this.Text = "formTaoTaiKhoan";
            this.Load += new System.EventHandler(this.formTaoTaiKhoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DS_TN_CSDLPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds_GV_CHUA_TAO_TK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbCOSO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbLoaiTK;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.TextBox txtMatKhau;
        private TN_CSDLPTDataSet DS_TN_CSDLPT;
        private System.Windows.Forms.BindingSource bds_GV_CHUA_TAO_TK;
        private TN_CSDLPTDataSetTableAdapters.SP_MA_GV_CHUA_TAO_TKTableAdapter SP_MA_GV_CHUA_TAO_TKTableAdapter;
        private TN_CSDLPTDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ComboBox cmbMAGV;
        private System.Windows.Forms.TextBox txtMAGV;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnThoat;
    }
}