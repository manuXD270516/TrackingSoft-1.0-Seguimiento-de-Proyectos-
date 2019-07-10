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
    public class Componente_Autorizacion
    {
        public Int32 ID { get; set; }

        public Componente CodComponente { get; set; }

        public Autorizacion CodAutoriz { get; set; }

        public Personal AliasPers { get; set; }

        public DateTime FechaMax_Fin { get; set; }

        public DateTime FechaFin_Real { get; set; }
        
        public Componente_Autorizacion()
        {
            this.CodComponente = new Componente();
            this.CodAutoriz = new Autorizacion();
            this.AliasPers = new Personal();
        }

        public Componente_Autorizacion(int id, Componente codcomp, Autorizacion codauto,Personal aliaspers,DateTime fechamax,DateTime fechaRealFin)
        {
            this.ID = id;
            this.CodComponente = codcomp;
            this.CodAutoriz = codauto;
            this.AliasPers = aliaspers;
            this.FechaMax_Fin = fechamax;
            this.FechaFin_Real = fechaRealFin;
        }

        public static void insertar(Componente_Autorizacion componenteAutorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6, p7, p8;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();
                p7 = new OdbcParameter();
                p8 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componenteAutorizacion.CodComponente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@codautoriz";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 10;
                p2.Value = componenteAutorizacion.CodAutoriz.Codigo;
                lista.Add(p2);

                p3.ParameterName = "@aliaspers";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 20;
                p3.Value = componenteAutorizacion.AliasPers.Alias;
                lista.Add(p3);

                p4.ParameterName = "@fechamax";
                p4.OdbcType = OdbcType.DateTime;
                p4.Value = componenteAutorizacion.FechaMax_Fin;
                lista.Add(p4);

                Conexion.EjecutarProcedimientoAlmacenado("insertComponente_Autorizacion", lista, "Escritura");            
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

        public static void delete(Componente_Autorizacion componenteAutorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@id";
                p1.OdbcType = OdbcType.Int;
                //p1.Size = 10;
                p1.Value = componenteAutorizacion.ID;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteComponente_Autorizacion", lista, "Escritura");                
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

        public static DataTable mostrarAutorizaciones(Componente_Autorizacion componenteAutorizacion)
        {
            try
            {
                DataTable dt = new DataTable();
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componenteAutorizacion.CodComponente.Codigo;
                lista.Add(p1);

                dt = Conexion.EjecutarProcedimientoMostrar("mostrarComponente_Autorizacion", lista);
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

        public void updateEstadoAsigAutorizC(bool valida)
        {
            try
            {
                Conexion.abrirConexion();
                string sql;
                if (valida)// actualiza con estado de finalizada
                {
                    sql = "update Componente_Autorizacion set IDEstado=7 where ID=" + this.ID + ";";
                }
                else // actualizar con el estado de en mora
                {
                    sql = "update Componente_Autorizacion set IDEstado=6 where ID=" + this.ID + ";";
                }
                Conexion.ejecutarConsulta(sql);
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

        public int getID()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select ID from Componente_Autorizacion where CodComponente='" + this.CodComponente.Codigo + "' and CodAutoriz='" + this.CodAutoriz.Codigo + "' ;";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                int id = -1;
                if (dr.Read())
                {
                    id = dr.GetInt32(0);
                }
                return id;
            }
            catch (Exception)
            {
                return -1; // valor abs
            }
            finally
            {
                Conexion.cerrarConexion();                
            }
        }

        public void updateFechaRealFinalizacion()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "update Componente_Autorizacion set FechaFin_Real='" + this.FechaFin_Real + "' where ID=" + this.ID;
                Conexion.ejecutarConsulta(sql);
                //Conexion.cerrarConexion();
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


        public string getNotificado()
        {
            string sql = "select Notificado from Componente_Autorizacion where ID=" + this.ID + ";";
            string x = "";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    x = dr.GetString(0);
                }
                return x;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public void updateNotificadoYes()
        {
            string sql = "update Componente_Autorizacion set Notificado='SI' where ID=" + this.ID + ";";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public List<Object> getDatosComponenteAutorizacion()
        {
            string sql = "select p.Nombre as [Proyecto],c.Nombre as [Componente], a.Descripcion as [Autorizacion],ca.FechaMax_Fin" +
                      " from Proyecto p,Componente_Autorizacion ca,Autorizacion a, Componente c" +
                      " where ca.CodComponente=c.Codigo and ca.CodAutoriz=a.Codigo and c.CodProyecto=p.Codigo and ca.ID="+this.ID+";";
            List<Object> datosGet = new List<Object>();
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    datosGet.Add(dr.GetString(0));
                    datosGet.Add(dr.GetString(1));
                    datosGet.Add(dr.GetString(2));
                    datosGet.Add(dr.GetDateTime(3));
                }
                dr.Close();
                return datosGet;
            }
            catch (Exception ex)
            {
                string errorr = ex.Message;
                return null;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public string getAliasPersona()
        {
            string sql = "select AliasPers from Componente_Autorizacion where ID=" + this.ID + ";";
            try
            {
                Conexion.abrirConexion();
                string alias = "";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    alias = dr.GetString(0);
                }
                dr.Close();
                return alias;
            }
            catch (Exception ex)
            {
                return "";
                throw;
            }
            finally 
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
