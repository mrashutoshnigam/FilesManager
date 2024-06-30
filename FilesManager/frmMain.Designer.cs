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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.clmHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDateCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLblFilePath = new System.Windows.Forms.ToolStripLabel();
            this.tStripBtnProceedToCopy = new System.Windows.Forms.ToolStripButton();
            this.FileprogressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewFileExtensions = new System.Windows.Forms.DataGridView();
            this.chrtControl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCopyMoveToogleButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkGooglePhotosMetaData = new System.Windows.Forms.CheckBox();
            this.chkIncludeFileName = new System.Windows.Forms.CheckBox();
            this.chkIncludeSubFolders = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxFileTypes = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonBreadCrumb1 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumb();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cBoxPathFormat = new System.Windows.Forms.ComboBox();
            this.kryptonBreadCrumbItem1 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem6 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem2 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem3 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem4 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem5 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.visualStudioToolStripExtender1 = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.kryptonBreadCrumbItem7 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem8 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.dataGridViewErrorList = new System.Windows.Forms.DataGridView();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileExtensions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxFileTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonBreadCrumb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrorList)).BeginInit();
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
            this.listViewFiles.Size = new System.Drawing.Size(567, 1016);
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
            this.toolStripMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLblFilePath,
            this.tStripBtnProceedToCopy,
            this.FileprogressBar,
            this.btnStop});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1904, 25);
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
            this.tStripBtnProceedToCopy.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tStripBtnProceedToCopy.Name = "tStripBtnProceedToCopy";
            this.tStripBtnProceedToCopy.Size = new System.Drawing.Size(95, 22);
            this.tStripBtnProceedToCopy.Text = "Arrange Files";
            this.tStripBtnProceedToCopy.Click += new System.EventHandler(this.tStripBtnProceedToCopy_Click);
            // 
            // FileprogressBar
            // 
            this.FileprogressBar.Name = "FileprogressBar";
            this.FileprogressBar.Size = new System.Drawing.Size(300, 22);
            this.FileprogressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::FilesManager.Properties.Resources.Stop;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewErrorList);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewFileExtensions);
            this.splitContainer1.Panel2.Controls.Add(this.chrtControl);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1904, 1016);
            this.splitContainer1.SplitterDistance = 567;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridViewFileExtensions
            // 
            this.dataGridViewFileExtensions.AllowUserToAddRows = false;
            this.dataGridViewFileExtensions.AllowUserToDeleteRows = false;
            this.dataGridViewFileExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFileExtensions.Location = new System.Drawing.Point(850, 14);
            this.dataGridViewFileExtensions.Name = "dataGridViewFileExtensions";
            this.dataGridViewFileExtensions.ReadOnly = true;
            this.dataGridViewFileExtensions.Size = new System.Drawing.Size(310, 667);
            this.dataGridViewFileExtensions.TabIndex = 14;
            // 
            // chrtControl
            // 
            this.chrtControl.BorderlineWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.chrtControl.ChartAreas.Add(chartArea1);
            this.chrtControl.IsSoftShadows = false;
            legend1.Name = "Legend1";
            this.chrtControl.Legends.Add(legend1);
            this.chrtControl.Location = new System.Drawing.Point(30, 193);
            this.chrtControl.Name = "chrtControl";
            this.chrtControl.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chrtControl.Series.Add(series1);
            this.chrtControl.Size = new System.Drawing.Size(798, 488);
            this.chrtControl.TabIndex = 1;
            this.chrtControl.Text = "chart1";
            this.chrtControl.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCopyMoveToogleButton);
            this.groupBox1.Controls.Add(this.chkGooglePhotosMetaData);
            this.groupBox1.Controls.Add(this.chkIncludeFileName);
            this.groupBox1.Controls.Add(this.chkIncludeSubFolders);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxFileTypes);
            this.groupBox1.Controls.Add(this.kryptonBreadCrumb1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.cBoxPathFormat);
            this.groupBox1.Location = new System.Drawing.Point(30, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination Path";
            // 
            // btnCopyMoveToogleButton
            // 
            this.btnCopyMoveToogleButton.Location = new System.Drawing.Point(153, 126);
            this.btnCopyMoveToogleButton.Name = "btnCopyMoveToogleButton";
            this.btnCopyMoveToogleButton.Size = new System.Drawing.Size(90, 25);
            this.btnCopyMoveToogleButton.TabIndex = 12;
            this.btnCopyMoveToogleButton.Values.Text = "Copy";
            this.btnCopyMoveToogleButton.Click += new System.EventHandler(this.btnCopyMoveToogleButton_Click);
            // 
            // chkGooglePhotosMetaData
            // 
            this.chkGooglePhotosMetaData.AutoSize = true;
            this.chkGooglePhotosMetaData.Location = new System.Drawing.Point(514, 119);
            this.chkGooglePhotosMetaData.Name = "chkGooglePhotosMetaData";
            this.chkGooglePhotosMetaData.Size = new System.Drawing.Size(179, 17);
            this.chkGooglePhotosMetaData.TabIndex = 10;
            this.chkGooglePhotosMetaData.Text = "Merge Google Photos MetaData";
            this.chkGooglePhotosMetaData.UseVisualStyleBackColor = true;
            this.chkGooglePhotosMetaData.CheckedChanged += new System.EventHandler(this.chkGooglePhotosMetaData_CheckedChanged);
            // 
            // chkIncludeFileName
            // 
            this.chkIncludeFileName.AutoSize = true;
            this.chkIncludeFileName.Location = new System.Drawing.Point(514, 92);
            this.chkIncludeFileName.Name = "chkIncludeFileName";
            this.chkIncludeFileName.Size = new System.Drawing.Size(149, 17);
            this.chkIncludeFileName.TabIndex = 9;
            this.chkIncludeFileName.Text = "Include Original File Name";
            this.chkIncludeFileName.UseVisualStyleBackColor = true;
            // 
            // chkIncludeSubFolders
            // 
            this.chkIncludeSubFolders.AutoSize = true;
            this.chkIncludeSubFolders.Location = new System.Drawing.Point(514, 65);
            this.chkIncludeSubFolders.Name = "chkIncludeSubFolders";
            this.chkIncludeSubFolders.Size = new System.Drawing.Size(120, 17);
            this.chkIncludeSubFolders.TabIndex = 8;
            this.chkIncludeSubFolders.Text = "Include Sub Folders";
            this.chkIncludeSubFolders.UseVisualStyleBackColor = true;
            this.chkIncludeSubFolders.CheckedChanged += new System.EventHandler(this.chkIncludeSubFolders_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "File Types";
            // 
            // comboBoxFileTypes
            // 
            this.comboBoxFileTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFileTypes.DropDownWidth = 230;
            this.comboBoxFileTypes.Location = new System.Drawing.Point(153, 96);
            this.comboBoxFileTypes.Name = "comboBoxFileTypes";
            this.comboBoxFileTypes.Size = new System.Drawing.Size(230, 21);
            this.comboBoxFileTypes.TabIndex = 6;
            this.comboBoxFileTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxFileTypes_SelectedIndexChanged);
            // 
            // kryptonBreadCrumb1
            // 
            this.kryptonBreadCrumb1.AutoSize = false;
            this.kryptonBreadCrumb1.Location = new System.Drawing.Point(153, 19);
            this.kryptonBreadCrumb1.Name = "kryptonBreadCrumb1";
            // 
            // 
            // 
            this.kryptonBreadCrumb1.RootItem.ShortText = "Root";
            this.kryptonBreadCrumb1.SelectedItem = this.kryptonBreadCrumb1.RootItem;
            this.kryptonBreadCrumb1.Size = new System.Drawing.Size(565, 28);
            this.kryptonBreadCrumb1.TabIndex = 5;
            this.kryptonBreadCrumb1.DoubleClick += new System.EventHandler(this.kryptonBreadCrumb1_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destination Folder Structure";
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
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(724, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(50, 28);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cBoxPathFormat
            // 
            this.cBoxPathFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxPathFormat.FormattingEnabled = true;
            this.cBoxPathFormat.Items.AddRange(new object[] {
            "Year/",
            "Year/Month/",
            "Year/Month/Day/",
            "Year/File Types/",
            "Year/Month/File Types/",
            "Year/Month/Day/File Types/",
            "File Types/",
            "File Types/Year/",
            "File Types/Year/Month/"});
            this.cBoxPathFormat.Location = new System.Drawing.Point(153, 61);
            this.cBoxPathFormat.Name = "cBoxPathFormat";
            this.cBoxPathFormat.Size = new System.Drawing.Size(230, 21);
            this.cBoxPathFormat.TabIndex = 0;
            // 
            // kryptonBreadCrumbItem1
            // 
            this.kryptonBreadCrumbItem1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem6});
            this.kryptonBreadCrumbItem1.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem6
            // 
            this.kryptonBreadCrumbItem6.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem2
            // 
            this.kryptonBreadCrumbItem2.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem3});
            this.kryptonBreadCrumbItem2.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem3
            // 
            this.kryptonBreadCrumbItem3.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem4});
            this.kryptonBreadCrumbItem3.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem4
            // 
            this.kryptonBreadCrumbItem4.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem5});
            this.kryptonBreadCrumbItem4.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem5
            // 
            this.kryptonBreadCrumbItem5.ShortText = "ListItem";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // visualStudioToolStripExtender1
            // 
            this.visualStudioToolStripExtender1.DefaultRenderer = null;
            // 
            // kryptonBreadCrumbItem7
            // 
            this.kryptonBreadCrumbItem7.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem8
            // 
            this.kryptonBreadCrumbItem8.ShortText = "ListItem";
            // 
            // dataGridViewErrorList
            // 
            this.dataGridViewErrorList.AllowUserToAddRows = false;
            this.dataGridViewErrorList.AllowUserToDeleteRows = false;
            this.dataGridViewErrorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewErrorList.Location = new System.Drawing.Point(30, 688);
            this.dataGridViewErrorList.Name = "dataGridViewErrorList";
            this.dataGridViewErrorList.ReadOnly = true;
            this.dataGridViewErrorList.Size = new System.Drawing.Size(1130, 199);
            this.dataGridViewErrorList.TabIndex = 15;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMain);
            this.Name = "frmMain";
            this.Text = "Main Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileExtensions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxFileTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonBreadCrumb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrorList)).EndInit();
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
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender visualStudioToolStripExtender1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem6;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem2;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem3;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem4;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem5;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem7;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem8;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumb kryptonBreadCrumb1;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox comboBoxFileTypes;
        private System.Windows.Forms.CheckBox chkIncludeSubFolders;
        private System.Windows.Forms.CheckBox chkIncludeFileName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtControl;
        private System.Windows.Forms.CheckBox chkGooglePhotosMetaData;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCopyMoveToogleButton;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.DataGridView dataGridViewFileExtensions;
        private System.Windows.Forms.DataGridView dataGridViewErrorList;
    }
}

