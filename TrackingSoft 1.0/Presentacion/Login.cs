using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using Negocio_TrackingSoft;
using TrackingSoft_1._0.Properties;
using System.Resources;
using System.Data.Odbc;

namespace TrackingSoft_1._0.Admistracion_Usuario
{
    public partial class Login : MetroForm
    {
        public Int32 x = Settings.Default.Estilo; // estilo [1..15]
        public bool y = true; // temas {Blanco , Negro}
        public string cnxBD;
        public ResourceManager recursos;
        public bool detenerVerif;
        public Login()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            txtUser.GotFocus += txtUser_GotFocus;    
            txtPassword.GotFocus += txtPassword_GotFocus;
            txtUser.LostFocus += txtUser_LostFocus;
            txtPassword.LostFocus += txtPassword_LostFocus;
            //cnxBD = Settings.Default.Seguimiento_ProyectosYPFB_AviacionConnectionString;
            cnxBD = getCadenaConexion();
            Auxiliar.setConexionStringBD(cnxBD);
            detenerVerif = false;
        }

        public string getCadenaConexion()
        {
            string cadena = "";
            OdbcConnection conexionPrueba = null;
            try // servidor remoto DESARROLLO .. pendiente a modificaciones
            {
                conexionPrueba = new OdbcConnection(Settings.Default.Seguimiento_ProyectosYPFB_AviacionConnectionStringRemote);
                conexionPrueba.Open();
                conexionPrueba.Close();
                cadena = Settings.Default.Seguimiento_ProyectosYPFB_AviacionConnectionStringRemote;                
            }
            catch (Exception) // servidor local HP
            {
                conexionPrueba = new OdbcConnection(Settings.Default.Seguimiento_ProyectosYPFB_AviacionConnectionString);
                conexionPrueba.Open();
                conexionPrueba.Close();
                cadena = Settings.Default.Seguimiento_ProyectosYPFB_AviacionConnectionString;                
            }
            return cadena;
        }
        void txtPassword_LostFocus(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(""))
            {
                if (y == true) // fondo normal 
                {
                    txtPassword.ForeColor = Color.LightGray;
                }
                else
                {
                    txtPassword.ForeColor = Color.White;
                }
                //txtPassword.ForeColor = Color.LightGray;
                
                txtPassword.Text = "Ingrese Password";
                
            }
        }

        void txtUser_LostFocus(object sender, EventArgs e)
        {
            if (txtUser.Text.Equals(""))
            {
                if (y == true) // fondo normal 
                {
                    txtUser.ForeColor = Color.LightGray;
                }
                else
                {
                    txtUser.ForeColor = Color.White;
                }
                //txtUser.ForeColor = Color.LightGray;

                txtUser.Text = "Ingrese User";
            }
        }

        void txtPassword_GotFocus(object sender, EventArgs e)
        {
            //txtPassword.Text = "";
            if (txtPassword.Text.Equals("Ingrese Password"))
            {
                if (y==true) // fondo normal
                {
                    txtPassword.ForeColor = Color.Black;
                }
                else
                {
                    txtPassword.ForeColor = Color.White;
                }
                
                txtPassword.Text = "";
                txtPassword.UseSystemPasswordChar = true;                
            }      
        }

        void txtUser_GotFocus(object sender, EventArgs e)
        {
            if (txtUser.Text.Equals("Ingrese User"))
            {
                if (y == true) // fondo normal
                {
                    txtUser.ForeColor = Color.Black;
                }
                else
                {
                    txtUser.ForeColor = Color.White;
                }
                
                txtUser.Text = "";
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // cambios donde se debe realizar las conexiones [MODIFICADO 25/01/17]
            //MessageBox.Show("\nRealizar los cambios de las conexiones para el servidor de la empresa\nAcceso Datos-> Bitacora\nPresentacion -> Inicio\nPresentacion -> Login\n\nPara todos los casos cambiar la conexion odbc fijarse en los settings");
            // iniciar con los datos que fueron guardados
            txtUser.Text = Properties.Settings.Default.Usuario;
            txtPassword.Text = Properties.Settings.Default.Password;
            txtPassword.UseSystemPasswordChar = true;
            this.metroStyleManager1.Style = (MetroColorStyle)Settings.Default.Estilo;
            Global.valorDeEstilo = Settings.Default.Estilo;
            rbtnRecordarLoggin.Checked = Properties.Settings.Default.Recordar;
            //Global.valorDeEstilo = x; // guardar valor de estilo
            y = true;
            
            this.Icon = Properties.Resources.yyyy;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
           // MetroMessageBox.Show(this, "ASFASFASF", "NADA de NADA", 200);
;
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            x++;
            if (x != 2) {
                this.metroStyleManager1.Style = (MetroColorStyle)x;
                //Settings.Default.Estilo = x;
                //Settings.Default.Save();
                Global.valorDeEstilo = x; // guardar valor de estilo
                if (x > 15)
                {
                    x = 1;
                }
            }
                    
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {
            //Bitmap imagenFondoLogin;
            if (y==true)
            {
                metroStyleManager1.Theme = MetroThemeStyle.Dark;
                ajustarColorFuente();
                pictureBox1.Image = Resources.monitoreo_4;
            }
            else
            {
                metroStyleManager1.Theme = MetroThemeStyle.Light;
                //pictureBox1.Image = Image.FromFile(@"C:\Users\USUARIO\Desktop\seguimiento.png");
                pictureBox1.Image = Resources.seguimiento;
            }
            Global.temaActual = metroStyleManager1.Theme;
            y = !y;
        }

        private void ajustarColorFuente()
        {
            //txtUser.ForeColor = Color.White;
            //txtPassword.ForeColor = Color.White;
            metroLabel1.ForeColor = Color.White;
            metroLabel1.ForeColor = Color.White;
            lnkOlvidoContraseña.ForeColor = Color.White;
            metroLink1.ForeColor = Color.White;
            metroLink2.ForeColor = Color.White;
            
            //pictureBox1.Image = Image.FromFile(@"C:\monitoreo-4.jpg");
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Length > 0 && txtPassword.Text.Length > 0 /*&& txtUser.Text.Equals("Ingrese User") && txtPassword.Text.Equals("Ingrese Password")*/)
            {
                pgSpnrLogin.Visible = true;
                tmrLogin.Start();
            }
            else
            {
                MetroMessageBox.Show(this, "Es necesario que Rellene los campos de [USERNAME] Y [PASSWORD] porfavor intente nuevamente...", "ACCESO DE USUARIOS");
            }
            /*int x=Gestionar_Usuario.validarUsuario(txtUser.Text, txtPassword.Text) ;
            //tmrLogin.Stop();
            if (x>0)
            {
                //tmrLogin.Start();
                asignarUsuarioActual();
                Gestionar_Usuario.iniciarSesion(Global.IDSesion, Global.IDUser);
                
                MetroMessageBox.Show(this,"BIENVENIDO AL SOFTWARE DE SEGUIMIENTO A PROYECTOS : TRACKINGSOFT 1.0","ACCESO DE USUARIOS");
                Inicio ini = new Inicio();
                ini.Show();
                this.Hide();
                 
                 
                /*ini.Visible = true;
                this.Visible = false;*/
                /*this.Hide();
                ini.ShowDialog();
                
                this.Close();*/
            /*}
            else 
            {
                MetroMessageBox.Show(Owner, "Usuario no Existente y/o datos invalidos de Password, intente Nuevamente...","ACCESO DE USUARIOS");
            }
            pgSpnrLogin.Visible = false;*/
        }

        private void asignarUsuarioActual()
        {
            Global.IDSesion = GenerarID(Gestionar_Usuario.lastIDSesion());
            DataTable dt = Gestionar_Usuario.ObtenerUsuario(txtUser.Text,txtPassword.Text);
            foreach (DataRow fila in dt.Rows ){
                Global.IDUser =fila[0].ToString().Trim();
                Global.userName = fila[1].ToString().Trim();
                Global.nombreCompleto = fila[2].ToString().Trim()+ " " + fila[3].ToString().Trim();
                Global.cargoPersonal = fila[4].ToString().Trim();
            }

        }
        
        private string GenerarID(string ultimoID)
        {
            int nro;
            if (!ultimoID.Equals("")) // ya existen sesiones iniciadas
            {
                nro = int.Parse(ultimoID.Substring(2));
                nro++;
            }
            else // primera sesion a iniciar
            {
                ultimoID = "Se";
                nro = 1;
            }
            if (nro < 10)
            {
                return ultimoID.Substring(0, 2) + '0' + nro.ToString();
            }
            else
            {
                return ultimoID.Substring(0, 2) + nro.ToString();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            detenerVerif = true;
            Global.verif = detenerVerif;
            //Application.Exit();
        }

        private void tmrLogin_Tick(object sender, EventArgs e)
        {
            pgSpnrLogin.Value += 10;
            if (pgSpnrLogin.Value.Equals(pgSpnrLogin.Maximum))//Equals(pgSpnrLogin.Maximum))
            {
                tmrLogin.Stop();
                int x=Gestionar_Usuario.validarUsuario(txtUser.Text, txtPassword.Text) ;
                //pgSpnrLogin.Visible = false;
                if (x > 0)
                {
                    //tmrLogin.Start();
                    asignarUsuarioActual();
                    Gestionar_Usuario.iniciarSesion(Global.IDSesion, Global.IDUser);

                    MetroMessageBox.Show(this, "BIENVENIDO AL SOFTWARE DE SEGUIMIENTO A PROYECTOS : TRACKINGSOFT 1.0", "ACCESO DE USUARIOS");

                    // guardar la sesion del usuario
                    Settings.Default.isLogin = true;
                    Settings.Default.Save();

                    Inicio indexForm = new Inicio();
                    indexForm.Show();
                    Close();
                }
                else
                {
                    pgSpnrLogin.Visible = false;
                    MetroMessageBox.Show(Owner, "Usuario no Existente y/o datos invalidos de Password, intente Nuevamente...", "ACCESO DE USUARIOS");
                }

                pgSpnrLogin.Value = pgSpnrLogin.Minimum;
            }
        }

        private void lnkOlvidoContraseña_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "Utilize su Alias como [USERNAME] Y [PASSWORD], caso contrario contactese con el administrador del Sistema...", "ACCESO DE USUARIOS");    
        }

        // evento que se ejecuta al seleccionar/desseleccionar el control
        private void rbtnRecordarLoggin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnRecordarLoggin.Checked)
            {
                Settings.Default.Usuario = txtUser.Text;
                Settings.Default.Password = txtPassword.Text;
                Settings.Default.Recordar = true;
                Settings.Default.Estilo = x;
                //Settings.Default.Save();
            }
            else
            {
                Settings.Default.Usuario = "";
                Settings.Default.Password = "";
                Settings.Default.Recordar = false;
                Settings.Default.Estilo = 3; // estilo por defecto
            }
            Settings.Default.Save();
        }
       
    }
}
