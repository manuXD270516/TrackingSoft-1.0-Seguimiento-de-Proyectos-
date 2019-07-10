using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;
using System.Data.SqlClient;

namespace Negocio_TrackingSoft
{
    public class Actualizar_Estados_Fases
    {
        public static Cronograma cronograma;
        public static Proyecto proyecto;
        public static Estado_FaseComponente estadoFaseComp;
        
        public static void insertarCronograma(DateTime fechaInicio,DateTime fechaconclusion)
        {
            cronograma = new Cronograma();
            cronograma.Fecha_Inicio = fechaInicio;
            cronograma.Fecha_Conclusion = fechaconclusion;
            Cronograma.insert(cronograma);
        }

        public static int obtenerLastIDCronograma()
        {
            return Cronograma.UltimoID();
        }

        public static DataTable mostrarFaseActualProyecto_Subcomponente(string codproy,int i)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codproy;
            return Proyecto.estadoActualFasesSubcomponentes(proyecto,i);
        }

        public static void actualizarNextFase(int idEstadFCSelect, int nroPasoFCSelect)
        {
            estadoFaseComp = new Estado_FaseComponente();
            estadoFaseComp.ID = idEstadFCSelect;
            estadoFaseComp.IDFase_Comp.IDFase.NroPaso = nroPasoFCSelect;
            Estado_FaseComponente.updateEstadoFase(estadoFaseComp) ;
        }
    }
}
