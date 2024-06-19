using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Shell32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace FilesManager
{
    public partial class frmProperties : DockContent
    {
        public frmProperties()
        {
            InitializeComponent();
        }

        public void LoadProperties(string fileName)
        {
            lstViewProperties.Items.Clear();
            var fileInfo = new System.IO.FileInfo(fileName);

            // Standard properties
            lstViewProperties.Items.Add(new ListViewItem(new[] { "Name", fileInfo.Name }));
            lstViewProperties.Items.Add(new ListViewItem(new[] { "Size", fileInfo.Length.ToString() }));
            lstViewProperties.Items.Add(new ListViewItem(new[] { "Created", fileInfo.CreationTime.ToString() }));
            lstViewProperties.Items.Add(new ListViewItem(new[] { "Modified", fileInfo.LastWriteTime.ToString() }));
            lstViewProperties.Items.Add(new ListViewItem(new[] { "Attributes", fileInfo.Attributes.ToString() }));

            //// Extended properties
            //Shell shell = new Shell();
            //Folder folder = shell.NameSpace(System.IO.Path.GetDirectoryName(fileName));
            //FolderItem folderItem = folder.ParseName(System.IO.Path.GetFileName(fileName));

        //    if (folderItem != null)
        //    {
        //        // Display a selection of extended properties
        //        var propertiesToShow = new Dictionary<int, string>
        //{
        //    { 2, "Type" },
        //    { 4, "Size" },
        //    { 9, "Author" },
        //    { 12, "Title" },
        //    { 18, "Date Taken" },
        //    { 19, "Tags" },
        //    { 20, "Rating" }
        //    // Add more indices based on the properties you need
        //};

        //        foreach (var property in propertiesToShow)
        //        {
        //            string value = folder.GetDetailsOf(folderItem, property.Key);
        //            if (!string.IsNullOrEmpty(value))
        //            {
        //                lstViewProperties.Items.Add(new ListViewItem(new[] { property.Value, value }));
        //            }
        //        }

        //    }
            ReadIptcMetadata(fileName);
            //ReadDateTaken(fileName);

        }
        public void ReadIptcMetadata(string imagePath)
        {
            try
            {
                // Extract metadata directories from the image
                IReadOnlyList<Directory> directories = ImageMetadataReader.ReadMetadata(imagePath);

                // Find the IPTC directory
                foreach (Directory iptcDirectory in directories)
                {
                    if (iptcDirectory != null)
                    {
                        foreach (var tag in iptcDirectory.Tags)
                        {
                            lstViewProperties.Items.Add(new ListViewItem(new[] { tag.Name, tag.Description }));
                            Console.WriteLine($"{tag.Name}: {tag.Description}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No IPTC data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading metadata: {ex.Message}");
            }
        }
        public void ReadDateTaken(string imagePath)
        {
            try
            {
                // Extract metadata directories from the image
                var directories = ImageMetadataReader.ReadMetadata(imagePath);

                // Find the EXIF IFD0 directory
                var exifIfd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

                // Alternatively, to directly find the EXIF SubIFD directory you can use:
                // var exifSubIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                if (exifIfd0Directory != null)
                {
                    // Attempt to retrieve the DateTimeOriginal value
                    if (exifIfd0Directory.TryGetDateTime(ExifDirectoryBase.TagDateTime, out DateTime dateTaken))
                    {
                        MessageBox.Show($"Date Taken: {dateTaken}");
                    }
                    else
                    {
                        MessageBox.Show("Date Taken tag not found.");
                    }
                }
                else
                {
                    MessageBox.Show("EXIF IFD0 directory not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading metadata: {ex.Message}");
            }
        }
    }
}