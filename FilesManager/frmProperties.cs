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
using Shell32;

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

            // Extended properties
            Shell shell = new Shell();
            Folder folder = shell.NameSpace(System.IO.Path.GetDirectoryName(fileName));
            FolderItem folderItem = folder.ParseName(System.IO.Path.GetFileName(fileName));

            if (folderItem != null)
            {
                // Display a selection of extended properties
                var propertiesToShow = new Dictionary<int, string>
        {
            { 2, "Type" },
            { 4, "Size" },
            { 9, "Author" },
            { 12, "Title" },
            { 18, "Date Taken" },
            { 19, "Tags" },
            { 20, "Rating" }
            // Add more indices based on the properties you need
        };

                foreach (var property in propertiesToShow)
                {
                    string value = folder.GetDetailsOf(folderItem, property.Key);
                    if (!string.IsNullOrEmpty(value))
                    {
                        lstViewProperties.Items.Add(new ListViewItem(new[] { property.Value, value }));
                    }
                }

            }


        }
    }
}