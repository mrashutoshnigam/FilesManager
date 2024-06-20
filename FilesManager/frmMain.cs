using MetadataExtractor.Formats.Exif;
using MetadataExtractor;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Shell32;
using static System.Environment;

namespace FilesManager
{
    public partial class frmMain : DockContent
    {
        frmProperties frmProperties;
        List<Dictionary<string, string>> filesList;
        public frmMain(string directoryPath)
        {
            InitializeComponent();
            toolStripLblFilePath.Text = directoryPath;
            loadFiles(directoryPath);
            listViewFiles.ListViewItemSorter = new ListViewItemComparer(0, SortOrder.Ascending);
            this.Load += new EventHandler(frmMain_Load);
            filesList = new List<Dictionary<string, string>>();
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
                    listViewItem.SubItems.Add("");//GetDirectorySize(directory).ToString()); // Size in bytes
                    listViewItem.SubItems.Add(directoryInfo.CreationTime.ToString("dd-MM-yyyy HH:mm:ss tt"));
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
                    listViewItem.SubItems.Add(fileInfo.CreationTime.ToString("dd-MM-yyyy HH:mm:ss tt"));
                    listViewFiles.Items.Add(listViewItem);
                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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
            if (System.IO.Directory.Exists(txtBoxDestination.Text))
            {
                filesList.Clear();
                string path = txtBoxDestination.Text;
                // Prepare the BackgroundWorker to run the copy operation
                if (!backgroundWorker1.IsBusy)
                {
                    // Pass the selected path as an argument
                    backgroundWorker1.RunWorkerAsync(path);
                }
            }
            else
            {
               MessageBox.Show("Destination path does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListAllFiles(string path)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    this.filesList.Add(new Dictionary<string, string>
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

            int totalFiles = filesList.Count;
            int processedFiles = 0;

            foreach (var file in filesList)
            {
                string sourceFile = file["Path"];
                var fileInfo = new System.IO.FileInfo(sourceFile);
                if (fileInfo.Exists)
                {
                    var dateCreated = GetFileCreatedTimeFromExifData(sourceFile);

                    var pathToCreate = System.IO.Path.Combine(path, dateCreated.Year.ToString(), dateCreated.ToString("MM"), dateCreated.ToString("dd"));
                    if (!System.IO.Directory.Exists(pathToCreate))
                    {
                        System.IO.Directory.CreateDirectory(pathToCreate);
                    }
                    var fileNameNew = dateCreated.ToString("yyyyMMddHHmmssfff") + fileInfo.Extension;
                    string destinationFile = System.IO.Path.Combine(pathToCreate, fileNameNew);
                    System.IO.File.Copy(sourceFile, destinationFile, true);

                    processedFiles++;
                    int progressPercentage = (int)((float)processedFiles / (float)totalFiles * 100);
                    backgroundWorker1.ReportProgress(progressPercentage);
                }
            }
        }

        private DateTime GetFileCreatedTimeFromExifData(string sourceFile)
        {
            DateTime dateTakenVar = new System.IO.FileInfo(sourceFile).CreationTime;
            var directories = ImageMetadataReader.ReadMetadata(sourceFile);

            // Find the EXIF IFD0 directory
            var exifIfd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

            // Alternatively, to directly find the EXIF SubIFD directory you can use:
            // var exifSubIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

            if (exifIfd0Directory != null)
            {
                // Attempt to retrieve the DateTimeOriginal value
                if (exifIfd0Directory.TryGetDateTime(ExifDirectoryBase.TagDateTime, out DateTime dateTaken))
                {
                    return dateTaken;
                }

            }
            return dateTakenVar;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileprogressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Operation completed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDlg.SelectedPath = txtBoxDestination.Text;
            }
            catch
            {
                folderBrowserDlg.SelectedPath = Environment.GetFolderPath(SpecialFolder.MyPictures);
            }
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtBoxDestination.Text = folderBrowserDlg.SelectedPath;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            txtBoxDestination.Text = Environment.GetFolderPath(SpecialFolder.MyPictures);
        }
    }
}

