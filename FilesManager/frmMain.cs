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

namespace FilesManager
{
    public partial class frmMain : DockContent
    {
        frmProperties frmProperties;
        frmErrorWindow frmErrorWindow;
        List<ErrorModel> errors;
        List<Dictionary<string, string>> filesList;
        string directoryPath;
        string destinationPath;
        Dictionary<string, GooglePhoto> googlePhotosDictionary;
        string selectedFileTypeFromCheckBox;
        bool IsGooglePhotosMetaDataChecked = false;
        Dictionary<string, List<string>> fileTypeGroups = new Dictionary<string, List<string>>
        {
            { "Images", new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg"} },
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
            filesList = new List<Dictionary<string, string>>();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            frmProperties = new frmProperties();
            frmProperties.Show(this.DockPanel, dockState: DockState.DockRightAutoHide);
            toolStripLblFilePath.Text = directoryPath;
            loadFiles(directoryPath);
            listViewFiles.ListViewItemSorter = new ListViewItemComparer(0, SortOrder.Ascending);
            this.destinationPath = Environment.GetFolderPath(SpecialFolder.MyPictures);
            SelectedBreadCrumb(Environment.GetFolderPath(SpecialFolder.MyPictures));
            GroupFilesByType(directoryPath);
            chkIncludeSubFolders.Checked = false;
            cBoxPathFormat.SelectedIndex = 2;
            errors = new List<ErrorModel>();
            frmErrorWindow = new frmErrorWindow();
            frmErrorWindow.ErrorModelList = errors;
            frmErrorWindow.Show(this.DockPanel, dockState: DockState.DockBottomAutoHide);
            frmErrorWindow.AutoHidePortion = 150;
            googlePhotosDictionary = LoadGooglePhotosJsonFiles(directoryPath);
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
                    listViewItem.SubItems.Add(fileInfo.Extension.ToLower()); // File type
                    listViewItem.SubItems.Add(fileInfo.Length.ToString()); // Size in bytes
                    listViewItem.SubItems.Add(fileInfo.CreationTime.ToString("dd-MM-yyyy HH:mm:ss tt"));
                    listViewFiles.Items.Add(listViewItem);
                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
                    foreach (var fileExtension in fileExtensions)
                    {
                        string[] files = System.IO.Directory.GetFiles(path, "*" + fileExtension, SearchOption.TopDirectoryOnly);
                        foreach (string file in files)
                        {
                            this.filesList.Add(new Dictionary<string, string>
                        {
                            { "Name", System.IO.Path.GetFileName(file) },
                            { "Path", file }
                        });
                        }
                    }
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

                if (chkIncludeSubFolders.Checked)
                {
                    foreach (string directory in directories)
                    {
                        ListAllDirectories(directory);
                        ListAllFiles(directory);
                    }
                }
                ListAllFiles(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error listing directories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            int processedFiles = 0;
            var fileGroup = this.selectedFileTypeFromCheckBox.Substring(0, this.selectedFileTypeFromCheckBox.LastIndexOf(" (")).Trim();

            foreach (var file in filesList)
            {
                string sourceFile = file["Path"];
                var fileInfo = new System.IO.FileInfo(sourceFile);
                if (fileInfo.Exists)
                {
                    var dateCreated = GetFileCreatedTimeFromExifData(sourceFile);
                    string fileType = fileInfo.Extension.ToLower();

                    // Dynamically construct the path based on the selected format
                    var pathToCreate = ConstructPathBasedOnFormat(path, dateCreated, fileGroup, selectedFormat);

                    // var pathToCreate = System.IO.Path.Combine(path, dateCreated.Year.ToString(), dateCreated.ToString("MM"), dateCreated.ToString("dd"));
                    if (!System.IO.Directory.Exists(pathToCreate))
                    {
                        System.IO.Directory.CreateDirectory(pathToCreate);
                    }                  

                    if (IsGooglePhotosMetaDataChecked)
                    {
                        if (googlePhotosDictionary.TryGetValue(fileInfo.Name + ".json", out GooglePhoto googlePhoto))
                        {
                            var dateCreatedGPhotos = googlePhoto.CreationTime;
                            var geoData = googlePhoto.GeoData;
                            if (geoData != null)
                            {
                                AddGeoDataToImageIfAbsent(fileInfo.FullName, geoData.Latitude, geoData.Longitude);
                            }
                            if (googlePhoto.People != null && googlePhoto.People.Count > 0)
                            {
                                AddPeopleNamesToImageIPTC(fileInfo.FullName, googlePhoto.People.Select(p => p.Name).ToList());
                            }
                            if (dateCreated.Date == DateTime.Now.Date)
                            {                              
                                try
                                {
                                    DateTime result = DateTime.Parse(googlePhoto.PhotoTakenTime.Formatted.Replace("UTC", "").Trim());
                                    dateCreated = result;
                                }
                                catch { }
                            }
                        }

                    }
                    var fileNameNew = dateCreated.ToString("yyyyMMddHHmmssfff");
                    if (chkIncludeFileName.Checked) // Assuming includeFileName is accessible here
                    {
                        fileNameNew += "_" + fileInfo.Name;
                    }
                    fileNameNew += fileInfo.Extension;
                    string destinationFile = System.IO.Path.Combine(pathToCreate, fileNameNew);
                    System.IO.File.Copy(sourceFile, destinationFile, true);

                    processedFiles++;
                    int progressPercentage = (int)((float)processedFiles / (float)totalFiles * 100);
                    backgroundWorker1.ReportProgress(progressPercentage);
                }
            }
           
        }

        private string ConstructPathBasedOnFormat(string basePath, DateTime dateCreated, string fileType, string selectedFormat)
        {
            List<string> pathParts = new List<string>() { basePath }; // Start with the base path

            // Determine the structure of the path based on the selected format
            switch (selectedFormat)
            {
                case "Year/":
                    pathParts.Add(dateCreated.Year.ToString());
                    break;
                case "Year/Month/":
                    pathParts.Add(dateCreated.Year.ToString());
                    pathParts.Add(dateCreated.ToString("MM"));
                    break;
                case "Year/Month/Day/":
                    pathParts.Add(dateCreated.Year.ToString());
                    pathParts.Add(dateCreated.ToString("MM"));
                    pathParts.Add(dateCreated.ToString("dd"));
                    break;
                case "Year/Month/Day/File Types/":
                    pathParts.Add(dateCreated.Year.ToString());
                    pathParts.Add(dateCreated.ToString("MM"));
                    pathParts.Add(dateCreated.ToString("dd"));
                    pathParts.Add(fileType);
                    break;
                case "Year/File Types/":
                    pathParts.Add(dateCreated.Year.ToString());
                    pathParts.Add(fileType);
                    break;
                case "Year/Month/File Types/":
                    pathParts.Add(dateCreated.Year.ToString());
                    pathParts.Add(dateCreated.ToString("MM"));
                    pathParts.Add(fileType);
                    break;
                case "File Types/":
                    pathParts.Add(fileType);
                    break;
                case "File Types/Year/":
                    pathParts.Add(fileType);
                    pathParts.Add(dateCreated.Year.ToString());
                    break;
                case "File Types/Year/Month/":
                    pathParts.Add(fileType);
                    pathParts.Add(dateCreated.Year.ToString());
                    pathParts.Add(dateCreated.ToString("MM"));
                    break;
                default:
                    throw new ArgumentException("Unsupported path format selected.");
            }

            return Path.Combine(pathParts.ToArray());
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
            catch { }
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

        public void GroupFilesByType(string folderPath)
        {
            // Expanded file type groups
            // Define file type groups


            // Initialize a dictionary to hold the count of files in each group
            var groupCounts = new Dictionary<string, int>();

            // Initialize all group counts to 0
            foreach (var group in fileTypeGroups.Keys)
            {
                groupCounts[group] = 0;
            }

            // Get all files in the folderPath
            var files = System.IO.Directory.GetFiles(folderPath);
            if (chkIncludeSubFolders.Checked)
            {
                files = System.IO.Directory.GetFiles(folderPath, "*.*", System.IO.SearchOption.AllDirectories);
            }

            // Iterate over each file and increment the count for its group
            foreach (var file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                var group = fileTypeGroups.FirstOrDefault(g => g.Value.Contains(extension)).Key;

                if (group != null)
                {
                    groupCounts[group]++;
                }
                else
                {
                    // If the file type does not match any group, categorize it as "Other"
                    if (!groupCounts.ContainsKey("Other"))
                    {
                        groupCounts["Other"] = 0;
                    }
                    groupCounts["Other"]++;
                }
            }

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
            // Existing code to calculate groupCounts...

            // Clear existing series and chart areas
            chrtControl.Series.Clear();

            chrtControl.ChartAreas.Clear();

            // Create and add a chart area
            ChartArea chartArea = new ChartArea();
            chrtControl.ChartAreas.Add(chartArea);

            // Create a series for displaying counts
            Series series = new Series("File Groups")
            {
                ChartType = SeriesChartType.Column, // Use Column chart type for displaying counts
                IsValueShownAsLabel = true // Display the value (count) as a label on the bar              
            };

            // Add data points to the series
            int pointIndex = 0;
            foreach (var groupCount in groupCounts)
            {
                if (groupCount.Value > 0)
                {
                    // Add a data point for each group
                    pointIndex = series.Points.AddY(groupCount.Value);
                    series.Points[pointIndex].Color = GetUniqueColor(pointIndex); // Assign a unique color
                    series.Points[pointIndex].AxisLabel = groupCount.Key; // Set the axis label to the group name
                    series.Points[pointIndex].Label = $"{groupCount.Key}-{groupCount.Value}"; // Display the count as a label on the bar
                                                                                              //series.Points[pointIndex].LegendText = groupCount.Key; // Set the legend text
                    series.Points[pointIndex].ToolTip = $"{groupCount.Key}: {groupCount.Value}"; // Set the tooltip text
                                                                                                 //series.Points[pointIndex].Tag = groupCount.Key; // Store the group name in the Tag property

                    // Customize label appearance
                    series.Points[pointIndex].LabelForeColor = Color.Black; // Set label text color
                    series.Points[pointIndex].LabelBackColor = Color.Transparent; // Set label background                   
                }
            }

            // Add the series to the chart
            chrtControl.Series.Add(series);

            // Optionally, customize the chart (e.g., Axis titles)
            chrtControl.ChartAreas[0].AxisX.Title = "File Groups";
            chrtControl.ChartAreas[0].AxisY.Title = "Files Counts";

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

        private void chkIncludeSubFolders_CheckedChanged(object sender, EventArgs e)
        {
            GroupFilesByType(this.directoryPath);
        }

        private Dictionary<string, GooglePhoto> LoadGooglePhotosJsonFiles(string directoryPath)
        {
            googlePhotosDictionary = new Dictionary<string, GooglePhoto>();

            try
            {

                // Get all .json files from the directory and subdirectories
                string[] jsonFiles = System.IO.Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);

                foreach (string filePath in jsonFiles)
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
                            googlePhotosDictionary[Path.GetFileName(filePath)] = photo;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add(new ErrorModel { File = filePath, ErrorMessage = ex.Message });
                    }
                }
                if (errors.Count > 0)
                {
                    frmErrorWindow.ErrorModelList = errors;
                    frmErrorWindow.DisplayErrorsInGrid();
                    frmErrorWindow.Show();
                }


            }
            catch (Exception ex) { }
            return googlePhotosDictionary;
        }

        public void AddGeoDataToImageIfAbsent(string imagePath, double latitude, double longitude)
        {
            using (var image = new MagickImage(imagePath))
            {
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

                // Console.WriteLine("Geodata added to the image.");
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
            using (var image = new MagickImage(imagePath))
            {
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

        }
    }
}

