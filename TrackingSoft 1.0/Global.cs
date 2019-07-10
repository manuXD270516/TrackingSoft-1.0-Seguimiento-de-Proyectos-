using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Forms;
using MetroFramework;
using System.Data;

namespace TrackingSoft_1._0
{
    public class Global
    {
        #region variables de Interfaz

        public static int valorDeEstilo;

        public static MetroThemeStyle temaActual;
        #endregion

        #region variables de Usuario e Inicio de Sesion
        // Usuario
        public static string IDUser;
        public static string userName;
        public static string nombreCompleto;
        public static string cargoPersonal;
        // Sesion
        public static string IDSesion;
        // Grupo
        public static int privilegios;
        // Proyecto
        public static string codProyecto;
        public static string nombreProyecto;
        public static string tipoProyecto;
        // Componente
        public static string codComponente;
        public static string nombreComponente;
        public static string tipoModifComponente;
        // condicional de Asignacion de Autorizaciones
        public static bool autorizAutomatica;
        // Asignacion de fases obtener el IDfase seleccionado de la tabla que se quiere cambiar el responsable
        public static string idFaseSelect;
        // variable para obtener la cadena de conexion
        //public static string cadenaCnx=TrackingSoft_1._0.Properties.Settings.Default.Seguimiento_ProyectosYPFB_AviacionConnectionString;
         

        // variable para detener la ejecucion del hilo para las verificaciones de estado
        public static bool verif=false;
        // variables para obtener las fechas de la fase_componente
        public static DateTime fechaInicio,fechaFin;
        // variablea para filtrar la seleccion de tareas de un usuario
        public static string filtroTareas;
        public static bool bandera;
        // variables para las notificaciones en autorizaciones
        public static string notifAsig;
        // varibles para tener referencias de los id de las notificaciones
        public static int idAlerta;
        // variable para tener referencia del id de la boleta seleccionada y de la tabla
        public static int idboleta;
        public static DataTable tablaBoletasPorVencer;
        // variables para tener referencia de la fase seleccionada en la actualizacion de dias
        public static string nombreFase;
        public static int diasFase;

        #endregion

        #region metodos Globales auxiliares
        public static  int primerDigito(string x)
        {
            //char[] v = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < x.Length; i++)
            {
                if (Char.IsDigit(x[i]))
                {
                    return i;
                }
            }
            return -1; // id sin digitos
        }
        public static  string GenerarID(string ultimoID, string cod)
        {
            int nro;
            if (!ultimoID.Equals("")) // ya existen tuplas en la tabla // proy09
            {
                nro = int.Parse(ultimoID.Substring(primerDigito(ultimoID)));
                ultimoID = ultimoID.Substring(0, primerDigito(ultimoID));
                nro++;
            }
            else // primera sesion a iniciar
            {
                ultimoID = cod;
                nro = 1;
            }
            if (nro < 10)
            {
                //return ultimoID.Substring(0, primerDigito(ultimoID)) + '0' + nro.ToString();
                return ultimoID + '0' + nro.ToString();
            }
            else
            {
                return ultimoID  + nro.ToString();
                //return ultimoID.Substring(0, primerDigito(ultimoID)) + nro.ToString();
            }
        }

        #endregion
    }
}
