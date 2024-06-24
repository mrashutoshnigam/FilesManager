using FilesManager.Models;
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
    
    public partial class frmErrorWindow : DockContent
    {
        public List<ErrorModel> ErrorModelList { get; set; }
        public frmErrorWindow()
        {
            InitializeComponent();
            dataGridViewErrors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DisplayErrorsInGrid();
        }

        public void DisplayErrorsInGrid()
        {
            // Assuming dataGridViewErrors is your DataGridView control
            // and ErrorModelList is your list of error models

            dataGridViewErrors.DataSource = ErrorModelList;

            // Auto-size columns based on both header and cell content
            dataGridViewErrors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
