using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace GVT_Unlock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex;
            try
            {
                mutex = Mutex.OpenExisting("GVTUnlock");
            }
            catch (WaitHandleCannotBeOpenedException waitHandleCannotBeOpenedException)
            {
                mutex = new Mutex(true, "GVTUnlock");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                return;
            }
            if (mutex != null)
            {
                MessageBox.Show("O aplicativo GVT Unock já está sendo executado!\r\nFeche-o antes de executa-lo novamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                mutex.Close();
            }
        }
    }
}
