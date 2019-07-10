using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos_TrackinSoft
{
    public class Componente
    {
        public String Codigo { get; set; }

        public String Nombre { get; set; }

        public String Descripcion { get; set; }

        public Proyecto CodProyecto { get; set; }

        public Componente CodComponente { get; set; }

        public Cronograma IDCronograma { get; set; }

        public Tipo_Modificacion IDTipo_Modif { get; set; }
        public Componente()
        {
            this.CodProyecto = new Proyecto();
            //this.CodComponente = new Componente();
            this.IDCronograma = new Cronograma();
            this.IDTipo_Modif = new Tipo_Modificacion();
        }

        public Componente(string codigo, string nombre, string descripcion, Proyecto codProy, Componente codcomponent,Cronograma idcrono,Tipo_Modificacion idtipoModif)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.CodProyecto = codProy;
            this.CodComponente = codcomponent;
            this.IDCronograma = idcrono;
            this.IDTipo_Modif = idtipoModif;
        }

        public static void insertarC(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6, p7;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();
                p7 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = componente.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = componente.Descripcion;
                lista.Add(p3);

                p4.ParameterName = "@codproy";
                p4.OdbcType = OdbcType.VarChar;
                p4.Size = 10;
                p4.Value = componente.CodProyecto.Codigo;
                lista.Add(p4);

                /*
                p5.ParameterName = "@codcomp";
                p5.OdbcType = OdbcType.VarChar;
                p5.Size = 10;
                componente.CodComponente = new Componente();
                p5.Value = default(string);//componente.CodComponente.Codigo;
                lista.Add(p5);
                */
                p6.ParameterName = "@idcrono";
                p6.OdbcType = OdbcType.Int;
                p6.Value = componente.IDCronograma.Id;
                lista.Add(p6);

                p7.ParameterName = "@idtipomodif";
                p7.OdbcType = OdbcType.Int;
                if (componente.IDTipo_Modif.ID == int.MaxValue) // valor null
                {
                    p7.Value = DBNull.Value;
                }
                else // valor existente
                {
                    p7.Value = componente.IDTipo_Modif.ID;

                }
                lista.Add(p7);
                Conexion.EjecutarProcedimientoAlmacenado("insertComponente", lista, "Escritura");            
            }
            catch (Exception)
            {
                return ;
            }
            finally
            {
                Conexion.cerrarConexion();
            }           
        }

        public static void insertarSC(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = componente.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = componente.Descripcion;
                lista.Add(p3);

                p4.ParameterName = "@codproy";
                p4.OdbcType = OdbcType.VarChar;
                p4.Size = 10;
                p4.Value = componente.CodProyecto.Codigo;
                lista.Add(p4);

                p5.ParameterName = "@codcomp";
                p5.OdbcType = OdbcType.VarChar;
                p5.Size = 10;
                //componente.CodComponente = new Componente();
                p5.Value = componente.CodComponente.Codigo;//componente.CodComponente.Codigo;
                lista.Add(p5);

                p6.ParameterName = "@idcrono";
                p6.OdbcType = OdbcType.Int;
                p6.Value = componente.IDCronograma.Id;
                lista.Add(p6);

                Conexion.EjecutarProcedimientoAlmacenado("insertSubcomponente", lista, "Escritura");            
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

        public static void updateComponente(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6, p7;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();
                p7 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = componente.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = componente.Descripcion;
                lista.Add(p3);

                p6.ParameterName = "@idcrono";
                p6.OdbcType = OdbcType.Int;
                p6.Value = componente.IDCronograma.Id;
                lista.Add(p6);

                p7.ParameterName = "@idtipomodif";
                p7.OdbcType = OdbcType.Int;
                p7.Value = componente.IDTipo_Modif.ID;
                lista.Add(p7);

                Conexion.EjecutarProcedimientoAlmacenado("updateComponente", lista, "Escritura");            
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

        public static void updateSubComponente(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();


                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = componente.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = componente.Descripcion;
                lista.Add(p3);

                p6.ParameterName = "@idcrono";
                p6.OdbcType = OdbcType.Int;
                p6.Value = componente.IDCronograma.Id;
                lista.Add(p6);

                Conexion.EjecutarProcedimientoAlmacenado("updateSubComponente", lista, "Escritura");            
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

        public static void deleteComponente(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteComponente", lista, "Escritura");           
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

        public static DataTable mostrarComponentes(Componente componente)
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
                p1.Value = componente.CodProyecto.Codigo;
                lista.Add(p1);
                dt = Conexion.EjecutarProcedimientoMostrar("mostrarComponente", lista);               
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


        public static List<Componente> listaDatos(string codProy,int tipoModif)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Codigo,Nombre from Componente where CodProyecto='" + codProy + "' and IDTipo_Modif=" + tipoModif);
                List<Componente> ltipo = new List<Componente>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Componente x = new Componente();
                    x.Codigo = dr.GetString(0);
                    x.Nombre = dr.GetString(1);
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

        public static DataTable obtenerDatos(Componente comp)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = comp.Codigo;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("obtenerComponente", lista);
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

        public static string ultimoCodigo()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Top(1) Codigo from Componente where  CodComponente is null order by 1 desc");
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

        public static List<Componente> listaDatosTotal(string codProy)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Codigo,Nombre from Componente where CodProyecto='" + codProy + "' and CodComponente is null");
                List<Componente> ltipo = new List<Componente>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Componente x = new Componente();
                    x.Codigo = dr.GetString(0);
                    x.Nombre = dr.GetString(1);
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

        public static DataTable obtenerDatosTotales(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("obtenerComponenteTotal", lista);                
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

        public static string ultimoCodigoSubComp()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select top(1) c.Codigo from Componente c where c.CodComponente is not null order by c.IDCronograma desc");
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

        public static List<Componente> listaDatosSubComp(string codcomp)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select c.Codigo,c.Nombre from Componente c where c.CodComponente is not null and c.CodComponente='" + codcomp + "'");
                List<Componente> ltipo = new List<Componente>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Componente x = new Componente();
                    x.Codigo = dr.GetString(0);
                    x.Nombre = dr.GetString(1);
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

        public static DataTable mostrarSubComponentes(Componente componente)
        {
            try
            {
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarSubComponentes", lista);
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

        public static DataTable obtenerDatosSubComp(Componente componente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codsubcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = componente.Codigo;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("obtenerSubComponente", lista);
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

        public static Cronograma obtenerCronograma(string codComp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select cr.Fecha_Inicio,cr.Fecha_Conclusion from Componente c, Cronograma cr where c.IDCronograma=cr.ID and c.Codigo='" + codComp + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    Cronograma x = new Cronograma(int.MaxValue, dr.GetDateTime(0), dr.GetDateTime(1));
                    return x;
                }
                return null;
                /*else
                {
                    //Conexion.cerrarConexion();
                    return null;
                }*/
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

        public static int getIDCronograma(string codcomp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "Select IDCronograma from Componente where Codigo='" + codcomp + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                int x = -1; // valor abs
                if (dr.Read())
                {
                    x = dr.GetInt32(0);                    
                    //return x;
                }
                return x;                
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
        public static string getCodigo(string nombreComp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "Select Codigo from Componente where Nombre='" + nombreComp + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    return dr.GetString(0);
                }
                else
                {
                    return null;
                }
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

        /*public static void updateFaseFinalSubcomponente(Componente subcomponente)
        {
            Conexion.ConectarBD();
            List<OdbcParameter> lista = new List<OdbcParameter>();
            OdbcParameter p1,p2;
            p1= new OdbcParameter();
            p2 = new OdbcParameter();

            p1.ParameterName="@codsubcomp";
            p1.OdbcType = OdbcType.VarChar;
            p1.Size = 10;
            p1.Value = subcomponente.Codigo;
            lista.Add(p1);

            p2.ParameterName = "@idestado";
            p2.OdbcType = OdbcType.Int;
            // IdEstado = 10 (Finalizado)
            p2.Value = 10;
            lista.Add(p2);


            Conexion.EjecutarProcedimientoAlmacenado("actualizarEstadoFaseComponente", lista,"Escritura");
            Conexion.DesconectarBD();
        }*/

        public static int getCantFasesAsignadas(string codsubcomp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select COUNT(*) from Fase_Componente fc where fc.CodComponente='" + codsubcomp + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                int cant = dr.GetInt32(0);
                return cant;
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

        public static Cronograma getIDCronograma_desdeSubcomponente(string codsubcomp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select cr.Fecha_Inicio,cr.Fecha_Conclusion from Componente c,Cronograma cr" +
                            " where c.IDCronograma=cr.Id and c.Codigo= (select c.CodComponente " +
                                                                        " from Componente c" +
                                                                         " where c.Codigo='" + codsubcomp + "')";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                Cronograma x = null;
                if (dr.Read())
                {
                    x = new Cronograma(int.MaxValue, dr.GetDateTime(0), dr.GetDateTime(1));
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

        public static Cronograma obtenerCronogramaProyecto(string codcomp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select cr.Fecha_Inicio,cr.Fecha_Conclusion from Componente c,Cronograma cr,Proyecto p " +
                              " where c.CodProyecto=p.Codigo and p.IDCronograma=cr.ID and c.Codigo='" + codcomp + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                Cronograma x = null;
                if (dr.Read())
                {
                    x = new Cronograma(int.MaxValue, dr.GetDateTime(0), dr.GetDateTime(1));
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

        public static string getDescripcion(string codigo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select Descripcion from componente where codigo='" + codigo + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string x = "";
                if (dr.Read())
                {
                    x = dr.GetString(0).Trim();
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

        public static void deleteAutorizaciones(string codcomp)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter("@codcomp", codcomp);
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteAutorizacionesComp", lista, "Escritura");                
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

        public static void deleteSubcomponentes(string codcomp)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "delete from componente where CodComponente='" + codcomp + "'";
                OdbcCommand comando = new OdbcCommand(sql, Conexion.conexion);
                comando.ExecuteNonQuery();                
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

        public string getIDByName()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select Codigo from Componente where Nombre='" + this.Nombre + "';";
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

        public static List<Componente> listaDatosSubcomponenteByProyecto(string codproy)
        {
            try
            {
                Conexion.abrirConexion();
                List<Componente> lista = new List<Componente>();
                OdbcDataReader dr = Conexion.ObtenerTuplas("Select Codigo,Nombre from Componente where CodComponente is not null and CodProyecto='" + codproy + "';");
                while (dr.Read())
                {
                    Componente x = new Componente();
                    x.Codigo = dr.GetString(0);
                    x.Nombre = dr.GetString(1);
                    lista.Add(x);
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

        public void deleteTxtCod()
        {
            
            string sql = "delete from poblarCod where codigotxt='" + this.Codigo + "';";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw new Exception(error);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public string getCodComponenteFromSubcomp()
        {
            string sql = "select cc.Codigo,cc.Nombre" +
                       " from Componente cc" +
                       " where cc.Codigo=(select c.CodComponente" +
                                         " from Componente c" +
                                         " where c.Codigo='"+this.CodComponente.Codigo+"')";
            string dato = "";
            try
            {                
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    dato = dr.GetString(1);
                }
                dr.Close();
                return dato;
            }
            catch (Exception ex)
            {
                return "";                
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
