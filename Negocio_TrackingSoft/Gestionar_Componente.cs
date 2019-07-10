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
    public class Gestionar_Componente
    {
        public static Componente componente,Subcomponente;
        public static Cronograma cronograma;
        public static Proyecto proyecto;
        public static Estado_Componente estadoComponente;
        
        public static void insertarComponente(string codigo, string nombre, string descrip, string codproy /*string codcomp*/, int idcrono,int idtipomodif)
        {
            Email x = new Email();
            
            componente = new Componente();
            componente.Codigo = codigo;
            componente.Nombre = nombre;
            componente.Descripcion = descrip;
            componente.CodProyecto.Codigo = codproy;
            /*componente.CodComponente = new Componente();
            componente.CodComponente.Codigo = codcomp;*/
            componente.IDCronograma.Id = idcrono;
            componente.IDTipo_Modif.ID = idtipomodif;
            Componente.insertarC(componente);
        }

        public static void insertarSubcomponente(string codigo, string nombre, string desc, string codproy, string codcomp, int idcrono)
        {
            componente = new Componente();
            componente.Codigo = codigo;
            componente.Nombre = nombre;
            componente.Descripcion = desc;
            componente.CodProyecto.Codigo = codproy;
            componente.CodComponente=new Componente();
            componente.CodComponente.Codigo = codcomp;
            componente.IDCronograma.Id = idcrono;
            Componente.insertarSC(componente);
        }

        // agregar el tipo de modificacion del componente
        public static void actualizarComponente(string codigo,string nombre,string desc,int idcrono,int idtipomodif)
        {
            componente = new Componente();
            componente.Codigo = codigo;
            componente.Nombre = nombre;
            componente.Descripcion = desc;
            componente.IDCronograma.Id = idcrono;
            componente.IDTipo_Modif.ID = idtipomodif;
            Componente.updateComponente(componente);
        }

        public static void actualizarSubComponente(string codigo, string nombre, string desc, int idcrono)
        {
            componente = new Componente();
            componente.Codigo = codigo;
            componente.Nombre = nombre;
            componente.Descripcion = desc;
            componente.IDCronograma.Id = idcrono;
            Componente.updateSubComponente(componente);
        }

        public static void eliminarComponente(string codigo)
        {
            componente = new Componente();
            componente.Codigo = codigo;
            Componente.deleteComponente(componente);
        }

        public static int cantidadFasesAsignadasSubcomponente(string codsubcomp)
        {
            return Componente.getCantFasesAsignadas(codsubcomp);
        }

        public static DataTable mostrarComponentes(string codproy)
        {
            componente = new Componente();
            componente.CodProyecto.Codigo = codproy;
            return Componente.mostrarComponentes(componente);
        }

        public static DataTable obtenerDatosComponente(string codigo)
        {
            componente = new Componente();
            componente.Codigo = codigo;
            return Componente.obtenerDatos(componente);
        }


        public static string lastCodigoComponente()
        {
            return Componente.ultimoCodigo();
        }

        public static DataTable obtenerDatosComponenteTotal(string codcomp)
        {
            componente=new Componente();
            componente.Codigo = codcomp;
            return Componente.obtenerDatosTotales(componente);
        }

        public static string lastCodigoSubComponente()
        {
            return Componente.ultimoCodigoSubComp();
        }

        public static DataTable obtenerDatosSubComponente(string codsubcomp)
        {
            componente = new Componente();
            componente.Codigo = codsubcomp;
            return Componente.obtenerDatosSubComp(componente);
        }

        public static DataTable mostrarSubComponentes(string codcomp)
        {
            componente = new Componente();
            componente.Codigo = codcomp;
            return Componente.mostrarSubComponentes(componente);
        }

        public static void iniciarEstadoComponente(string codComp)
        {
            estadoComponente = new Estado_Componente();
            estadoComponente.CodComponente.Codigo = codComp;
            Estado_Componente.asignarEstadoInicial(estadoComponente);

        }

        public static void iniciarEstadoSubcomponente(string codComp)
        {
            estadoComponente = new Estado_Componente();
            estadoComponente.CodComponente.Codigo = codComp;
            Estado_Componente.asignarEstadoInicial(estadoComponente);

        }

        public static Cronograma obtenerCronogramaComponente(string codComp)
        {
            return Componente.obtenerCronograma(codComp);
        }

        public static string getCodigoComponente(string nombreComp)
        {
            return Componente.getCodigo(nombreComp);
        }

        public static void fnalizarSubcomponente(string codSubcompSelect)
        {
            estadoComponente= new Estado_Componente();
            estadoComponente.CodComponente.Codigo = codSubcompSelect;
            Estado_Componente.updateFaseFinalSubcomponente(estadoComponente);
        }


        public static void updateEstadoSubcomponente(string codsubcomp)
        {
            estadoComponente=new Estado_Componente();
            estadoComponente.CodComponente.Codigo=codsubcomp;
            Estado_Componente.updateEstado(estadoComponente);
        }

        public static void updateEstadoComponente(string codcomp)
        {
            estadoComponente = new Estado_Componente();
            estadoComponente.CodComponente.Codigo = codcomp;
            Estado_Componente.updateEstado(estadoComponente);
        
        }

        public static void verificarEstadoFinalizacionComponentes()
        {
            Estado_Componente.updateAllEstadoComponentes();
        }

        public static int obtenerIDCronogramaComp_o_Subcomp(string codigo)
        {
            return Componente.getIDCronograma(codigo);
        }

        public static Cronograma obtenerCronogramaComponente_desdeSubcomponente(string codsubcomp)
        {
            return Componente.getIDCronograma_desdeSubcomponente(codsubcomp);
        }

        public static string obtenerDescComponente(string codigo)
        {
            return Componente.getDescripcion(codigo);
        }

        public static void quitarAutorizacionesComponente(string codcomp)
        {
            Componente.deleteAutorizaciones(codcomp);
        }

        public static void eliminarSubcomponentes(string codcomp)
        {
            Componente.deleteSubcomponentes(codcomp);
        }

        public static string obtenerIDComponente(string nombreComponente)
        {
            componente = new Componente();
            componente.Nombre = nombreComponente;
            return componente.getIDByName();
        }

        public static void eliminarCodigoTxt(string codSubcomp)
        {
            componente = new Componente();
            componente.Codigo = codSubcomp;
            componente.deleteTxtCod();

        }

        public static string obtenerNombreComponenteFromSubcomp(string codsubcomp)
        {
            componente = new Componente();
            componente.CodComponente = new Componente();
            componente.CodComponente.Codigo = codsubcomp;
            return componente.getCodComponenteFromSubcomp();

        }
    }
}
