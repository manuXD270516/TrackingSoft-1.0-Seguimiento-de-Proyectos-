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
    public class Estado_Proyecto
    {
        public Int32 ID { get; set; }

        public Estado IDEstado { get; set; }

        public Proyecto CodProyecto { get; set; }

        public Estado_Proyecto()
        {
            this.IDEstado = new Estado();
            this.CodProyecto=new Proyecto();
        }

        public Estado_Proyecto(int id, Estado idestado, Proyecto codProy)
        {
            this.ID = id;
            this.IDEstado = idestado;
            this.CodProyecto = codProy;
        }


        public static void asignarEstadoInicial(Estado_Proyecto estadoProyecto)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1;
                p1 = new OdbcParameter();

                p1.ParameterName = "@codproy";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = estadoProyecto.CodProyecto.Codigo;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("iniciarEstadoProyecto", lista, "Escritura");
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

        public static void updateEstado(Estado_Proyecto estadoProyecto)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@codproy";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = estadoProyecto.CodProyecto.Codigo;
                lista.Add(p1);

                p2.ParameterName = "@idestado";
                p2.OdbcType = OdbcType.Int;
                // IDEstado=9 (Ejecucion)
                p2.Value = 9;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("updateEstadoProyecto", lista, "Escritura");
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

        public static void updateAllEstadoProyectos()
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();

                Conexion.EjecutarProcedimientoAlmacenado("verificarAllEstadoProyectos", lista, "Escritura");
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
