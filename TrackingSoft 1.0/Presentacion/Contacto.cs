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

namespace TrackingSoft_1._0.Presentacion
{
    public partial class Contacto : MetroForm
    {
        public EnvioEmail servidorCorreos;
        public Contacto()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            metroStyleManager1.Style = (MetroColorStyle)Global.valorDeEstilo;
            metroStyleManager1.Theme = Global.temaActual;
        }

        private void Contacto_Load(object sender, EventArgs e)
        {
            // cambiar el icono del formulario manualmente
            this.Icon = Properties.Resources.tttt;
            txtFrom_Contacto.Text = Gestionar_Usuario.obtenerEmail(Global.IDUser);
            servidorCorreos = new EnvioEmail();
        }

        private void btnEnviarEmail_Contacto_Click(object sender, EventArgs e)
        {
            string direccionCorreoSelect=cmbTo_Contacto.GetItemText(cmbTo_Contacto.SelectedItem);
            servidorCorreos.añadirDestinatario(direccionCorreoSelect);
            string asunto = txtFrom_Contacto.Text + " ----> " + txtSubject_Contacto.Text;
            string mensaje = txtBody_Contacto.Text;
            if (servidorCorreos.enviarCorreo(asunto,mensaje))
            {
                MessageBox.Show("Mensaje Enviado Satisfactoriamente!!!");
            }
            else
            {
                MessageBox.Show("No se pudo enviar el Mensaje, vuelva a intentarlo!!!");
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "La funcion de Adjuntar Documentos, aun no esta disponible", "TRACKINGSOFT");
        }
    }
}
