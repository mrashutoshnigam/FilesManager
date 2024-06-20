namespace FilesManager
{
    partial class frmProperties
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
            this.lstViewProperties = new System.Windows.Forms.ListView();
            this.clmHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lstViewProperties
            // 
            this.lstViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmHeaderName,
            this.clmHeaderValue});
            this.lstViewProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewProperties.GridLines = true;
            this.lstViewProperties.HideSelection = false;
            this.lstViewProperties.Location = new System.Drawing.Point(0, 0);
            this.lstViewProperties.Name = "lstViewProperties";
            this.lstViewProperties.Size = new System.Drawing.Size(409, 902);
            this.lstViewProperties.TabIndex = 2;
            this.lstViewProperties.UseCompatibleStateImageBehavior = false;
            this.lstViewProperties.View = System.Windows.Forms.View.Details;
            // 
            // clmHeaderName
            // 
            this.clmHeaderName.Text = "Name";
            // 
            // clmHeaderValue
            // 
            this.clmHeaderValue.Text = "Value";           
            
            // 
            // frmProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 925);
            this.Controls.Add(this.lstViewProperties);
            this.Controls.Add(this.progressBar1);
            this.Name = "frmProperties";
            this.Text = "frmProperties";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstViewProperties;
        private System.Windows.Forms.ColumnHeader clmHeaderName;
        private System.Windows.Forms.ColumnHeader clmHeaderValue;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}