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
    public class Estado
    {
        public Int32 ID { get; set; }

        public String Accion { get; set; }

        public Estado()
        {
            //////////////
        }

        public Estado(int id, string accion)
        {
            this.ID = id;
            this.Accion = accion;
        }

        public static List<Estado> listaDatosTareas(int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                List<Estado> lista = new List<Estado>();
                string sql;
                if (tipo.Equals(1)) // etapas
                {
                    sql = "select * from estado where (id between 4 and 7)";

                }
                else // tipo=2 , autorizaciones 
                {
                    sql = "select * from Estado where ID between 6 and 8";
                }
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                while (dr.Read())
                {
                    lista.Add(new Estado(dr.GetInt32(0), dr.GetString(1)));
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

        public string getNameByID()
        {
            try
            {
                string sql = "select Accion from Estado where ID=" + this.ID + ";";
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string x = "";
                if (dr.Read())
                {
                    x = dr.GetString(0);
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
    }
}
