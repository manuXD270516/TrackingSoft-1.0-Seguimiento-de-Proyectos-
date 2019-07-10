using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos_TrackinSoft
{
    public class Email
    {

        #region campos constantes sin modificacion para el servidor SMTP 
        
        private static string HOST = "AVEXCHANGE.ypfbaviacion.com.bo";
        private static int PUERTO = 25;
        private static bool HABILITAR_SSL = false;
        private static bool USAR_CREDENCIALES_DEFAULT = false;
        private static string USER = "seg_py";
        private static string PASS = "pr0yecto";


        #endregion
        #region Atributos y Campos Necesarios

        public List<string> To;
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHTML { get; set; }
        
        
        #endregion

        public Email()
        {
            //////////////////
            To = new List<string>();
        }

        public Email(List<string> to, string from, string subject, string body,bool isBodyHtml)
        {
            this.To = to;
            this.From = from;
            this.Subject = subject;
            this.Body = body;
            this.IsBodyHTML = isBodyHtml;
        }

        public void addTO(string correo)
        {
            To.Add(correo);
        }

        public bool enviarEmail()
        {
            MailMessage mail = new MailMessage();
            // recorrer la lista de direcciones a las cuales se enviara el correo
            foreach (string dir in To)
            {
                mail.To.Add(dir);
            }
            mail.From = new MailAddress(this.From);
            mail.Subject = this.Subject;
            mail.Body = this.Body;
            mail.IsBodyHtml = this.IsBodyHTML;
            mail.Priority = MailPriority.Normal;
            // crear la instancia para el servidor SMTP
            SmtpClient smtp = new SmtpClient();
            smtp.Host = HOST;
            smtp.Port = PUERTO;
            smtp.EnableSsl = HABILITAR_SSL;
            smtp.UseDefaultCredentials = USAR_CREDENCIALES_DEFAULT;
            smtp.Credentials = new NetworkCredential(USER, PASS);
            try
            {
                smtp.Send(mail);
                mail.Dispose();
                return true;
            }
            catch (Exception ex )
            {
                string error = ex.Message;
                return false;
                //throw new Exception(error);
            }            
        }
    }
}
