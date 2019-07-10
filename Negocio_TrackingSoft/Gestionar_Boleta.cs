using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos_TrackinSoft;
using System.Data;

namespace Negocio_TrackingSoft
{
    public class Gestionar_Boleta
    {

        public static Boleta boleta;

        public static void registrarBoleta(string tipo, decimal monto, string banco, string proveedor, DateTime emitida, DateTime vencimiento, string codsubcomp,string codproy, string personal)
        {
            boleta = new Boleta();
            boleta.Tipo = tipo;
            boleta.Monto = monto;
            boleta.Banco = banco;
            boleta.Proveedor = proveedor;
            boleta.Fecha_Emitida = emitida;
            boleta.Fecha_Vencimiento = vencimiento;
            boleta.CodSubcomp = codsubcomp;
            boleta.CodProyecto = codproy;
            boleta.AliasPers = personal;
            boleta.addBoleta();
        }

        public static DataTable showBoletas()
        {
            boleta = new Boleta();
            return boleta.getTableBoletas();
        }

        public static void devolverBoleta(int idboleta)
        {
            boleta = new Boleta();
            boleta.ID = idboleta;
            boleta.updateDevolucion();
        }

        public static void renovarCronogramaBoleta(int idboltaSelect, DateTime rEmision, DateTime rVencimiento)
        {
            boleta = new Boleta();
            boleta.ID = idboltaSelect;
            boleta.Renovacion_Emitida = rEmision;
            boleta.Renovacion_Vencimiento = rVencimiento;
            boleta.updateCronogramaRenovacion();
        }

        public static DataTable obtenerBoletasPorVencer()
        {
            boleta = new Boleta();
            return boleta.getBoletasPorVencer();
        }

        public static void verificarActualizarEstadoBoletas()
        {
            boleta = new Boleta();
            boleta.updateAndVerifStateAllBoleta();
        }

        public static DataTable obtenerBoletasPorVencerEmail(string alias)
        {
            boleta = new Boleta();
            boleta.AliasPers = alias;
            return boleta.getBoletarPorVencerEmail();
        }

        public static string  obtenerEstadoEntrega(int id)
        {
            boleta=new Boleta();
            boleta.ID = id;
            return boleta.getEstadoEntrega();
        }
    }
}
