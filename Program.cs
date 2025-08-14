using System;
using System.Windows.Forms;
using Store_X;

// Namespace gốc của dự án
namespace Store_X
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Dòng này vẫn không đổi và bây giờ sẽ hoạt động chính xác
            // vì FrmLogin cũng nằm trong namespace "Store_X".
            Application.Run(new FrmLogin());
        }
    }
}
