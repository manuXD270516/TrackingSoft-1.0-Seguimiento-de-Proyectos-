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
    public class Proyecto
    {
        public String Codigo { get; set; }

        public String Nombre { get; set; }

        public String Descripcion { get; set; }

        public Tipo_Proyecto CodTipo_Proyecto { get; set; }

        public Gestion IDGestion { get; set; }

        public Decimal Costo { get; set; }

        public Cronograma IDCronograma { get; set; }

        /* Modificar el codAeropuerto --> codDpto
        public Aeropuerto CodAeropuerto { get; set; }
        */
        public Departamento CodDepartamento { get; set; }

        public Proyecto()
        {
            this.CodTipo_Proyecto = new Tipo_Proyecto();
            this.IDGestion = new Gestion();
            this.IDCronograma = new Cronograma();
            //this.CodAeropuerto = new Aeropuerto();
            this.CodDepartamento = new Departamento();
        }

        public Proyecto(string codigo, string nombre, string desc,Tipo_Proyecto codtipoproy,Gestion idgestion,decimal costo,Cronograma idcronograma,/*Aeropuerto aeropuerto*/Departamento codDpto)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Descripcion = desc;
            this.CodTipo_Proyecto = codtipoproy;
            this.IDGestion = idgestion;
            this.Costo = costo;
            this.IDCronograma = idcronograma;
            //this.CodAeropuerto = aeropuerto;
            this.CodDepartamento = codDpto;
        }

        public static void insertar1(Proyecto proyecto)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6, p7, p8, p9;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();
                p7 = new OdbcParameter();
                p8 = new OdbcParameter();
                p9 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = proyecto.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = proyecto.Descripcion;
                lista.Add(p3);

                /*
                p4.ParameterName = "@idtipoModif";
                p4.OdbcType = OdbcType.Int;
                //p4.Size = 10;
                p4.Value = proyecto.IDTipo_Modif.ID;
                lista.Add(p4);*/

                p5.ParameterName = "@codtipo_proy";
                p5.OdbcType = OdbcType.Int;
                //p5.Size = 10;
                //proyecto.Codproyecto = new proyecto();
                p5.Value = proyecto.CodTipo_Proyecto.Codigo;//proyecto.Codproyecto.Codigo;
                lista.Add(p5);

                p6.ParameterName = "@idgest";
                p6.OdbcType = OdbcType.Int;
                p6.Value = proyecto.IDGestion.ID;
                lista.Add(p6);

                p7.ParameterName = "@costo";
                p7.OdbcType = OdbcType.Decimal;
                p7.Value = proyecto.Costo;
                lista.Add(p7);

                p8.ParameterName = "@idcrono";
                p8.OdbcType = OdbcType.Int;
                p8.Value = proyecto.IDCronograma.Id;
                lista.Add(p8);

                p9.ParameterName = "@codDpto";
                p9.OdbcType = OdbcType.VarChar;
                p9.Size = 10;
                p9.Value = proyecto.CodDepartamento.Codigo;
                lista.Add(p9);

                Conexion.EjecutarProcedimientoAlmacenado("insertProyecto1", lista, "Escritura");            
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

        public static void insertar2(Proyecto proyecto)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5, p6, p7, p8, p9;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p5 = new OdbcParameter();
                p6 = new OdbcParameter();
                p7 = new OdbcParameter();
                p8 = new OdbcParameter();
                p9 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = proyecto.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = proyecto.Descripcion;
                lista.Add(p3);

                p5.ParameterName = "@codtipo_proy";
                p5.OdbcType = OdbcType.Int;
                //p5.Size = 10;
                //proyecto.Codproyecto = new proyecto();
                p5.Value = proyecto.CodTipo_Proyecto.Codigo;//proyecto.Codproyecto.Codigo;
                lista.Add(p5);

                p6.ParameterName = "@idgest";
                p6.OdbcType = OdbcType.Int;
                p6.Value = proyecto.IDGestion.ID;
                lista.Add(p6);

                p7.ParameterName = "@costo";
                p7.OdbcType = OdbcType.Decimal;
                p7.Value = proyecto.Costo;
                lista.Add(p7);

                p8.ParameterName = "@idcrono";
                p8.OdbcType = OdbcType.Int;
                p8.Value = proyecto.IDCronograma.Id;
                lista.Add(p8);

                /*
                p9.ParameterName = "@codAero";
                p9.OdbcType = OdbcType.VarChar;
                p9.Value = proyecto.CodAeropuerto.Codigo;
                lista.Add(p9);
                */
                Conexion.EjecutarProcedimientoAlmacenado("insertProyecto2", lista, "Escritura");            
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
        public static void update(Proyecto proyecto,string nuevocodigo)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p6, px;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p6 = new OdbcParameter();
                px = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                px.ParameterName = "@newcodigo";
                px.OdbcType = OdbcType.VarChar;
                px.Size = 10;
                px.Value = nuevocodigo;
                lista.Add(px);

                p2.ParameterName = "@nombre";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 60;
                p2.Value = proyecto.Nombre;
                lista.Add(p2);

                p3.ParameterName = "@desc";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 200;
                p3.Value = proyecto.Descripcion;
                lista.Add(p3);

                p4.ParameterName = "@costo";
                p4.OdbcType = OdbcType.Decimal;
                //p4.Size = 200;
                p4.Value = proyecto.Costo;
                lista.Add(p4);

                p6.ParameterName = "@idcrono";
                p6.OdbcType = OdbcType.Int;
                p6.Value = proyecto.IDCronograma.Id;
                lista.Add(p6);

                Conexion.EjecutarProcedimientoAlmacenado("updateProyecto", lista, "Escritura");            
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

        public static void delete(Proyecto proyecto)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteProyecto", lista, "Escritura");            
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

        public static DataTable mostrarproyectos(int tipo)
        {
            try
            {
                DataTable dt = new DataTable();
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                if (tipo == 1) // tipo de proyectos de planta nueva
                {
                    dt = Conexion.EjecutarProcedimientoMostrar("mostrarProyectos1", lista);
                }
                else // proyectos de continuidad operativa
                {
                    dt = Conexion.EjecutarProcedimientoMostrar("mostrarProyectos2", lista);

                }
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
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Top(1) Codigo from Proyecto order by 1 desc");
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

        public static List<Proyecto> listaDatos(int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Codigo,Nombre from Proyecto where CodTipo_Proyecto=" + tipo);
                List<Proyecto> ltipo = new List<Proyecto>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Proyecto x = new Proyecto();
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

        public static DataTable obtenerDatos(Proyecto proyecto)
        {
            try
            {
                DataTable dt = new DataTable();
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                dt = Conexion.EjecutarProcedimientoMostrar("obtenerDatosProyecto", lista);
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
        public static DataTable obtener(Proyecto proyecto)
        {
            try
            {
                DataTable dt = new DataTable();
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@codtipo";
                p2.OdbcType = OdbcType.Int;
                p2.Value = proyecto.CodTipo_Proyecto.Codigo;
                lista.Add(p2);

                dt = Conexion.EjecutarProcedimientoMostrar("obtenerProyecto", lista);
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

        public static DataTable estadoActualFasesSubcomponentes(Proyecto proyecto,int i)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@codproy";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = proyecto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@ind";
                p2.OdbcType = OdbcType.Int;
                p2.Value = i;
                lista.Add(p2);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarFaseActualSubcomponenteProyecto", lista);
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

        public static List<Proyecto> obtenerListProyectos()
        {
            try
            {
                Conexion.abrirConexion();
                List<Proyecto> lista = new List<Proyecto>();
                string sql = "Select Codigo,Nombre from Proyecto ";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                while (dr.Read())
                {
                    Proyecto proyecto = new Proyecto();
                    proyecto.Codigo = dr.GetString(0);
                    proyecto.Nombre = dr.GetString(1);
                    lista.Add(proyecto);
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

        public static Cronograma obtenerCronograma(string codigo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "Select cr.Fecha_Inicio,cr.Fecha_Conclusion from Proyecto p,Cronograma cr where p.IDCronograma=cr.ID and p.Codigo='" + codigo + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    //Conexion.DesconectarBD();
                    Cronograma x = new Cronograma(int.MaxValue, dr.GetDateTime(0), dr.GetDateTime(1));
                    return x;
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

        public static int getIDCronograma(string codigo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select IDCronograma from Proyecto where Codigo='" + codigo + "'";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    int x = dr.GetInt32(0);
                    return x;
                }
                return -1;
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
        public static string getCodigo(string nombre)
        {
            Conexion.abrirConexion();
            string sql = "Select Codigo from Proyecto where Nombre='" + nombre + "'";
            OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
            if (dr.Read())
            {
                string x=dr.GetString(0);
                Conexion.cerrarConexion();
                return x;
            }
            else
            {
                return null;
            }
        }

        public static string getDescripcion(string codigo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select Descripcion from proyecto where codigo='" + codigo + "';";
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

        public string getCodigoByNombre()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "Select Codigo from proyecto where Nombre='" + this.Nombre + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string codigo = "";
                if (dr.Read())
                {
                    codigo = dr.GetString(0);
                }
                return codigo;
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

        public static List<Proyecto> listaDatosAll()
        {
            return obtenerListProyectos();
        }
    }

}
