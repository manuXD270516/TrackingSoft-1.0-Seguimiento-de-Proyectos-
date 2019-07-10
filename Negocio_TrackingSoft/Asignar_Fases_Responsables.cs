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
    public class Asignar_Fases_Responsables
    {
        public static Fase_Componente faseComponente;
        public static Fase fase;
        public static void agregarFasesInicialesFechasDefault(string codsubcomp, int total)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codsubcomp;
            Fase_Componente.insertarInicialFechasDefault(faseComponente, total);    
        }

        public static void actualizarFasesConResponsablesIniciales(string codsubcomp)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codsubcomp;
            Fase_Componente.actualizarResponsablesIniciales(faseComponente);
        }

        public static void actualizarFasesResponsablesAlternos(string codsubcomp,string idfase,string aliaspers,int idcrono)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codsubcomp;
            faseComponente.IDFase.ID = idfase;
            faseComponente.AliasPers.Alias = aliaspers;
            faseComponente.IDCronograma.Id = idcrono;
            Fase_Componente.actualizarResponsableAlterno(faseComponente);
        }

        public static DataTable mostrarFasesAsignadasResposablesDefault(string codsubcomp,int i)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codsubcomp;
            return Fase_Componente.mostrarFasesResponsables(faseComponente,i);
        }

        public static void iniciarEstadosFases(string codSubComponente)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codSubComponente;
            Fase_Componente.startEstadosInicialesFases(faseComponente);
        }

        public static void actualizarEstadoActual(string codsubcomp, int nroPasoFase)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codsubcomp;
            faseComponente.IDFase.NroPaso = nroPasoFase;
            Fase_Componente.updateEstadoFaseComponente(faseComponente);
        }

        public static Cronograma obtenerCronogramaFaseComponente(string idfase,string codsubcomp)
        {
            return Fase_Componente.getCronograma(idfase,codsubcomp);
        }

        public static void updateCronogramasNextFases(string idfase, string codSubcomp,int diasDif)
        {
            faseComponente = new Fase_Componente();
            faseComponente.IDFase.ID = idfase;
            faseComponente.CodComponente.Codigo = codSubcomp;
            Fase_Componente.updateCronogramas(faseComponente,diasDif);
        }

        public static DataTable showAllFases()
        {
            fase = new Fase();
            return fase.getTableFases();
        }

        public static string getIDFase(string nombreFase)
        {
            fase = new Fase();
            fase.Nombre = nombreFase;
            return fase.getIDbyName();
        }

        public static string obtenerIDAreaDeFaseAsingada(string idfase)
        {
            fase = new Fase();
            fase.ID = idfase;
            return fase.getIDArea();
        }

        public static int validarInicializacionDeProcesos(string codsubcomp)
        {
            faseComponente = new Fase_Componente();
            faseComponente.CodComponente.Codigo = codsubcomp;
            return faseComponente.IniciadoFlujoProceso();
        }
    }
}
