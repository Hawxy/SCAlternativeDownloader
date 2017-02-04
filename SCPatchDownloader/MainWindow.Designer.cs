namespace SCPatchDownloader
{
    partial class MainWindow
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
            this.relSelector = new System.Windows.Forms.ComboBox();
            this.toolTip_check = new System.Windows.Forms.ToolTip(this.components);
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.releaseSelect = new MaterialSkin.Controls.MaterialFlatButton();
            this.downloadDir = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.check_nativefile = new MaterialSkin.Controls.MaterialCheckBox();
            this.browseDir = new MaterialSkin.Controls.MaterialRaisedButton();
            this.downloadSrt = new MaterialSkin.Controls.MaterialRaisedButton();
            this.butCancel = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.label_status = new MaterialSkin.Controls.MaterialLabel();
            this.infoProg = new MaterialSkin.Controls.MaterialProgressBar();
            this.label_MB = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.gitButton = new MaterialSkin.Controls.MaterialFlatButton();
            this.SuspendLayout();
            // 
            // relSelector
            // 
            this.relSelector.FormattingEnabled = true;
            this.relSelector.Location = new System.Drawing.Point(15, 105);
            this.relSelector.Name = "relSelector";
            this.relSelector.Size = new System.Drawing.Size(141, 21);
            this.relSelector.TabIndex = 0;
            // 
            // toolTip_check
            // 
            this.toolTip_check.IsBalloon = true;
            this.toolTip_check.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip_check.ToolTipTitle = "Information";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 74);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(144, 19);
            this.materialLabel1.TabIndex = 18;
            this.materialLabel1.Text = "Select Release Type";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(12, 153);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(116, 19);
            this.materialLabel2.TabIndex = 19;
            this.materialLabel2.Text = "Select Directory";
            // 
            // releaseSelect
            // 
            this.releaseSelect.AutoSize = true;
            this.releaseSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.releaseSelect.Depth = 0;
            this.releaseSelect.Icon = null;
            this.releaseSelect.Location = new System.Drawing.Point(192, 96);
            this.releaseSelect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.releaseSelect.MouseState = MaterialSkin.MouseState.HOVER;
            this.releaseSelect.Name = "releaseSelect";
            this.releaseSelect.Primary = false;
            this.releaseSelect.Size = new System.Drawing.Size(130, 36);
            this.releaseSelect.TabIndex = 20;
            this.releaseSelect.Text = "Select Release";
            this.releaseSelect.UseVisualStyleBackColor = true;
            this.releaseSelect.Click += new System.EventHandler(this.releaseSelect_Click);
            // 
            // downloadDir
            // 
            this.downloadDir.Depth = 0;
            this.downloadDir.Hint = "";
            this.downloadDir.Location = new System.Drawing.Point(12, 184);
            this.downloadDir.MaxLength = 32767;
            this.downloadDir.MouseState = MaterialSkin.MouseState.HOVER;
            this.downloadDir.Name = "downloadDir";
            this.downloadDir.PasswordChar = '\0';
            this.downloadDir.SelectedText = "";
            this.downloadDir.SelectionLength = 0;
            this.downloadDir.SelectionStart = 0;
            this.downloadDir.Size = new System.Drawing.Size(268, 23);
            this.downloadDir.TabIndex = 21;
            this.downloadDir.TabStop = false;
            this.downloadDir.UseSystemPasswordChar = false;
            // 
            // check_nativefile
            // 
            this.check_nativefile.AutoSize = true;
            this.check_nativefile.Depth = 0;
            this.check_nativefile.Font = new System.Drawing.Font("Roboto", 10F);
            this.check_nativefile.Location = new System.Drawing.Point(16, 222);
            this.check_nativefile.Margin = new System.Windows.Forms.Padding(0);
            this.check_nativefile.MouseLocation = new System.Drawing.Point(-1, -1);
            this.check_nativefile.MouseState = MaterialSkin.MouseState.HOVER;
            this.check_nativefile.Name = "check_nativefile";
            this.check_nativefile.Ripple = true;
            this.check_nativefile.Size = new System.Drawing.Size(213, 30);
            this.check_nativefile.TabIndex = 23;
            this.check_nativefile.Text = "Preserve Native File Structure";
            this.check_nativefile.UseVisualStyleBackColor = true;
            // 
            // browseDir
            // 
            this.browseDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseDir.Depth = 0;
            this.browseDir.Icon = null;
            this.browseDir.Location = new System.Drawing.Point(290, 184);
            this.browseDir.MouseState = MaterialSkin.MouseState.HOVER;
            this.browseDir.Name = "browseDir";
            this.browseDir.Primary = true;
            this.browseDir.Size = new System.Drawing.Size(32, 23);
            this.browseDir.TabIndex = 24;
            this.browseDir.Text = "...";
            this.browseDir.UseVisualStyleBackColor = true;
            this.browseDir.Click += new System.EventHandler(this.browseDir_Click);
            // 
            // downloadSrt
            // 
            this.downloadSrt.AutoSize = true;
            this.downloadSrt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadSrt.Depth = 0;
            this.downloadSrt.Icon = null;
            this.downloadSrt.Location = new System.Drawing.Point(32, 273);
            this.downloadSrt.MouseState = MaterialSkin.MouseState.HOVER;
            this.downloadSrt.Name = "downloadSrt";
            this.downloadSrt.Primary = true;
            this.downloadSrt.Size = new System.Drawing.Size(96, 36);
            this.downloadSrt.TabIndex = 25;
            this.downloadSrt.Text = "Download";
            this.downloadSrt.UseVisualStyleBackColor = true;
            this.downloadSrt.Click += new System.EventHandler(this.downloadSrt_Click);
            // 
            // butCancel
            // 
            this.butCancel.AutoSize = true;
            this.butCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butCancel.Depth = 0;
            this.butCancel.Icon = null;
            this.butCancel.Location = new System.Drawing.Point(211, 273);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.butCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.butCancel.Name = "butCancel";
            this.butCancel.Primary = false;
            this.butCancel.Size = new System.Drawing.Size(73, 36);
            this.butCancel.TabIndex = 26;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(101, 337);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(113, 18);
            this.materialLabel3.TabIndex = 27;
            this.materialLabel3.Text = "Program Status";
            // 
            // label_status
            // 
            this.label_status.Depth = 0;
            this.label_status.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_status.Location = new System.Drawing.Point(16, 355);
            this.label_status.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(306, 33);
            this.label_status.TabIndex = 28;
            this.label_status.Text = "Loading Manifest...";
            this.label_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoProg
            // 
            this.infoProg.Depth = 0;
            this.infoProg.Location = new System.Drawing.Point(16, 391);
            this.infoProg.MouseState = MaterialSkin.MouseState.HOVER;
            this.infoProg.Name = "infoProg";
            this.infoProg.Size = new System.Drawing.Size(306, 5);
            this.infoProg.TabIndex = 29;
            // 
            // label_MB
            // 
            this.label_MB.AutoSize = true;
            this.label_MB.Depth = 0;
            this.label_MB.Font = new System.Drawing.Font("Roboto", 11F);
            this.label_MB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_MB.Location = new System.Drawing.Point(118, 410);
            this.label_MB.MouseState = MaterialSkin.MouseState.HOVER;
            this.label_MB.Name = "label_MB";
            this.label_MB.Size = new System.Drawing.Size(76, 19);
            this.label_MB.TabIndex = 30;
            this.label_MB.Text = "N/A MB/s";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(13, 462);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(281, 17);
            this.materialLabel4.TabIndex = 31;
            this.materialLabel4.Text = "Developed by Hawx. Based on work by NimmoG.";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(13, 495);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(226, 17);
            this.materialLabel5.TabIndex = 32;
            this.materialLabel5.Text = "Supported by Zephyr Auxiliary Services";
            // 
            // gitButton
            // 
            this.gitButton.AutoSize = true;
            this.gitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gitButton.Depth = 0;
            this.gitButton.Icon = null;
            this.gitButton.Location = new System.Drawing.Point(246, 485);
            this.gitButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.gitButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.gitButton.Name = "gitButton";
            this.gitButton.Primary = false;
            this.gitButton.Size = new System.Drawing.Size(70, 36);
            this.gitButton.TabIndex = 33;
            this.gitButton.Text = "Github";
            this.gitButton.UseVisualStyleBackColor = true;
            this.gitButton.Click += new System.EventHandler(this.gitButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 530);
            this.Controls.Add(this.gitButton);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.label_MB);
            this.Controls.Add(this.infoProg);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.downloadSrt);
            this.Controls.Add(this.browseDir);
            this.Controls.Add(this.check_nativefile);
            this.Controls.Add(this.downloadDir);
            this.Controls.Add(this.releaseSelect);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.relSelector);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(345, 530);
            this.Name = "MainWindow";
            this.Text = "Star Citizen Alternative Patcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox relSelector;
        private System.Windows.Forms.ToolTip toolTip_check;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialFlatButton releaseSelect;
        private MaterialSkin.Controls.MaterialSingleLineTextField downloadDir;
        private MaterialSkin.Controls.MaterialCheckBox check_nativefile;
        private MaterialSkin.Controls.MaterialRaisedButton browseDir;
        private MaterialSkin.Controls.MaterialRaisedButton downloadSrt;
        private MaterialSkin.Controls.MaterialFlatButton butCancel;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel label_status;
        private MaterialSkin.Controls.MaterialProgressBar infoProg;
        private MaterialSkin.Controls.MaterialLabel label_MB;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialFlatButton gitButton;
    }
}

