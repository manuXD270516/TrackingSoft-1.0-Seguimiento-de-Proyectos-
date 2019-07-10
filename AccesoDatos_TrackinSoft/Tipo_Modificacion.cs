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
    public class Tipo_Modificacion
    {
        public Int32 ID { get; set; }

        public String Tipo { get; set; }

        public Tipo_Modificacion()
        {
            /////////////////
        }

        public Tipo_Modificacion(int id, string tipo)
        {
            this.ID = id;
            this.Tipo = tipo;
        }


        public static List<Tipo_Modificacion> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select ID,Tipo from Tipo_Modificacion");
                List<Tipo_Modificacion> ltipo = new List<Tipo_Modificacion>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Tipo_Modificacion x = new Tipo_Modificacion();
                    x.ID = dr.GetInt32(0);
                    x.Tipo = dr.GetString(1).Trim();
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
