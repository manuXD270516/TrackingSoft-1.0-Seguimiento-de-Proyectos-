using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;


namespace Negocio_TrackingSoft
{
    public class Gestionar_Personal
    {
        public static Personal personal;
        public static bool registrarPersonal(string alias, string nombres, string apellidos, char sexo, string email, string idcargo)
        {
            personal = new Personal();
            personal.Alias = alias;
            personal.Nombres = nombres;
            personal.Apellidos = apellidos;
            personal.Sexo = sexo;
            personal.Email = email;
            personal.IDCargo.ID = idcargo;
            return personal.addPersonal();
        }

        public static DataTable showPersonal()
        {
            personal = new Personal();
            return personal.getTablePersonal();
        }

        public static bool actualizarPersonal(string aliasUpdate, string nombresUpdate, string apellidosUpdate, string emailUpdate, string idcargoUpdate)
        {
            personal = new Personal();
            personal.Alias = aliasUpdate;
            personal.Nombres = nombresUpdate;
            personal.Apellidos = apellidosUpdate;
            personal.Email = emailUpdate;
            personal.IDCargo.ID = idcargoUpdate;
            return personal.updatePersonal();
        }

        public static bool eliminarPersonal(string aliasSelect)
        {
            personal=new Personal();
            personal.Alias = aliasSelect;
            return personal.deletePersonal();
        }
    }
}
