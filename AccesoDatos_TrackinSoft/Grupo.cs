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
    public class Grupo
    {
        public String ID { get; set; }

        public String Nombre { get; set; }

        public Grupo()
        {
            ////////////////
        }

        public Grupo(string id, string nombre)
        {
            this.ID = id;
            this.Nombre = nombre;
        }

        public static List<Grupo> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                List<Grupo> lista = new List<Grupo>();
                string sql = "Select ID,Nombre from Grupo";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                while (dr.Read())
                {
                    lista.Add(new Grupo(dr.GetString(0), dr.GetString(1)));
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
    }
}
