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
    public class Estado_Componente
    {
        public Int32 ID { get; set; }

        public Componente CodComponente { get; set; }

        public Estado IDEstado { get; set; }


        public Estado_Componente()
        {
            CodComponente = new Componente();
            IDEstado = new Estado();
        }

        public Estado_Componente(int id, string codcomponente, int idestado)
        {
            this.ID = id;
            this.CodComponente.Codigo = codcomponente;
            this.IDEstado.ID = idestado;
        }


        public static void asignarEstadoInicial(Estado_Componente estadoComponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1;
                p1 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = estadoComponente.CodComponente.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("iniciarEstadoComponente", lista, "Escritura");
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


        public static void updateFaseFinalSubcomponente(Estado_Componente estadoSubcomponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = estadoSubcomponente.CodComponente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@idestado";
                p2.OdbcType = OdbcType.Int;
                // IdEstado = 10 (Finalizado)
                p2.Value = 10;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("updateEstadoComponente", lista, "Escritura");
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


        public static void updateEstado(Estado_Componente estadoSubcomponente)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@codcomp";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = estadoSubcomponente.CodComponente.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@idestado";
                p2.OdbcType = OdbcType.Int;
                // IdEstado = 9 (Ejecucion)
                p2.Value = 9;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("updateEstadoComponente", lista, "Escritura");
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

        public static void updateAllEstadoComponentes()
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();

                Conexion.EjecutarProcedimientoAlmacenado("verificarAllEstadoComponentes", lista, "Escritura");
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
    }
}
