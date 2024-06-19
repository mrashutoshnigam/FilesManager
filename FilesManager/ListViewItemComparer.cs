using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesManager
{
    class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            ListViewItem item1 = x as ListViewItem;
            ListViewItem item2 = y as ListViewItem;

            // Sort by name
            if (col == 0)
            {
                returnVal = String.Compare(item1.Text, item2.Text);
            }
            // Sort by file size
            else if (col == 2) // Assuming the size is in the third column (index 2)
            {
                // Convert string to long and compare
                long size1 = long.Parse(item1.SubItems[col].Text);
                long size2 = long.Parse(item2.SubItems[col].Text);
                returnVal = size1.CompareTo(size2);
            }

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                returnVal *= -1;

            return returnVal;
        }
    }

}
