using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLease_Admin.UserControls.Methods
{
    public class SetInput
    {
        public void SetDatepicker(DateTimePicker dtp, DateTime value, int min, int max)
        {
            dtp.MinDate = DateTime.Now.AddYears(min);
            dtp.MaxDate = DateTime.Now.AddYears(max);
            dtp.Value = value;
        }

        public void SetNumUpDown(NumericUpDown numPicker, int value, int min, int max)
        {
            numPicker.Maximum = max;
            numPicker.Minimum = min;
            numPicker.Value = value;
        }
    }
}
