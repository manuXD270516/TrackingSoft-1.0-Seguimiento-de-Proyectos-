using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Telefono
    {
        public Int32 ID { get; set; }

        public Int32 Tipo { get; set; }

        public Int32 Numero { get; set; }

        public Personal CIPersonal { get; set; }

        public Telefono()
        {
            this.CIPersonal = new Personal();
        }

        public Telefono(int id, int tipo, int nro, Personal ciperson)
        {
            this.ID = id;
            this.Tipo = tipo;
            this.Numero=nro;
            this.CIPersonal = ciperson;
        }
    }
}
