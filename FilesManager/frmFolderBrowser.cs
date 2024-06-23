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
    public partial class frmFolderBrowser : DockContent
    {
        public frmFolderBrowser()
        {
            InitializeComponent();

            // write code to load all drives, directories and folder structure in tree
            LoadDrives();

        }

        private void LoadDrives()
        {
            foreach (var drive in System.IO.DriveInfo.GetDrives().Where(x => x.DriveType == System.IO.DriveType.Fixed))
            {
                TreeNode driveNode = new TreeNode(drive.Name, 1, 1);
                driveNode.Tag = drive.Name;
                trViewFolders.Nodes.Add(driveNode);
                TreeNode dummyTreeNode = new TreeNode("dummy", 4, 4);
                driveNode.Nodes.Add(dummyTreeNode);
            }
            if (trViewFolders.Nodes.Count > 0)
            {
                trViewFolders.Nodes[0].Expand();
                trViewFolders.SelectedNode = trViewFolders.Nodes[0];
            }

        }

        private void LoadDirectories(string name, TreeNode driveNode)
        {
            try
            {
                var specialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (driveNode.Nodes.Count == 1 && driveNode.Nodes[0].Text == "dummy")
                {
                    driveNode.Nodes.Clear();
                }
                var directories = System.IO.Directory.GetDirectories(name);
                if (directories.Length == 0)
                {
                    driveNode.ImageIndex = 5;
                    driveNode.SelectedImageIndex = 5;
                    return;
                }
                driveNode.Nodes.Clear();
                foreach (var dir in directories)
                {
                    TreeNode dirNode = new TreeNode(System.IO.Path.GetFileName(dir), 4, 4);
                    dirNode.Tag = dir;
                    driveNode.Nodes.Add(dirNode);
                    if (specialFolderPath.Contains(dir))
                    {
                        TreeNode dummyTreeNode = new TreeNode("dummy", 4, 4);
                        dirNode.Nodes.Add(dummyTreeNode);
                        dirNode.Expand();
                    }
                    else
                    {
                        TreeNode dummyTreeNode = new TreeNode("dummy", 4, 4);
                        dirNode.Nodes.Add(dummyTreeNode);
                    }


                }
            }
            catch { }

        }

        private void trViewFolders_AfterExpand(object sender, TreeViewEventArgs e)
        {
            LoadDirectories(e.Node.Tag.ToString(), e.Node);
        }

        private void trViewFolders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                TreeNode node = trViewFolders.SelectedNode;
                frmMain childForm = new frmMain(node.Tag.ToString());
                childForm.Text = node.Text.ToString();
                childForm.Tag = node.Tag.ToString();
                childForm.Show(this.DockPanel, dockState: WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
            catch { }
        }

        private void trViewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }
    }
}
