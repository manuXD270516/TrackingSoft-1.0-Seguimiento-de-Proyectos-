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
    public class Administrar_Tareas_Notificaciones
    {

        public static Estado_FaseComponente estadoFaseComponente;
        public static Alerta alerta;
        public static Fase_Componente etapa;
        public static Fase fase;
        public static Proyecto_Autorizacion asignacionAutorizacionP;
        public static Componente_Autorizacion asignacionAutorizacionC;
        public static Estado estado;
        public static DataTable mostrarTareasFasesUser(string username,int valor,int x)
        {
            return Estado_FaseComponente.getTareasUser(username, valor, x) ; 
        }

        public static DataTable mostrarTareasSelectUser(string username, int valorEstadoSelect, int tipoTarea)
        {
            return Estado_FaseComponente.getTareasPorEstado(username, valorEstadoSelect, tipoTarea);
        }

        public static void generarAlertaEtapaUser(string iduser, int idfasecomp)
        {
            alerta = new Alerta();
            alerta.IDUser = iduser;
            alerta.IDFase_Comp = idfasecomp;
            alerta.generarAlertaEtapa();
        }

        public static int obtenerIDEtapa(string idfase, string codsubcomp)
        {
            etapa = new Fase_Componente();
            etapa.CodComponente.Codigo = codsubcomp;
            etapa.IDFase.ID = idfase;
            return etapa.getID();
        }

        public static int getNotificacionesPendientes(string iduser)
        {           
            alerta = new Alerta();
            alerta.IDUser = iduser;
            return alerta.getCantidadNotificaciones();
        }

        public static DataTable showAlertasForUSer(string iduser,int tipo, string estado)
        {
            //alerta.ID = tipo;
            alerta.IDUser = iduser;
            alerta.Revisado=estado;
            return alerta.getTableForUser(tipo);
        }

        public static void generarAlertaAutorizaciones(string tipo, int idAsigAut, string iduser)
        {
            alerta = new Alerta();
            alerta.IDUser = iduser;
            if (tipo.Equals("Proyecto"))
            {
                alerta.IDProy_Autorz = idAsigAut;
                alerta.generarAlertaAutorizacionProyecto();
            }
            else
            {
                alerta.IDComp_Autorz = idAsigAut;
                alerta.generarAlertaAutorizacionComponente();
            }
            
        }

        public static int obtenerIDProyAutorizacion(int idalerta)
        {
            alerta = new Alerta();
            alerta.ID = idalerta;
            return alerta.getIDProyAutorizacion();
        }

        public static int obtenerIDCompAutorizacion(int idalerta)
        {
            alerta = new Alerta();
            alerta.ID = idalerta;
            return alerta.getIDCompAutorizacion();
        }

        public static int obtenerIDFaseComponente(int idalerta)
        {
            alerta = new Alerta();
            alerta.ID = idalerta;
            return alerta.getIDFaseComponente();
        }

        public static List<Object> obtenerDatos(string tablaSelect, int idTablaSelect)
        {
            alerta = new Alerta();
            return alerta.getDatos(tablaSelect, idTablaSelect) ;           
        }

       
        public static void actualizarEstadoNotificacion(int idnotif)
        {
            alerta = new Alerta();
            alerta.ID = idnotif;
            alerta.updateEstadoRevision();
        }

       
        public static string obtenerIDFase(string nombreFase)
        {
            fase = new Fase();
            fase.Nombre = nombreFase;
            return fase.getIDbyName();
        }

        
        // metodo para actualizar un estado especifico de la etapa seleccionada
        public static void actualizarEstadoTareaEtapa(int idEtapa, string nombreEstado)
        {
            estadoFaseComponente = new Estado_FaseComponente();
            estadoFaseComponente.IDFase_Comp.ID = idEtapa;
            estadoFaseComponente.Descripcion = nombreEstado; // parche para la seleccion del estado
            estadoFaseComponente.updateEstadoEspecificoTarea();
        }

        public static void actualizarEstadoTareaPendiente(int idEtapa,int decision)
        {
            estadoFaseComponente = new Estado_FaseComponente();
            estadoFaseComponente.IDFase_Comp.ID = idEtapa;
            estadoFaseComponente.updateEstadoTareaPendiente(decision);
        }
       
        public static void actualizarEstadoTareaAFinalizada(int idEtapa,int dateValida)
        {
            etapa = new Fase_Componente();
            etapa.ID = idEtapa;            
            etapa.updateEstadoTareaFinalizada(dateValida);
        }

        public static void actualizarEstadoAsignacionAutorizacionFinalizada(int idTabla, int tipo,bool valida)
        {
            if (tipo.Equals(1)) // proyectos
            {
                asignacionAutorizacionP = new Proyecto_Autorizacion();
                asignacionAutorizacionP.ID = idTabla;
                asignacionAutorizacionP.updateEstadoAsigAutorizP(valida);
            }
            else // componentes
            {
                asignacionAutorizacionC = new Componente_Autorizacion();
                asignacionAutorizacionC.ID = idTabla;
                asignacionAutorizacionC.updateEstadoAsigAutorizC(valida);
            }
        }

        public static void validarActualizacionEstadoTareaNext(int idEtapa, string idSubcomponente)
        {
            etapa = new Fase_Componente();
            etapa.ID = idEtapa;
            etapa.CodComponente.Codigo = idSubcomponente;
            etapa.validateUpdateEstadoTareaNext();
        }

        public static int obtenerIDAlertaByEtapa(int idEtapa)
        {
            alerta=new Alerta();
            alerta.IDFase_Comp = idEtapa;
            return alerta.getIDByEtapa();
        }

        public static int obtenerIDAlertaByAsigAutorizaciones(int idTabla, int tipo)
        {
            alerta = new Alerta();           
            if (tipo.Equals(1)) // proyectos
            {
                alerta.IDProy_Autorz = idTabla;                
            }
            else // componentes
            {
                alerta.IDComp_Autorz = idTabla;
            }
            return alerta.getIDByAsignacionAutorizaciones(tipo);
        }

        public static void actualizarFechaFinRealFaseAsignada(int idetapa,DateTime fechaReal)
        {
            etapa = new Fase_Componente();
            etapa.ID = idetapa;
            etapa.FechaFin_Real = fechaReal;
            etapa.updateFechaRealFinalizacion();
        }

        public static string obtenerNombreFase(string idfase)
        {
            fase = new Fase();
            fase.ID = idfase;
            return fase.getNameByID();
        }

        public static string obtenerAccionEstado(int idestado)
        {
            estado = new Estado();
            estado.ID = idestado;
            return estado.getNameByID();
        }

        public static void actualizarInformacionFase(string idfaseSelect,string newNombreFase, int cantDias)
        {
            fase = new Fase();
            fase.ID = idfaseSelect;
            fase.Nombre = newNombreFase;
            fase.DiasPlazo = cantDias;
            fase.updateInformacionFase();
        }

        public static List<Object> obtenerDatosGeneralesEmail(string iduserSelect, string idfase, string codsubcomp)
        {
            etapa = new Fase_Componente();
            etapa.IDFase = new Fase();
            etapa.IDFase.ID = idfase;
            etapa.CodComponente = new Componente();
            etapa.CodComponente.Codigo = codsubcomp;
            etapa.AliasPers = new Personal();
            etapa.AliasPers.Alias = iduserSelect;
            return etapa.getDatosParaEmail();
        }

        public static List<Object> obtenerDatosPreEnviarEmail(int idEtapa, string idFase)
        {
            etapa = new Fase_Componente();
            etapa.ID = idEtapa;
            etapa.IDFase.ID = idFase;
            return etapa.getDatosPrevEnvioEmail();
        }

        public static List<Object> obtenerDatosEmailPrevFase(int idEtapa)
        {
            etapa = new Fase_Componente();
            etapa.ID = idEtapa;
            return etapa.getDatosPrevFase();
        }

        public static string obtenerIDUSerFromFaseActual(int idEtapa)
        {
            etapa = new Fase_Componente();
            etapa.ID = idEtapa;
            return etapa.getIDUser();
        }

        public static string obtenerAliasPersonaSeleccionadaFase(int idetapa)
        {
            etapa = new Fase_Componente();
            etapa.ID = idetapa;
            return etapa.getAliasPersona();
        }

        public static string obtenerPersonaResponable(int idEtapa)
        {
            etapa = new Fase_Componente();
            etapa.ID = idEtapa;
            return etapa.getResponsable();
        }
    }
}
