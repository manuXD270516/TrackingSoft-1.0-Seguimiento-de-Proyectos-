using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;
using System.Windows.Forms;

namespace Negocio_TrackingSoft
{
    public class Auxiliar
    {
        // Clase extrictamente para agregar metodos auxiliares de rellando de controle con BD
        public static List<Tipo_Proyecto> obtenerTuplasTipoProyecto()
        {
            return Tipo_Proyecto.listaDatos();
        }

        public static List<Tipo_Modificacion> obtenerTuplasTipoModifProyecto() 
        {
            return Tipo_Modificacion.listaDatos();        
        }

        public static List<Cargo> obtenerTuplasCargo(string idarea)
        {
            return Cargo.listaDatos(idarea);
        }

        public static List<Autorizacion> obtenerTuplasAutorizacion(int tipo)
        {
            return Autorizacion.listaDatos(tipo);
        }

        public static List<Personal> obtenerTuplasPersonal()
        {
            return Personal.listaDatos();
        }
        public static void ajustarTamañoTabla(DataGridView tabla)
        {
            for (int i = 0; i < tabla.RowCount; i++)
            {
                DataGridViewRow d = tabla.Rows[i];
                for (int j = 0; j < d.Cells.Count; j++)
                {
                    string x = d.Cells[j].Value.ToString();
                    d.Cells[j].Value = d.Cells[j].Value.ToString().Trim();
                }
                /*string x = d.Cells[1].Value.ToString();
                d.Cells[1].Value = d.Cells[1].Value.ToString().Trim();*/
            }
            for (int j = 0; j < tabla.ColumnCount; j++)
            {
                tabla.AutoResizeColumn(j);
            }
            //tabla.Rows.RemoveAt(tabla.Rows.Count-1);
        }


        public static List<Proyecto> obtenerTuplasProyecto(int tipo)
        {
            return Proyecto.listaDatos(tipo);
        }

        public static List<Componente> obtenerTuplasComponente(string codProy,int tipoModif)
        {
            return Componente.listaDatos(codProy,tipoModif);
        }

        public static List<Componente> obtenerTuplasComponenteTotal(string codProy)
        {
            return Componente.listaDatosTotal(codProy);
        }

        public static List<Aeropuerto> obtenerTuplasAeropuerto()
        {
            return Aeropuerto.listaDatos();
        }

        public static List<Componente> obtenerTuplasSubComponente(string codcomp)
        {
            return Componente.listaDatosSubComp(codcomp);
        }

        public static List<Fase> obtenerTuplasFasesDisponibles(string codsubcomp)
        {
            return Fase.listaDatosDisponibles(codsubcomp);
        }

        public static List<Personal> obtenerTuplasPersonalParaFases(string idFase,int tipo)
        {
            return Personal.listaDatosDispFases(idFase,tipo);
        }

        public static void setConexionStringBD(string cnxBD)
        {
            Conexion.setCadenaConexionBD(cnxBD);
        }

        public static List<Departamento> obtenerTuplasDepartamento()
        {
            return Departamento.listaDatos();
        }

        public static List<Grupo> obtenerTuplasGrupo()
        {
            return Grupo.listaDatos();
        }

        public static List<Usuario> obtenerTuplasUsuario()
        {
            return Usuario.listaDatos();
        }

        public static List<Personal> obtenerTuplasPersonalUsuariio()
        {
            return Personal.listaDatos2();
        }

        public static List<Estado> obtenerTuplasEstadosTareas(int tipo)
        {
            return Estado.listaDatosTareas(tipo);
        }

       
        public static void execCiclo(bool valor)
        {
            Bitacora.getGlobalVerif(valor);
        }


        public static List<Proyecto> obtenerTuplasProyectoAll()
        {
            return Proyecto.listaDatosAll();
        }

        public static object obtenerTuplasFasesAll()
        {
            return Fase.listaDatosAll();
        }

        public static List<Componente> obtenerTuplasSubComponenteByProyecto(string codproy)
        {
            return Componente.listaDatosSubcomponenteByProyecto(codproy);
        }

        public static List<Area> obtenerTuplasArea()
        {
            return Area.listDatos();
        }
    }
}
