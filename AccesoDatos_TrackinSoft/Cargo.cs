using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Cargo
    {
        public String ID { get; set; }

        public String Nombre { get; set; }

        public Area IDArea { get; set; } 

        public Cargo()
        {
            IDArea = new Area();
        }

        public Cargo(string id, string nombre,Area idearea)
        {
            this.ID = id;
            this.Nombre = nombre;
            this.IDArea = idearea;
        }

        public static List<Cargo> listaDatos(string idarea)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select ID,Nombre from Cargo where IDArea='" + idarea + "';");
                List<Cargo> ltipo = new List<Cargo>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Cargo x = new Cargo();
                    x.ID = dr.GetString(0).Trim();
                    x.Nombre = dr.GetString(1).Trim();
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
    }
}
