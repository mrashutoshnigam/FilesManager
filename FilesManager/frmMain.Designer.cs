namespace FilesManager
{
    partial class frmMain
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
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.clmHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDateCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLblFilePath = new System.Windows.Forms.ToolStripLabel();
            this.tStripBtnProceedToCopy = new System.Windows.Forms.ToolStripButton();
            this.FileprogressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmHeaderName,
            this.clmHeaderType,
            this.clmHeaderSize,
            this.clmDateCreated});
            this.listViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(0, 0);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(429, 926);
            this.listViewFiles.TabIndex = 0;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            this.listViewFiles.DoubleClick += new System.EventHandler(this.listViewFiles_DoubleClick);
            // 
            // clmHeaderName
            // 
            this.clmHeaderName.Text = "Name";
            this.clmHeaderName.Width = 246;
            // 
            // clmHeaderType
            // 
            this.clmHeaderType.Text = "Type";
            this.clmHeaderType.Width = 64;
            // 
            // clmHeaderSize
            // 
            this.clmHeaderSize.Text = "Size";
            this.clmHeaderSize.Width = 100;
            // 
            // clmDateCreated
            // 
            this.clmDateCreated.Text = "Date Created";
            this.clmDateCreated.Width = 200;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLblFilePath,
            this.tStripBtnProceedToCopy,
            this.FileprogressBar});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1288, 25);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLblFilePath
            // 
            this.toolStripLblFilePath.DoubleClickEnabled = true;
            this.toolStripLblFilePath.Image = global::FilesManager.Properties.Resources.Opened_Folder;
            this.toolStripLblFilePath.Name = "toolStripLblFilePath";
            this.toolStripLblFilePath.Size = new System.Drawing.Size(65, 22);
            this.toolStripLblFilePath.Text = "FilePath";
            this.toolStripLblFilePath.DoubleClick += new System.EventHandler(this.toolStripLblFilePath_DoubleClick);
            // 
            // tStripBtnProceedToCopy
            // 
            this.tStripBtnProceedToCopy.Image = global::FilesManager.Properties.Resources.Next;
            this.tStripBtnProceedToCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripBtnProceedToCopy.Name = "tStripBtnProceedToCopy";
            this.tStripBtnProceedToCopy.Size = new System.Drawing.Size(70, 22);
            this.tStripBtnProceedToCopy.Text = "Proceed";
            this.tStripBtnProceedToCopy.Click += new System.EventHandler(this.tStripBtnProceedToCopy_Click);
            // 
            // FileprogressBar
            // 
            this.FileprogressBar.Name = "FileprogressBar";
            this.FileprogressBar.Size = new System.Drawing.Size(100, 22);
            this.FileprogressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewFiles);
            this.splitContainer1.Size = new System.Drawing.Size(1288, 926);
            this.splitContainer1.SplitterDistance = 429;
            this.splitContainer1.TabIndex = 2;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 951);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMain);
            this.Name = "frmMain";
            this.Text = "Main Form";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader clmHeaderName;
        private System.Windows.Forms.ColumnHeader clmHeaderType;
        private System.Windows.Forms.ColumnHeader clmHeaderSize;
        private System.Windows.Forms.ColumnHeader clmDateCreated;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripLabel toolStripLblFilePath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton tStripBtnProceedToCopy;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripProgressBar FileprogressBar;
    }
}

