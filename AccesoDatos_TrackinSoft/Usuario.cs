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
    public class Usuario
    {
        public String ID { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public Int32 Estado { get; set; }

        public Grupo IDGrupo { get; set; }

        public Personal AliasPers { get; set; }

       
        public Usuario()
        {
            this.AliasPers = new Personal();
            this.IDGrupo = new Grupo();
        }

        public Usuario(string id, string username, string password, int estado, Personal aliaspers, Grupo idgrup)
        {
            this.ID = id;
            this.UserName = username;
            this.Password = password;
            this.Estado = estado;
            this.AliasPers = aliaspers;
            this.IDGrupo = idgrup;
        }

        public static int validar(Usuario user)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@username";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 100;
                p1.Value = user.UserName;
                lista.Add(p1);

                p2.ParameterName = "@password";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 20;
                p2.Value = user.Password;
                lista.Add(p2);

                int x = Conexion.EjecutarProcedimientoAlmacenado("validarUsuario", lista, "Lectura");
                return x;
            }
            catch (Exception)
            {
                return -1; // valor absurdo
            }
            finally
            {
                Conexion.cerrarConexion();                
            }
            
        }

        public static DataTable obtenerDatos(Usuario user)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();

                p1.ParameterName = "@username";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 100;
                p1.Value = user.UserName;
                lista.Add(p1);

                p2.ParameterName = "@password";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 20;
                p2.Value = user.Password;
                lista.Add(p2);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("obtenerUsuario", lista);
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

        public static void insertar(Usuario usuario)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1, p2, p3, p4, p5;
                p1 = new OdbcParameter();
                p2 = new OdbcParameter();
                p3 = new OdbcParameter();
                p4 = new OdbcParameter();
                p5 = new OdbcParameter();

                p1.ParameterName = "@id";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = usuario.ID;
                lista.Add(p1);

                p2.ParameterName = "@username";
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 100;
                p2.Value = usuario.UserName;
                lista.Add(p2);

                p3.ParameterName = "@password";
                p3.OdbcType = OdbcType.VarChar;
                p3.Size = 20;
                p3.Value = usuario.Password;
                lista.Add(p3);

                p4.ParameterName = "@idgrupo";
                p4.OdbcType = OdbcType.VarChar;
                p4.Size = 10;
                p4.Value = usuario.IDGrupo.ID;
                lista.Add(p4);

                p5.ParameterName = "@aliaspers";
                p5.OdbcType = OdbcType.VarChar;
                p5.Size = 20;
                p5.Value = usuario.AliasPers.Alias;
                lista.Add(p5);

                Conexion.EjecutarProcedimientoAlmacenado("insertUsuario", lista, "Escritura");                
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

        public static string ultimoID()
        {
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas("select Top(1) ID from Usuario order by 1 desc");
                string x;
                if (dr.Read())
                {
                    x = dr.GetString(0);
                }
                else
                {
                    x = "";
                }
                return x.Trim();
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

        public static List<Usuario> listaDatos()
        {
            try
            {
                Conexion.abrirConexion();
                List<Usuario> lista = new List<Usuario>();
                OdbcDataReader dr = Conexion.ObtenerTuplas("Select ID,UserName from usuario");
                while (dr.Read())
                {
                    Usuario x = new Usuario();
                    x.ID = dr.GetString(0);
                    x.UserName = dr.GetString(1);
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

        // cambiar la forma de trabajar la ejecucion de la consulta SQL
        public static DataTable mostrar()
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                /*OdbcCommand x = new OdbcCommand("Select * from ", Conexion.conexion);
                x.CommandType = CommandType.Text;
                OdbcDataAdapter t = new OdbcDataAdapter(x);
                t.Fill*/
                DataTable dt = Conexion.EjecutarProcedimientoMostrar("mostrarUsuarios", lista);
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

        public static DataTable obtenerPorPersonal(Usuario user)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@personal";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 50;
                p1.Value = user.AliasPers.Nombres;
                lista.Add(p1);

                DataTable dt = Conexion.EjecutarProcedimientoMostrar("obtenerUsuarioPorPersonal", lista);
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

        public static void delete(Usuario user)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter();

                p1.ParameterName = "@id";
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 10;
                p1.Value = user.ID;
                lista.Add(p1);

                Conexion.EjecutarProcedimientoAlmacenado("deleteUsuario", lista, "Escritura");            
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

        public static void updateGrupo(Usuario user)
        {
            try
            {
                Conexion.abrirConexion();
                List<OdbcParameter> lista = new List<OdbcParameter>();
                OdbcParameter p1 = new OdbcParameter("@username", user.UserName);
                p1.OdbcType = OdbcType.VarChar;
                p1.Size = 100;
                lista.Add(p1);
                OdbcParameter p2 = new OdbcParameter("@idgrupo", user.IDGrupo.ID);
                p2.OdbcType = OdbcType.VarChar;
                p2.Size = 10;
                lista.Add(p2);

                Conexion.EjecutarProcedimientoAlmacenado("updateGrupoUser", lista, "Escritura");            
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

        public string getID()
        {
            string sql = "select u.ID from Usuario u, Personal p where u.AliasPers=p.Alias and p.Alias='" + this.AliasPers.Alias + "';";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string id = "";
                if (dr.Read())
                {
                    id = dr.GetString(0);
                }
                dr.Close();
                return id;
            }
            catch (Exception ex)
            {
                return "";
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();            
            }          
        }

        public string getNameLastNameByIDUser()
        {
            try
            {
                Conexion.abrirConexion();
                string sql = "select RTRIM(p.Nombres)+' '+RTRIM(p.Apellidos) as [Personal]" +
                            " from Personal p,Usuario u" +
                            " where u.AliasPers=p.Alias and u.ID='" + this.ID + "';";
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

        public string getEmail()
        {
            string sql = "select p.Email from Personal p,Usuario u where u.AliasPers=p.Alias and u.ID='" + this.ID + "';";            
            try
            {
                string correo="";
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                if (dr.Read())
                {
                    correo = dr.GetString(0);
                }
                return correo;
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }

        public string getAliasPersona()
        {
            string sql = "select AliasPers from usuario where ID='" + this.ID + "';";
            try
            {
                Conexion.abrirConexion();
                OdbcDataReader dr = Conexion.ObtenerTuplas(sql);
                string alias = "";
                if (dr.Read())
                {
                    alias = dr.GetString(0);
                }
                dr.Close();
                return alias;
            }
            catch (Exception ex)
            {
                return "";
                //throw;
            }
            finally
            {
                Conexion.cerrarConexion();
            }
        }


    }
}
