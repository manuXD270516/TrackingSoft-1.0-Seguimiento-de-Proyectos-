using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Fase_Componente
    {
        public Int32 ID { get; set; }

        public Fase IDFase { get; set; }

        public Componente CodComponente { get; set; }

        public Cronograma IDCronograma { get; set; }

        public Personal AliasPers { get; set; }

        public String Estado_Pago { get; set; }

        public DateTime FechaFin_Real { get; set; }

        public Fase_Componente()
        {
            this.IDFase = new Fase();
            this.CodComponente = new Componente();
            this.IDCronograma = new Cronograma();
            this.AliasPers = new Personal();
        }

        public Fase_Componente(int id, Fase idfase, Componente codcompo,Cronograma idcronograma, Personal aliaspers,String estadoPago,DateTime fechaFinReal)
        {
            this.ID = id;
            this.IDFase = idfase;
            this.CodComponente = codcompo;
            this.IDCronograma = idcronograma;
            this.AliasPers = aliaspers;
            this.Estado_Pago = estadoPago;
            this.FechaFin_Real = fechaFinReal;
        }

        public static void insertarInicialFechasDefault(Fase_Componente faseComponente,int total)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista, laux;
                lista = new List<OdbcParameter>();
                laux = new List<OdbcParameter>();
                OdbcParameter p1, p2, px;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                px = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p1);

                px.ParameterName = "@codsubcomp";
                px.OdbcType = OdbcType.VarChar;
                px.Size = 10;
                px.Value = faseComponente.CodComponente.Codigo;
                laux.Add(px);


                p2.ParameterName = "@total";
                p2.OdbcType = OdbcType.Int;
                p2.Value = total;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("AsignarTodasFases", lista, "Escritura");
                Conexion.EjecutarProcedimientoAlmacenado("ActualizarCronogramaInicial", laux, "Escritura");            
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

        public static void actualizarResponsablesIniciales(Fase_Componente faseComponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1;
                p1 = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("updateFase_Componente_ResponsableInicial", lista, "Escritura");            
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

        
        public static DataTable mostrarFasesResponsables(Fase_Componente faseComponente,int i)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarFasesAsignadasResponsables", lista);
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

        public static void actualizarResponsableAlterno(Fase_Componente faseComponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@idfase";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 6;
                p2.Value = faseComponente.IDFase.ID;
                lista.Add(p2);

                p3.ParameterName = "@aliaspers";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 20;
                p3.Value = faseComponente.AliasPers.Alias;
                lista.Add(p3);

                p4.ParameterName = "@idcrono";
                p4.OdbcType = OdbcType.Int;
                p4.Value = faseComponente.IDCronograma.Id;
                lista.Add(p4);
                Conexion.EjecutarProcedimientoAlmacenado("updateFase_Componente_Responsable", lista, "Escritura");
            
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

        public static void startEstadosInicialesFases(Fase_Componente faseComponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1;
                p1 = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("actualizarEstdosInicialesFases", lista, "Escritura");            
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

        public static void updateEstadoFaseComponente(Fase_Componente faseComponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nroPasoFase";
                p2.OdbcType = OdbcType.Int;
                p2.Value = faseComponente.IDFase.NroPaso;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("actualizarFaseDeEjecucion", lista, "Escritura");            
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

        public static Cronograma getCronograma(string idfase,string codsubcomp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select fc.ID,fc.IDFase,fc.CodComponente,fc.IDCronograma,cr.Fecha_Inicio,cr.Fecha_Conclusion,fc.AliasPers" +
                            " from Fase_Componente fc,Cronograma cr" +
                            " where fc.IDCronograma=cr.Id and fc.CodComponente='" + codsubcomp + "' and fc.IDFase='" + idfase + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                Cronograma x = null;
                if (dr.Read())
                {
                    x = new Cronograma(dr.GetInt32(3), dr.GetDateTime(4), dr.GetDateTime(5));
                }
                return x;
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

        public static void updateCronogramas(Fase_Componente faseComponente,int diasDif)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();

                p1.ParameterName = "@idfase";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = faseComponente.IDFase.ID;
                lista.Add(p1);

                p2.ParameterName = "@codsubcomp";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 10;
                p2.Value = faseComponente.CodComponente.Codigo;
                lista.Add(p2);

                p3.ParameterName = "@diasDif";
                p3.OdbcType = OdbcType.Int;
                p3.Value = diasDif;
                lista.Add(p3);

                Conexion.EjecutarProcedimientoAlmacenado("updateCronogramasNextFasesAsignadas", lista, "Escritura");            
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
                string sql = "Select ID from Fase_Componente where IDFase='" + this.IDFase.ID + "' and CodComponente='" + this.CodComponente.Codigo + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                int x = -1;
                if (dr.Read())
                {
                    x = dr.GetInt32(0);
                }
                return x;
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

        public void updateEstadoTareaFinalizada(int dateValida)
        {
            try
            {
                Conexion.abrirConexion();
                string sql;
                if (dateValida.Equals(1)) // actualizar el estado a [Finalizada]
                {
                    sql = "update Estado_FaseComponente set IDEstado=7 where IDFase_Comp=" + this.ID;
                    //";\n update Estado_FaseComponente set IDEstado=4 where IDFase_Comp="+(this.ID);
                }
                else  // actualizar el estado a [En Mora]
                {
                    sql = "update Estado_FaseComponente set IDEstado=6 where IDFase_Comp=" + this.ID + ";";
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

        public void validateUpdateEstadoTareaNext()
        {
            try
            {
                Conexion.abrirConexion();
                string sqlPrev = "select top(1) ID from Fase_Componente where CodComponente='" + this.CodComponente.Codigo + "' order by 1 desc";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sqlPrev);
                if (dr.Read())
                {
                    int idFaseComp = dr.GetInt32(0);
                    dr.Close();
                    // validar si ya no existen mas fases dentro del subcomponente
                    if (!idFaseComp.Equals(this.ID)) // aun existen mas fases
                    {
                        string sql = "update Estado_FaseComponente set IDEstado=4 where IDFase_Comp=" + (this.ID + 1);//+"\n"+
                        //" insert into Alerta(IDUser,Descripcion,IDFase_Comp,Fecha_Envio,Revisado) values('"+;
                        Conexion.ejecutarConsulta(sql);
                        // notificar a la siguiente persona responsable
                        string sql2 = "select u.ID from Fase_Componente fc,Usuario u where fc.AliasPers=u.AliasPers and fc.ID=" + (this.ID + 1) + ";";
                        OdbcDataReader dr2 = Conexion.ObtenerTuplas(sql2);
                        if (dr2.Read())
                        {
                            string idUser = dr2.GetString(0);
                            int idFaseCompNext = this.ID + 1;
                            dr2.Close();
                            string sql3 = "insert into Alerta(IDUser,Descripcion,IDFase_Comp,Fecha_Envio,Revisado) values('" + idUser + "','Alerta de Etapa Asignada para su realizacion'," + idFaseCompNext + ",getdate(),'NO');";
                            Conexion.ejecutarConsulta(sql3);
                        }
                    }
                }            
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

        public void updateFechaRealFinalizacion()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "update Fase_Componente set FechaFin_Real='" + this.FechaFin_Real + "' where ID=" + this.ID;
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

        public List<Object> getDatosParaEmail()
        {
            string sql = "select p.Nombre as [Proyecto],c.Nombre as [Componente],f.Nombre as [Fase],cr.Fecha_Inicio,cr.Fecha_Conclusion" +
                       " from Fase_Componente fc,Fase f,Componente c, Proyecto p,Cronograma cr, Usuario u" +
                       " where fc.CodComponente=c.Codigo and fc.IDFase=f.ID and c.CodProyecto=p.Codigo" +
                       " and fc.IDCronograma=cr.Id and fc.AliasPers=u.AliasPers and u.ID='" + this.AliasPers.Alias + "' and f.ID='" + this.IDFase.ID + "' and c.Codigo='" + this.CodComponente.Codigo + "';";
            List<Object> datosGet=new List<Object>();
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
                    datosGet.Add(dr.GetDateTime(4));
                }
                dr.Close();
                return datosGet;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public List<Object> getDatosPrevEnvioEmail()
        {
            string sql = "declare @idfase char(6),@idetapa int\n" +
                        "set @idfase='" + this.IDFase.ID + "'\n" +
                        "set @idetapa=" + this.ID + "\n" +
                        "if @idfase<>(select MAX(ID)from Fase)\n" +
                        "    begin\n" +
                        "        set @idetapa=@idetapa+1\n" +
                        "        select u.ID,fc.AliasPers,fc.CodComponente,fc.IDFase,p.Email\n" +
                        "        from Fase_Componente fc,Personal p,Usuario u\n" +
                        "        where fc.AliasPers=p.Alias and fc.ID=@idetapa and p.Alias=u.AliasPers\n" +
                        "    end";
            List<Object> datos=new List<Object>();
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    for (int i = 0; i < 5; i++)
                    {
                        datos.Add(dr.GetString(i));
                    }
                }
                dr.Close();
                return datos;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();
            }

       }

        public List<Object> getDatosPrevFase()
        {
            string sql = "select p.Email, pr.Nombre as Proyecto " +
                       " from Fase_Componente fc,Usuario u,Personal p,Proyecto pr, Componente c" +
                       " where fc.AliasPers=p.Alias and u.AliasPers=p.Alias and fc.ID="+(this.ID-1)+" and fc.CodComponente=c.Codigo" +
                       "       and c.CodProyecto=pr.Codigo";
            List<Object> lista = new List<Object>();
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    lista.Add(dr.GetString(0));
                    lista.Add(dr.GetString(1));
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();
            }

        }

        public string getIDUser()
        {
            string sql = "select u.ID" +
                       " from Fase_Componente fc,Usuario u,Personal p" +
                       " where fc.AliasPers=p.Alias and u.AliasPers=p.Alias and fc.ID=" + this.ID + ";";
            try
            {
                string id="";               
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    id = dr.GetString(0);
                }
                dr.Close();
                return id;                
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

        public string getAliasPersona()
        {
            string sql = "select AliasPers from Fase_Componente where ID=" + this.ID + ";";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string alias = "";
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

        public string getResponsable()
        {
            string sql = "select RTRIM(p.Nombres)+' '+RTRIM(p.Apellidos) as [Responsable]"+
                        " from Fase_Componente fc,Personal p"+
                        " where fc.AliasPers=p.Alias and fc.ID="+this.ID+";";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string personal="";
                if (dr.Read())
                {
                    personal = dr.GetString(0);
                }
                dr.Close();
                return personal;
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

        public int IniciadoFlujoProceso()
        {
            string sql = "select COUNT(*) as Total" +
                        " from Fase_Componente fc,Estado_FaseComponente efc" +
                        " where efc.IDFase_Comp=fc.ID and fc.CodComponente='" + this.CodComponente.Codigo + "' and efc.IDEstado<>14";
            try
            {
                int x=-1;
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    x = dr.GetInt32(0);
                }
                dr.Close();
                return x;
            }
            catch (Exception ex)
            {
                return -1;
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
