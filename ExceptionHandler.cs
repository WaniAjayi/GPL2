using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPL2
{
    internal class ExceptionHandler
    {
        public static void ExecuteWithHandling(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                // Instead of logging, you might just show a user-friendly error message
                ShowUserFriendlyError(ex.Message);
            }
        }

        private static void ShowUserFriendlyError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
