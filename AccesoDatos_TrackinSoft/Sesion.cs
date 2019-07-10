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
    public class Sesion
    {
        public String ID { get; set; }

        public DateTime Hora_Inicio { get; set; }

        public DateTime Hora_Fin { get; set; }

        public Usuario IDUsuario { get; set; }

        public Sesion()
        {
            this.IDUsuario = new Usuario();
        }

        public Sesion(string id, DateTime horainicio, DateTime horafin, Usuario iduser)
        {
            this.ID = id;
            this.Hora_Inicio = horainicio;
            this.Hora_Fin = Hora_Fin;
            this.IDUsuario = iduser;
        }

        public static void iniciar(Sesion sesion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@id";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = sesion.ID;
                lista.Add(p1);

                p2.ParameterName = "@iduser";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 10;
                p2.Value = sesion.IDUsuario.ID;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("iniciarSesion", lista, "Escritura");            
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Conexion.cerrarConexion();
            }            
        }

        public static void cerrar(Sesion sesion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@id";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = sesion.ID;
                lista.Add(p1);

                p2.ParameterName = "@iduser";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 10;
                p2.Value = sesion.IDUsuario.ID;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("cerrarSesion", lista, "Escritura");            
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Conexion.cerrarConexion();
            }            
        }

        public static string ultimoID()
        {
            try
            {
                Conexion.abrirConexion();
                //OdbcDataReader dr = Conexion.ObtenerTuplas("select Top(1) ID from Sesion order by 1 desc");
                OdbcDataReader dr = Conexion.ObtenerTuplas("select ID from Sesion where Hora_Inicio=(select MAX(Hora_Inicio) from Sesion)");
                string x;
                if (dr.Read())
                {
                    x = dr.GetString(0);
                }
                else
                {
                    x = "";
                }
                return x.Trim();            
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

        public int getNroSessionToday()
        {
            string sql = "select count(*) from Sesion where CAST(Hora_Inicio as date)=CAST(GETDATE() as date)";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                int x = -1;
                if (dr.Read())
                {
                    x = dr.GetInt32(0);
                }
                return x;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return -1;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }
    }
}
