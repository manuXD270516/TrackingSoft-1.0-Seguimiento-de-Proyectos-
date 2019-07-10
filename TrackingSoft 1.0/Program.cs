using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackingSoft_1._0.Admistracion_Usuario;
using TrackingSoft_1._0.Properties;

namespace TrackingSoft_1._0
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new Presentacion.prueba());
            //Application.Run(new Admistracion_Usuario.Login());
            if (bool.Parse(Settings.Default["isLogin"].ToString())) // usuario dejo su cuenta abierta (ir IndexView)
            {
                Inicio formIndex = new Inicio();
                formIndex.FormClosed += ViewForm_FormClosed;
                formIndex.Show();
            }
            else // usuario log out (ir LogingView)
            {
                Login formLogin = new Login();
                formLogin.FormClosed += ViewForm_FormClosed;
                formLogin.Show();
            }
            Application.Run();
        }

        private static void ViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= ViewForm_FormClosed;

            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
            else
            {
                Application.OpenForms[0].FormClosed += ViewForm_FormClosed;
            }
        }
    }
}
