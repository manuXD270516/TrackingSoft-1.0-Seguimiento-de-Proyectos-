using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;


namespace Negocio_TrackingSoft
{
    public class EnvioEmail
    {
        public Email email;

        public EnvioEmail()
        {
            email = new Email();
        }

        public void añadirDestinatario(string dirCorreo)
        {
            email.addTO(dirCorreo);
        }
        public bool enviarCorreo(string asunto,string cuerpoMensaje)
        {
            email.From = "Seguimiento.Proyecto@ypfbaviacion.com.bo";
            email.Subject = asunto;
            email.Body = cuerpoMensaje;
            email.IsBodyHTML = true;
            if (email.To.Count > 0)
            {
                return email.enviarEmail();
            }
            return false;
        }
    }
}
