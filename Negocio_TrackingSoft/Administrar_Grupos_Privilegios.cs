using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace Negocio_TrackingSoft
{
    public class Administrar_Grupos_Privilegios
    {
        public static Usuario user;
        public static Grupo grupo;
        public static Privilegios privilegios;
        public static Grupo_Privilegios grupoPrivilegios;

        public static void actualizarPermisos(string username, string idgrupo)
        {
            user = new Usuario();
            user.UserName = username;
            user.IDGrupo.ID = idgrupo;
            Usuario.updateGrupo(user);
        }
        public static DataTable mostrarPermisos(string idgrupo)
        {
            return Grupo_Privilegios.obtenerPrivilegios(idgrupo);
        }

        public static int verificarPermisos(string iduser)
        {
            string sql = "select IDGrupo from Usuario where ID='" + iduser + "';";
            Conexion.abrirConexion();
            int x=-1;
            OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
            if (dr.Read())
            {
                if (dr.GetString(0).Trim().Equals("Gr01"))
                {
                    x=1;
                }
                else
                {
                    x= 2;
                }                 
            }
            Conexion.cerrarConexion();
            return x;
        }
    }
}
