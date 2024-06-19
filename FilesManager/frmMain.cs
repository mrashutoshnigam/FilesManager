using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace FilesManager
{
    public partial class frmMain : DockContent
    {
        frmProperties frmProperties;
        List<Dictionary<string, string>> files;
        public frmMain(string directoryPath)
        {
            InitializeComponent();
            toolStripLblFilePath.Text = directoryPath;
            loadFiles(directoryPath);
            listViewFiles.ListViewItemSorter = new ListViewItemComparer(0, SortOrder.Ascending);
            this.Load += new EventHandler(frmMain_Load);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmProperties = new frmProperties();
            frmProperties.Show(this.DockPanel, dockState: DockState.DockRight);
        }

        private void loadFiles(string directoryPath)
        {
            try
            {
                listViewFiles.Items.Clear(); // Clear existing items

                // Load directories
                var directories = System.IO.Directory.GetDirectories(directoryPath);
                foreach (var directory in directories)
                {

                    var directoryInfo = new System.IO.DirectoryInfo(directory);
                    var listViewItem = new ListViewItem(directoryInfo.Name);
                    listViewItem.Tag = directory;
                    listViewItem.SubItems.Add("Folder"); // Type
                    //listViewItem.SubItems.Add(GetDirectorySize(directory).ToString()); // Size in bytes
                    listViewItem.SubItems.Add(directoryInfo.CreationTime.Date.ToString("d"));
                    listViewFiles.Items.Add(listViewItem);
                }

                // Load files
                var files = System.IO.Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    string fileName = System.IO.Path.GetFileName(file);
                    var fileInfo = new System.IO.FileInfo(file);
                    var listViewItem = new ListViewItem(fileName);
                    listViewItem.Tag = file;
                    listViewItem.SubItems.Add(fileInfo.Extension); // File type
                    listViewItem.SubItems.Add(fileInfo.Length.ToString()); // Size in bytes
                    listViewItem.SubItems.Add(fileInfo.CreationTime.Date.ToString("d"));
                    listViewFiles.Items.Add(listViewItem);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error loading files and folders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Helper method to calculate the size of a directory
        private long GetDirectorySize(string directoryPath)
        {
            var files = System.IO.Directory.GetFiles(directoryPath, "*.*", System.IO.SearchOption.AllDirectories);
            long size = 0;
            foreach (var file in files)
            {
                var fileInfo = new System.IO.FileInfo(file);
                size += fileInfo.Length;
            }
            return size; // Size in bytes
        }

        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = listViewFiles.SelectedItems[0];
            if (item.SubItems[1].Text == "Folder")
            {
                string directoryPath = item.Tag.ToString();
                frmMain childForm = new frmMain(directoryPath);
                childForm.Text = item.Text;
                childForm.Tag = item.Tag;
                childForm.Show(this.DockPanel, dockState: DockState.Document);
            }
            else
            {
                // get all properties of the file and then display them in frmProperties list view
                string filePath = item.Tag.ToString();
                System.Diagnostics.Process.Start(filePath);
            }
        }

        private void toolStripLblFilePath_DoubleClick(object sender, EventArgs e)
        {
            // Open the folder in Windows Explorer
            string directoryPath = this.Tag.ToString();
            System.Diagnostics.Process.Start(directoryPath);
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewFiles.SelectedItems[0];
                if (item != null && item.SubItems[1].Text != "Folder")
                {
                    frmProperties.Text = item.Text;
                    frmProperties.Tag = item.Tag;
                    frmProperties.LoadProperties(item.Tag.ToString());
                    frmProperties.Show(this.DockPanel, DockState.DockRight);
                }
            }

        }

        private void tStripBtnProceedToCopy_Click(object sender, EventArgs e)
        {
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDlg.SelectedPath;
                ListAllDirectories(toolStripLblFilePath.Text);

                foreach (var file in files)
                {
                    string sourceFile = file["Path"];
                    var fileInfo = new System.IO.FileInfo(sourceFile);
                    if (fileInfo.Exists)
                    {
                        var dateCreated = fileInfo.CreationTime;
                        var dateModified = fileInfo.LastWriteTime;
                        var pathToCreate = System.IO.Path.Combine(path, dateCreated.Year.ToString(), dateCreated.ToString("MMMM"), dateCreated.ToString("dd"));
                        if (!System.IO.Directory.Exists(pathToCreate))
                        {
                            System.IO.Directory.CreateDirectory(pathToCreate);
                        }
                        var fileNameNew = "Img_" + dateCreated.ToString("yyyyMMddHHmmss") + fileInfo.Extension;
                        string destinationFile = System.IO.Path.Combine(pathToCreate, fileNameNew);
                        System.IO.File.Copy(sourceFile, destinationFile, true);
                    }
                }
            }
        }

        private void ListAllFiles(string path)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    this.files.Add(new Dictionary<string, string>
                    {
                        { "Name", System.IO.Path.GetFileName(file) },
                        { "Path", file }
                    });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error listing files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListAllDirectories(string path)
        {
            try
            {
                string[] directories = System.IO.Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    ListAllDirectories(directory);
                    ListAllFiles(directory);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error listing directories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = e.Argument.ToString();
            ListAllDirectories(toolStripLblFilePath.Text);

            int totalFiles = files.Count;
            int processedFiles = 0;

            foreach (var file in files)
            {
                string sourceFile = file["Path"];
                var fileInfo = new System.IO.FileInfo(sourceFile);
                if (fileInfo.Exists)
                {
                    var dateCreated = fileInfo.CreationTime;
                    var pathToCreate = System.IO.Path.Combine(path, dateCreated.Year.ToString(), dateCreated.ToString("MMMM"), dateCreated.ToString("dd"));
                    if (!System.IO.Directory.Exists(pathToCreate))
                    {
                        System.IO.Directory.CreateDirectory(pathToCreate);
                    }
                    var fileNameNew = "Img_" + dateCreated.ToString("yyyyMMddHHmmss") + fileInfo.Extension;
                    string destinationFile = System.IO.Path.Combine(pathToCreate, fileNameNew);
                    System.IO.File.Copy(sourceFile, destinationFile, true);

                    processedFiles++;
                    int progressPercentage = (int)((float)processedFiles / (float)totalFiles * 100);
                    backgroundWorker1.ReportProgress(progressPercentage);
                }
            }
        }
    }
}

