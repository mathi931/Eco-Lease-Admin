using System.Collections.Generic;
using System.Windows.Forms;

namespace EcoLease_Admin.UserControls.Functions
{
    public static class DataGridViewMethods
    {
        public static bool isSelected(DataGridView dgv)
        {
            return dgv.SelectedRows.Count > 0 ? true : false;
        }

        public static void updateDgv<T>(DataGridView dgv, IEnumerable<T> list, IEnumerable<T> local)
        {
            local = list;
            dgv.DataSource = local;
        }
    }
}
