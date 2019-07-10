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
using Negocio_TrackingSoft;
using MetroFramework;

namespace TrackingSoft_1._0.Presentacion
{
    public partial class NotificacionBoletas : MetroForm
    {
        public NotificacionBoletas()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            metroStyleManager1.Style = (MetroColorStyle)Global.valorDeEstilo;
            metroStyleManager1.Theme = Global.temaActual;            
        }

        private void NotificacionBoletas_Load(object sender, EventArgs e)
        {
            // cambiar manualmente el icono
            this.Icon = Properties.Resources.uuuu;
            this.StyleManager = metroStyleManager1;
            metroStyleManager1.Style = (MetroColorStyle)Global.valorDeEstilo;
            metroStyleManager1.Theme = Global.temaActual;   
           
            gvBoletasPorVencer.DataSource = Global.tablaBoletasPorVencer;
            gvBoletasPorVencer.Style = (MetroColorStyle)Global.valorDeEstilo;
            Auxiliar.ajustarTamañoTabla(gvBoletasPorVencer);
        }
    }
}
