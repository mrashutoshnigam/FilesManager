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
            this.components = new System.ComponentModel.Container();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxPathFormat = new System.Windows.Forms.ComboBox();
            this.txtBoxDestination = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.visualStudioToolStripExtender1 = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtBoxDestination);
            this.groupBox1.Controls.Add(this.cBoxPathFormat);
            this.groupBox1.Location = new System.Drawing.Point(30, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination Path";
            // 
            // cBoxPathFormat
            // 
            this.cBoxPathFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxPathFormat.FormattingEnabled = true;
            this.cBoxPathFormat.Items.AddRange(new object[] {
            "Year",
            "Year/Month",
            "Year/Month/Date",
            "Year/File Types/",
            "Year/Month/File Types",
            "File Types",
            "File Types/Year",
            "File Types/Year/Month"});
            this.cBoxPathFormat.Location = new System.Drawing.Point(153, 53);
            this.cBoxPathFormat.Name = "cBoxPathFormat";
            this.cBoxPathFormat.Size = new System.Drawing.Size(230, 21);
            this.cBoxPathFormat.TabIndex = 0;
            // 
            // txtBoxDestination
            // 
            this.txtBoxDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtBoxDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtBoxDestination.Location = new System.Drawing.Point(153, 23);
            this.txtBoxDestination.Name = "txtBoxDestination";
            this.txtBoxDestination.Size = new System.Drawing.Size(230, 20);
            this.txtBoxDestination.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(399, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(50, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // visualStudioToolStripExtender1
            // 
            this.visualStudioToolStripExtender1.DefaultRenderer = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Destination Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destination Folder Structure";
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBoxPathFormat;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBoxDestination;
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender visualStudioToolStripExtender1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

