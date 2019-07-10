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
    public class Estado_FaseComponente
    {
        public Int32 ID { get; set; }

        public Estado IDEstado { get; set; }

        public Fase_Componente IDFase_Comp { get; set; }

        public String Descripcion { get; set; }

        public Estado_FaseComponente()
        {
            this.IDEstado = new Estado();
            this.IDFase_Comp = new Fase_Componente();
        }

        public Estado_FaseComponente(int id, Estado idestado, Fase_Componente idfasecomp,string descripcion)
        {
            this.ID = id;
            this.IDEstado = idestado;
            this.IDFase_Comp = idfasecomp;
            this.Descripcion = descripcion;
        }
       
        public static void updateEstadoFase(Estado_FaseComponente estadoFaseComp/*, int * FCSelect*/)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@idEstadoFaseComp";
                p1.OdbcType = OdbcType.Int;
                p1.Value = estadoFaseComp.ID;
                lista.Add(p1);

                p2.ParameterName = "@nroPasoFase";
                p2.OdbcType = OdbcType.Int;
                p2.Value = estadoFaseComp.IDFase_Comp.IDFase.NroPaso;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("updateEstadosNextFaseSubcomponente", lista, "Escritura");            
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

        public static DataTable getTareasUser(string username,int valor,int x)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3;

                p2 = new OdbcParameter();
                p1 = new OdbcParameter();
                p3 = new OdbcParameter();

                p1.ParameterName = "@username";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 50;
                p1.Value = username;
                lista.Add(p1);

                p2.ParameterName = "@ind";
                p2.OdbcType = OdbcType.Int;
                p2.Value = valor;
                lista.Add(p2);

                p3.ParameterName = "@x";
                p3.OdbcType = OdbcType.Int;
                p3.Value = x;
                lista.Add(p3);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarTareasPorUsuario", lista);
                return dt;
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

        public static DataTable getTareasPorEstado(string username, int valorEstadoSelect, int tipoTarea)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3;

                p2 = new OdbcParameter();
                p1 = new OdbcParameter();
                p3 = new OdbcParameter();

                p1.ParameterName = "@username";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 50;
                p1.Value = username;
                lista.Add(p1);

                p2.ParameterName = "@idestado";
                p2.OdbcType = OdbcType.Int;
                p2.Value = valorEstadoSelect;
                lista.Add(p2);

                p3.ParameterName = "@tipotarea";
                p3.OdbcType = OdbcType.Int;
                p3.Value = tipoTarea;
                lista.Add(p3);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarTareasPorUsuarioYEstado", lista);                
                return dt;
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

        public void updateEstadoEspecificoTarea()
        {
            string sql = "";
            switch (this.Descripcion)
            {
                case "Pendiente": // el nombre del proximo estado seria [ejecucion] - idestado=5
                    sql = "update Estado_FaseComponente set IDEstado=5 where IDFase_Comp=" + this.IDFase_Comp.ID + ";";
                    break;
                case "En Ejecucion": // el nombre del proximo estado seria [en mora] o [finalizada] - idestado=6,7  
                    sql = "update Estado_FaseComponente set IDEstado";
                        break;
                default:
                    break;
            }
        }
       
        public void updateEstadoTareaPendiente(int decision)
        {
            try
            {
                Conexion.abrirConexion();
                string sql;
                if (decision.Equals(1))  // se acepto la tarea -->  actualizar su estado a [En Ejecucion id=5]
                {
                    sql = "update Estado_FaseComponente set IDEstado=5 where IDFase_Comp=" + this.IDFase_Comp.ID + ";";
                }
                else // se rechazo la tarea --> actualizar el estado de la tarea anterior a [En Ejecucion id=5]
                {
                    sql = "update Estado_FaseComponente set IDEstado=5 where IDFase_Comp=" + (this.IDFase_Comp.ID - 1) + "; " +
                         "update Fase_Componente set FechaFin_Real=NULL where ID=" + (this.IDFase_Comp.ID - 1) + ";";
                }
                Conexion.ejecutarConsulta(sql);
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
