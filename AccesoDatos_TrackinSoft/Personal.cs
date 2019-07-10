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
    public class Personal
    {

        public String Alias { get; set; }

        public String Nombres { get; set; }

        public String Apellidos { get; set; }

        public DateTime Fecha_Nac { get; set; }

        public String Lugar_Nac { get; set; }

        public Char Sexo { get; set; }

        public String Email { get; set; }

        public Cargo IDCargo { get; set; }

        public Personal()
        {
            this.IDCargo = new Cargo();
        }

        public Personal(string alias,string nombres,string apellidos,DateTime fechaNac,string lugarNac,char sexo,string email,Cargo cargo)
        {
            this.Alias = alias;
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.Fecha_Nac=fechaNac;
            this.Lugar_Nac = lugarNac;
            this.Sexo = sexo;
            this.Email = email;
            this.IDCargo = IDCargo;
        }

        public static List<Personal> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Alias, rtrim(Nombres)+' '+ rtrim(Apellidos) AS Persona FROM Personal");
                List<Personal> ltipo = new List<Personal>();
                while (dr.Read())
                {
                    //ltipo.Add(new Tipo_Personal(int.Parse(dr["Codigo"].ToString()), dr["Tipo"].ToString()));
                    Personal x = new Personal();
                    x.Alias = dr.GetString(0);
                    x.Nombres = dr.GetString(1);
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

        /*
         * "
         * /
        */ 
        public static List<Personal> listaDatosDispFases(string idFase,int tipo)
        {
            try
            {
                Conexion.abrirConexion();
                string sql;
                if (tipo.Equals(2))
                {
                    sql = "select p.Alias,RTRIM(p.Nombres)+ ' '+RTRIM(p.Apellidos) as Responsable from Cargo c,Area a,Fase f,Personal p where c.IDArea=a.ID and f.IDArea=a.ID and p.IDCargo=c.ID and f.ID='" + idFase + "';";
                }
                else
                {
                    sql = "select p.Alias,RTRIM(p.Nombres)+ ' '+RTRIM(p.Apellidos) as Responsable from Cargo c,Area a,Fase f,Personal p where c.IDArea=a.ID and p.IDCargo=c.ID and (a.ID<>'A07' and a.ID<>'A09') and f.ID='" + idFase + "';";
                }
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                List<Personal> ltipo = new List<Personal>();
                while (dr.Read())
                {
                    Personal x = new Personal();
                    x.Alias = dr.GetString(0);
                    x.Nombres = dr.GetString(1);
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

        public static List<Personal> listaDatos2()
        {
            try
            {
                Conexion.abrirConexion();
                List<Personal> lista = new List<Personal>();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select u.UserName,RTRIM(p.Nombres)+' '+RTRIM(p.Apellidos) as [Personal]" +
                                                        " from Personal p,Usuario u" +
                                                        " where u.AliasPers=p.Alias and p.Alias in (select u.AliasPers from Usuario u) ");
                while (dr.Read())
                {
                    Personal x = new Personal();
                    x.Alias = dr.GetString(0);
                    x.Nombres = dr.GetString(1);
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

        public bool addPersonal()
        {
            string sql = "insert into Personal values('" + this.Alias + "','" + this.Nombres + "','" + this.Apellidos + "','" + this.Sexo + "','" + this.Email + "','" + this.IDCargo.ID + "');";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);
                return true;
            }
            catch (Exception ex)
            {
                string error=ex.Message;
                return false;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public DataTable getTablePersonal()
        {
            try
            {
                Conexion.cerrarConexion();
                string sql = "Select p.Alias,p.Nombres,p.Apellidos,p.Email,c.Nombre as Cargo,a.Nombre as Area" +
                           " from Personal p,Cargo c,Area a where p.IDCargo=c.ID and c.IDArea=a.ID";
                OdbcCommand comando = new OdbcCommand(sql, Conexion.conexion);
                //Conexion.ComandExct = new OdbcCommand(sql, Conexion.conexion);
                DataTable dt = new DataTable();
                OdbcDataAdapter adaptador = new OdbcDataAdapter(comando);
                adaptador.Fill(dt);
                return dt;
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

        public bool updatePersonal()
        {
            string sql = "update Personal set Nombres='" + this.Nombres + "', Apellidos='" + this.Apellidos + "', Email='" + this.Email + "', IDCargo='" + this.IDCargo.ID + "' where Alias='" + this.Alias + "';";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public bool deletePersonal()
        {
            string sql = "delete from Personal where Alias='" + this.Alias + "';";
            try
            {
                Conexion.abrirConexion();
                Conexion.ejecutarConsulta(sql);
                return true;
            }
            catch (Exception ex )
            {
                string error = ex.Message;
                return false;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }
    }
}
