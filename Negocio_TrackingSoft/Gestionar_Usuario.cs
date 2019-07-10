using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;

namespace Negocio_TrackingSoft
{
    public class Gestionar_Usuario
    {
        public static Usuario user;
        public static Sesion sesion;

        public static int validarUsuario(string username, string password)
        {
            user = new Usuario();
            user.UserName = username;
            user.Password = password;
            return Usuario.validar(user);
        }

        public static DataTable ObtenerUsuario(string username, string password)
        {
            user = new Usuario();
            user.UserName = username;
            user.Password = password;
            return Usuario.obtenerDatos(user);
        }

        public static void iniciarSesion(string id, string iduser)
        {
            sesion = new Sesion();
            sesion.ID = id;
            sesion.IDUsuario.ID = iduser;
            Sesion.iniciar(sesion);
        }

        public static void cerrarSesion(string id, string iduser)
        {
            sesion = new Sesion();
            sesion.ID = id;
            sesion.IDUsuario.ID = iduser;
            Sesion.cerrar(sesion);
        }

        public static string lastIDSesion()
        {
            return Sesion.ultimoID();
        }

        public static string lastIDUsuario()
        {
            return Usuario.ultimoID();
        }

        public static void registrarUsuario(string iduser, string aliaspers, string grupo, string username, string password)
        {
            user = new Usuario();
            user.ID = iduser;
            user.AliasPers.Alias = aliaspers;
            user.IDGrupo.ID = grupo;
            user.UserName = username;
            user.Password = password;
            Usuario.insertar(user);
        }

        public static DataTable mostrarUsuarios()
        {
            return Usuario.mostrar();
        }

        public static DataTable getUsuario(string personalSelect)
        {
            user = new Usuario();
            user.AliasPers.Nombres = personalSelect;
            return Usuario.obtenerPorPersonal(user);
        }

        public static void eliminarUsuario(string idUserSelect)
        {
            user = new Usuario();
            user.ID = idUserSelect;
            Usuario.delete(user);
        }

        public static string obtenerID(string aliaspersona)
        {
            user = new Usuario();
            user.AliasPers.Alias = aliaspersona;
            return user.getID();
        }

        public static string obtenerPersonal(string iduser)
        {
            user = new Usuario();
            user.ID = iduser;
            return user.getNameLastNameByIDUser();
        }

        public static int obtenerNroSesionesIniciadasHoy()
        {
            sesion = new Sesion();
            return sesion.getNroSessionToday();
        }

        public static string obtenerEmail(string iduser)
        {
            user = new Usuario();
            user.ID = iduser;
            return user.getEmail();
        }

        public static string obtnerAliasPersona(string iduser)
        {
            user = new Usuario();
            user.ID = iduser;
            return user.getAliasPersona();
        }
    }
}