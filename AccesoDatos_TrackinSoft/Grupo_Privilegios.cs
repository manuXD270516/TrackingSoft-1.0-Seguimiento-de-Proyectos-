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
    public class Grupo_Privilegios
    {
        public Int32 ID { get; set; }

        public Grupo IDGrupo { get; set; }

        public Privilegios IDPrivilegios { get; set; }

        public Grupo_Privilegios()
        {
            this.IDGrupo = new Grupo();
            this.IDPrivilegios = new Privilegios();
        }

        public Grupo_Privilegios(int id, Grupo idgrupo, Privilegios idprivilegios)
        {
            this.ID = id;
            this.IDGrupo = idgrupo;
            this.IDPrivilegios = idprivilegios;
        }


        public static DataTable obtenerPrivilegios(string idgrupo)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter x = new OdbcParameter("@idgrupo", OdbcType.VarChar, 10);
                x.Value = idgrupo;
                lista.Add(x);
                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarPrivilegiosGrupo", lista);
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
    }
}
