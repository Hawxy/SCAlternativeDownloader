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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.comboReleaseSelector = new System.Windows.Forms.ComboBox();
            this.labelRT = new MaterialSkin.Controls.MaterialLabel();
            this.DirLabel = new MaterialSkin.Controls.MaterialLabel();
            this.textBoxDownloadDirectory = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.checkBoxNativeFile = new MaterialSkin.Controls.MaterialCheckBox();
            this.buttonBrowseDirectory = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonDownloadStart = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonCancel = new MaterialSkin.Controls.MaterialFlatButton();
            this.labelPS = new MaterialSkin.Controls.MaterialLabel();
            this.labelCurrentStatus = new MaterialSkin.Controls.MaterialLabel();
            this.progressBarStatus = new MaterialSkin.Controls.MaterialProgressBar();
            this.labelMegaBytes = new MaterialSkin.Controls.MaterialLabel();
            this.labelDBH = new MaterialSkin.Controls.MaterialLabel();
            this.labelZAS = new MaterialSkin.Controls.MaterialLabel();
            this.buttonGithub = new MaterialSkin.Controls.MaterialFlatButton();
            this.toolTip_Native = new MaterialSkin.Controls.MaterialToolTip(this.components);
            this.progressBarFile = new MaterialSkin.Controls.MaterialProgressBar();
            this.labelCurrentFile = new MaterialSkin.Controls.MaterialLabel();
            this.customBuildSelect = new MaterialSkin.Controls.MaterialFlatButton();
            this.SuspendLayout();
            // 
            // comboReleaseSelector
            // 
            this.comboReleaseSelector.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboReleaseSelector.FormattingEnabled = true;
            this.comboReleaseSelector.Location = new System.Drawing.Point(15, 105);
            this.comboReleaseSelector.Name = "comboReleaseSelector";
            this.comboReleaseSelector.Size = new System.Drawing.Size(141, 25);
            this.comboReleaseSelector.TabIndex = 0;
            this.comboReleaseSelector.SelectedIndexChanged += new System.EventHandler(this.comboReleaseSelector_SelectedIndexChanged);
            // 
            // labelRT
            // 
            this.labelRT.AutoSize = true;
            this.labelRT.Depth = 0;
            this.labelRT.Font = new System.Drawing.Font("Roboto Medium", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelRT.Location = new System.Drawing.Point(12, 74);
            this.labelRT.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelRT.Name = "labelRT";
            this.labelRT.Size = new System.Drawing.Size(142, 18);
            this.labelRT.TabIndex = 18;
            this.labelRT.Text = "Select Release Type";
            this.labelRT.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // DirLabel
            // 
            this.DirLabel.AutoSize = true;
            this.DirLabel.Depth = 0;
            this.DirLabel.Font = new System.Drawing.Font("Roboto Medium", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DirLabel.Location = new System.Drawing.Point(12, 153);
            this.DirLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.DirLabel.Name = "DirLabel";
            this.DirLabel.Size = new System.Drawing.Size(115, 18);
            this.DirLabel.TabIndex = 19;
            this.DirLabel.Text = "Select Directory";
            this.DirLabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // textBoxDownloadDirectory
            // 
            this.textBoxDownloadDirectory.Depth = 0;
            this.textBoxDownloadDirectory.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDownloadDirectory.Hint = "";
            this.textBoxDownloadDirectory.Location = new System.Drawing.Point(12, 184);
            this.textBoxDownloadDirectory.MaxLength = 32767;
            this.textBoxDownloadDirectory.MouseState = MaterialSkin.MouseState.HOVER;
            this.textBoxDownloadDirectory.Name = "textBoxDownloadDirectory";
            this.textBoxDownloadDirectory.PasswordChar = '\0';
            this.textBoxDownloadDirectory.SelectedText = "";
            this.textBoxDownloadDirectory.SelectionLength = 0;
            this.textBoxDownloadDirectory.SelectionStart = 0;
            this.textBoxDownloadDirectory.Size = new System.Drawing.Size(268, 23);
            this.textBoxDownloadDirectory.TabIndex = 21;
            this.textBoxDownloadDirectory.TabStop = false;
            this.textBoxDownloadDirectory.UseSystemPasswordChar = false;
            // 
            // checkBoxNativeFile
            // 
            this.checkBoxNativeFile.AutoSize = true;
            this.checkBoxNativeFile.Checked = true;
            this.checkBoxNativeFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNativeFile.Depth = 0;
            this.checkBoxNativeFile.Font = new System.Drawing.Font("Roboto", 10F);
            this.checkBoxNativeFile.Location = new System.Drawing.Point(16, 222);
            this.checkBoxNativeFile.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxNativeFile.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBoxNativeFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBoxNativeFile.Name = "checkBoxNativeFile";
            this.checkBoxNativeFile.Ripple = true;
            this.checkBoxNativeFile.Size = new System.Drawing.Size(213, 30);
            this.checkBoxNativeFile.TabIndex = 23;
            this.checkBoxNativeFile.Text = "Preserve Native File Structure";
            this.checkBoxNativeFile.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseDirectory
            // 
            this.buttonBrowseDirectory.AutoSize = true;
            this.buttonBrowseDirectory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonBrowseDirectory.Depth = 0;
            this.buttonBrowseDirectory.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowseDirectory.Icon = null;
            this.buttonBrowseDirectory.Location = new System.Drawing.Point(290, 184);
            this.buttonBrowseDirectory.MaximumSize = new System.Drawing.Size(32, 23);
            this.buttonBrowseDirectory.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonBrowseDirectory.Name = "buttonBrowseDirectory";
            this.buttonBrowseDirectory.Primary = true;
            this.buttonBrowseDirectory.Size = new System.Drawing.Size(32, 23);
            this.buttonBrowseDirectory.TabIndex = 24;
            this.buttonBrowseDirectory.Text = "...";
            this.buttonBrowseDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseDirectory.Click += new System.EventHandler(this.BrowseDirectoryButtonClick);
            // 
            // buttonDownloadStart
            // 
            this.buttonDownloadStart.AutoSize = true;
            this.buttonDownloadStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDownloadStart.Depth = 0;
            this.buttonDownloadStart.Enabled = false;
            this.buttonDownloadStart.Icon = null;
            this.buttonDownloadStart.Location = new System.Drawing.Point(32, 273);
            this.buttonDownloadStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonDownloadStart.Name = "buttonDownloadStart";
            this.buttonDownloadStart.Primary = true;
            this.buttonDownloadStart.Size = new System.Drawing.Size(96, 36);
            this.buttonDownloadStart.TabIndex = 25;
            this.buttonDownloadStart.Text = "Download";
            this.buttonDownloadStart.UseVisualStyleBackColor = true;
            this.buttonDownloadStart.Click += new System.EventHandler(this.DownloadStartButtonClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCancel.Depth = 0;
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Icon = null;
            this.buttonCancel.Location = new System.Drawing.Point(211, 273);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Primary = false;
            this.buttonCancel.Size = new System.Drawing.Size(73, 36);
            this.buttonCancel.TabIndex = 26;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // labelPS
            // 
            this.labelPS.AutoSize = true;
            this.labelPS.Depth = 0;
            this.labelPS.Font = new System.Drawing.Font("Roboto Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPS.Location = new System.Drawing.Point(101, 337);
            this.labelPS.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelPS.Name = "labelPS";
            this.labelPS.Size = new System.Drawing.Size(128, 18);
            this.labelPS.TabIndex = 27;
            this.labelPS.Text = "Program Status";
            this.labelPS.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // labelCurrentStatus
            // 
            this.labelCurrentStatus.Depth = 0;
            this.labelCurrentStatus.Font = new System.Drawing.Font("Roboto Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCurrentStatus.Location = new System.Drawing.Point(16, 355);
            this.labelCurrentStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCurrentStatus.Name = "labelCurrentStatus";
            this.labelCurrentStatus.Size = new System.Drawing.Size(306, 33);
            this.labelCurrentStatus.TabIndex = 28;
            this.labelCurrentStatus.Text = "Loading Manifest...";
            this.labelCurrentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCurrentStatus.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Depth = 0;
            this.progressBarStatus.Location = new System.Drawing.Point(16, 391);
            this.progressBarStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(306, 5);
            this.progressBarStatus.TabIndex = 29;
            // 
            // labelMegaBytes
            // 
            this.labelMegaBytes.AutoSize = true;
            this.labelMegaBytes.Depth = 0;
            this.labelMegaBytes.Font = new System.Drawing.Font("Roboto Medium", 11F);
            this.labelMegaBytes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelMegaBytes.Location = new System.Drawing.Point(134, 465);
            this.labelMegaBytes.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelMegaBytes.Name = "labelMegaBytes";
            this.labelMegaBytes.Size = new System.Drawing.Size(75, 18);
            this.labelMegaBytes.TabIndex = 30;
            this.labelMegaBytes.Text = "N/A MB/s";
            this.labelMegaBytes.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // labelDBH
            // 
            this.labelDBH.Depth = 0;
            this.labelDBH.Font = new System.Drawing.Font("Roboto Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDBH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelDBH.Location = new System.Drawing.Point(16, 507);
            this.labelDBH.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelDBH.Name = "labelDBH";
            this.labelDBH.Size = new System.Drawing.Size(120, 19);
            this.labelDBH.TabIndex = 31;
            this.labelDBH.Text = "Developed by Hawx";
            this.labelDBH.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // labelZAS
            // 
            this.labelZAS.Depth = 0;
            this.labelZAS.Font = new System.Drawing.Font("Roboto Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZAS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelZAS.Location = new System.Drawing.Point(16, 540);
            this.labelZAS.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelZAS.Name = "labelZAS";
            this.labelZAS.Size = new System.Drawing.Size(226, 19);
            this.labelZAS.TabIndex = 32;
            this.labelZAS.Text = "Supported by Zephyr Auxiliary Services";
            this.labelZAS.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // buttonGithub
            // 
            this.buttonGithub.AutoSize = true;
            this.buttonGithub.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonGithub.Depth = 0;
            this.buttonGithub.Icon = null;
            this.buttonGithub.Location = new System.Drawing.Point(236, 528);
            this.buttonGithub.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonGithub.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonGithub.Name = "buttonGithub";
            this.buttonGithub.Primary = false;
            this.buttonGithub.Size = new System.Drawing.Size(70, 36);
            this.buttonGithub.TabIndex = 33;
            this.buttonGithub.Text = "Github";
            this.buttonGithub.UseVisualStyleBackColor = true;
            this.buttonGithub.Click += new System.EventHandler(this.GitHubButtonClick);
            // 
            // toolTip_Native
            // 
            this.toolTip_Native.Depth = 0;
            this.toolTip_Native.MouseState = MaterialSkin.MouseState.HOVER;
            this.toolTip_Native.OwnerDraw = true;
            // 
            // progressBarFile
            // 
            this.progressBarFile.Depth = 0;
            this.progressBarFile.Location = new System.Drawing.Point(15, 457);
            this.progressBarFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.progressBarFile.Name = "progressBarFile";
            this.progressBarFile.Size = new System.Drawing.Size(306, 5);
            this.progressBarFile.TabIndex = 34;
            // 
            // labelCurrentFile
            // 
            this.labelCurrentFile.Depth = 0;
            this.labelCurrentFile.Font = new System.Drawing.Font("Roboto Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCurrentFile.Location = new System.Drawing.Point(16, 402);
            this.labelCurrentFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCurrentFile.Name = "labelCurrentFile";
            this.labelCurrentFile.Size = new System.Drawing.Size(305, 52);
            this.labelCurrentFile.TabIndex = 35;
            this.labelCurrentFile.Text = "...";
            this.labelCurrentFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCurrentFile.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // customBuildSelect
            // 
            this.customBuildSelect.AutoSize = true;
            this.customBuildSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customBuildSelect.Depth = 0;
            this.customBuildSelect.Icon = null;
            this.customBuildSelect.Location = new System.Drawing.Point(163, 98);
            this.customBuildSelect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.customBuildSelect.MouseState = MaterialSkin.MouseState.HOVER;
            this.customBuildSelect.Name = "customBuildSelect";
            this.customBuildSelect.Primary = false;
            this.customBuildSelect.Size = new System.Drawing.Size(158, 36);
            this.customBuildSelect.TabIndex = 36;
            this.customBuildSelect.Text = "Use custom build...";
            this.customBuildSelect.UseVisualStyleBackColor = true;
            this.customBuildSelect.Click += new System.EventHandler(this.customBuildSelect_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 571);
            this.Controls.Add(this.customBuildSelect);
            this.Controls.Add(this.labelCurrentFile);
            this.Controls.Add(this.progressBarFile);
            this.Controls.Add(this.buttonGithub);
            this.Controls.Add(this.labelZAS);
            this.Controls.Add(this.labelDBH);
            this.Controls.Add(this.labelMegaBytes);
            this.Controls.Add(this.progressBarStatus);
            this.Controls.Add(this.labelCurrentStatus);
            this.Controls.Add(this.labelPS);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonDownloadStart);
            this.Controls.Add(this.buttonBrowseDirectory);
            this.Controls.Add(this.checkBoxNativeFile);
            this.Controls.Add(this.textBoxDownloadDirectory);
            this.Controls.Add(this.DirLabel);
            this.Controls.Add(this.labelRT);
            this.Controls.Add(this.comboReleaseSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(345, 571);
            this.Name = "MainWindow";
            this.Text = "Star Citizen Alternative Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboReleaseSelector;
        private MaterialSkin.Controls.MaterialLabel labelRT;
        private MaterialSkin.Controls.MaterialLabel DirLabel;
        private MaterialSkin.Controls.MaterialSingleLineTextField textBoxDownloadDirectory;
        private MaterialSkin.Controls.MaterialCheckBox checkBoxNativeFile;
        private MaterialSkin.Controls.MaterialRaisedButton buttonBrowseDirectory;
        private MaterialSkin.Controls.MaterialRaisedButton buttonDownloadStart;
        private MaterialSkin.Controls.MaterialFlatButton buttonCancel;
        private MaterialSkin.Controls.MaterialLabel labelPS;
        private MaterialSkin.Controls.MaterialLabel labelCurrentStatus;
        private MaterialSkin.Controls.MaterialProgressBar progressBarStatus;
        private MaterialSkin.Controls.MaterialLabel labelMegaBytes;
        private MaterialSkin.Controls.MaterialLabel labelDBH;
        private MaterialSkin.Controls.MaterialLabel labelZAS;
        private MaterialSkin.Controls.MaterialFlatButton buttonGithub;
        private MaterialSkin.Controls.MaterialToolTip toolTip_Native;
        private MaterialSkin.Controls.MaterialProgressBar progressBarFile;
        private MaterialSkin.Controls.MaterialLabel labelCurrentFile;
        private MaterialSkin.Controls.MaterialFlatButton customBuildSelect;
    }
}

