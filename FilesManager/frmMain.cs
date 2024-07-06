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
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using FilesManager.GooglePhotos;
using Newtonsoft.Json;
using FilesManager.Models;
using Nevron.Nov.Graphics;
using ExifLibrary;
using ImageMagick;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using MediaInfoLib;
using System.Runtime.InteropServices.ComTypes;


namespace FilesManager
{
    public partial class frmMain : DockContent
    {
        frmProperties frmProperties;

        Dictionary<string, string> filesList;
        string directoryPath;
        string destinationPath;
        Dictionary<string, GooglePhoto> googlePhotosDictionary;
        string selectedFileTypeFromCheckBox;
        bool IsGooglePhotosMetaDataChecked = false;
        bool isCopyOperation = true;
        private int progressCounter = 0;
        Dictionary<string, List<string>> fileTypeGroups = new Dictionary<string, List<string>>
        {
            { "Images", new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg", ".heic"} },
            { "PDFs", new List<string> { ".pdf" } },
            { "Documents", new List<string> { ".doc", ".docx", ".txt", ".odt", ".rtf", ".xls", ".xlsx", ".ppt", ".pptx" } },
            { "Compressed", new List<string> { ".zip", ".rar", ".7z", ".tar", ".gz", ".bz2" } },
            { "Audio", new List<string> { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".m4a" } },
            { "Videos", new List<string> { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv" } },
            { "Photoshop Files", new List<string> { ".psd" } },
            { "Raw", new List<string> { ".cr2", ".nef", ".orf", ".raw", ".dng" } },
            { "Lightroom Catalogs", new List<string> { ".lrcat" } },
            { "Setup Files", new List<string> { ".exe", ".msi", ".bat", ".cmd" } },
            // Add more groups as needed
        };
        public frmMain(string directoryPath)
        {
            InitializeComponent();
            this.directoryPath = directoryPath;
            this.Load += new EventHandler(frmMain_Load);
            filesList = new Dictionary<string, string>();

            btnCopyMoveToogleButton.PerformClick();

        }

        private async void frmMain_Load(object sender, EventArgs e)
        {

            frmProperties = new frmProperties();
            frmProperties.Text = "Metadata- " + this.Text;
            frmProperties.Show(this.DockPanel, dockState: DockState.DockRightAutoHide);
            toolStripLblFilePath.Text = directoryPath;
            loadFiles(directoryPath);
            listViewFiles.ListViewItemSorter = new ListViewItemComparer(0, SortOrder.Ascending);
            this.destinationPath = Environment.GetFolderPath(SpecialFolder.MyPictures);
            SelectedBreadCrumb(Environment.GetFolderPath(SpecialFolder.MyDocuments));
            await GroupFilesByTypeAsync(directoryPath);
            chkIncludeSubFolders.Checked = false;
            cBoxPathFormat.SelectedIndex = 2;
            ConfigureDataGridViewErrorList();
            googlePhotosDictionary = await LoadGooglePhotosJsonFiles(directoryPath);
            dataGridViewFileExtensions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void loadFiles(string directoryPath)
        {
            try
            {
                listViewFiles.BeginUpdate();
                listViewFiles.Items.Clear(); // Clear existing items

                // Use ConcurrentBag to collect items from parallel operations
                var items = new ConcurrentBag<ListViewItem>();

                // Load directories in parallel
                Parallel.ForEach(System.IO.Directory.GetDirectories(directoryPath), (directory) =>
                {
                    var directoryInfo = new DirectoryInfo(directory);
                    var listViewItem = new ListViewItem(directoryInfo.Name)
                    {
                        Tag = directory,
                        SubItems = { "Folder", "", directoryInfo.CreationTime.ToString("dd-MM-yyyy HH:mm:ss tt") }
                    };
                    items.Add(listViewItem);
                });

                // Load files in parallel
                Parallel.ForEach(System.IO.Directory.GetFiles(directoryPath), (file) =>
                {
                    string fileName = Path.GetFileName(file);
                    var fileInfo = new FileInfo(file);
                    var listViewItem = new ListViewItem(fileName)
                    {
                        Tag = file,
                        SubItems = { fileInfo.Extension.ToLower(), fileInfo.Length.ToString(), fileInfo.CreationTime.ToString("dd-MM-yyyy HH:mm:ss tt") }
                    };
                    items.Add(listViewItem);
                });

                // Update ListView on the UI thread
                listViewFiles.Items.AddRange(items.ToArray());

                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error loading files and folders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                listViewFiles.EndUpdate();
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
                    frmProperties.Text = "Metadata- " + this.Text + ":" + item.Text;
                    frmProperties.Tag = item.Tag;
                    frmProperties.LoadProperties(item.Tag.ToString());
                    //frmProperties.Show(this.DockPanel, DockState.DockRight);
                }
            }

        }

        private void tStripBtnProceedToCopy_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.destinationPath))
            {
                filesList.Clear();
                string path = this.destinationPath;
                string selectedFormat = cBoxPathFormat.Text.ToString();
                string selectedFileGroup = comboBoxFileTypes.Text.ToString();
                // Prepare the BackgroundWorker to run the copy operation
                if (!backgroundWorker1.IsBusy)
                {
                    EnableDisableAllControls();
                    // Pass the selected path as an argument
                    backgroundWorker1.RunWorkerAsync(new object[] { path, selectedFormat, selectedFileGroup });
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
                var selectedFileTypeWithCount = this.selectedFileTypeFromCheckBox;
                var selectedFileType = selectedFileTypeWithCount.Substring(0, selectedFileTypeWithCount.LastIndexOf(" (")).Trim();
                if (fileTypeGroups.TryGetValue(selectedFileType, out List<string> fileExtensions))
                {
                    // Prepare a HashSet for faster lookup
                    var extensionsSet = new HashSet<string>(fileExtensions.Select(ext => ext.ToLowerInvariant()));

                    // Get all files in the directory once
                    string[] allFiles = System.IO.Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

                    // Use Parallel.ForEach for better performance on large directories
                    Parallel.ForEach(allFiles, (file) =>
                    {
                        string extension = Path.GetExtension(file).ToLowerInvariant();
                        if (extensionsSet.Contains(extension))
                        {
                            // Thread-safe addition to the list
                            lock (filesList)
                            {
                                string key = Path.GetFileName(file);
                                if (filesList.ContainsKey(key) == false)
                                    filesList.Add(key, file);
                            }
                        }
                    });
                }
                else
                {
                    if (selectedFileType == "Other")
                    {
                        string[] allFiles = System.IO.Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
                        Parallel.ForEach(allFiles, (file) =>
                        {
                            // Thread-safe addition to the list
                            lock (filesList)
                            {
                                string key = Path.GetFileName(file);
                                if (filesList.ContainsKey(key) == false)
                                    filesList.Add(key, file);
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Consider logging the error or displaying a message to the user
                MessageBox.Show($"Error listing files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListAllDirectories(string path)
        {
            try
            {
                var directories = System.IO.Directory.GetDirectories(path);

                // Use Parallel.ForEach to traverse directories in parallel
                Parallel.ForEach(directories, (directory) =>
                {
                    if (chkIncludeSubFolders.Checked)
                    {
                        ListAllDirectories(directory); // Recursively list subdirectories
                    }
                    ListAllFiles(directory); // List files in the current directory
                });

                // Always list files in the initial directory
                // This call is outside the parallel loop to ensure it's done at least once
                // and to avoid concurrent modification issues with filesList if ListAllFiles is not thread-safe
                ListAllFiles(path);
            }
            catch (Exception ex)
            {
                // Consider logging the error or displaying a message to the user
                MessageBox.Show($"Error listing directories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (object[])e.Argument;
            string path = args[0].ToString();
            string selectedFormat = (string)args[1];
            string selectedFileGroup = (string)args[2];

            this.filesList.Clear();
            ListAllDirectories(toolStripLblFilePath.Text);

            int totalFiles = filesList.Count;
            ConcurrentBag<ErrorModel> concurrentErrors = new ConcurrentBag<ErrorModel>();
            var fileGroup = this.selectedFileTypeFromCheckBox.Substring(0, this.selectedFileTypeFromCheckBox.LastIndexOf(" (")).Trim();
            ConcurrentDictionary<string, bool> createdDirectories = new ConcurrentDictionary<string, bool>();

            Parallel.ForEach(filesList, file =>
            {
                try
                {
                    string sourceFile = file.Value;
                    var fileInfo = new FileInfo(sourceFile);
                    if (fileInfo.Exists)
                    {
                        DateTime dateCreated = DateTime.Now;
                        if (fileGroup != "Other")
                            dateCreated = GetFileCreatedTimeFromExifData(sourceFile);
                        string fileType = fileInfo.Extension.ToLower();
                        if (fileGroup == "Images")
                        {
                            // Additional processing for Google Photos metadata
                            ProcessGooglePhotosMetadata(fileInfo, ref dateCreated, googlePhotosDictionary);
                        }
                        else if (fileGroup == "Raw")
                        {
                            // Read all metadata from the image
                            var directories = ImageMetadataReader.ReadMetadata(file.Value);

                            // Find the so-called Exif "SubIFD" (which may be null)
                            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                            // Read the DateTime tag value
                            var dateTime = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                            if (dateTime.HasValue)
                            {
                                dateCreated = dateTime.Value;
                            }
                            else
                            {
                                dateCreated = fileInfo.LastWriteTime;
                            }

                        }
                        else if (fileGroup == "PDFs" || fileGroup == "Documents" || fileGroup == "Compressed" || fileGroup == "Audio")
                        {
                            dateCreated = fileInfo.LastWriteTime;
                        }
                        else if (fileGroup == "Videos")
                        {
                            DateTime creationDate = fileInfo.CreationTime;

                            // Create a new instance of MediaInfo
                            var mediaInfo = new MediaInfo();

                            // Open the video file
                            mediaInfo.Open(fileInfo.FullName);
                            string encodedDate = mediaInfo.Get(StreamKind.General, 0, "Encoded_Date");
                            string format = "UTC yyyy-MM-dd HH:mm:ss";
                            try
                            {
                                dateCreated = DateTime.ParseExact(encodedDate, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                            }
                            catch
                            {
                                dateCreated = fileInfo.LastWriteTime;
                            }
                            //dateCreated = Convert.ToDateTime(encodedDate);
                            //string taggedDate = mediaInfo.Get(StreamKind.General, 0, "Tagged_Date");

                        }
                        else if (fileGroup == "Photoshop Files" || fileGroup == "Lightroom Catalogs" || fileGroup == "Setup Files")
                        {
                            dateCreated = fileInfo.LastWriteTime;
                        }
                        else if (fileGroup == "Other")
                        {
                            dateCreated = fileInfo.LastWriteTime;
                        }
                        var pathToCreate = ConstructPathBasedOnFormat(path, dateCreated, fileGroup, selectedFormat);
                        createdDirectories.TryAdd(pathToCreate, true);

                        if (!System.IO.Directory.Exists(pathToCreate))
                        {
                            System.IO.Directory.CreateDirectory(pathToCreate); // Consider thread-safety or moving this outside the loop
                        }


                        var fileNameNew = ConstructNewFileName(dateCreated, fileInfo, chkIncludeFileName.Checked);
                        string destinationFile = Path.Combine(pathToCreate, fileNameNew);

                        try
                        {
                            if (isCopyOperation)
                            {
                                File.Copy(sourceFile, destinationFile, true);
                                AddRowToDataGridViewErrorList("success", $"Source: {sourceFile}, Dest: {destinationFile}", "File Copied Successfully.");
                            }
                            else
                            {
                                MoveFileWithChecks(sourceFile, destinationFile, concurrentErrors);
                                AddRowToDataGridViewErrorList("success", $"Source: {sourceFile}, Dest: {destinationFile}", "File Moved Successfully.");
                            }
                        }
                        catch (Exception ex)
                        {
                            AddRowToDataGridViewErrorList("error", sourceFile, ex.Message);
                            //concurrentErrors.Add(new ErrorModel { File = sourceFile, ErrorMessage = ex.Message });
                        }

                        // Report progress
                        // Note: This part needs to be thread-safe. Consider using Interlocked.Increment for a thread-safe counter.
                    }
                    int newProgress = Interlocked.Increment(ref progressCounter);
                    backgroundWorker1.ReportProgress((int)((newProgress / (double)totalFiles) * 100));
                }
                catch (Exception ex)
                {
                    AddRowToDataGridViewErrorList("error", file.Value, ex.Message);
                    //concurrentErrors.Add(new ErrorModel { File = file.Value, ErrorMessage = ex.Message });
                }
            });

            //errors.AddRange(concurrentErrors); // Assuming errors is thread-safe or accessed in a thread-safe manner later
        }

        private void ProcessGooglePhotosMetadata(FileInfo fileInfo, ref DateTime dateCreated, Dictionary<string, GooglePhoto> googlePhotosDictionary)
        {
            if (IsGooglePhotosMetaDataChecked && googlePhotosDictionary.TryGetValue(fileInfo.Name + ".json", out GooglePhoto googlePhoto))
            {
                var geoData = googlePhoto.GeoData;
                if (geoData != null)
                {
                    AddGeoDataToImageIfAbsent(fileInfo.FullName, geoData.Latitude, geoData.Longitude);
                }
                if (googlePhoto.People != null && googlePhoto.People.Count > 0)
                {
                    AddPeopleNamesToImageIPTC(fileInfo.FullName, googlePhoto.People.Select(p => p.Name).ToList());
                }
                try
                {
                    if (googlePhoto.PhotoTakenTime != null)
                    {
                        DateTime result = DateTime.Parse(googlePhoto.PhotoTakenTime.Formatted.Replace("UTC", "").Trim(), CultureInfo.InvariantCulture);
                        dateCreated = result;
                    }
                }
                catch
                {

                    // Log or handle the error if the date parsing fails
                }
            }
        }

        private void MoveFileWithChecks(string sourceFile, string destinationFile, ConcurrentBag<ErrorModel> errors)
        {
            try
            {
                if (File.Exists(destinationFile))
                {
                    FileInfo sourceFileInfo = new FileInfo(sourceFile);
                    FileInfo destinationFileInfo = new FileInfo(destinationFile);

                    bool isSizeEqual = sourceFileInfo.Length == destinationFileInfo.Length;
                    bool isLastWriteTimeEqual = sourceFileInfo.LastWriteTime == destinationFileInfo.LastWriteTime;

                    if (isSizeEqual && isLastWriteTimeEqual)
                    {
                        // If both size and last write time are equal, consider them similar and ignore
                        AddRowToDataGridViewErrorList("info", sourceFile, $"File Exists: Source : {sourceFile}, Dest: {destinationFile}");
                    }
                    else
                    {
                        // If the files are different, delete the destination and move the source file
                        File.Delete(destinationFile);
                        File.Move(sourceFile, destinationFile);
                    }
                }
                else
                {
                    File.Move(sourceFile, destinationFile);
                }
            }
            catch (Exception ex)
            {
                AddRowToDataGridViewErrorList("error", sourceFile, ex.Message);
                //errors.Add(new ErrorModel { File = sourceFile, ErrorMessage = $"Error moving file: {ex.Message}" });
            }
        }

        private string ConstructNewFileName(DateTime dateCreated, FileInfo fileInfo, bool includeOriginalFileName)
        {
            // Start with a base file name using the date and time to ensure uniqueness
            var fileNameNew = dateCreated.ToString("yyyyMMddHHmmssfff");

            // If the original file name should be included, append it to the base file name
            if (includeOriginalFileName)
            {
                // Ensure that the original file name is valid and does not contain any characters that are not allowed in file names
                string safeFileName = MakeFileNameSafe(fileInfo.Name);
                fileNameNew += "_" + Path.GetFileNameWithoutExtension(safeFileName);
            }

            // Append the file extension
            fileNameNew += fileInfo.Extension;

            return fileNameNew;
        }

        private string MakeFileNameSafe(string originalFileName)
        {
            // Define a list of characters that are not allowed in file names
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Replace any invalid characters in the original file name with an underscore
            foreach (char c in invalidChars)
            {
                originalFileName = originalFileName.Replace(c, '_');
            }

            return originalFileName;
        }

        private string ConstructPathBasedOnFormat(string basePath, DateTime dateCreated, string fileType, string selectedFormat)
        {
            // Initialize a StringBuilder with the base path
            StringBuilder pathBuilder = new StringBuilder(basePath);

            // Split the selected format into parts based on '/'
            var formatParts = selectedFormat.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in formatParts)
            {
                switch (part)
                {
                    case "Year":
                        pathBuilder.Append(Path.DirectorySeparatorChar + dateCreated.Year.ToString());
                        break;
                    case "Month":
                        pathBuilder.Append(Path.DirectorySeparatorChar + dateCreated.ToString("MM"));
                        break;
                    case "Day":
                        pathBuilder.Append(Path.DirectorySeparatorChar + dateCreated.ToString("dd"));
                        break;
                    case "File Types":
                        pathBuilder.Append(Path.DirectorySeparatorChar + fileType);
                        break;
                    default:
                        throw new ArgumentException("Unsupported path format part: " + part);
                }
            }

            return pathBuilder.ToString();
        }


        private DateTime GetFileCreatedTimeFromExifData(string sourceFile)
        {
            DateTime dateTakenVar = new System.IO.FileInfo(sourceFile).CreationTime;
            try
            {

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
            }
            catch (Exception ex)
            {
                AddRowToDataGridViewErrorList("error", sourceFile, ex.Message);
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
            EnableDisableAllControls();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDlg.SelectedPath = this.destinationPath;
            }
            catch
            {
                folderBrowserDlg.SelectedPath = Environment.GetFolderPath(SpecialFolder.MyPictures);
            }
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                this.destinationPath = folderBrowserDlg.SelectedPath;
                SelectedBreadCrumb(folderBrowserDlg.SelectedPath);

            }
        }

        private void SelectedBreadCrumb(string path)
        {
            var lst = path.Split('\\').ToList();
            kryptonBreadCrumb1.RootItem.Items.Clear();
            this.destinationPath = path;
            if (lst.Count == 0)
            {
                return;
            }
            kryptonBreadCrumb1.RootItem.ShortText = lst[0];
            KryptonBreadCrumbItem parentItem = kryptonBreadCrumb1.RootItem;
            string path1 = lst[0];
            foreach (var item in lst.Skip(1))
            {
                KryptonBreadCrumbItem kryptonBreadCrumbItem = new KryptonBreadCrumbItem(item);
                path1 += "\\" + item;
                kryptonBreadCrumbItem.Tag = path;
                parentItem.Items.Add(kryptonBreadCrumbItem);
                parentItem = kryptonBreadCrumbItem;

            }
            kryptonBreadCrumb1.SelectedItem = parentItem;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        public async Task GroupFilesByTypeAsync(string folderPath)
        {
            // Define file type groups
            // Assuming fileTypeGroups is already defined and initialized

            // Initialize a dictionary to hold the count of files in each group
            var groupCounts = new Dictionary<string, int>();

            // Initialize all group counts to 0
            foreach (var group in fileTypeGroups.Keys)
            {
                groupCounts[group] = 0;
            }

            // Use Task.Run to process files in a background thread
            await Task.Run(() =>
            {
                // Get all files in the folderPath
                var searchOption = chkIncludeSubFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var files = System.IO.Directory.GetFiles(folderPath, "*.*", searchOption);

                // Use PLINQ for parallel processing of files
                files.AsParallel().ForAll(file =>
                {
                    string extension = Path.GetExtension(file).ToLower();
                    var group = fileTypeGroups.FirstOrDefault(g => g.Value.Contains(extension)).Key;

                    if (group != null)
                    {
                        // Use lock to safely update the count for the group
                        lock (groupCounts)
                        {
                            groupCounts[group]++;
                        }
                    }
                    else
                    {
                        // If the file type does not match any group, categorize it as "Other"
                        lock (groupCounts)
                        {
                            if (!groupCounts.ContainsKey("Other"))
                            {
                                groupCounts["Other"] = 0;
                            }
                            groupCounts["Other"]++;
                        }
                    }
                });
            });

            // Update UI elements on the UI thread
            this.Invoke(new Action(() =>
            {
                UpdateComboBoxAndChart(groupCounts);
            }));
        }

        private void UpdateComboBoxAndChart(Dictionary<string, int> groupCounts)
        {
            // Clear existing items in the ComboBox
            comboBoxFileTypes.Items.Clear();

            // Add each group and its count to the ComboBox
            foreach (var groupCount in groupCounts)
            {
                if (groupCount.Value > 0) // Only add groups that have at least one file
                {
                    comboBoxFileTypes.Items.Add($"{groupCount.Key} ({groupCount.Value})");
                }
            }
            UpdateChartWithGroupCounts(groupCounts);
            // Update chart with groupCounts...
            // Similar to the existing code in GroupFilesByType for updating the chart
        }
        private void UpdateChartWithGroupCounts(Dictionary<string, int> groupCounts)
        {
            // Clear existing series
            chrtControl.Series.Clear();

            // Create a new series for file types
            Series series = new Series("FileTypes")
            {
                ChartType = SeriesChartType.Column, // Or any other chart type that suits your needs
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Int32
            };

            // Add data points to the series
            foreach (var group in groupCounts)
            {
                series.Points.AddXY(group.Key, group.Value);
            }
            //series.ChartType = SeriesChartType.Bar;
            // Add the series to the chart
            chrtControl.Series.Add(series);

            // Optionally, adjust the chart appearance, labels, etc.
            chrtControl.ChartAreas[0].AxisX.Interval = 1;
            chrtControl.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chrtControl.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Verdana", 8);
            chrtControl.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Verdana", 8);

            // Refresh the chart to display the updated data
            chrtControl.Invalidate();
        }

        // Helper method to generate a unique color based on an index
        private Color GetUniqueColor(int index)
        {
            // Predefined colors or generate dynamically for larger datasets
            Color[] colors = new Color[] {        Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange,
        Color.Purple, Color.Cyan, Color.Magenta, Color.Brown, Color.Olive, Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Aquamarine, Color.Azure,
        Color.Beige, Color.Bisque, Color.Black, Color.BlanchedAlmond, Color.BlueViolet, Color.BurlyWood, Color.CadetBlue, Color.Chartreuse, Color.Chocolate,
    };

            // Return a color based on the index
            return colors[index % colors.Length];
        }

        private async void chkIncludeSubFolders_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                await CountFilesByTypeAsync(this.directoryPath);
                await GroupFilesByTypeAsync(this.directoryPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<Dictionary<string, GooglePhoto>> LoadGooglePhotosJsonFiles(string directoryPath)
        {
            var googlePhotosConcurrentDictionary = new ConcurrentDictionary<string, GooglePhoto>();
            var parallelErrors = new ConcurrentBag<ErrorModel>();

            try
            {
                // Get all .json files from the directory and subdirectories
                string[] jsonFiles = System.IO.Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);

                Parallel.ForEach(jsonFiles, (filePath) =>
                {
                    try
                    {
                        // Read the JSON file content
                        string jsonContent = File.ReadAllText(filePath);

                        // Deserialize the JSON content to a GooglePhoto object
                        GooglePhoto photo = JsonConvert.DeserializeObject<GooglePhoto>(jsonContent);

                        if (photo != null)
                        {
                            // Use the file name as the key and the deserialized object as the value
                            googlePhotosConcurrentDictionary[Path.GetFileName(filePath)] = photo;
                        }
                    }
                    catch (Exception ex)
                    {
                        AddRowToDataGridViewErrorList("error", filePath, ex.Message);
                        //parallelErrors.Add(new ErrorModel { File = filePath, ErrorMessage = ex.Message });
                    }
                });

                // Display errors if any
                //if (parallelErrors.Count > 0)
                //{
                //    errors.AddRange(parallelErrors); // Assuming 'errors' is a List<ErrorModel> accessible in this context
                //    frmErrorWindow.ErrorModelList = errors;
                //    frmErrorWindow.DisplayErrorsInGrid();
                //    frmErrorWindow.Show();
                //}
            }
            catch (Exception ex)
            {
                AddRowToDataGridViewErrorList("error", directoryPath, ex.Message);
                // Handle potential exceptions from Directory.GetFiles or other initial setup steps
            }

            // Convert the concurrent dictionary back to a regular dictionary for return
            return new Dictionary<string, GooglePhoto>(googlePhotosConcurrentDictionary);
        }

        public void AddGeoDataToImageIfAbsent(string imagePath, double latitude, double longitude)
        {
            try
            {
                if (imagePath.ToLower().Contains(".heic"))
                {
                    return;
                }
                var image = new MagickImage(imagePath);


                // Check if the image already contains geodata
                var gpsLatitude = image.GetExifProfile()?.GetValue(ImageMagick.ExifTag.GPSLatitude);
                var gpsLongitude = image.GetExifProfile()?.GetValue(ImageMagick.ExifTag.GPSLongitude);

                if (gpsLatitude != null && gpsLongitude != null)
                {
                    //Console.WriteLine("Image already contains geodata.");
                    return;
                }

                // Create or get the ExifProfile
                var profile = image.GetExifProfile() ?? new ExifProfile();

                // Convert latitude and longitude to degrees, minutes, and seconds (DMS)
                var latDMS = ConvertDecimalDegreesToDMS(latitude);
                var lonDMS = ConvertDecimalDegreesToDMS(longitude);

                // Set GPS latitude and longitude tags
                profile.SetValue(ImageMagick.ExifTag.GPSLatitude, latDMS);
                profile.SetValue(ImageMagick.ExifTag.GPSLatitudeRef, latitude >= 0 ? "N" : "S");
                profile.SetValue(ImageMagick.ExifTag.GPSLongitude, lonDMS);
                profile.SetValue(ImageMagick.ExifTag.GPSLongitudeRef, longitude >= 0 ? "E" : "W");

                // Add or update the ExifProfile
                image.SetProfile(profile);

                // Save the image
                image.Write(imagePath);


                // Console.WriteLine("Geodata added to the image.");}
            }
            catch (Exception ex)
            {
                AddRowToDataGridViewErrorList("error", imagePath, ex.Message);
                // Handle exceptions
                //MessageBox.Show($"Error adding geodata to the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private static ImageMagick.Rational[] ConvertDecimalDegreesToDMS(double decimalDegrees)
        {
            // Convert decimal degrees to degrees, minutes, and seconds
            var degrees = (int)decimalDegrees;
            var minutes = (int)((decimalDegrees - degrees) * 60);
            var seconds = (decimalDegrees - degrees - minutes / 60.0) * 3600.0;

            return new[]
            {
        new ImageMagick.Rational(Math.Abs(degrees)),
        new ImageMagick.Rational(Math.Abs(minutes)),
        new ImageMagick.Rational(Math.Abs(seconds))
    };
        }
        public void AddPeopleNamesToImageIPTC(string imagePath, List<string> peopleNames)
        {
            try
            {
                if (imagePath.ToLower().Contains(".heic"))
                {
                    return;
                }
                MagickImage image = new MagickImage(imagePath);

                var fileInfo = new FileInfo(imagePath);
                // Create or get the IPTC profile
                var profile = image.GetIptcProfile() ?? new IptcProfile();
                profile.SetValue(IptcTag.Caption, fileInfo.Name);
                // Add people names to the IPTC profile
                foreach (var name in peopleNames)
                {
                    profile.SetValue(IptcTag.Keyword, name);
                }

                // Add or update the IPTC profile
                image.SetProfile(profile);

                // Save the image
                image.Write(imagePath);

                //Console.WriteLine("People names added to the image IPTC metadata.");
            }
            catch (Exception ex)
            {
                AddRowToDataGridViewErrorList("error", imagePath, ex.Message);
            }

        }

        private void comboBoxFileTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedFileTypeFromCheckBox = comboBoxFileTypes.SelectedItem.ToString();
        }

        private void chkGooglePhotosMetaData_CheckedChanged(object sender, EventArgs e)
        {
            this.IsGooglePhotosMetaDataChecked = chkGooglePhotosMetaData.Checked;
        }

        private void EnableDisableAllControls()
        {
            btnBrowse.Enabled = !btnBrowse.Enabled;
            tStripBtnProceedToCopy.Enabled = !tStripBtnProceedToCopy.Enabled;
            comboBoxFileTypes.Enabled = !comboBoxFileTypes.Enabled;
            chkIncludeSubFolders.Enabled = !chkIncludeSubFolders.Enabled;
            chkGooglePhotosMetaData.Enabled = !chkGooglePhotosMetaData.Enabled;
            cBoxPathFormat.Enabled = !cBoxPathFormat.Enabled;
            btnCopyMoveToogleButton.Enabled = !btnCopyMoveToogleButton.Enabled;
            btnStop.Enabled = !btnStop.Enabled;
            chkIncludeFileName.Enabled = !chkIncludeFileName.Enabled;
            progressCounter = 0;
            FileprogressBar.Value = 0;
        }

        private void kryptonBreadCrumb1_DoubleClick(object sender, EventArgs e)
        {
            btnBrowse.PerformClick();

        }

        private void btnCopyMoveToogleButton_Click(object sender, EventArgs e)
        {
            if (isCopyOperation)
            {
                isCopyOperation = false;
                btnCopyMoveToogleButton.Text = "Move";
            }
            else
            {
                isCopyOperation = true;
                btnCopyMoveToogleButton.Text = "Copy";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            EnableDisableAllControls();
        }



        private async Task CountFilesByTypeAsync(string directoryPath)
        {
            try
            {
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    throw new DirectoryNotFoundException($"The folder '{directoryPath}' was not found.");
                }

                var fileTypeCounts = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Get all files from the folder and subfolders
                var files = System.IO.Directory.EnumerateFiles(directoryPath, "*.*", chkIncludeSubFolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                // Create a list of tasks for processing files
                var tasks = files.Select(async file =>
                {
                    string extension = Path.GetExtension(file);

                    if (string.IsNullOrEmpty(extension))
                    {
                        extension = "No Extension";
                    }

                    // Use a lock or concurrent collection for thread-safe operations
                    fileTypeCounts.AddOrUpdate(extension, 1, (key, oldValue) => oldValue + 1);
                });

                // Await the completion of all tasks
                await Task.WhenAll(tasks);

                // Convert the ConcurrentDictionary to a binding-friendly format
                var bindingList = fileTypeCounts.Select(kvp => new { Extension = kvp.Key.ToUpperInvariant(), Count = kvp.Value }).ToList();

                // Update the UI on the UI thread
                this.Invoke(new Action(() =>
                {
                    dataGridViewFileExtensions.DataSource = bindingList;
                    dataGridViewFileExtensions.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                }));
            }
            catch (Exception ex)
            {
                AddRowToDataGridViewErrorList("error", directoryPath, ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }
        private void AddRowToDataGridViewErrorList(string messageType, string filePath, string errorMessage)
        {
            // Check if the call is from a non-UI thread
            if (dataGridViewErrorList.InvokeRequired)
            {
                // Use Invoke to run this method on the UI thread
                dataGridViewErrorList.Invoke(new Action(() => AddRowToDataGridViewErrorList(messageType, filePath, errorMessage)));
                return;
            }

            // Add a new row to the DataGridView
            int rowIndex = dataGridViewErrorList.Rows.Add();

            // Set the values for the new row
            dataGridViewErrorList.Rows[rowIndex].Cells[0].Value = messageType;
            dataGridViewErrorList.Rows[rowIndex].Cells[1].Value = filePath;
            dataGridViewErrorList.Rows[rowIndex].Cells[2].Value = errorMessage;

            // Color the "Error" cell based on the messageType
            switch (messageType.ToLower())
            {
                case "error":
                    dataGridViewErrorList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                    break;
                case "info":
                    dataGridViewErrorList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Blue;
                    break;
                case "success":
                    dataGridViewErrorList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Green;
                    break;
                default:
                    // Default color if messageType is unrecognized
                    dataGridViewErrorList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Gray;
                    break;
            }
        }

        private void ConfigureDataGridViewErrorList()
        {
            dataGridViewErrorList.ColumnCount = 3;
            dataGridViewErrorList.Columns[0].Name = "Type";
            dataGridViewErrorList.Columns[1].Name = "File Path";
            dataGridViewErrorList.Columns[2].Name = "Error Message";
            dataGridViewErrorList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewErrorList.Columns[0].Width = 50;

        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            dataGridViewErrorList.Rows.Clear();
        }
    }
}

