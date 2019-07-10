using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Privilegios
    {
        public String ID { get; set; }

        public String Permiso { get; set; }

        public Privilegios()
        {
            ///////////////////
        }

        public Privilegios(string id,string permiso)
        {
            this.ID = id;
            this.Permiso = permiso;
        }


    }
}
