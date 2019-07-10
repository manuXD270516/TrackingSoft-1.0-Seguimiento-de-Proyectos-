using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;


namespace AccesoDatos_TrackinSoft
{
    public class Boleta
    {
    
        #region Properties
         public Int32 ID { get; set; } 

         public String Tipo { get; set; } 

         public Decimal Monto { get; set; } 

         public String Banco { get; set; } 

         public String Proveedor { get; set; } 

         public DateTime Fecha_Emitida { get; set; } 

         public DateTime Fecha_Vencimiento { get; set; }

         public DateTime? Renovacion_Emitida { get; set; }

         public DateTime? Renovacion_Vencimiento { get; set; } 

         public String Estado { get; set; } 

         public String CodSubcomp { get; set; }

         public String CodProyecto { get; set; }
         public String AliasPers { get; set; }

         public String Devolucion { get; set; }
    #endregion 
        
        public Boleta()
        {
             ///////////////////////
        }

        public Boleta(int id, string tipo, decimal monto, string banco, string proveedor, DateTime fechaEmision, DateTime fechaVenc,string estado, string codsubcomp,string codproy, string aliaspers)
        {
            this.ID = id;
            this.Tipo = tipo;
            this.Monto = monto;
            this.Banco = banco;
            this.Proveedor = proveedor;
            this.Fecha_Emitida = fechaEmision;
            this.Fecha_Vencimiento = fechaVenc;
            this.Estado = estado;
            this.CodSubcomp = codsubcomp;
            this.CodProyecto = codproy;
            this.AliasPers = aliaspers;
        }

        public void addBoleta()
        {
            string sql;
            if (this.Fecha_Vencimiento >= DateTime.Today)
            {
                sql = "insert into Boleta(Tipo,Monto,Banco,Proveedor,Fecha_Emitida,Fecha_Vencimiento,Estado,CodSubcomp,AliasPers,Devolucion,CodProyecto) values('" + this.Tipo + "'," + this.Monto.ToString().Replace(',','.') + ",'" + Banco + "','" + this.Proveedor + "','" + Fecha_Emitida.ToString("yyyy-MM-dd HH:mm:ss") + "','" +  Fecha_Vencimiento.ToString("yyyy-MM-dd HH:mm:ss") +"','Vigente','" + this.CodSubcomp + "','" + this.AliasPers + "','En Custodia','"+ this.CodProyecto+"');";
            }
            else
            {
                sql = "insert into Boleta(Tipo,Monto,Banco,Proveedor,Fecha_Emitida,Fecha_Vencimiento,Estado,CodSubcomp,AliasPers,Devolucion,CodProyecto) values('" + this.Tipo + "'," + this.Monto.ToString().Replace(',', '.') + ",'" + this.Banco + "','" + this.Proveedor + "','" + Fecha_Emitida.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Fecha_Vencimiento.ToString("yyyy-MM-dd HH:mm:ss") + "','Vencida','" + this.CodSubcomp + "','" + this.AliasPers + "','En Custodia','"+this.CodProyecto+"');"; 
            }
            //string sql = "insert into Boleta(Tipo,Monto,Banco,Proveedor,Fecha_Emitida,Fecha_Vencimiento,CodSubcomp,AliasPers) values('" + this.Tipo + "'," + this.Monto + ",'" + this.Banco + "','" + this.Proveedor + "','" + this.Fecha_Emitida + "','" + this.Fecha_Vencimiento + "','" + this.CodSubcomp + "','" + this.AliasPers + "');";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);            
            }
            catch (Exception ex)
            {
                string error = ex.Message;                             
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public DataTable getTableBoletas()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "select b.ID,b.Tipo as [Tipo Boleta],c.Nombre as [Subcomponente],p.Nombre as [Proyecto],b.Monto,b.Banco,b.Proveedor,ISNULL(b.Renovacion_Emitida,b.Fecha_Emitida) as [Emitida],ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento) as [Vencimiento],b.Estado as [Estado Tiempo],b.Devolucion as [Estado Entrega]" +
                            " from Boleta b,Componente c,Proyecto p" +
                            " where b.CodSubcomp=c.Codigo and c.CodProyecto=p.Codigo";
                Conexion.ComandExct = new OdbcCommand(sql, Conexion.conexion);
                OdbcDataAdapter adapter = new OdbcDataAdapter(Conexion.ComandExct);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
            /*finally
            {
                Conexion.abrirConexion(); 
            }
            // */           
        }

        public void updateDevolucion()
        {
            string sql = "update Boleta set Devolucion='Devuelta' where ID=" + this.ID + ";";
            Conexion.abrirConexion();
            try
            {
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }

        public void updateCronogramaRenovacion()
        {
            string sql = "update Boleta set Renovacion_Emitida='" + this.Renovacion_Emitida + "', Renovacion_Vencimiento='" + this.Renovacion_Vencimiento + "' where ID=" + this.ID;
            Conexion.abrirConexion();
            try
            {
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                Conexion.cerrarConexion();
            }

        }

        public DataTable getBoletasPorVencer()
        {
            try
            {
                string sql = "select b.ID,b.Tipo as [Tipo Boleta],c.Nombre as [Subcomponente],p.Nombre as [Proyecto],ISNULL(b.Renovacion_Emitida,b.Fecha_Emitida) as [Emitida],ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento) as [Vencimiento],b.Estado as [Estado Tiempo],b.Devolucion as [Estado Entrega]" +
                        " from Boleta b,Componente c,Proyecto p" +
                        " where b.CodSubcomp=c.Codigo and c.CodProyecto=p.Codigo and ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento)-GETDATE()<=5 and b.Estado<>'Vencida' and b.Devolucion<>'Devuelta'";
                Conexion.ComandExct = new OdbcCommand(sql, Conexion.conexion);
                DataTable dt = new DataTable();
                OdbcDataAdapter dw = new OdbcDataAdapter(Conexion.ComandExct);
                dw.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public DataTable getBoletarPorVencerEmail()
        {
            string sql;
            if (this.AliasPers.Length> 0)// si existe un correo o alias distinto del por default
            {
                sql="select b.ID,b.Tipo as [Tipo Boleta],c.Nombre as [Subcomponente],p.Nombre as [Proyecto],ISNULL(b.Renovacion_Emitida,b.Fecha_Emitida) as [Emitida],ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento) as [Vencimiento],datediff(day,GETDATE(),ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento)) as [Dias],b.Estado as [Estado Tiempo],b.Devolucion as [Estado Entrega]" +
                    " from Boleta b,Componente c,Proyecto p" +
                    " where b.CodSubcomp=c.Codigo and b.AliasPers='" + this.AliasPers + "' and b.AliasPers<>'callr' and c.CodProyecto=p.Codigo and ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento)-GETDATE()<=5 and b.Estado<>'Vencida' and b.Devolucion<>'Devuelta'";
            }
            else // solo es el usuario por defecto
            {
                sql = "select b.ID,b.Tipo as [Tipo Boleta],c.Nombre as [Subcomponente],p.Nombre as [Proyecto],ISNULL(b.Renovacion_Emitida,b.Fecha_Emitida) as [Emitida],ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento) as [Vencimiento],datediff(day,GETDATE(),ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento)) as [Dias],b.Estado as [Estado Tiempo],b.Devolucion as [Estado Entrega]" +
                        " from Boleta b,Componente c,Proyecto p" +
                        " where b.CodSubcomp=c.Codigo and c.CodProyecto=p.Codigo and ISNULL(b.Renovacion_Vencimiento,b.Fecha_Vencimiento)-GETDATE()<=5 and b.Estado<>'Vencida' and b.Devolucion<>'Devuelta'";            
            }
            try
            {
                Conexion.ComandExct = new OdbcCommand(sql, Conexion.conexion);
                DataTable dt = new DataTable();
                OdbcDataAdapter dw = new OdbcDataAdapter(Conexion.ComandExct);
                dw.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;                
            }
        }

        public void updateAndVerifStateAllBoleta()
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                Conexion.EjecutarProcedimientoAlmacenado("updateAllEstadoBoletas", lista, "Escritura");
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Conexion.cerrarConexion();
            }            
        }

        public string getEstadoEntrega()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select Devolucion from boleta where ID=" + this.ID;
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string x = "";
                if (dr.Read())
                {
                    x = dr.GetString(0);
                }                
                return x;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
            
        }
    }
}
