using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;

namespace Negocio_TrackingSoft
{
    public class Gestionar_Proyecto
    {
        public static Proyecto proyecto;
        public static Aeropuerto aeropuerto;
        public static Estado_Proyecto estadoProyecto;
        public static Tipo_Proyecto tipoProyecto;
        public static void insertarProyecto1(string codigo, string nombre, string desc, int codtipo, int idgestion, decimal costo, int idcrono,string coddpto)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codigo;
            proyecto.Nombre = nombre;
            proyecto.Descripcion = desc;
            proyecto.CodTipo_Proyecto.Codigo = codtipo;
            proyecto.IDGestion.ID = idgestion;
            proyecto.Costo = costo;
            proyecto.IDCronograma.Id = idcrono;
            //proyecto.CodAeropuerto.Codigo = codaero;
            proyecto.CodDepartamento.Codigo = coddpto;
            Proyecto.insertar1(proyecto);
        }

        public static void insertarProyecto2(string codigo, string nombre, string desc, int codtipo, int idgestion, decimal costo, int idcrono)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codigo;
            proyecto.Nombre = nombre;
            proyecto.Descripcion = desc;
            proyecto.CodTipo_Proyecto.Codigo = codtipo;
            proyecto.IDGestion.ID = idgestion;
            proyecto.Costo = costo;
            proyecto.IDCronograma.Id = idcrono;
            Proyecto.insertar2(proyecto);
        }

        public static void ActualizarProyecto(string codigo,string nuevocodigo, string nombre, string desc,decimal costo, int idcrono)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codigo;
            proyecto.Nombre = nombre;
            proyecto.Descripcion = desc;
            proyecto.Costo = costo;
            proyecto.IDCronograma.Id = idcrono;
            Proyecto.update(proyecto,nuevocodigo);
        }

        public static void eliminarProyecto(string codigo)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codigo;
            Proyecto.delete(proyecto);
        }

        public static DataTable mostrarProyectos(int tipo)
        {
            return Proyecto.mostrarproyectos(tipo);
        }

        public static string lastCodigoProyecto()
        {
            return Proyecto.ultimoCodigo();
        }

        public static void insertarAeropuerto(string codigo,string coddpto)
        {
            aeropuerto = new Aeropuerto();
            aeropuerto.Codigo = codigo;
            aeropuerto.JefePlanta = "Sin Jefe Fijo";
            aeropuerto.CodDepartamento.Codigo = coddpto;
            Aeropuerto.insertar(aeropuerto);
        }

        public static DataTable obtenerProyecto(string codigo,int tipoProy)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codigo;
            proyecto.CodTipo_Proyecto.Codigo = tipoProy;
            return Proyecto.obtener(proyecto);
        }

        public static DataTable obtenerDatosProyecto(string codigo)
        {
            proyecto = new Proyecto();
            proyecto.Codigo = codigo;
            return Proyecto.obtenerDatos(proyecto);
        }

        public static List<Proyecto> getProyectos()
        {
            return Proyecto.obtenerListProyectos();
        }

        public static void iniciarEstado(string codigo)
        {
            estadoProyecto = new Estado_Proyecto();
            estadoProyecto.CodProyecto.Codigo = codigo;
            Estado_Proyecto.asignarEstadoInicial(estadoProyecto);
        }

        public static Cronograma obtenerCronogramaProyecto(string codProy)
        {
            return Proyecto.obtenerCronograma(codProy);
        }

        public static string obtenerCodigoProyecto(string nombreProy)
        {
            return Proyecto.getCodigo(nombreProy);
        }

        public static void updateEstadoProyecto(string codproy)
        {
            estadoProyecto = new Estado_Proyecto();
            estadoProyecto.CodProyecto.Codigo = codproy;
            Estado_Proyecto.updateEstado(estadoProyecto);
        }

        public static void verificarEstadoFinalizacionProyectos()
        {
            Estado_Proyecto.updateAllEstadoProyectos();
        }

        public static int obtenerIDCronogramaProyecto(string codigo)
        {
            return Proyecto.getIDCronograma(codigo);
        }

        public static Cronograma obtenerCronogramaProyecto_desdeComponente(string codcomp)
        {
            return Componente.obtenerCronogramaProyecto(codcomp);
        }

        public static string obtenerDescrProyecto(string codigo)
        {
            return Proyecto.getDescripcion(codigo);
        }

        public static string obtenerIDProyecto(string objAsignado)
        {
            proyecto = new Proyecto();
            proyecto.Nombre = objAsignado;
            return proyecto.getCodigoByNombre();
        }

        public static string obtenerNombreTipoProyecto(object codtipo)
        {
            tipoProyecto = new Tipo_Proyecto();
            tipoProyecto.Codigo = (int)codtipo;
            return tipoProyecto.getTipo();
        }
    }
}
