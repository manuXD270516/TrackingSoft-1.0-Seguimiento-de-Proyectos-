using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Tipo_Proyecto
    {
        public Int32 Codigo { get; set; }

        public String Tipo { get; set; }

        public Tipo_Proyecto()
        {
            /////////////////////
        }

        public Tipo_Proyecto(int codigo, string tipo)
        {
            this.Codigo = codigo;
            this.Tipo = Tipo;
        }

        public static List<Tipo_Proyecto> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Codigo,Tipo from Tipo_Proyecto");
                List<Tipo_Proyecto> ltipo = new List<Tipo_Proyecto>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Tipo_Proyecto x = new Tipo_Proyecto();
                    x.Codigo = dr.GetInt32(0);
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

        public string getTipo()
        {
            try
            {
                string sql = "select Tipo from Tipo_Proyecto where Codigo=" + this.Codigo + ";";
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
