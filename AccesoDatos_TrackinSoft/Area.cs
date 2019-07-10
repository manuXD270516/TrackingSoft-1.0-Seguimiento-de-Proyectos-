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
    public class Area
    {
        public String ID { get; set; }
        public String Nombre { get; set; } 

        public Area()
        {
            ///////////////
        }

        public Area(string id,string nombre)
        {
            this.ID = id;
            this.Nombre = nombre;
        }

        public static List<Area> listDatos()
        {
            try
            {
                string sql = "Select * from area where ID<>'A12'";
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                List<Area> lista = new List<Area>();
                while (dr.Read())
                {
                    lista.Add(new Area(dr.GetString(0), dr.GetString(1)));
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
            // validar que el area de ingeneira de se quede sin ningun cargo a elegir
            
        }
    }
}
