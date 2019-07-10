using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Revision
    {
        public Int32 ID { get; set; }

        public Fase_Componente IDFase_Comp { get; set; }

        public Estado IDEstado { get; set; }

        public Personal AliasPers { get; set; }

        public Revision()
        {
            this.IDFase_Comp = new Fase_Componente();
            this.IDEstado = new Estado();
            this.AliasPers = new Personal();
        }

        public Revision(int id,Fase_Componente idfasecom, Personal aliaspers,Estado idestado)
        {
            this.ID = id;
            this.IDFase_Comp = idfasecom;
            this.AliasPers = aliaspers;
            this.IDEstado = idestado;
        }
    }
}
