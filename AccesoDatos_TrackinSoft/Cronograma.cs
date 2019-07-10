using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AccesoDatos_TrackinSoft;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;
namespace AccesoDatos_TrackinSoft
{
    public class Cronograma
    {
        public Int32 Id { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Conclusion { get; set; }

        public Cronograma()
        {
            //////////////////
        }

        public Cronograma(int id, DateTime fechaInicio, DateTime fechaFin)
        {
            this.Id = id;
            this.Fecha_Inicio = fechaInicio;
            this.Fecha_Conclusion = fechaFin;
        }

        public static void insert(Cronograma cronograma)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@fechaInicio";
                p1.OdbcType = OdbcType.DateTime;
                p1.Value = cronograma.Fecha_Inicio;//.ToString("dd/MM/yyyy");
                lista.Add(p1);

                p2.ParameterName = "@fechaConclusion";
                p2.OdbcType = OdbcType.DateTime;
                p2.Value = cronograma.Fecha_Conclusion;//.ToString("dd/MM/yyyy");
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("insertCronograma", lista, "Escritura");            
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

        public static void update(Cronograma cronograma)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, px;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                px = new OdbcParameter();

                px.ParameterName = "@id";
                px.OdbcType = OdbcType.Int;
                px.Value = cronograma.Id;
                lista.Add(px);

                p1.ParameterName = "@fechaInicio";
                p1.OdbcType = OdbcType.Date;
                p1.Value = cronograma.Fecha_Inicio;
                lista.Add(p1);

                p2.ParameterName = "@fechaConclusion";
                p2.OdbcType = OdbcType.Date;
                p2.Value = cronograma.Fecha_Conclusion;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("updateCronograma", lista, "Escritura");            
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

        public static void delete(Cronograma cronograma)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter px;
                px = new OdbcParameter();

                px.ParameterName = "@id";
                px.OdbcType = OdbcType.Int;
                px.Value = cronograma.Id;
                lista.Add(px);

                Conexion.EjecutarProcedimientoAlmacenado("deleteCronograma", lista, "Escritura");            
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

        public static int UltimoID()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Top(1) ID from Cronograma order by 1 desc");
                int x;
                if (dr.Read())
                {
                    x = dr.GetInt32(0);
                }
                else
                {
                    x = -1;
                }
                return x;                
            }
            catch (Exception)
            {
                return 1;//valor abs'
            }
            finally
            {
                Conexion.cerrarConexion();            
            }
            
        }

        public static int obtenerID(string tabla,string tipoCod,object identificador)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr;
                if (tipoCod.Equals("Codigo")) // se selecciono un campo identificador con sintaxis : "Codigo" [string]
                {
                    dr = Conexion.ObtenerTuplas("select IDCronograma from " + tabla + " where " + tipoCod + "='" + identificador + "'");
                }
                else // se selecciono un campo identificador con sintaxis : "ID" [int]
                {
                    dr = Conexion.ObtenerTuplas("select IDCronograma from " + tabla + " where " + tipoCod + "=" + identificador);
                }
                int idcronograma;
                if (dr.Read())
                {
                    idcronograma = dr.GetInt32(0);
                }
                else
                {
                    idcronograma = -1; // valor absurdo -> No se encontro el idcronograma 
                }
                return idcronograma;
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
