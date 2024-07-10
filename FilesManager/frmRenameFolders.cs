using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Directory = System.IO.Directory;

namespace FilesManager
{
    public partial class frmRenameFolders : Form
    {
        public frmRenameFolders()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialogBox.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialogBox.SelectedPath;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtFolderPath.Text)) {
                lstBoxErrors.Text = "";
                RenameDirectories(txtFolderPath.Text);
                OpenFolderInExplorer(txtFolderPath.Text);
            }
            else
            {

               MessageBox.Show("Please select a valid folder path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void RenameDirectories(string rootDirectory)
        {
            // Collect all directories that need to be renamed
            var directoriesToRename = new List<string>();
            CollectDirectories(rootDirectory, directoriesToRename);

            // Rename directories
            foreach (var directory in directoriesToRename)
            {
                string newDirectoryName = Path.Combine(Path.GetDirectoryName(directory), Path.GetFileName(directory).Replace(" ", "_"));

                if (directory != newDirectoryName)
                {
                    try
                    {
                        Directory.Move(directory, newDirectoryName);
                        lstBoxErrors.Text += $"Renamed: {directory} -> {newDirectoryName}\n";
                    }
                    catch (Exception ex)
                    {
                        lstBoxErrors.Text += $"Error renaming {directory}: {ex.Message}\n";
                    }
                }
            }
        }
        private void CollectDirectories(string currentDirectory, List<string> directoriesToRename)
        {
            // Get all subdirectories
            string[] subDirectories = Directory.GetDirectories(currentDirectory);

            foreach (string subDirectory in subDirectories)
            {
                // Recursively collect subdirectories
                CollectDirectories(subDirectory, directoriesToRename);

                // Add the current subdirectory to the list
                directoriesToRename.Add(subDirectory);
            }

            // Add the current directory to the list
            directoriesToRename.Add(currentDirectory);
        }
        private void OpenFolderInExplorer(string folderPath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = folderPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
