using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;
using System.Data.SqlClient;

namespace Negocio_TrackingSoft
{
    public class VisualizarHistorialAcciones
    {
        public static Bitacora bitacora;

        public static DataTable mostrarHistorialAcciones(DateTime fecha,int tipo){
            bitacora = new Bitacora();
            bitacora.Fecha_Hora = fecha;
            return bitacora.getTableBitacora(tipo);
        }

        public static void generarBackupDB(string nombreArchivo, string fechaActual)
        {
            bitacora = new Bitacora();
            bitacora.Accion = fechaActual + '_' + nombreArchivo;
            bitacora.makeBackupDB();
        }

        public static void restoreDatabase(string direccion)
        {
            bitacora = new Bitacora();
            bitacora.Accion = direccion;
            bitacora.getRestoreDB();
        }
    }
}
