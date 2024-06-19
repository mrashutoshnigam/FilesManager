namespace FilesManager
{
    partial class frmFolderBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFolderBrowser));
            this.trViewFolders = new System.Windows.Forms.TreeView();
            this.imageLst = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // trViewFolders
            // 
            this.trViewFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trViewFolders.ImageIndex = 0;
            this.trViewFolders.ImageList = this.imageLst;
            this.trViewFolders.Location = new System.Drawing.Point(0, 0);
            this.trViewFolders.Name = "trViewFolders";
            this.trViewFolders.SelectedImageIndex = 0;
            this.trViewFolders.Size = new System.Drawing.Size(357, 884);
            this.trViewFolders.TabIndex = 0;
            this.trViewFolders.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trViewFolders_AfterExpand);
            this.trViewFolders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trViewFolders_MouseDoubleClick);
            // 
            // imageLst
            // 
            this.imageLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageLst.ImageStream")));
            this.imageLst.TransparentColor = System.Drawing.Color.Transparent;
            this.imageLst.Images.SetKeyName(0, "Workstation.png");
            this.imageLst.Images.SetKeyName(1, "HDD.png");
            this.imageLst.Images.SetKeyName(2, "USB Connected.png");
            this.imageLst.Images.SetKeyName(3, "Opened Folder.png");
            this.imageLst.Images.SetKeyName(4, "Folder.png");
            // 
            // frmFolderBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 884);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.trViewFolders);
            this.Name = "frmFolderBrowser";
            this.Text = "frmFolderBrowser";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trViewFolders;
        private System.Windows.Forms.ImageList imageLst;
    }
}