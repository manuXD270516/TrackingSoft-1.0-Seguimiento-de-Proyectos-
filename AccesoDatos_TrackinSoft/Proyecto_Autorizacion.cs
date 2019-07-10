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
    public class Proyecto_Autorizacion
    {
        public Int32 ID { get; set; }

        public Proyecto CodProyecto { get; set; }

        public Autorizacion CodAutoriz { get; set; }

        public Personal AliasPers { get; set; }

        public DateTime FechaMax_Fin { get; set; }

        public DateTime FechaFin_Real { get; set; }

        public Proyecto_Autorizacion()
        {
            this.CodProyecto = new Proyecto();
            this.CodAutoriz = new Autorizacion();
            this.AliasPers = new Personal();
        }

        public Proyecto_Autorizacion(int id, Proyecto codproy, Autorizacion codauto,Personal aliaspers,DateTime fechamax,DateTime fechaFinReal)
        {
            this.ID = id;
            this.CodProyecto = codproy;
            this.CodAutoriz = codauto;
            this.AliasPers = aliaspers;
            this.FechaMax_Fin=fechamax;
            this.FechaFin_Real = fechaFinReal;
        }

        public static void insertar(Proyecto_Autorizacion proyectoAutorizacion)
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

                p1.ParameterName = "@codproy";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyectoAutorizacion.CodProyecto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@codautoriz";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 10;
                p2.Value = proyectoAutorizacion.CodAutoriz.Codigo;
                lista.Add(p2);

                p3.ParameterName = "@aliaspers";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 20;
                p3.Value = proyectoAutorizacion.AliasPers.Alias;
                lista.Add(p3);

                p4.ParameterName = "@fechafin";
                p4.OdbcType = OdbcType.DateTime;
                p4.Value = proyectoAutorizacion.FechaMax_Fin;
                lista.Add(p4);

                Conexion.EjecutarProcedimientoAlmacenado("insertProyecto_Autorizacion", lista, "Escritura");            
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

       

        public static void delete(Proyecto_Autorizacion proyectoautorizacion)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@id";
                p1.OdbcType = OdbcType.Int;
                //p1.Size = 10;
                p1.Value = proyectoautorizacion.ID;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteProyecto_Autorizacion", lista, "Escritura");            
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

        public static DataTable mostrarAutorizaciones(Proyecto_Autorizacion proyectoautorizacion)
        {
            try
            {
                DataTable dt = new DataTable();
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codproy";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyectoautorizacion.CodProyecto.Codigo;
                lista.Add(p1);
                dt = Conexion.EjecutarProcedimientoMostrar("mostrarProyecto_Autorizacion", lista);
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

        public void updateEstadoAsigAutorizP(bool valida)
        {
            try
            {
                string sql;
                if (valida)// actualiza con estado de finalizada
                {
                    sql = "update Proyecto_Autorizacion set IDEstado=7 where ID=" + this.ID + ";";
                }
                else // actualizar con el estado de en mora
                {
                    sql = "update Proyecto_Autorizacion set IDEstado=6 where ID=" + this.ID + ";";
                }
                Conexion.abrirConexion();
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
                string sql = "select ID from Proyecto_Autorizacion where CodProyecto='" + this.CodProyecto.Codigo + "' and CodAutoriz='" + this.CodAutoriz.Codigo + "' ;";
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
                string sql = "update Proyecto_Autorizacion set FechaFin_Real='" + this.FechaFin_Real + "' where ID=" + this.ID;
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

        public string getNotificado()
        {
            string sql = "select Notificado from Proyecto_Autorizacion where ID=" + this.ID + ";";
            string x="";
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
            string sql="update Proyecto_Autorizacion set Notificado='SI' where ID="+this.ID+";";
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

        public List<Object> getDatosProyectoAutorizacion()
        {
            string sql="select p.Nombre as [Proyecto], a.Descripcion as [Autorizacion],pa.FechaMax_Fin"+
                       " from Proyecto p,Proyecto_Autorizacion pa,Autorizacion a"+
                       " where pa.CodProyecto=p.Codigo and pa.CodAutoriz=a.Codigo and pa.ID="+this.ID+";";
            List<Object> datosGet=new List<Object>();
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    datosGet.Add(dr.GetString(0));
                    datosGet.Add(dr.GetString(1));
                    datosGet.Add(dr.GetDateTime(2));
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
            string sql = "select AliasPers from Proyecto_Autorizacion where ID=" + this.ID + ";";
            try
            {
                Conexion.abrirConexion();
                string alias="";
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
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
