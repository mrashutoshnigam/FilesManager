using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesManager
{
    public partial class frmMDIParent : Form
    {
        private int childFormNumber = 0;

        public frmMDIParent()
        {
            InitializeComponent();
            this.dockPnl.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            frmFolderBrowser childForm = new frmFolderBrowser();
            childForm.Show(this.dockPnl, dockState: WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            frmMain childForm = new frmMain("None");

            childForm.Text = "Window " + childFormNumber++;
            childForm.Show(this.dockPnl, dockState: WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {

            try
            {
                // Specify the URL you want to open
                string url = "https://www.mrashutoshnigam.in";

                // Open the URL in the default browser
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                // Handle exceptions, if any
                MessageBox.Show("Unable to open the URL: " + ex.Message);
            }

        }
    }
}
