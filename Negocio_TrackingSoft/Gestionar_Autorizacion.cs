using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;

namespace Negocio_TrackingSoft
{
    public class Gestionar_Autorizacion
    {
        public static Autorizacion autorizacion;
        public static Proyecto_Autorizacion proyec_autoriz;
        public static Componente_Autorizacion comp_autoriz;

        public static void insertarAutorizacion(string codigo,string desc,string enteRegulador,int tipoAutoriz)
        {
            autorizacion = new Autorizacion();
            autorizacion.Codigo = codigo;
            autorizacion.Descripcion = desc;
            autorizacion.EnteRegulador = enteRegulador;
            autorizacion.CodTipo_Proyecto.Codigo = tipoAutoriz;
            Autorizacion.insertar(autorizacion);
        }
        // arreglar el metodo de abajo
        public static void actualizarAutorizacion(string codigo, string desc/*, string enteRegulador*/)
        {
            autorizacion = new Autorizacion();
            autorizacion.Codigo = codigo;
            autorizacion.Descripcion = desc;
            //autorizacion.EnteRegulador = enteRegulador;
            Autorizacion.update(autorizacion);
        }
        
        public static void eliminarAutorizacion(string codigo)
        {
            autorizacion = new Autorizacion();
            autorizacion.Codigo = codigo;
            Autorizacion.delete(autorizacion);
        }

        public static DataTable mostrarAutorizaciones(int tipo)
        {
            autorizacion = new Autorizacion();
            autorizacion.CodTipo_Proyecto.Codigo = tipo;
            return Autorizacion.mostrar(autorizacion);
        }

        public static DataTable mostrarAutorizacionesProyecto(string codproyecto)
        {
            proyec_autoriz=new Proyecto_Autorizacion();
            proyec_autoriz.CodProyecto.Codigo=codproyecto;
            return Proyecto_Autorizacion.mostrarAutorizaciones(proyec_autoriz);
        }

        public static DataTable mostrarAutorizacionesComponente(string codcomp)
        {
            comp_autoriz = new Componente_Autorizacion();
            comp_autoriz.CodComponente.Codigo = codcomp;
            return Componente_Autorizacion.mostrarAutorizaciones(comp_autoriz);
        }

        public static void insertarAutorizacionProyecto(string codproy, string codautoriz, string aliasper, DateTime fechamax)
        {
            proyec_autoriz = new Proyecto_Autorizacion();
            proyec_autoriz.CodProyecto.Codigo = codproy;
            proyec_autoriz.CodAutoriz.Codigo = codautoriz;
            proyec_autoriz.AliasPers.Alias = aliasper;
            proyec_autoriz.FechaMax_Fin = fechamax;
            Proyecto_Autorizacion.insertar(proyec_autoriz);
        }

        public static void eliminarAutorizacionProyecto(int id)
        {
            proyec_autoriz = new Proyecto_Autorizacion();
            proyec_autoriz.ID = id;
            Proyecto_Autorizacion.delete(proyec_autoriz);
        }

        public static void eliminarAutorizacionComponente(int id)
        {
            comp_autoriz = new Componente_Autorizacion();
            comp_autoriz.ID = id;
            Componente_Autorizacion.delete(comp_autoriz);
        }
        public static void insertarAutorizacionComponente(string codcomp, string codautoriz, string aliaspers, DateTime fechamax)
        {
            comp_autoriz = new Componente_Autorizacion();
            comp_autoriz.CodComponente.Codigo = codcomp;
            comp_autoriz.CodAutoriz.Codigo = codautoriz;
            comp_autoriz.AliasPers.Alias = aliaspers;
            comp_autoriz.FechaMax_Fin = fechamax;
            Componente_Autorizacion.insertar(comp_autoriz);
        }

        public static string lastCodigoAutoriacion()
        {
            return Autorizacion.ultimoCodigo();
        }

        public static DataTable obtenerDatosAutorizacion(string codigoAut)
        {
            autorizacion = new Autorizacion();
            autorizacion.Codigo = codigoAut;
            return Autorizacion.obtenerDatos(autorizacion);
        }

        public static string obtenerIDAutorizacion(string nombreAutoriz)
        {
            autorizacion = new Autorizacion();
            autorizacion.Descripcion = nombreAutoriz;
            return autorizacion.getIDByName();
        }

        public static int obtenerIDProyAutoriz(string codProy, string codAutoriz)
        {
            proyec_autoriz = new Proyecto_Autorizacion();
            proyec_autoriz.CodProyecto.Codigo = codProy;
            proyec_autoriz.CodAutoriz.Codigo = codAutoriz;
            return proyec_autoriz.getID();
   
        }

        public static int obtenerIDCompAutoriz(string codComp, string codAutoriz)
        {            
            comp_autoriz = new Componente_Autorizacion();
            comp_autoriz.CodComponente.Codigo = codComp;
            comp_autoriz.CodAutoriz.Codigo = codAutoriz;
            return comp_autoriz.getID();
        }

        public static void actualizarFechaFinRealAutorizacionAsignada(int idTabla, DateTime fechaFinReal, int tipo)
        {
            if (tipo.Equals(1)) // proyecto_autorizacion
            {
                proyec_autoriz = new Proyecto_Autorizacion();
                proyec_autoriz.ID = idTabla;
                proyec_autoriz.FechaFin_Real = fechaFinReal;
                proyec_autoriz.updateFechaRealFinalizacion();                    
            }
            else // componente_autorizacion
            {
                comp_autoriz = new Componente_Autorizacion();
                comp_autoriz.ID = idTabla;
                comp_autoriz.FechaFin_Real = fechaFinReal;
                comp_autoriz.updateFechaRealFinalizacion();
            }
        }

        public static string obtenerEstadoNotifAsignacionAutorizacion(int idTabla, string tipoNotif)
        {
            if (tipoNotif.Equals("Proyecto"))
            {
                proyec_autoriz = new Proyecto_Autorizacion();
                proyec_autoriz.ID = idTabla;                
                return proyec_autoriz.getNotificado();
            }
            else
            {
                comp_autoriz = new Componente_Autorizacion();
                comp_autoriz.ID = idTabla;
                return comp_autoriz.getNotificado();
            }
        }

        public static void actualizarSINotificado(string tipo, int idAsig)
        {
            if (tipo.Equals("Proyecto"))
            {
                proyec_autoriz = new Proyecto_Autorizacion();
                proyec_autoriz.ID = idAsig;
                proyec_autoriz.updateNotificadoYes();
            }
            else
            {
                comp_autoriz = new Componente_Autorizacion();
                comp_autoriz.ID = idAsig;
                comp_autoriz.updateNotificadoYes();
            }
        }

        public static List<Object> obtenerDatosAsignacionAutorizacionProyecto(int idProyAutoriz)
        {
            proyec_autoriz = new Proyecto_Autorizacion();
            proyec_autoriz.ID = idProyAutoriz;
            return proyec_autoriz.getDatosProyectoAutorizacion();
        }

        public static List<Object> obtenerDatosAsignacionAutorizacionComponente(int idCompAutoriz)
        {
            comp_autoriz = new Componente_Autorizacion();
            comp_autoriz.ID = idCompAutoriz;
            return comp_autoriz.getDatosComponenteAutorizacion();
        }

        public static string obtenerAliasPersonaRespAutoriz(int id,string tipo)
        {
            if (tipo.Equals("Proyecto"))
            {
                proyec_autoriz = new Proyecto_Autorizacion();
                proyec_autoriz.ID = id;
                return proyec_autoriz.getAliasPersona();
            }
            else
            {
                comp_autoriz = new Componente_Autorizacion();
                comp_autoriz.ID = id;
                return comp_autoriz.getAliasPersona();
            }
        }
    }
}
