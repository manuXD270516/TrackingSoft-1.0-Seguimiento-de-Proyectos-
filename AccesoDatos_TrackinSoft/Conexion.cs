using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AccesoDatos_TrackinSoft.Properties;

using System.Data.Odbc;

namespace AccesoDatos_TrackinSoft
{
    public class Conexion
    {
        /*public static OdbcConnection conexion;
        public static OdbcCommand ComandExct;
        public static string cadenaConexion;*/
        /////////////////////////////////////
        public static OdbcConnection conexion;
        public static OdbcCommand ComandExct;
        public static string cadenaConexion;

        public static void setCadenaConexionBD(string cnxBD)
        {
        //    conexion = new OdbcConnection(cnxBD);
            cadenaConexion = cnxBD;
            
        }
        public static  void abrirConexion()
        {
            //ComandExct = new OdbcCommand()
            //conexion =new OdbcConnection(sbyte,)
            //conexion = new OdbcConnection("Data Source=.\\MSSQLSERVER1;Initial Catalog=Seguimiento_ProyectosYPFB_Aviacion;Integrated Security=True");
            //conexion = new OdbcConnection(ConfigurationManager.ConnectionStrings["TrackingSoft_1._0.Properties.Settings.Seguimiento_ProyectosYPFB_AviacionConnectionString"].ToString());
            //conexion = new OdbcConnection(Settings.Default.);
            
            //conexion = new OdbcConnection(cadenaConexion);
            
            
            //conexion=new OdbcConnection(ConfigurationManager.ConnectionStrings[2].ToString());
            //conexion = new OdbcConnection(ConfigurationManager.ConnectionStrings("asdfasd"));
            conexion = new OdbcConnection(cadenaConexion);
            conexion.Open();
           
            /* Ejecutar consulta sql prueba*/
                        
        }

        public static void ejecutarConsulta(string query)
        {
            //ComandExct = new OdbcCommand("select ID, UserName,Estado,CIPersonal,IDGrupo from Usuario", conexion);
           /* ComandExct = new OdbcCommand("select * from Area",conexion);
            ComandExct.ExecuteNonQuery();
            DataTable datos = new DataTable();
            //OdbcDataReader reader = ComandExct.ExecuteReader();
            OdbcDataAdapter adapter = new OdbcDataAdapter(ComandExct);
            adapter.Fill(datos);
            return datos;/*/
            
            //ComandExct = new OdbcCommand(query, conexion);
            ComandExct = new OdbcCommand(query, conexion);
            ComandExct.ExecuteNonQuery();
        }
        public static void cerrarConexion()
        {
            conexion.Close();
        }

        /*public static int EjecutarQuery(string SqlQuery) // devuelve el nro de filas afectadas en la BD
        {
            ComandExct = new OdbcCommand(SqlQuery,conexion);
            return ComandExct.ExecuteNonQuery();
        }*/

        public static int EjecutarProcedimientoAlmacenado(string nameProcAlm, /*List<OdbcParameter>*/ List<OdbcParameter> lista, string accion)
        {
            //OdbcCommand SqlCmd = new OdbcCommand();
            string x = "(";
            for (int i = 0; i < lista.Count; i++)
            {
                if (i== lista.Count-1)
                {
                    x = x + "?";
                }
                else
                {
                    x = x + "?,";
                }                
            }
            x = x + ")";
            if (lista.Count.Equals(0))
            {
                x = "";
            }
            OdbcCommand OdbcCmd = new OdbcCommand("{? = call "+nameProcAlm+x+"}", conexion);
            //SqlCmd.Connection = conexion;
            
            //OdbcCmd.Connection = conexion;
            
            //SqlCmd.CommandType = CommandType.StoredProcedure;            
            OdbcCmd.CommandType = CommandType.StoredProcedure;
            //SqlCmd.CommandText = nameProcAlm;
            //OdbcCmd.CommandText = nameProcAlm;
            OdbcParameter parameter = OdbcCmd.Parameters.Add("@RETURN_VALUE", OdbcType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            foreach (/*OdbcParameter*/ OdbcParameter param in lista)
            {
                //SqlCmd.Parameters.Add(param);
                OdbcCmd.Parameters.Add(param);
            }
         
            
            if (accion == "Lectura")
            {
                int tuplas = 0;
                //OdbcDataReader dr = SqlCmd.ExecuteReader();
                OdbcDataReader dr = OdbcCmd.ExecuteReader();
                if  (dr.Read())
                {
                    tuplas++;
                }
                return tuplas;
            }
            else // escritura de datos 
            {
                //OdbcCmd.Parameters.RemoveAt(0);
                //return SqlCmd.ExecuteNonQuery();
                //return OdbcCmd.ExecuteReader().RecordsAffected;
                OdbcDataReader dr = OdbcCmd.ExecuteReader();
                return 1;
                //return OdbcCmd.ExecuteNonQuery();
            }
        }

        public static /*OdbcDataReader*/ OdbcDataReader obtenerTuplasSP(string nameProc, /*List<OdbcParameter>*/ List<OdbcParameter> lista)
        {
            /*OdbcCommand SqlCmd = new OdbcCommand();
            SqlCmd.Connection = conexion;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = nameProc;*/
            OdbcCommand OdbcCmd = new OdbcCommand();
            OdbcCmd.Connection = conexion;
            OdbcCmd.CommandType = CommandType.StoredProcedure;
            OdbcCmd.CommandText = nameProc;
            /*foreach (OdbcParameter param in lista)
            {
                SqlCmd.Parameters.Add(param);
            }  */
            foreach (OdbcParameter param in lista)
            {
                OdbcCmd.Parameters.Add(lista);
            }
            //OdbcDataReader dr = SqlCmd.ExecuteReader();
            OdbcDataReader dr = OdbcCmd.ExecuteReader();
            return dr;
        }
        public static DataTable EjecutarProcedimientoMostrar(string nombre, List</*OdbcParameter*/OdbcParameter> lista)
        {
            DataTable dt = new DataTable();
            /*OdbcCommand SqlCmd = new OdbcCommand();
            SqlCmd.Connection = conexion;
            SqlCmd.CommandText = nombre;
            SqlCmd.CommandType = CommandType.StoredProcedure;*/
            //OdbcCommand OdbcCmd = new OdbcCommand();
            string x = " (";
            for (int i = 0; i < lista.Count; i++)
            {
                if (i == lista.Count - 1)
                {
                    x = x + "?";
                }
                else
                {
                    x = x + "?,";
                }
            }
            x = x + ")";
            if (lista.Count.Equals(0))
            {
                x = "";
            }
            OdbcCommand OdbcCmd = new OdbcCommand("{? = call " + nombre + x + "}", conexion);
            //OdbcCmd.Connection = conexion;
            //OdbcCmd.CommandText = nombre;
            OdbcCmd.CommandType = CommandType.StoredProcedure;
            OdbcParameter parameter = OdbcCmd.Parameters.Add("RETURN_VALUE", OdbcType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            /*foreach (OdbcParameter param in lista)
            {
                SqlCmd.Parameters.Add(param);
            }
            OdbcDataAdapter tableAdaper = new OdbcDataAdapter(SqlCmd);*/
            foreach (OdbcParameter param in lista)
            {
                OdbcCmd.Parameters.Add(param);
            }
            OdbcDataAdapter tableAdapter = new OdbcDataAdapter(OdbcCmd);
            tableAdapter.Fill(dt);
            return dt;

        }

        public static /*OdbcDataReader*/OdbcDataReader ObtenerTuplas(string Query)
        {
            //conexion.Open();
            //OdbcCommand SqlCmd = new OdbcCommand(Query, conexion);
            OdbcCommand OdbcCmd = new OdbcCommand(Query, conexion);
            /*SqlCmd.Connection = conexion;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = "Select * from Tipo_Proyecto";*/
            
            //OdbcDataReader dr = SqlCmd.ExecuteReader();
            OdbcDataReader dr = OdbcCmd.ExecuteReader();
            return dr;     
        }

      
    }
}
