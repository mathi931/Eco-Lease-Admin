using System;
using System.Data;

namespace EcoLease_Admin.UserControls.Functions
{
    public static class DataTableExtensions
    {
        public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        {
            var colIndex = 0; foreach (var colName in columnNames) table.Columns[colName].SetOrdinal(colIndex++);
        }
    }
}
