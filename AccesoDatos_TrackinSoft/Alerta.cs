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
    public class Alerta
    {

        #region Instance Properties

        public Int32 ID { get; set; }

        public String IDUser { get; set; } 

        public String Descripcion { get; set; }

        public Int32? IDFase_Comp { get; set; }

        public Int32? IDProy_Autorz { get; set; }

        public Int32? IDComp_Autorz { get; set; }

        public DateTime Fecha_Envio { get; set; }

        public String Revisado { get; set; }

        #endregion

        public Alerta()
        {
            ///////////////////////
        }

        public Alerta(int id, string iduser,string descripcion, int idfasecomp, int idproyautoz, int idcompautoz, DateTime fechaenvio, string revisado)
        {
            this.ID = id;
            this.IDUser = iduser;
            this.Descripcion = descripcion;
            this.IDFase_Comp = idfasecomp;
            this.IDProy_Autorz = idproyautoz;
            this.IDComp_Autorz = idcompautoz;
            this.Fecha_Envio = fechaenvio;
            this.Revisado = revisado;
        }

        public void generarAlertaEtapa()
        {
            Conexion.abrirConexion();
            string sql = "insert into Alerta(IDUser,Descripcion,IDFase_Comp,Fecha_Envio,Revisado) values('"+this.IDUser+"','Alerta de Etapa Asignada para su realizacion'," + this.IDFase_Comp + ",getdate(),'NO')";
            try
            {
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public void generarAlertaAutorizacionProyecto()
        {
            Conexion.abrirConexion();
            string sql = "insert into Alerta(IDUser,Descripcion,IDProy_Autorz,Fecha_Envio,Revisado) values('" + this.IDUser + "','Alerta de Autorizaciones de Proyectos Asignada para su realizacion'," + this.IDProy_Autorz + ",getdate(),'NO')";
            try
            {
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public void generarAlertaAutorizacionComponente()
        {
            Conexion.abrirConexion();
            string sql = "insert into Alerta(IDUser,Descripcion,IDComp_Autorz,Fecha_Envio,Revisado) values('" + this.IDUser + "','Alerta de Autorizaciones de Componentes Asignada para su realizacion'," + this.IDComp_Autorz + ",getdate(),'NO')";
            try
            {
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            

        }

        public int getCantidadNotificaciones()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select COUNT(*) as [Cantidad] from Alerta where Revisado='NO' and IDUser='" + this.IDUser + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                int cant = -1;
                if (dr.Read())
                {
                    cant = dr.GetInt32(0);
                }
                
                return cant;
            }
            catch (Exception)
            {
                return -1; // valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();
            }
           
        }

        public DataTable getTableForUser(int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                DataTable dt = new DataTable();
                /*if (tipo.Equals(1)) // ver el listado general de las notificaciones
                {
                    Conexion.ComandExct = new OdbcCommand("select * from vistaNotificaciones", Conexion.conexion);
                    OdbcDataAdapter adapter = new OdbcDataAdapter(Conexion.ComandExct);
                    adapter.Fill(dt);
                    Conexion.cerrarConexion();
                    return dt;
                }*/
                // ver el listado filtrado 
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p3, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();

                p1.ParameterName = "@iduser";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = this.IDUser;
                lista.Add(p1);

                p2.ParameterName = "@tipo";
                p2.OdbcType = OdbcType.Int;
                p2.Value = tipo;
                lista.Add(p2);

                p3.ParameterName = "@estado";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 2;
                p3.Value = this.Revisado;
                lista.Add(p3);

                dt = Conexion.EjecutarProcedimientoMostrar("mostrarNotificacionesUser", lista);
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

        public int getIDProyAutorizacion()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select IDProy_Autorz from Alerta where ID=" + this.ID;
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
                return -1; // valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public int getIDCompAutorizacion()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select IDComp_Autorz from Alerta where ID=" + this.ID;
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
                return -1;// valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();
            }
           
        }

        public int getIDFaseComponente()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select IDFase_Comp from Alerta where ID=" + this.ID;
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
                return -1; // valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public List<Object> getDatos(string tablaSelect, int idTablaSelect)
        {
            try
            {
                Conexion.abrirConexion();
                List<Object> lista = new List<Object>();
                string sql;
                switch (tablaSelect)
                {
                    case "Proyecto_Autorizacion":
                        sql = "select p.Nombre as Proyecto,a.Descripcion as Autorizacion,pa.FechaMax_Fin,e.Accion as Estado " +
                            " from Proyecto_Autorizacion pa,Proyecto p,Autorizacion a,Estado e " +
                            " where pa.CodProyecto=p.Codigo and pa.CodAutoriz=a.Codigo and pa.IDEstado=e.ID and pa.ID=" + idTablaSelect;
                        break;
                    case "Componente_Autorizacion":
                        sql = "select c.Nombre as Componente,a.Descripcion as Autorizacion,ca.FechaMax_Fin,e.Accion as Estado " +
                            " from Componente_Autorizacion ca,Componente c,Autorizacion a,Estado e " +
                            " where ca.CodComponente=c.Codigo and ca.CodAutoriz=a.Codigo and ca.IDEstado=e.ID and ca.ID=" + idTablaSelect;
                        break;
                    default:  // seleccionada Fase_Componente
                        sql = "select c.Nombre as Subcomponente,f.Nombre as Fase,cr.Fecha_Inicio, cr.Fecha_Conclusion,e.Accion as Estado " +
                            " from Fase_Componente fc,Componente c,Fase f,Cronograma cr, Estado e,Estado_FaseComponente efc " +
                            " where fc.CodComponente=c.Codigo and fc.IDFase=f.ID and fc.IDCronograma=cr.Id " +
                            " and efc.IDFase_Comp=fc.ID and efc.IDEstado=e.ID and fc.ID=" + idTablaSelect;
                        break;
                }
                // ejecutar la consulta obtenida
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        lista.Add(dr.GetValue(i));
                    }
                }
                return lista;
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

        public void updateEstadoRevision()
        {
            string sql = "update alerta set Revisado='SI' where ID=" + this.ID;            
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);            
            }
            catch (Exception ex)
            {
                return;                  
            }
            finally
            {
                Conexion.cerrarConexion();
            }            
        }


        public int getIDByEtapa()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select ID from alerta where IDFase_Comp=" + this.IDFase_Comp + ";";
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
                return -1; // valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();
            }
      
        }

        public int getIDByAsignacionAutorizaciones(int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "";
                if (tipo.Equals(1)) // proyectos
                {
                    sql = "select ID from alerta where IDProy_Autorz=" + this.IDProy_Autorz + ";";
                }
                else // componentes
                {
                    sql = "select ID from alerta where IDComp_Autorz=" + this.IDComp_Autorz + ";";
                }
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
                return -1; // valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();           
            }
            
        }
    }
}
