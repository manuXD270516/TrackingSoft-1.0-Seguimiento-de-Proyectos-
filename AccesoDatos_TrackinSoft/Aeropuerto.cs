using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Aeropuerto
    {
        public String Codigo { get; set; }

        public String JefePlanta { get; set; }

        public Departamento CodDepartamento { get; set; }

        public Aeropuerto()
        {
            CodDepartamento = new Departamento();
        }

        public Aeropuerto(string codigo, string jefeplanta, Departamento codigodpto)
        {
            this.Codigo = codigo;
            this.JefePlanta = jefeplanta;
            this.CodDepartamento = codigodpto;
        }

        public static void insertar(Aeropuerto aeropuerto)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();

                p1.ParameterName = "@codigo";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 20;
                p1.Value = aeropuerto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@Jefe";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 100;
                p2.Value = aeropuerto.JefePlanta;
                lista.Add(p2);

                p3.ParameterName = "@coddpto";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 10;
                p3.Value = aeropuerto.CodDepartamento.Codigo;
                lista.Add(p3);

                Conexion.EjecutarProcedimientoAlmacenado("insertAeropuerto", lista, "Escritura");
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

        public static List<Aeropuerto> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Codigo,CodDepartamento from Aeropuerto");
                List<Aeropuerto> ltipo = new List<Aeropuerto>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Proyecto(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Aeropuerto x = new Aeropuerto();
                    x.Codigo = dr.GetString(0);
                    x.JefePlanta = dr.GetString(1); // artificio para poder cargar el CodDepartamento en un atributo distinto
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
