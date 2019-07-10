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
    public class Departamento
    {
        public String Codigo { get; set; }

        public String Ciudad { get; set; }

        public Departamento()
        {
            //////////////////////
        }

        public Departamento(string codigo, string ciudad)
        {
            this.Codigo = codigo;
            this.Ciudad = ciudad;
        }

        public static List<Departamento> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "Select * from Departamento";
                List<Departamento> lista = new List<Departamento>();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                while (dr.Read())
                {
                    lista.Add(new Departamento(dr.GetString(0), dr.GetString(1)));
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
