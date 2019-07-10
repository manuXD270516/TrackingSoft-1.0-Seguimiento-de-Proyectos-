using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Fase
    {
        public String ID { get; set; }

        public Int32 NroPaso { get; set; }

        public String Nombre { get; set; }

        public Area IDArea { get; set; }

        public Int32 DiasPlazo { get; set; } 

        public Fase()
        {
            IDArea = new Area();
        }

        public Fase(string id, int nroPaso, string nombre,Area idarea,int diasplazo)
        {
            this.ID = id;
            this.NroPaso = nroPaso;
            this.Nombre = nombre;
            this.IDArea = idarea;
            this.DiasPlazo = diasplazo;
        }

        public static List<Fase> listaDatosDisponibles(string codsubcomp)
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select f.ID,f.Nombre from Fase f,Fase_Componente fc,Componente c where fc.CodComponente=c.Codigo and fc.IDFase=f.ID and c.Codigo='" + codsubcomp + "'");
                List<Fase> ltipo = new List<Fase>();
                while (dr.Read())
                {
                    Fase x = new Fase();
                    x.ID = dr.GetString(0);
                    x.Nombre = dr.GetString(1);
                    ltipo.Add(x);
                }
                return ltipo;
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

        public string getIDbyName()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select ID from Fase where Nombre='" + this.Nombre + "';";
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string idfase = "";
                if (dr.Read())
                {
                    idfase = dr.GetString(0);
                }
                return idfase;
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

        public static List<Fase> listaDatosAll()
        {
            try
            {
                Conexion.abrirConexion();
                List<Fase> lista = new List<Fase>();
                OdbcDataReader dr = Conexion.ObtenerTuplas("Select ID,Nombre from fase");
                while (dr.Read())
                {
                    Fase x = new Fase();
                    x.ID = dr.GetString(0);
                    x.Nombre = dr.GetString(1);
                    lista.Add(x);
                }
                return lista;
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

        public string getNameByID()
        {
            try
            {
                string sql = "select Nombre from Fase where ID='" + this.ID + "';";
                Conexion.abrirConexion();
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

        public DataTable getTableFases()
        {
            try
            {
                string sql = "select f.NroPaso as [#],f.Nombre as [Fase],a.Nombre as [Area],f.DiasPlazo as [Dias]" +
                       " from Fase f,Area a" +
                       " where f.IDArea=a.ID";
                Conexion.ComandExct = new OdbcCommand(sql, Conexion.conexion);
                OdbcDataAdapter adapter = new OdbcDataAdapter(Conexion.ComandExct);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public void updateInformacionFase()
        {
            string sql = "update Fase set DiasPlazo=" + this.DiasPlazo + ", Nombre='"+this.Nombre+"' where ID='" + this.ID + "';";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message+" error de codigo");
            }
            finally
            {
                Conexion.cerrarConexion();
            }                        
        }

        public string getIDArea()
        {
            string sql = "select IDArea from fase where ID='" + this.ID + "';";
            string x="";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    x = dr.GetString(0);
                }
                return x;
            }
            catch (Exception ex)
            {
                return ex.Message;                 
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
