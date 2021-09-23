using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLease_Admin.UserControls.Methods
{
    public static class MessageBoxes
    {
        public static void errorMessage(string text, string title)
        {
            MessageBox.Show(text, title,
    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult exitMessage()
        {
            var msg = MessageBox.Show("Are you sure to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return msg;
        }

        public static DialogResult deleteMessage(string target)
        {
            var msg = MessageBox.Show($"Are you sure to remove {target}?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return msg;
        }

        public static DialogResult infoMessage(string content)
        {
            var msg = MessageBox.Show(content, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return msg;
        }
        public static DialogResult dynamicQuestion(string action, string target, string title)
        {
            var msg = MessageBox.Show($"Are you sure to {action} {target}?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return msg;
        }
    }
}
