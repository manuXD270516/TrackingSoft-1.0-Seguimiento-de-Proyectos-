using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Bitacora
    {
        public Int32 ID { get; set; }

        public DateTime Fecha_Hora { get; set; }
        public String Usuario { get; set; } 
        public String Accion { get; set; }

        public String UserSystem { get; set; }

        public String Aplicacion { get; set; }

        public String Terminal { get; set; }

        public Sesion IDSesion { get; set; }

        public Bitacora()
        {
            IDSesion = new Sesion();
        }

        public Bitacora(int id,DateTime fecha,string usuario, string accion, string usersystem, string app, string terminal, Sesion idSesion)
        {
            this.ID = id;
            this.Fecha_Hora = fecha;
            this.Usuario = usuario;
            this.Accion = accion;
            this.UserSystem=usersystem;
            this.Aplicacion = app;
            this.Terminal = terminal;
            this.IDSesion = IDSesion;
        }


        public DataTable getTableBitacora(int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql;
                if (tipo.Equals(1)) // mostrar general
                {
                    sql = "select ID as [#Reg],Tabla,Accion,Fecha_Hora as [Fecha],UserSystem,Aplicacion,Terminal from Bitacora";
                }
                else // mostrar por fechas
                {
                    sql = "select ID as [#Reg],Tabla,Accion,Fecha_Hora as [Fecha],UserSystem,Aplicacion,Terminal from Bitacora where Fecha_Hora>='" + this.Fecha_Hora + "';";
                }
                DataTable dt = new DataTable();
                OdbcDataAdapter dr = new OdbcDataAdapter(sql, Conexion.conexion);
                dr.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public void makeBackupDB()
        {
            Conexion.abrirConexion();
            string nameComplete = this.Accion + ".bak";
            // formatear a una cadena accesible en el SQL
            nameComplete=nameComplete.Replace('/','_');
            nameComplete=nameComplete.Replace(':','_');
            // comando SQL
            string sql = "BACKUP DATABASE [Seguimiento_ProyectosYPFB_Aviacion] TO  DISK = N'C:\\" + nameComplete + "'" +
                        " WITH NOFORMAT, INIT,  NAME = N'Seguimiento_ProyectosYPFB_Aviacion-Completa Base de Datos Copia de Seguridad'" +
                        " , SKIP, NOREWIND, NOUNLOAD,  STATS = 10";//\nGO";
            try
            {
                Conexion.ejecutarConsulta(sql);                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                Conexion.cerrarConexion();
            }

            /*VisualizarHistorialAcciones.realizarBackup
             * BACKUP DATABASE [PSF] TO  DISK = N'C:\test.bak' WITH NOFORMAT
             * , INIT,  NAME = N'PSF-Completa Base de datos Copia de seguridad', SKIP, NOREWIND
             * , NOUNLOAD,  STATS = 10 GO
             * */
        }

        public OdbcConnection selectConexion()
        {
            OdbcConnection conexion=null;
            try // servidor remoto DESARROLLO .. pendiente de modificaciones
            {
                conexion = new OdbcConnection(Properties.Settings.Default.conexionAuxiliarMaster2/*"Data Source=.;Initial Catalog=master;Integrated Security=True"*/);
                conexion.Open();
                conexion.Close();             
            }
            catch (Exception) // servidor local HP
            {
                conexion = new OdbcConnection(Properties.Settings.Default.conexionAuxiliarMaster/*"Data Source=.;Initial Catalog=master;Integrated Security=True"*/);
                conexion.Open();
                conexion.Close();
            }
            return conexion;
        }
        public void getRestoreDB()
        {
            //Conexion.abrirConexion();
            OdbcConnection conexion = selectConexion();
            //OdbcConnection conexion = new OdbcConnection(Properties.Settings.Default.conexionAuxiliarMaster/*"Data Source=.;Initial Catalog=master;Integrated Security=True"*/);
            conexion.Open();
            string sql="RESTORE DATABASE [Seguimiento_ProyectosYPFB_Aviacion] FROM DISK = N'"+this.Accion+"' WITH FILE = 1,  MOVE N'Seguimiento_ProyectosYPFB_Aviacion_log' TO N'C:\\Program Files\\Microsoft SQL Server\\MSSQL10.MSSQLSERVER\\MSSQL\\DATA\\Seguimiento_ProyectosYPFB_Aviacion.ldf', NOUNLOAD,  REPLACE,  STATS = 10";
            try
            {
                OdbcCommand ComandExct = new OdbcCommand(sql, conexion);
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta("use master");
                Conexion.cerrarConexion();
                ComandExct.ExecuteNonQuery();
                conexion.Close();
                
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta("use Seguimiento_ProyectosYPFB_Aviacion");
                Conexion.cerrarConexion();
                
                //Conexion.ejecutarConsulta(sql);
                //Conexion.cerrarConexion();            
            }
            catch (Exception ex)
            {
                string error = ex.Message;                
            }
            
            /*
             * RESTORE DATABASE [PruebaSql] FROM 
             * DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\Backup\PruebaSql.bak' WITH 
             * FILE = 1,  MOVE N'PruebaSql_log' TO N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\PruebaSql_1.LDF',
             * NOUNLOAD,  REPLACE,  STATS = 10 GO
            */
        }

        public static void getGlobalVerif(bool valor)
        {
            valor = !valor;  
        }

       
    }
}
