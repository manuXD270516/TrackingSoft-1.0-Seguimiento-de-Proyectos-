using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Autorizacion
    {
        public String Codigo { get; set; }

        public String Descripcion { get; set; }

        public String EnteRegulador { get; set; }

        public Tipo_Proyecto CodTipo_Proyecto { get; set; }

        public Autorizacion()
        {
            CodTipo_Proyecto=new Tipo_Proyecto();
        }

        public Autorizacion(string codigo,string descripcion,string enteregulador,Tipo_Proyecto codtipoProy)
        {
            this.Codigo = codigo;
            this.Descripcion = descripcion;
            this.EnteRegulador = enteregulador;
            this.CodTipo_Proyecto = codtipoProy;
        }

        public static void insertar(Autorizacion autorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p3, p4, p5;
                p1 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = autorizacion.Codigo;
                lista.Add(p1);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 250;
                p3.Value = autorizacion.Descripcion;
                lista.Add(p3);

                p4.ParameterName = "@enteR";
                p4.OdbcType = OdbcType.VarChar;
                p4.Size = 50;
                p4.Value = autorizacion.EnteRegulador;
                lista.Add(p4);

                p5.ParameterName = "@codtipo_proy";
                p5.OdbcType = OdbcType.Int;
                p5.Value = autorizacion.CodTipo_Proyecto.Codigo;
                lista.Add(p5);

                Conexion.EjecutarProcedimientoAlmacenado("insertAutorizacion", lista, "Escritura");
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

        public static void update(Autorizacion autorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p3, p4;
                p1 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = autorizacion.Codigo;
                lista.Add(p1);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 250;
                p3.Value = autorizacion.Descripcion;
                lista.Add(p3);

                /*p4.ParameterName = "@enteR";
                p4.OdbcType = OdbcType.VarChar;
                p4.Size = 50;
                p4.Value = autorizacion.EnteRegulador;
                lista.Add(p4);*/

                Conexion.EjecutarProcedimientoAlmacenado("updateAutorizacion", lista, "Escritura");
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

        public static void delete(Autorizacion autorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = autorizacion.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteAutorizacion", lista, "Escritura");
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

        public static DataTable mostrar(Autorizacion autorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                DataTable dt = new DataTable();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@tipoAutoriz";
                p1.OdbcType = OdbcType.Int;
                p1.Value = autorizacion.CodTipo_Proyecto.Codigo;
                lista.Add(p1);

                dt = Conexion.EjecutarProcedimientoMostrar("mostrarAutorizacion", lista); 
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

        public static List<Autorizacion> listaDatos(int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Codigo,Descripcion from Autorizacion where CodTipo_Proy=" + tipo);
                List<Autorizacion> ltipo = new List<Autorizacion>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Autorizacion x = new Autorizacion();
                    x.Codigo = dr.GetString(0).Trim();
                    x.Descripcion = dr.GetString(1).Trim();
                    ltipo.Add(x);
                }
                return ltipo;
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

        public static string ultimoCodigo()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Top(1) Codigo from Autorizacion order by 1 desc");
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

        public static DataTable obtenerDatos(Autorizacion autorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codautoriz";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = autorizacion.Codigo;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("obtenerAutorizacion", lista);
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

        public string getIDByName()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select Codigo from Autorizacion where Descripcion='" + this.Descripcion + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string cod = "";
                if (dr.Read())
                {
                    cod = dr.GetString(0);
                }
                return cod;
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
    }
}
