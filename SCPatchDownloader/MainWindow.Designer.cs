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
            this.downloadDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.browseDir = new System.Windows.Forms.Button();
            this.downloadSrt = new System.Windows.Forms.Button();
            this.infoProg = new System.Windows.Forms.ProgressBar();
            this.label_status = new System.Windows.Forms.Label();
            this.releaseSelect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.label_MB = new System.Windows.Forms.Label();
            this.check_nativefile = new System.Windows.Forms.CheckBox();
            this.toolTip_check = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // relSelector
            // 
            this.relSelector.FormattingEnabled = true;
            this.relSelector.Location = new System.Drawing.Point(29, 51);
            this.relSelector.Name = "relSelector";
            this.relSelector.Size = new System.Drawing.Size(121, 21);
            this.relSelector.TabIndex = 0;
            // 
            // downloadDir
            // 
            this.downloadDir.Location = new System.Drawing.Point(29, 119);
            this.downloadDir.Name = "downloadDir";
            this.downloadDir.Size = new System.Drawing.Size(217, 20);
            this.downloadDir.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Step One - Select Release Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Step Two - Confirm Directory";
            // 
            // browseDir
            // 
            this.browseDir.Location = new System.Drawing.Point(252, 119);
            this.browseDir.Name = "browseDir";
            this.browseDir.Size = new System.Drawing.Size(33, 21);
            this.browseDir.TabIndex = 6;
            this.browseDir.Text = "...";
            this.browseDir.UseVisualStyleBackColor = true;
            this.browseDir.Click += new System.EventHandler(this.browseDir_Click);
            // 
            // downloadSrt
            // 
            this.downloadSrt.Enabled = false;
            this.downloadSrt.Location = new System.Drawing.Point(64, 208);
            this.downloadSrt.Name = "downloadSrt";
            this.downloadSrt.Size = new System.Drawing.Size(75, 23);
            this.downloadSrt.TabIndex = 7;
            this.downloadSrt.Text = "Download";
            this.downloadSrt.UseVisualStyleBackColor = true;
            this.downloadSrt.Click += new System.EventHandler(this.downloadSrt_Click);
            // 
            // infoProg
            // 
            this.infoProg.Location = new System.Drawing.Point(29, 295);
            this.infoProg.Name = "infoProg";
            this.infoProg.Size = new System.Drawing.Size(256, 23);
            this.infoProg.TabIndex = 8;
            // 
            // label_status
            // 
            this.label_status.Location = new System.Drawing.Point(29, 268);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(256, 24);
            this.label_status.TabIndex = 9;
            this.label_status.Text = "Loading Manifest...";
            this.label_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // releaseSelect
            // 
            this.releaseSelect.Location = new System.Drawing.Point(177, 43);
            this.releaseSelect.Name = "releaseSelect";
            this.releaseSelect.Size = new System.Drawing.Size(98, 34);
            this.releaseSelect.TabIndex = 10;
            this.releaseSelect.Text = "Select Release";
            this.releaseSelect.UseVisualStyleBackColor = true;
            this.releaseSelect.Click += new System.EventHandler(this.releaseSelect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(105, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Program Status";
            // 
            // butCancel
            // 
            this.butCancel.Enabled = false;
            this.butCancel.Location = new System.Drawing.Point(171, 208);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 12;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // label_MB
            // 
            this.label_MB.Location = new System.Drawing.Point(105, 321);
            this.label_MB.Name = "label_MB";
            this.label_MB.Size = new System.Drawing.Size(100, 23);
            this.label_MB.TabIndex = 13;
            this.label_MB.Text = "N/A MB/s";
            this.label_MB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // check_nativefile
            // 
            this.check_nativefile.AutoSize = true;
            this.check_nativefile.Location = new System.Drawing.Point(64, 156);
            this.check_nativefile.Name = "check_nativefile";
            this.check_nativefile.Size = new System.Drawing.Size(167, 17);
            this.check_nativefile.TabIndex = 14;
            this.check_nativefile.Text = "Preserve Native File Structure";
            this.check_nativefile.UseVisualStyleBackColor = true;
            // 
            // toolTip_check
            // 
            this.toolTip_check.IsBalloon = true;
            this.toolTip_check.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip_check.ToolTipTitle = "Information";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(342, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Developed by Hawx. Based on VB.NET downloader by NimmoG";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 429);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Supported by Zephyr Auxilary Services";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 466);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.check_nativefile);
            this.Controls.Add(this.label_MB);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.releaseSelect);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.infoProg);
            this.Controls.Add(this.downloadSrt);
            this.Controls.Add(this.browseDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.downloadDir);
            this.Controls.Add(this.relSelector);
            this.Name = "MainWindow";
            this.Text = "Star Citizen Alternative Patcher - C# Edition";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox relSelector;
        private System.Windows.Forms.TextBox downloadDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseDir;
        private System.Windows.Forms.Button downloadSrt;
        private System.Windows.Forms.ProgressBar infoProg;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button releaseSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label label_MB;
        private System.Windows.Forms.CheckBox check_nativefile;
        private System.Windows.Forms.ToolTip toolTip_check;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

