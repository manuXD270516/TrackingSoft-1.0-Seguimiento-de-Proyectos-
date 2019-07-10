using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos_TrackinSoft
{
    public class Gestion
    {
        public Int32 ID { get; set; }

        public Int32 Año { get; set; }

        public Gestion()
        {
            ///////////////
        }

        public Gestion(int id, int año)
        {
            this.ID = id;
            this.Año = año;

        }

    }
}
