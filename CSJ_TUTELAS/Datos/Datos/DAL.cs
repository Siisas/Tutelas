using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Datos.Modelo;
using System.Globalization;

namespace Datos
{
    /// <summary>
    /// Clase de la capa de datos (Direct Access Layer)
    /// </summary>
    public class DAL
    {

        #region Consultas Detalle

        public List<mPartesProcesales> getPartesProcesales(string NumProceso)
        {
            List<mPartesProcesales> lista = new List<mPartesProcesales>();

            int Error = 0;

            using (ConexionCC objConn = new ConexionCC())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getPartesProcesales", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@p_numeroProceso", SqlDbType.VarChar).Value = NumProceso;
                    cmd.Parameters.Add("@p_errNumber", SqlDbType.Int).Value = Error;
                    cmd.Parameters[1].Direction = ParameterDirection.Output;


                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);



                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mPartesProcesales objRes = new mPartesProcesales();

                        if (objReader["idproceso"] != DBNull.Value)
                            objRes.idproceso = objReader["idproceso"].ToString();

                        if (objReader["Numero_Identificacion"] != DBNull.Value)
                            objRes.Numero_Identificacion = objReader["Numero_Identificacion"].ToString();

                        if (objReader["Tipo_Identificacion"] != DBNull.Value)
                            objRes.Tipo_Identificacion = objReader["Tipo_Identificacion"].ToString();

                        if (objReader["Tipo_Sujeto"] != DBNull.Value)
                            objRes.Tipo_Sujeto = objReader["Tipo_Sujeto"].ToString();

                        if (objReader["Primer_Nombre"] != DBNull.Value)
                            objRes.Primer_Nombre = objReader["Primer_Nombre"].ToString();

                        if (objReader["Segundo_Nombre"] != DBNull.Value)
                            objRes.Segundo_Nombre = objReader["Segundo_Nombre"].ToString();

                        if (objReader["Primer_Apellido"] != DBNull.Value)
                            objRes.Primer_Apellido = objReader["Primer_Apellido"].ToString();

                        if (objReader["Segundo_Apellido"] != DBNull.Value)
                            objRes.Segundo_Apellido = objReader["Segundo_Apellido"].ToString();

                        if (objReader["Entidad"] != DBNull.Value)
                            objRes.Entidad = objReader["Entidad"].ToString();

                        if (objReader["Departamento_Contacto"] != DBNull.Value)
                            objRes.Departamento_Contacto = objReader["Departamento_Contacto"].ToString();

                        if (objReader["Ciudad_Contacto"] != DBNull.Value)
                            objRes.Ciudad_Contacto = objReader["Ciudad_Contacto"].ToString();

                        if (objReader["Correo_Electronico_Contacto"] != DBNull.Value)
                            objRes.Correo_Electronico_Contacto = objReader["Correo_Electronico_Contacto"].ToString();

                        if (objReader["Telefono_Contacto"] != DBNull.Value)
                            objRes.Telefono_Contacto = objReader["Telefono_Contacto"].ToString();

                        if (objReader["Celular_Contacto"] != DBNull.Value)
                            objRes.Celular_Contacto = objReader["Celular_Contacto"].ToString();

                        lista.Add(objRes);
                    }
                }
            }

            return lista;
        }

        public List<mArchivosActuacionesProceso> getArchivosActuaciones(string NumProceso)
        {
            List<mArchivosActuacionesProceso> lista = new List<mArchivosActuacionesProceso>();

            int Error = 0;

            using (ConexionCC objConn = new ConexionCC())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getArchivosActuaciones", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@p_numeroProceso", SqlDbType.VarChar).Value = NumProceso;
                    cmd.Parameters.Add("@p_errNumber", SqlDbType.Int).Value = Error;
                    cmd.Parameters[1].Direction = ParameterDirection.Output;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mArchivosActuacionesProceso objRes = new mArchivosActuacionesProceso();

                        if (objReader["id"] != DBNull.Value)
                            objRes.id = objReader["id"].ToString();

                        if (objReader["idproceso"] != DBNull.Value)
                            objRes.idproceso = objReader["idproceso"].ToString();

                        if (objReader["Nombreactuacion"] != DBNull.Value)
                            objRes.Nombreactuacion = objReader["Nombreactuacion"].ToString();

                        if (objReader["Archivo"] != DBNull.Value)
                            objRes.Archivo = (byte[])objReader["Archivo"];

                        if (objReader["NombreArchivo"] != DBNull.Value)
                            objRes.NombreArchivo = objReader["NombreArchivo"].ToString();

                        if (objReader["Codigo_Hash"] != DBNull.Value)
                            objRes.Codigo_Hash = objReader["Codigo_Hash"].ToString();

                        lista.Add(objRes);
                    }
                }
            }

            return lista;
        }

        public List<mArchivosActuacionesProceso> getArchivosProceso(string NumProceso)
        {
            List<mArchivosActuacionesProceso> lista = new List<mArchivosActuacionesProceso>();

            int Error = 0;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getArchivosProceso", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@p_numeroProceso", SqlDbType.VarChar).Value = NumProceso;
                    cmd.Parameters.Add("@p_errNumber", SqlDbType.Int).Value = Error;
                    cmd.Parameters[1].Direction = ParameterDirection.Output;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mArchivosActuacionesProceso objRes = new mArchivosActuacionesProceso();

                        if (objReader["id"] != DBNull.Value)
                            objRes.id = objReader["id"].ToString();

                        if (objReader["Archivo"] != DBNull.Value)
                            objRes.Archivo = (byte[])objReader["Archivo"];

                        if (objReader["NombreArchivo"] != DBNull.Value)
                            objRes.NombreArchivo = objReader["NombreArchivo"].ToString();

                        if (objReader["Codigo_Hash"] != DBNull.Value)
                            objRes.Codigo_Hash = objReader["Codigo_Hash"].ToString();

                        if (objReader["Tipo"] != DBNull.Value)
                            objRes.Tipo = objReader["Tipo"].ToString();

                        lista.Add(objRes);
                    }
                }
            }

            return lista;
        }

        public bool Insertar_Radicado(string NumProceso, string IdEstado, string Observaciones,string IdUsuario)
        {
            bool Res = false;
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@p_numeroProceso", N_VALOR = NumProceso });
            arrayparam.Add(new mDataDdl { N_ID = "@p_idEstado", N_VALOR = IdEstado });
            arrayparam.Add(new mDataDdl { N_ID = "@p_observaciones", N_VALOR = Observaciones });
            arrayparam.Add(new mDataDdl { N_ID = "@p_IdUsuario", N_VALOR= IdUsuario });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = this.Ejecutar("sp_insRadicado", ref mensajerror, "sp", arrayparam);

            if (mensajerror == "")
            {
                Res = true;
            }

            return Res;
        }

        #endregion

        #region ParametrosGenerales        
        /// <summary>
        /// Gets the parametros generales by tipo.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IQueryable<mParametrosGenerales> getParametrosGeneralesByTipo(int id)
        {
            List<mParametrosGenerales> lista = new List<mParametrosGenerales>();
            IQueryable<mParametrosGenerales> listaQueryable;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getParametrosGeneralesByTipo", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mParametrosGenerales objRes = new mParametrosGenerales();

                        objRes.idParametro = Convert.ToInt32(objReader["idParametro"]);

                        if (objReader["Parametro"] != DBNull.Value)
                            objRes.Parametro = objReader["Parametro"].ToString();

                        if (objReader["Descripcion"] != DBNull.Value)
                            objRes.Descripcion = objReader["Descripcion"].ToString();

                        if (objReader["idTipoParametro"] != DBNull.Value)
                            objRes.idTipoParametro = Convert.ToInt32(objReader["idTipoParametro"]);

                        lista.Add(objRes);
                    }
                }
            }

            listaQueryable = lista.AsQueryable();
            return listaQueryable;
        }
        /// <summary>
        /// Gets the value parameter grales.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int getValParamGrales(int id)
        {
            int value = 0;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_valParamGral", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        value = Convert.ToInt32(objReader["value"]);
                    }
                }
            }

            return value;
        }
        /// <summary>
        /// Gets the data DDL.
        /// </summary>
        /// <param name="tabla">The tabla.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="valor">The valor.</param>
        /// <param name="iddefault">The iddefault.</param>
        /// <param name="valordefault">The valordefault.</param>
        /// <param name="wherecolumn">The wherecolumn.</param>
        /// <param name="wherevalue">The wherevalue.</param>
        /// <returns></returns>
        public IQueryable<mDataDdl> getDataDdl(string tabla, string id, string valor, string iddefault, string valordefault, string wherecolumn, string wherevalue)
        {
            List<mDataDdl> lista = new List<mDataDdl>();
            IQueryable<mDataDdl> listaQueryable;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getDataDdl", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tabla", SqlDbType.VarChar).Value = tabla;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                    cmd.Parameters.Add("@iddefault", SqlDbType.VarChar).Value = iddefault;
                    cmd.Parameters.Add("@valordefault", SqlDbType.VarChar).Value = valordefault;
                    cmd.Parameters.Add("@wherecolumn", SqlDbType.VarChar).Value = wherecolumn;
                    cmd.Parameters.Add("@wherevalue", SqlDbType.VarChar).Value = wherevalue;

                    try
                    {
                        SqlDataReader objReader = cmd.ExecuteReader();

                        while (objReader.Read())
                        {
                            mDataDdl objRes = new mDataDdl();

                            objRes.N_ID = objReader["N_ID"].ToString();

                            if (objReader["N_VALOR"] != DBNull.Value)
                                objRes.N_VALOR = objReader["N_VALOR"].ToString();

                            lista.Add(objRes);
                        }
                    }
                    catch (Exception ex) { }


                }
            }

            listaQueryable = lista.AsQueryable();
            return listaQueryable;
        }
        /// <summary>
        /// Gets the parametros generales.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IQueryable<mParametrosGenerales> getParametrosGenerales(int id)
        {
            List<mParametrosGenerales> lista = new List<mParametrosGenerales>();
            IQueryable<mParametrosGenerales> listaQueryable;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getParametrosGenerales", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ppadre", SqlDbType.Int).Value = id;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mParametrosGenerales objRes = new mParametrosGenerales();

                        objRes.idParametro = Convert.ToInt32(objReader["idParametro"]);

                        if (objReader["Parametro"] != DBNull.Value)
                            objRes.Parametro = objReader["Parametro"].ToString();

                        if (objReader["Descripcion"] != DBNull.Value)
                            objRes.Descripcion = objReader["Descripcion"].ToString();

                        if (objReader["idTipoParametro"] != DBNull.Value)
                            objRes.idTipoParametro = Convert.ToInt32(objReader["idTipoParametro"]);

                        lista.Add(objRes);
                    }
                }
            }

            listaQueryable = lista.AsQueryable();
            return listaQueryable;
        }
        /// <summary>
        /// Gets the parameter grales by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public mParametrosGenerales getParamGralesById(int id)
        {
            mParametrosGenerales objRes = new mParametrosGenerales();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getParametrosGeneralesById", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    SqlDataReader objReader = cmd.ExecuteReader();

                    while (objReader.Read())
                    {
                        objRes.idParametro = Convert.ToInt32(objReader["idParametro"]);

                        if (objReader["Parametro"] != DBNull.Value)
                        {
                            objRes.Parametro = objReader["Parametro"].ToString();
                        }

                        if (objReader["Descripcion"] != DBNull.Value)
                        {
                            objRes.Descripcion = objReader["Descripcion"].ToString();
                        }

                        if (objReader["idTipoParametro"] != DBNull.Value)
                        {
                            objRes.idTipoParametro = Convert.ToInt32(objReader["idTipoParametro"]);
                        }

                    }

                }
            }

            return objRes;
        }
        /// <summary>
        /// Deletes the parameter grales.
        /// </summary>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object[] delParamGrales(string idUsuario, int id)
        {
            bool Res = false;
            string msgError = "";
            object[] obj = new object[2];

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_delParamGrales", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idParametro", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                    cmd.Parameters.Add("@err_message", SqlDbType.VarChar).Value = "";
                    cmd.Parameters[2].Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        msgError = cmd.Parameters["@err_message"].Value.ToString();
                        Res = true;
                        obj[0] = Res;
                        obj[1] = msgError;

                    }
                    catch (Exception ex)
                    {
                        obj[0] = Res;
                        obj[1] = msgError;
                    }

                }
            }

            return obj;
        }

        #endregion

        #region Utilidades

        /// <summary>
        /// Ejecuta el query especificado.
        /// </summary>
        /// <param name="query">El query.</param>
        /// <param name="mensajeDeError">El mensaje de error.</param>
        /// <param name="tipo">El tipo.</param>
        /// <param name="lstparametros">La Lista de parametros.</param>
        /// <returns></returns>
        public bool Ejecutar(string query, ref string mensajeDeError, string tipo = "", List<mDataDdl> lstparametros = null)
        {

            bool Res = false;

            try
            {

                using (Conexion objConn = new Conexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, objConn.cnn))
                    {
                        switch (tipo)
                        {
                            case "":
                                cmd.CommandType = CommandType.Text;
                                break;
                            case "sp":
                                cmd.CommandType = CommandType.StoredProcedure;
                                break;
                        };

                        if (lstparametros != null)
                        {
                            for (int i = 0; i <= lstparametros.Count - 1; i++)
                            {
                                if (lstparametros[i].N_ID == "@err_message")
                                {
                                    cmd.Parameters.Add(new SqlParameter { ParameterName = lstparametros[i].N_ID, Value = lstparametros[i].N_VALOR, Size = 4000, Direction = ParameterDirection.Output });
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(lstparametros[i].N_ID, lstparametros[i].N_VALOR);
                                }
                            }
                        }

                        cmd.ExecuteNonQuery();

                        try
                        {

                            mensajeDeError = cmd.Parameters["@err_message"].Value.ToString();
                        }
                        catch (Exception ex)
                        {
                            mensajeDeError = "";
                        }
                        if (mensajeDeError == "")
                        {
                            Res = true;
                        }

                    }
                }

            }
            catch (SqlException exception)
            {
                Res = false;
                mensajeDeError = string.Format("{0}", exception.Message);
            }

            return Res;
        }





        #endregion

        #region Usuario

        public List<MembershipModel> get_all_users()
        {
            List<MembershipModel> lista = new List<MembershipModel>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllUsuarios", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        MembershipModel objRes = new MembershipModel();

                        if (objReader["Id"] != DBNull.Value)
                            objRes.User_Id = objReader["Id"].ToString();

                        if (objReader["UserName"] != DBNull.Value)
                            objRes.User_NickName = objReader["UserName"].ToString();

                        if (objReader["first_name"] != DBNull.Value)
                            objRes.User_Name = objReader["first_name"].ToString();

                        if (objReader["last_name"] != DBNull.Value)
                            objRes.User_LastName = objReader["last_name"].ToString();

                        if (objReader["Email"] != DBNull.Value)
                            objRes.User_Mail = objReader["Email"].ToString();

                        if (objReader["NombreDespacho"] != DBNull.Value)
                        {
                            objRes.UserDespacho = objReader["NombreDespacho"].ToString();
                        }
                        else
                        {
                            objRes.UserDespacho = "";
                        }


                        if (objReader["Role"] != DBNull.Value)
                            objRes.User_Role = objReader["Role"].ToString();

                        if (objReader["Enable"] != DBNull.Value)
                            objRes.User_Enable = objReader["Enable"].ToString();

                        lista.Add(objRes);
                    }
                }
            }

            return lista;
        }

        ///<summary>
        /// Metodo: Insert_usuario_rol_new : .
        /// <Author>Dennys Lopez</Author>
        /// <date>2016-11-11</date>
        ///</summary>
        ///<remarks>
        /// Metodo de Crear nueva usuario rol.
        ///</remarks>
        public bool Insert_usuario_rol_new(string idusuario, string idrol)
        {
            bool Res = false;
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@UserId", N_VALOR = idusuario });
            arrayparam.Add(new mDataDdl { N_ID = "@RoleId", N_VALOR = idrol });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = this.Ejecutar("sp_insUsuarioRol", ref mensajerror, "sp", arrayparam);

            if (mensajerror == "")
            {
                Res = true;
            }

            return Res;
        }

        public bool Insert_usuario_seccional(string idusuario, string idseccional)
        {
            bool Res = false;
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@UserId", N_VALOR = idusuario });
            arrayparam.Add(new mDataDdl { N_ID = "@SeccionalId", N_VALOR = idseccional });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = this.Ejecutar("sp_insUsuarioSecc", ref mensajerror, "sp", arrayparam);

            if (mensajerror == "")
            {
                Res = true;
            }

            return Res;
        }

        public bool Update_user_info(string uid, string usuario, string nombre, string apellido, string correo, string roles, string Despacho)
        {
            bool Res = false;
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@UserId", N_VALOR = uid });
            arrayparam.Add(new mDataDdl { N_ID = "@UserNickName", N_VALOR = usuario });
            arrayparam.Add(new mDataDdl { N_ID = "@UserName", N_VALOR = nombre });
            arrayparam.Add(new mDataDdl { N_ID = "@UserLastName", N_VALOR = apellido });
            arrayparam.Add(new mDataDdl { N_ID = "@Email", N_VALOR = correo });
            arrayparam.Add(new mDataDdl { N_ID = "@RoleId", N_VALOR = roles });
            arrayparam.Add(new mDataDdl { N_ID = "@DespachoId", N_VALOR = Despacho });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = this.Ejecutar("sp_updUsuario", ref mensajerror, "sp", arrayparam);

            if (mensajerror == "")
            {
                Res = true;
            }

            return Res;
        }

        public MembershipModel get_user_info(string userid)
        {
            MembershipModel objmembership = new MembershipModel();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getUsuarioById", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = userid;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        objmembership = new MembershipModel();
                        objmembership.User_Id = objReader["Id"].ToString();
                        objmembership.User_NickName = objReader["UserName"].ToString();
                        objmembership.User_Name = objReader["first_name"].ToString();
                        objmembership.User_LastName = objReader["last_name"].ToString();
                        objmembership.User_Mail = objReader["Email"].ToString();
                        objmembership.User_Role = objReader["RoleId"].ToString();
                        objmembership.UserDespacho = objReader["UserDespacho"].ToString();
                    }
                }
            }

            return objmembership;
        }

        public bool set_status_user(string userId, string est)
        {
            bool Res = false;
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@UserId", N_VALOR = userId });
            arrayparam.Add(new mDataDdl { N_ID = "@Enabled", N_VALOR = est });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = this.Ejecutar("sp_updUsuarioStatus", ref mensajerror, "sp", arrayparam);

            if (mensajerror == "")
            {
                Res = true;
            }

            return Res;
        }

        #endregion

        #region Menu

        /// <summary>
        /// Gets all menu.
        /// </summary>
        /// <param name="idRol">The identifier rol.</param>
        /// <returns></returns>
        public List<mMenu> get_all_menu(string idRol)
        {
            List<mMenu> lista = new List<mMenu>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllMenu", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idRol", SqlDbType.NVarChar).Value = idRol;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mMenu objRes = new mMenu();

                        if (objReader["idMenu"] != DBNull.Value)
                            objRes.idMenu = objReader["idMenu"].ToString();

                        if (objReader["Nombre"] != DBNull.Value)
                            objRes.Nombre = objReader["Nombre"].ToString();

                        if (objReader["Image"] != DBNull.Value)
                            objRes.Image = objReader["Image"].ToString();

                        if (objReader["rutaMenu"] != DBNull.Value)
                            objRes.rutaMenu = objReader["rutaMenu"].ToString();

                        if (objReader["idPadre"] != DBNull.Value)
                            objRes.idPadre = objReader["idPadre"].ToString();

                        if (objReader["Activo"] != DBNull.Value)
                            objRes.Activo = objReader["Activo"].ToString();

                        lista.Add(objRes);
                    }
                }
            }

            return lista;
        }



        #endregion

        #region Permisos

        /// <summary>
        /// Gets the permissions by role.
        /// </summary>
        /// <param name="idrol">The idrol.</param>
        /// <param name="idmenu">The idmenu.</param>
        /// <returns></returns>
        public List<PermissionModel> get_permissions_by_role(string idrol, int idmenu)
        {
            List<PermissionModel> lista = new List<PermissionModel>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getPermisosByRole", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PIdRole", SqlDbType.NVarChar).Value = idrol;
                    cmd.Parameters.Add("@PIdMenu", SqlDbType.Int).Value = idmenu;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        PermissionModel objRes = new PermissionModel();

                        if (objReader["idMenu"] != DBNull.Value)
                            objRes.Id = objReader["idMenu"].ToString();

                        if (objReader["Nombre"] != DBNull.Value)
                            objRes.Name = objReader["Nombre"].ToString();

                        if (objReader["Opciones"] != DBNull.Value)
                            objRes.Opciones = objReader["Opciones"].ToString();

                        if (objReader["Permisos"] != DBNull.Value)
                            objRes.Permisos = objReader["Permisos"].ToString();

                        if (objReader["Enable"] != DBNull.Value)
                            objRes.Enable = Convert.ToBoolean(objReader["Enable"]);

                        if (objReader["Orden"] != DBNull.Value)
                            objRes.Orden = Convert.ToInt32(objReader["Orden"]);

                        lista.Add(objRes);
                    }
                }
            }

            return lista;
        }

        ///<summary>
        /// Metodo: add_permissions_by_role : .
        /// <Author>Josser Ortega</Author>
        /// <date>2017-06-14</date>
        ///</summary>
        ///<remarks>
        /// Metodo de Asignar permisos al rol.
        ///</remarks>
        public bool add_permissions_by_role(string idRol, string Permisos)
        {
            bool Res = false;
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@idRol", N_VALOR = idRol });
            arrayparam.Add(new mDataDdl { N_ID = "@Permisos", N_VALOR = Permisos });

            var result = this.Ejecutar("sp_insPermisosByRole", ref mensajerror, "sp", arrayparam);

            if (mensajerror == "")
            {
                Res = true;
            }

            return Res;
        }

        #endregion

        #region Auditoría

        /// <summary>
        /// Gets the auditoria all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<mAuditoria> get_auditoria_all()
        {
            List<mAuditoria> lista = new List<mAuditoria>();
            IQueryable<mAuditoria> listaQueryable;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllAuditoria", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mAuditoria objRes = new mAuditoria();

                        if (objReader["idAuditoria"] != DBNull.Value)
                            objRes.IdAuditoria = Convert.ToInt32(objReader["idAuditoria"]);

                        if (objReader["idUsuario"] != DBNull.Value)
                            objRes.IdUsuario = objReader["idUsuario"].ToString();

                        if (objReader["tabla"] != DBNull.Value)
                            objRes.Tabla = objReader["tabla"].ToString();

                        if (objReader["idRegistro"] != DBNull.Value)
                            objRes.IdRegistro = objReader["idRegistro"].ToString();

                        if (objReader["fechaAccion"] != DBNull.Value)
                            objRes.FechaAccion = objReader["fechaAccion"].ToString();

                        if (objReader["accion"] != DBNull.Value)
                            objRes.Accion = objReader["accion"].ToString();

                        lista.Add(objRes);
                    }
                }
            }
            listaQueryable = lista.AsQueryable();

            return listaQueryable;
        }

        /// <summary>
        /// Gets the auditoria by identifier.
        /// </summary>
        /// <param name="idAuditoria">The identifier auditoria.</param>
        /// <returns></returns>
        public mAuditoria get_AuditoriaById(int idAuditoria)
        {
            mAuditoria objRes = new mAuditoria();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAuditoriaById", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idAuditoria", SqlDbType.Int).Value = idAuditoria;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        if (objReader["idAuditoria"] != DBNull.Value)
                            objRes.IdAuditoria = Convert.ToInt32(objReader["idAuditoria"]);

                        if (objReader["idUsuario"] != DBNull.Value)
                            objRes.IdUsuario = objReader["idUsuario"].ToString();

                        if (objReader["tabla"] != DBNull.Value)
                            objRes.Tabla = objReader["tabla"].ToString();

                        if (objReader["idRegistro"] != DBNull.Value)
                            objRes.IdRegistro = objReader["idRegistro"].ToString();

                        if (objReader["fechaAccion"] != DBNull.Value)
                            objRes.FechaAccion = objReader["fechaAccion"].ToString();

                        if (objReader["accion"] != DBNull.Value)
                            objRes.Accion = objReader["accion"].ToString();
                    }
                }
            }

            return objRes;
        }

        /// <summary>
        /// Gets the auditoria detalle by identifier.
        /// </summary>
        /// <param name="idAuditoria">The identifier auditoria.</param>
        /// <returns></returns>
        public IQueryable<mAuditoriaDetalle> get_auditoriaDetalleById(int idAuditoria)
        {
            List<mAuditoriaDetalle> lista = new List<mAuditoriaDetalle>();
            IQueryable<mAuditoriaDetalle> listaQueryable;

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAuditoriaDetalleById", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idAuditoria", SqlDbType.Int).Value = idAuditoria;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mAuditoriaDetalle objRes = new mAuditoriaDetalle();

                        if (objReader["campo"] != DBNull.Value)
                            objRes.Campo = objReader["campo"].ToString();

                        if (objReader["valorAnterior"] != DBNull.Value)
                            objRes.ValorAnterior = objReader["valorAnterior"].ToString();

                        if (objReader["valorNuevo"] != DBNull.Value)
                            objRes.ValorNuevo = objReader["valorNuevo"].ToString();

                        lista.Add(objRes);
                    }
                }
            }
            listaQueryable = lista.AsQueryable();

            return listaQueryable;
        }

        #endregion

        #region Secretaria
        public List<mSorteo> get_AllSorteosSeleccion()
        {
            List<mSorteo> sorteos = new List<mSorteo>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllSorteosSeleccion", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mSorteo sorteo = new mSorteo();

                        if (objReader["AÑO"] != DBNull.Value)
                            sorteo.Ano = Convert.ToInt32(objReader["AÑO"]);

                        if (objReader["MES"] != DBNull.Value)
                            sorteo.Mes = Convert.ToInt32(objReader["MES"]);

                        if (objReader["MES"] != DBNull.Value)
                            sorteo.MesString = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(sorteo.Mes);

                        if (objReader["DIA"] != DBNull.Value)
                            sorteo.Dia = Convert.ToInt32(objReader["DIA"]);

                        if (objReader["DESPACHO1"] != DBNull.Value)
                            sorteo.Despacho1 = objReader["DESPACHO1"].ToString();

                        if (objReader["DESPACHO2"] != DBNull.Value)
                            sorteo.Despacho2 = objReader["DESPACHO2"].ToString();

                        sorteos.Add(sorteo);
                    }
                }
            }

            return sorteos;
        }

        public string generarNuevoSorteoSeleccion()
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_sorteoDespachos", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("err_message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["err_message"].Value.ToString();
                }
            }
        }

        public List<mSorteoRevision> get_AllSorteosRevision()
        {
            List<mSorteoRevision> sorteos = new List<mSorteoRevision>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllSorteoRevision", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mSorteoRevision sorteo = new mSorteoRevision();

                        if (objReader["Id"] != DBNull.Value)
                            sorteo.Id = Convert.ToInt32(objReader["Id"]);

                        if (objReader["Año"] != DBNull.Value)
                            sorteo.Ano = Convert.ToInt32(objReader["Año"]);

                        if (objReader["Mes"] != DBNull.Value)
                            sorteo.Mes = Convert.ToInt32(objReader["Mes"]);

                        if (objReader["Mes"] != DBNull.Value)
                            sorteo.MesString = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(sorteo.Mes);

                        if (objReader["Dia"] != DBNull.Value)
                            sorteo.Dia = Convert.ToInt32(objReader["Dia"]);

                        if (objReader["IdDespacho"] != DBNull.Value)
                            sorteo.IdDespacho = Convert.ToInt32(objReader["IdDespacho"]);

                        if (objReader["Despacho"] != DBNull.Value)
                            sorteo.Despacho.NombreDespacho = objReader["Despacho"].ToString();

                        sorteos.Add(sorteo);
                    }

                    return sorteos;
                }
            }
        }

        public string ins_SorteoRevision(int despacho_1, int despacho_2, int despacho_3)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_insSorteoRevision", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("err_message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("despacho_1", despacho_1);
                    cmd.Parameters.AddWithValue("despacho_2", despacho_2);
                    cmd.Parameters.AddWithValue("despacho_3", despacho_3);

                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["err_message"].Value.ToString();
                }
            }
        }


        public string ins_LimiteProcessoRadicador(int cantidad)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_insLimitProcesosRadicador", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CantidadLimite", cantidad);
                    cmd.ExecuteNonQuery();
                }
            }
            return "Ok";
        }



        public List<mSorteo> ConsultarLimiteReparto()
        {

            List<mSorteo> lista = new List<mSorteo>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetALlLimiteProcesos", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mSorteo Limite = new mSorteo();


                        if (objReader["CantidadLimite"] != DBNull.Value)
                            Limite.CantidadLimite = Convert.ToInt32(objReader["CantidadLimite"]);

                        if (objReader["FechaModificacion"] != DBNull.Value)
                            Limite.FechaModificacion = objReader["FechaModificacion"].ToString();



                        lista.Add(Limite);
                    }


                }
            }
            return lista;
        }


        public string ins_SorteoSeleccionManual(int despacho_1, int despacho_2)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_insSorteoSeleccionManual", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("err_message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("despacho_1", despacho_1);
                    cmd.Parameters.AddWithValue("despacho_2", despacho_2);
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters["err_message"].Value.ToString();
                }
            }
        }
        #endregion

        #region Despachos
        public List<mDespacho> get_AllDespachos()
        {
            List<mDespacho> despachos = new List<mDespacho>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllDespacho", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mDespacho despacho = new mDespacho();

                        if (objReader["Id"] != DBNull.Value)
                            despacho.Id = Convert.ToInt32(objReader["Id"]);

                        if (objReader["NombreDespacho"] != DBNull.Value)
                            despacho.NombreDespacho = objReader["NombreDespacho"].ToString();

                        if (objReader["DescripcionDespacho"] != DBNull.Value)
                            despacho.DescripcionDespacho = objReader["DescripcionDespacho"].ToString();

                        if (objReader["Activo"] != DBNull.Value)
                            despacho.Activo = Convert.ToBoolean(objReader["Activo"]);

                        despachos.Add(despacho);
                    }

                    return despachos;
                }
            }
        }
        #endregion

        #region getAllConsulta()


        public List<mConsulta> recibeAdCoordinador(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();
            mConsulta consultaObj = new mConsulta();

            //Estas son las columnas que va a mostrar para el Coordinador Despacho
            if (dt.Columns.Count == 7)
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    if (objReader["Nombre"] != DBNull.Value)
                    {
                        consultaObj.Nombre = (objReader["Nombre"].ToString());
                    }
                    if (objReader["Id"] != DBNull.Value)
                    {
                        consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            return listaConsulta;
        }

        public List<mConsulta> recibeVarios(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();

            //Estas son las columnas que va a mostrar para el Coordinador Despacho
            if (dt.Columns.Count == 7)
            {
                while (objReader.Read())
                {
                    mConsulta consultaObj = new mConsulta();

                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    if (objReader["Nombre"] != DBNull.Value)
                    {
                        consultaObj.Nombre = (objReader["Nombre"].ToString());
                    }
                    if (objReader["Id"] != DBNull.Value)
                    {
                        consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            return listaConsulta;
        }
        public List<mConsulta> recibeAdCoordinadorSeleccion(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();
            mConsulta consultaObj = new mConsulta();

            //Estas son las columnas que va a mostrar para el Coordinador Despacho
            if (dt.Columns.Count == 4)
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["NumeroProceso"] != DBNull.Value)
                    {
                        consultaObj.NumeroProceso = (objReader["NumeroProceso"].ToString());
                    }
                    if (objReader["Nombre"] != DBNull.Value)
                    {
                        consultaObj.Nombre = (objReader["Nombre"].ToString());
                    }
                    if (objReader["Id"] != DBNull.Value)
                    {
                        consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            return listaConsulta;
        }

        public List<mConsulta> recibeRadicador(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();

            if (dt.Columns.Count == 6)
            {
                while (objReader.Read())
                {
                    mConsulta consultaObj = new mConsulta();
                    if (objReader["IdReparto"] != DBNull.Value)
                    {
                        consultaObj.IdReparto = Convert.ToInt32(objReader["IdReparto"]);
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    if (objReader["Activo"] != DBNull.Value)
                    {
                        consultaObj.Activo = Convert.ToInt32(objReader["Activo"]);
                    }
                    listaConsulta.Add(consultaObj);
                }

            }


            return listaConsulta;
        }

        public List<mConsulta> recibeAdHonorem(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();
            mConsulta consultaObj = new mConsulta();
            if (dt.Columns.Count == 6)
            {
                while (objReader.Read())
                {
                    if (objReader["IdReparto"] != DBNull.Value)
                    {
                        consultaObj.IdReparto = Convert.ToInt32(objReader["IdReparto"]);
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    //if (objReader["Activo"] != DBNull.Value)
                    //{
                    //    consultaObj.Activo = Convert.ToInt32(objReader["Activo"]);
                    //}
                    listaConsulta.Add(consultaObj);
                }

            }
            //Estas son las columnas que va a mostrar para el Coordinador Despacho
            if (dt.Columns.Count == 7)
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    if (objReader["Nombre"] != DBNull.Value)
                    {
                        consultaObj.Nombre = (objReader["Nombre"].ToString());
                    }
                    if (objReader["Id"] != DBNull.Value)
                    {
                        consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }

            if (dt.Columns.Count == 5)
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            else
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = objReader["NumeroRadicadoUnico"].ToString();
                    }
                    if (objReader["NumeroProceso"] != DBNull.Value)
                    {
                        consultaObj.NumeroProceso = objReader["NumeroProceso"].ToString();
                    }
                    if (objReader["FechaRadicado"] != DBNull.Value)
                    {
                        consultaObj.FechaRadicado = Convert.ToDateTime(objReader["FechaRadicado"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            return listaConsulta;
        }

        public List<mConsulta> recibeTutor(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();
            mConsulta consultaObj = new mConsulta();
            if (dt.Columns.Count == 6)
            {
                while (objReader.Read())
                {
                    if (objReader["IdReparto"] != DBNull.Value)
                    {
                        consultaObj.IdReparto = Convert.ToInt32(objReader["IdReparto"]);
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    //if (objReader["Activo"] != DBNull.Value)
                    //{
                    //    consultaObj.Activo = Convert.ToInt32(objReader["Activo"]);
                    //}
                    listaConsulta.Add(consultaObj);
                }

            }
            //Estas son las columnas que va a mostrar para el Coordinador Despacho
            if (dt.Columns.Count == 7)
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    if (objReader["Nombre"] != DBNull.Value)
                    {
                        consultaObj.Nombre = (objReader["Nombre"].ToString());
                    }
                    if (objReader["Id"] != DBNull.Value)
                    {
                        consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }

            if (dt.Columns.Count == 5)
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                    }
                    if (objReader["IdUsuario"] != DBNull.Value)
                    {
                        consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                    }
                    if (objReader["ProcesoRadicado"] != DBNull.Value)
                    {
                        consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                    }
                    if (objReader["Fecha"] != DBNull.Value)
                    {
                        consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                    }
                    if (objReader["TipoReparto"] != DBNull.Value)
                    {
                        consultaObj.TipoReparto = Convert.ToInt32(objReader["TipoReparto"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            else
            {
                while (objReader.Read())
                {
                    //mConsulta consultaObj = new mConsulta();
                    if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                    {
                        consultaObj.NumeroRadicadoUnico = objReader["NumeroRadicadoUnico"].ToString();
                    }
                    if (objReader["NumeroProceso"] != DBNull.Value)
                    {
                        consultaObj.NumeroProceso = objReader["NumeroProceso"].ToString();
                    }
                    if (objReader["FechaRadicado"] != DBNull.Value)
                    {
                        consultaObj.FechaRadicado = Convert.ToDateTime(objReader["FechaRadicado"]);
                    }
                    listaConsulta.Add(consultaObj);
                }
            }
            return listaConsulta;
        }





        public List<mConsulta> recibeAdSecretario(DataTable dt, SqlDataReader objReader)
        {
            List<mConsulta> listaConsulta = new List<mConsulta>();


            //Estas son las columnas que va a mostrar para el Secretario Despacho

            while (objReader.Read())
            {
                mConsulta consultaObj = new mConsulta();
                //mConsulta consultaObj = new mConsulta();
                if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                {
                    consultaObj.NumeroRadicadoUnico = (objReader["NumeroRadicadoUnico"].ToString());
                }
                if (objReader["ProcesoRadicado"] != DBNull.Value)
                {
                    consultaObj.ProcesoRadicado = objReader["ProcesoRadicado"].ToString();
                }
                if (objReader["Fecha"] != DBNull.Value)
                {
                    consultaObj.Fecha = Convert.ToDateTime(objReader["Fecha"]);
                }

                if (objReader["Nombre"] != DBNull.Value)
                {
                    consultaObj.Nombre = (objReader["Nombre"].ToString());
                }
                if (objReader["IdEstado"] != DBNull.Value)
                {
                    consultaObj.IdEstado = Convert.ToInt32(objReader["IdEstado"]);
                }
                if (objReader["AdjuntoInsistencia"] != DBNull.Value)
                {
                    consultaObj.AdjuntoInsistencia = objReader["AdjuntoInsistencia"].ToString();
                }
                if (objReader["AdjuntoEscrito"] != DBNull.Value)
                {
                    consultaObj.AdjuntoEscrito = objReader["AdjuntoEscrito"].ToString();
                }
                listaConsulta.Add(consultaObj);
            }

            return listaConsulta;
        }

        public List<mConsulta> getAllConsultaSecretarioIdEstadoNoSel(string IdRol)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> listaConsulta = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllNoSel", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();


                    if (IdRol == "ConsultaEscritosEInsistencias")
                    {
                        return recibeAdSecretario(dt, objReader);
                    }
                    return recibeVarios(dt, objReader);
                }
            }
        }


        public List<mConsulta> getAllConsultaSecretarioIdEstado(string IdRol, string IdUsuario, int IdEstado)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> listaConsulta = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getConsultaNumProcesoNumRadicado", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Rol", IdRol);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("@IdEstado", IdEstado);
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();
                    //Radicador
                    if (IdRol == "bc600e11-ba76-4729-bc36-35eab571fee2")
                    {
                        return recibeRadicador(dt, objReader);
                    }
                    //Adhonorem
                    if (IdRol == "4b365b55-eb91-4437-af19-5f00dd9bf55b")
                    {
                        return recibeAdHonorem(dt, objReader);
                    }
                    //Tutor
                    if (IdRol == "b76d800c-f34d-43a2-a583-549317968a4e")
                    {
                        return recibeTutor(dt, objReader);
                    }
                    //Coordinador despacho
                    if (IdRol == "9e42ad3f-f449-4582-9b37-37343dfa5596")
                    {
                        return recibeAdCoordinador(dt, objReader);
                    }
                    //Coodinador revision 
                    if (IdRol == "8cbd9e16-5dcf-4ebd-a2b4-7011fac4ed25")
                    {
                        return recibeAdCoordinador(dt, objReader);
                    }
                    //Coodinador Seleccion 
                    if (IdRol == "cb06f9d4-13d6-4341-8b69-7ec6199c85ac")
                    {
                        return recibeAdCoordinadorSeleccion(dt, objReader);
                    }
                    //Secretario
                    if (IdRol == "dc9a678b-f9c8-4567-b4bc-a6ef71f59483")
                    {
                        return recibeAdSecretario(dt, objReader);
                    }
                    if (IdRol == "ConsultaEscritosEInsistencias")
                    {
                        return recibeAdSecretario(dt, objReader);
                    }
                    return recibeVarios(dt, objReader);
                }
            }
        }

        public List<mConsulta> getAllConsulta(string IdRol, string IdUsuario)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> listaConsulta = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getConsultaNumProcesoNumRadicado", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Rol", IdRol);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();
                    //Radicador
                    if (IdRol == "bc600e11-ba76-4729-bc36-35eab571fee2")
                    {
                        return recibeRadicador(dt, objReader);
                    }
                    //Adhonorem
                    if (IdRol == "4b365b55-eb91-4437-af19-5f00dd9bf55b")
                    {
                        return recibeAdHonorem(dt, objReader);
                    }
                    //Tutor
                    if (IdRol == "b76d800c-f34d-43a2-a583-549317968a4e")
                    {
                        return recibeTutor(dt, objReader);
                    }
                    //Coordinador despacho
                    if (IdRol == "9e42ad3f-f449-4582-9b37-37343dfa5596")
                    {
                        return recibeAdCoordinador(dt, objReader);
                    }
                    //Coodinador revision 
                    if (IdRol == "8cbd9e16-5dcf-4ebd-a2b4-7011fac4ed25")
                    {
                        return recibeAdCoordinador(dt, objReader);
                    }
                    //Coodinador Seleccion 
                    if (IdRol == "cb06f9d4-13d6-4341-8b69-7ec6199c85ac")
                    {
                        return recibeAdCoordinadorSeleccion(dt, objReader);
                    }
                    //Secretario
                    if (IdRol == "dc9a678b-f9c8-4567-b4bc-a6ef71f59483")
                    {
                        return recibeAdSecretario(dt, objReader);
                    }
                    if (IdRol == "ConsultaEscritosEInsistencias")
                    {
                        return recibeAdSecretario(dt, objReader);
                    }
                    return recibeVarios(dt, objReader);
                }
            }
        }

        public int validDespachoSeleccion(int IdDespacho)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_validDespachoSeleccion", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdDespacho", SqlDbType.Int).Value = IdDespacho;
                    //cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    //SqlDataReader objReader = cmd.ExecuteReader();

                    return Convert.ToInt32(dt.Rows[0][0]);
                }
            }
        }

        public int validDespachosRevision(int IdDespacho)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidDespachoRevision", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdDespacho", SqlDbType.Int).Value = IdDespacho;
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);

                    return Convert.ToInt32(dt.Rows[0][0]);
                }
            }
        }

        public string ins_FalloRadicado(string idRadicado, string path)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_insFalloRadicado", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idRadicado", idRadicado);
                    cmd.Parameters.AddWithValue("@rutaArchivo", path);
                    cmd.Parameters.Add("@err_message", SqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["@err_message"].Value.ToString();
                }
            }
        }

        public List<string> getProcesosPreSeleccionados(int IdDespacho)
        {
            List<string> listaConsulta = new List<string>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getProcesosPreSeleccionados", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdDespacho", SqlDbType.VarChar).Value = IdDespacho;
                    //cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    //SqlDataReader objReader = cmd.ExecuteReader();

                    foreach (DataRow dr in dt.Rows)
                    {
                        string a = "";
                        if (dr["NumeroProceso"] != DBNull.Value)
                            a = dr["NumeroProceso"].ToString();
                        listaConsulta.Add(a);
                    }

                    return listaConsulta;
                }
            }
        }

        public List<mDataDdl> getUsuariosSeleccion(int IdDespacho)
        {
            List<mDataDdl> listaConsulta = new List<mDataDdl>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getUsersSeleccionByDespacho", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdDespacho", SqlDbType.VarChar).Value = IdDespacho;
                    cmd.ExecuteNonQuery();
                    SqlDataReader objReader = cmd.ExecuteReader();
                    //Radicador
                    while (objReader.Read())
                    {

                        mDataDdl objRes = new mDataDdl();
                        if (objReader["Id"] != DBNull.Value)
                            objRes.N_ID = objReader["Id"].ToString();

                        if (objReader["UserName"] != DBNull.Value)
                            objRes.N_VALOR = objReader["UserName"].ToString();

                        listaConsulta.Add(objRes);

                    }
                }
            }

            return listaConsulta;
        }

        public List<mConsulta> getRepartoAdHonorem(List<mPorcentajes> Lista, int IdDespacho)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> listaConsulta = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("[sp_insPorcentaje]", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (mPorcentajes lista in Lista)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@p_porcentaje", lista.valor);
                        cmd.Parameters.AddWithValue("@p_idUsuario", lista.indice);
                        cmd.Parameters.Add("@err_message", SqlDbType.Char, 4000);
                        cmd.Parameters["@err_message"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                    return listaConsulta;
                }
            }
        }

        public List<mConsulta> getRepartoTutores(List<mPorcentajes> Lista, int IdDespacho)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> listaConsulta = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {

                using (SqlCommand cmd = new SqlCommand("[sp_insAsociarTutor]", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (mPorcentajes lista in Lista)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tutor", lista.tutor);
                        cmd.Parameters.AddWithValue("@adHonorem", lista.adHonorem);
                        //cmd.Parameters.Add("@err_message", SqlDbType.Char, 4000);
                        //cmd.Parameters["@err_message"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    return listaConsulta;
                    //var dato = Lista[1];
                    //string nombre = dato.Adhonorem;

                }
            }
        }

        public void RepartoTutores(int IdDespacho)
        {

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("[sp_insRepartoTutores]", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_IdDespacho", IdDespacho);
                    cmd.Parameters.Add("@err_message", SqlDbType.Char, 4000);
                    cmd.Parameters["@err_message"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RepartoHonorem(int IdDespacho)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("[sp_insRepartoAdHonorem]", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_IdDespacho", IdDespacho);
                    cmd.Parameters.Add("@err_message", SqlDbType.Char, 4000);
                    cmd.Parameters["@err_message"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<mConsulta> GetAllAdhonoremTutorsByIdDespacho(int ConsultaUsuarioParaReparto, int IdDespacho)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> ListaRadicados = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getConsultaNumProcesoNumRadicado", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConsultarUsuariosParaReparto", ConsultaUsuarioParaReparto);
                    cmd.Parameters.AddWithValue("@IdUsuario", "0e4f7323-cbfe-45e1-ad1d-6dfccf2c5abb");
                    cmd.Parameters.AddWithValue("@UserDespacho", IdDespacho);
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mConsulta consultaObj = new mConsulta();
                        if (objReader["IdRol"] != DBNull.Value)
                        {
                            consultaObj.IdRol = objReader["IdRol"].ToString();
                        }
                        if (objReader["first_name"] != DBNull.Value)
                        {
                            consultaObj.first_name = objReader["first_name"].ToString();
                        }
                        if (objReader["IdUsuario"] != DBNull.Value)
                        {
                            consultaObj.IdUsuario = objReader["IdUsuario"].ToString();
                        }
                        ListaRadicados.Add(consultaObj);
                    }
                }
                return ListaRadicados;
            }
        }



        public List<mConsulta> ConsultaAdHonorem()
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> ListaRadicados = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("[sp_getAllDDLEstado]", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mConsulta consultaObj = new mConsulta();
                        if (objReader["Id"] != DBNull.Value)
                        {
                            consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                        }
                        if (objReader["Nombre"] != DBNull.Value)
                        {
                            consultaObj.Nombre = objReader["Nombre"].ToString();
                        }
                        if (objReader["Activo"] != DBNull.Value)
                        {
                            consultaObj.Activo = Convert.ToInt32(objReader["Activo"]);
                        }
                        ListaRadicados.Add(consultaObj);
                    }
                }
                return ListaRadicados;
            }
        }




        public List<mConsulta> GetAllDDL()
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> ListaRadicados = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("[sp_getAllDDLEstado]", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mConsulta consultaObj = new mConsulta();
                        if (objReader["Id"] != DBNull.Value)
                        {
                            consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                        }
                        if (objReader["Nombre"] != DBNull.Value)
                        {
                            consultaObj.Nombre = objReader["Nombre"].ToString();
                        }
                        if (objReader["Activo"] != DBNull.Value)
                        {
                            consultaObj.Activo = Convert.ToInt32(objReader["Activo"]);
                        }
                        ListaRadicados.Add(consultaObj);
                    }
                }
                return ListaRadicados;
            }
        }

        #endregion

        #region Despachos

        public List<mDespacho> getAllDespacho()
        {
            List<mDespacho> listaDespacho = new List<mDespacho>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getAllDespacho", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    SqlDataReader objReader = cmd.ExecuteReader();

                    while (objReader.Read())
                    {
                        mDespacho DespachoObj = new mDespacho();
                        if (objReader["Id"] != DBNull.Value)
                        {
                            DespachoObj.Id = Convert.ToInt32(objReader["Id"].ToString());
                        }
                        if (objReader["NombreDespacho"] != DBNull.Value)
                        {
                            DespachoObj.NombreDespacho = objReader["NombreDespacho"].ToString();
                        }
                        if (objReader["DescripcionDespacho"] != DBNull.Value)
                        {
                            DespachoObj.DescripcionDespacho = objReader["DescripcionDespacho"].ToString();
                        }
                        if (objReader["Activo"] != DBNull.Value)
                        {
                            DespachoObj.Activo = Convert.ToBoolean(objReader["Activo"]);
                        }
                        listaDespacho.Add(DespachoObj);
                    }
                }
                return listaDespacho;
            }
        }

        public mDespacho getDespachoById(int idDespacho)
        {
            mDespacho despacho = new mDespacho();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getDespachoById", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("p_id", idDespacho);
                    SqlDataReader objReader = cmd.ExecuteReader();

                    while (objReader.Read())
                    {
                        if (objReader["Id"] != DBNull.Value)
                        {
                            despacho.Id = Convert.ToInt32(objReader["Id"].ToString());
                        }
                        if (objReader["NombreDespacho"] != DBNull.Value)
                        {
                            despacho.NombreDespacho = objReader["NombreDespacho"].ToString();
                        }
                        if (objReader["DescripcionDespacho"] != DBNull.Value)
                        {
                            despacho.DescripcionDespacho = objReader["DescripcionDespacho"].ToString();
                        }
                        if (objReader["Activo"] != DBNull.Value)
                        {
                            despacho.Activo = Convert.ToBoolean(objReader["Activo"]);
                        }
                    }
                }
                return despacho;
            }
        }
        #endregion

        #region Resena
        //public mResena GetResenaByNumProceso(string numProceso)
        //{
        //    mResena resena = new mResena();
        //    using (Conexion objConn = new Conexion())
        //    {
        //        using (SqlCommand cmd = new SqlCommand("sp_getResenaByNumProceso", objConn.cnn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Clear();
        //            cmd.Parameters.AddWithValue("NumeroProceso", numProceso);
        //            SqlDataReader objReader = cmd.ExecuteReader();

        //            while (objReader.Read())
        //            {
        //                if (objReader["FechaRadicado"] != DBNull.Value)
        //                {
        //                    resena.FechaRadicado = objReader["FechaRadicado"].ToString().ToString();
        //                }
        //                if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
        //                {
        //                    resena.Expediente = objReader["NumeroRadicadoUnico"].ToString().ToString();
        //                }
        //                if (objReader["OrientacionSexual"] != DBNull.Value)
        //                {
        //                    resena.OrientacionSexual = objReader["OrientacionSexual"].ToString().ToString();
        //                }
        //                if (objReader["ObservacionesImpugnacion"] != DBNull.Value)
        //                {
        //                    resena.ObservacionesImpugnacion = objReader["ObservacionesImpugnacion"].ToString().ToString();
        //                }
        //                if (objReader["Id"] != DBNull.Value)
        //                {
        //                    resena.Id = objReader["Id"].ToString().ToString();
        //                }
        //                if (objReader["IdRadicado"] != DBNull.Value)
        //                {
        //                    resena.IdRadicado = objReader["IdRadicado"].ToString();
        //                }
        //                if (objReader["Demandado"] != DBNull.Value)
        //                {
        //                    resena.Demandado = objReader["Demandado"].ToString();
        //                }
        //                if (objReader["Demandante"] != DBNull.Value)
        //                {
        //                    resena.Demandante = objReader["Demandante"].ToString();
        //                }
        //                if (objReader["Legitimacion"] != DBNull.Value)
        //                {
        //                    resena.Legitimacion = objReader["Legitimacion"].ToString();
        //                }
        //                if (objReader["Despacho1raInstancia"] != DBNull.Value)
        //                {
        //                    resena.Despacho1raInstancia = objReader["Despacho1raInstancia"].ToString();
        //                }
        //                if (objReader["Despacho2daInstancia"] != DBNull.Value)
        //                {
        //                    resena.Despacho2daInstancia = objReader["Despacho2daInstancia"].ToString();
        //                }
        //                if (objReader["DerechosVulnerados"] != DBNull.Value)
        //                {
        //                    resena.DerechosVulnerados = objReader["DerechosVulnerados"].ToString();
        //                }
        //                if (objReader["Pretensiones"] != DBNull.Value)
        //                {
        //                    resena.Pretensiones = objReader["Pretensiones"].ToString();
        //                }
        //                if (objReader["Hechos"] != DBNull.Value)
        //                {
        //                    resena.Hechos = objReader["Hechos"].ToString();
        //                }
        //                if (objReader["PrimeraInstancia"] != DBNull.Value)
        //                {
        //                    resena.PrimeraInstancia = objReader["PrimeraInstancia"].ToString();
        //                }
        //                if (objReader["Observaciones1raInstancia"] != DBNull.Value)
        //                {
        //                    resena.Observaciones1raInstancia = objReader["Observaciones1raInstancia"].ToString();
        //                }
        //                if (objReader["SegundaInstancia"] != DBNull.Value)
        //                {
        //                    resena.SegundaInstancia = objReader["SegundaInstancia"].ToString();
        //                }
        //                if (objReader["Observaciones2daInstancia"] != DBNull.Value)
        //                {
        //                    resena.Observaciones2daInstancia = objReader["Observaciones2daInstancia"].ToString();
        //                }
        //                if (objReader["Impugnacion"] != DBNull.Value)
        //                {
        //                    resena.Impugnacion = objReader["Impugnacion"].ToString();
        //                }
        //                if (objReader["Especialproteccion"] != DBNull.Value)
        //                {
        //                    resena.Especialproteccion = objReader["Especialproteccion"].ToString();
        //                }
        //                if (objReader["TipoSujetoEspecialproteccion"] != DBNull.Value)
        //                {
        //                    resena.TipoSujetoEspecialproteccion = objReader["TipoSujetoEspecialproteccion"].ToString();
        //                }
        //                if (objReader["OrigenSujeto"] != DBNull.Value)
        //                {
        //                    resena.OrigenSujeto = objReader["OrigenSujeto"].ToString();
        //                }
        //                if (objReader["ExtranjeroSujeto"] != DBNull.Value)
        //                {
        //                    resena.ExtranjeroSujeto = objReader["ExtranjeroSujeto"].ToString();
        //                }
        //                if (objReader["CondicionPersonalSujeto"] != DBNull.Value)
        //                {
        //                    resena.CondicionPersonalSujeto = objReader["CondicionPersonalSujeto"].ToString();
        //                }
        //                if (objReader["CondicionDiscapacidadSujeto"] != DBNull.Value)
        //                {
        //                    resena.CondicionDiscapacidadSujeto = objReader["CondicionDiscapacidadSujeto"].ToString();
        //                }
        //                if (objReader["CondicionSocialSujeto"] != DBNull.Value)
        //                {
        //                    resena.CondicionSocialSujeto = objReader["CondicionSocialSujeto"].ToString();
        //                }
        //                if (objReader["RestituciondeTierrasSujeto"] != DBNull.Value)
        //                {
        //                    resena.RestitucionDeTierrasSujeto = objReader["RestituciondeTierrasSujeto"].ToString();
        //                }
        //                if (objReader["CriteriosObjetivos"] != DBNull.Value)
        //                {
        //                    resena.CriteriosObjetivos = objReader["CriteriosObjetivos"].ToString();
        //                }
        //                if (objReader["CriteriosSubjetivos"] != DBNull.Value)
        //                {
        //                    resena.CriteriosSubjetivos = objReader["CriteriosSubjetivos"].ToString();
        //                }
        //                if (objReader["CriteriosComplementarios"] != DBNull.Value)
        //                {
        //                    resena.CriteriosComplementarios = objReader["CriteriosComplementarios"].ToString();
        //                }
        //                if (objReader["CriteriosAdicionales"] != DBNull.Value)
        //                {
        //                    resena.CriteriosAdicionales = objReader["CriteriosAdicionales"].ToString();
        //                }
        //                if (objReader["ProblemaJuridico"] != DBNull.Value)
        //                {
        //                    resena.ProblemaJuridico = objReader["ProblemaJuridico"].ToString();
        //                }
        //                if (objReader["ObservacionesProblemaJuridico"] != DBNull.Value)
        //                {
        //                    resena.ObservacionesProblemaJuridico = objReader["ObservacionesProblemaJuridico"].ToString();
        //                }
        //                if (objReader["FechaResena"] != DBNull.Value)
        //                {
        //                    resena.FechaResena = objReader["FechaResena"].ToString();
        //                }
        //            }
        //        }
        //        return resena;
        //    }
        //}
        #endregion

        #region Resena
        public mResena GetResenaByNumProceso(string numProceso)
        {
            mResena resena = new mResena();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getResenaByNumProceso", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NumeroProceso", numProceso);
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();

                    while (objReader.Read())
                    {
                        if (objReader["FechaRadicado"] != DBNull.Value)
                        {
                            resena.FechaRadicado = objReader["FechaRadicado"].ToString().ToString();
                        }
                        if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                        {
                            resena.Expediente = objReader["NumeroRadicadoUnico"].ToString().ToString();
                        }
                        if (objReader["OrientacionSexual"] != DBNull.Value)
                        {
                            resena.OrientacionSexual = objReader["OrientacionSexual"].ToString().ToString();
                        }
                        if (objReader["ObservacionesImpugnacion"] != DBNull.Value)
                        {
                            resena.ObservacionesImpugnacion = objReader["ObservacionesImpugnacion"].ToString().ToString();
                        }
                        if (objReader["Id"] != DBNull.Value)
                        {
                            resena.Id = objReader["Id"].ToString().ToString();
                        }
                        if (objReader["IdRadicado"] != DBNull.Value)
                        {
                            resena.IdRadicado = objReader["IdRadicado"].ToString();
                        }
                        if (objReader["Demandado"] != DBNull.Value)
                        {
                            resena.Demandado = objReader["Demandado"].ToString();
                        }
                        if (objReader["Demandante"] != DBNull.Value)
                        {
                            resena.Demandante = objReader["Demandante"].ToString();
                        }
                        if (objReader["Legitimacion"] != DBNull.Value)
                        {
                            resena.Legitimacion = objReader["Legitimacion"].ToString();
                        }
                        if (objReader["Despacho1raInstancia"] != DBNull.Value)
                        {
                            resena.Despacho1raInstancia = objReader["Despacho1raInstancia"].ToString();
                        }
                        if (objReader["Despacho2daInstancia"] != DBNull.Value)
                        {
                            resena.Despacho2daInstancia = objReader["Despacho2daInstancia"].ToString();
                        }
                        if (objReader["DerechosVulnerados"] != DBNull.Value)
                        {
                            resena.DerechosVulnerados = objReader["DerechosVulnerados"].ToString();
                        }
                        if (objReader["Pretensiones"] != DBNull.Value)
                        {
                            resena.Pretensiones = objReader["Pretensiones"].ToString();
                        }
                        if (objReader["Hechos"] != DBNull.Value)
                        {
                            resena.Hechos = objReader["Hechos"].ToString();
                        }
                        if (objReader["PrimeraInstancia"] != DBNull.Value)
                        {
                            resena.PrimeraInstancia = objReader["PrimeraInstancia"].ToString();
                        }
                        if (objReader["Observaciones1raInstancia"] != DBNull.Value)
                        {
                            resena.Observaciones1raInstancia = objReader["Observaciones1raInstancia"].ToString();
                        }
                        if (objReader["SegundaInstancia"] != DBNull.Value)
                        {
                            resena.SegundaInstancia = objReader["SegundaInstancia"].ToString();
                        }
                        if (objReader["Observaciones2daInstancia"] != DBNull.Value)
                        {
                            resena.Observaciones2daInstancia = objReader["Observaciones2daInstancia"].ToString();
                        }
                        if (objReader["Impugnacion"] != DBNull.Value)
                        {
                            resena.Impugnacion = objReader["Impugnacion"].ToString();
                        }
                        if (objReader["Especialproteccion"] != DBNull.Value)
                        {
                            resena.Especialproteccion = objReader["Especialproteccion"].ToString();
                        }
                        if (objReader["TipoSujetoEspecialproteccion"] != DBNull.Value)
                        {
                            resena.TipoSujetoEspecialproteccion = objReader["TipoSujetoEspecialproteccion"].ToString();
                        }
                        if (objReader["OrigenSujeto"] != DBNull.Value)
                        {
                            resena.OrigenSujeto = objReader["OrigenSujeto"].ToString();
                        }
                        if (objReader["ExtranjeroSujeto"] != DBNull.Value)
                        {
                            resena.ExtranjeroSujeto = objReader["ExtranjeroSujeto"].ToString();
                        }
                        if (objReader["CondicionPersonalSujeto"] != DBNull.Value)
                        {
                            resena.CondicionPersonalSujeto = objReader["CondicionPersonalSujeto"].ToString();
                        }
                        if (objReader["CondicionDiscapacidadSujeto"] != DBNull.Value)
                        {
                            resena.CondicionDiscapacidadSujeto = objReader["CondicionDiscapacidadSujeto"].ToString();
                        }
                        if (objReader["CondicionSocialSujeto"] != DBNull.Value)
                        {
                            resena.CondicionSocialSujeto = objReader["CondicionSocialSujeto"].ToString();
                        }
                        if (objReader["RestituciondeTierrasSujeto"] != DBNull.Value)
                        {
                            resena.RestitucionDeTierrasSujeto = objReader["RestituciondeTierrasSujeto"].ToString();
                        }
                        if (objReader["CriteriosObjetivos"] != DBNull.Value)
                        {
                            resena.CriteriosObjetivos = objReader["CriteriosObjetivos"].ToString();
                        }
                        if (objReader["CriteriosSubjetivos"] != DBNull.Value)
                        {
                            resena.CriteriosSubjetivos = objReader["CriteriosSubjetivos"].ToString();
                        }
                        if (objReader["CriteriosComplementarios"] != DBNull.Value)
                        {
                            resena.CriteriosComplementarios = objReader["CriteriosComplementarios"].ToString();
                        }
                        if (objReader["CriteriosAdicionales"] != DBNull.Value)
                        {
                            resena.CriteriosAdicionales = objReader["CriteriosAdicionales"].ToString();
                        }
                        if (objReader["ProblemaJuridico"] != DBNull.Value)
                        {
                            resena.ProblemaJuridico = objReader["ProblemaJuridico"].ToString();
                        }
                        if (objReader["ObservacionesProblemaJuridico"] != DBNull.Value)
                        {
                            resena.ObservacionesProblemaJuridico = objReader["ObservacionesProblemaJuridico"].ToString();
                        }
                        if (objReader["FechaResena"] != DBNull.Value)
                        {
                            resena.FechaResena = objReader["FechaResena"].ToString();
                        }
                    }
                }
                return resena;
            }
        }
        #endregion

        #region Estado
        public mEstado GetEstadoByNumProceso(string numProceso)
        {
            mEstado estado = new mEstado();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getEstadoByNumProceso", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NumeroProceso", numProceso);
                    SqlDataReader objReader = cmd.ExecuteReader();

                    while (objReader.Read())
                    {
                        if (objReader["IdEstado"] != DBNull.Value)
                        {
                            estado.Id = Convert.ToInt32(objReader["IdEstado"]);
                        }
                        if (objReader["Nombre"] != DBNull.Value)
                        {
                            estado.Nombre = objReader["Nombre"].ToString();
                        }
                    }
                }
                return estado;
            }
        }
        #endregion

        #region Guardar Archivo Insisntecias

        public string InsEscritos(string Path, string NumProceso, int NumRadicado, string IdUsuario, string peticionario)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("spInsArchivoEscritoCiudadano", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EscritoCiudadano", Path);
                    cmd.Parameters.AddWithValue("@NumeroProceso", NumProceso);
                    cmd.Parameters.AddWithValue("@IdRadicado", NumRadicado);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("@IdEstado", 2);
                    cmd.Parameters.AddWithValue("@PeticionarioEscrito", peticionario);
                    cmd.ExecuteNonQuery();

                    //return cmd.Parameters["err_message"].Value.ToString();
                }
            }
            return "Ok";
        }

        public string InsInsistencia(string Path, string NumProceso, string peticionario)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("spInsArchivoInsistencia", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Insistencia", Path);
                    cmd.Parameters.AddWithValue("@NumeroProceso", NumProceso);
                    cmd.Parameters.AddWithValue("@PeticionarioInsistencia", peticionario);
                    cmd.ExecuteNonQuery();

                    //return cmd.Parameters["err_message"].Value.ToString();
                }
            }
            return "Ok";
        }
        #endregion

        public List<mConsulta> GetAllArchivosAdjuntos(string NumProceso)
        {
            mConsulta consulta = new mConsulta();
            List<mConsulta> ListaRadicados = new List<mConsulta>();
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllFilesAttach", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumeroProceso", NumProceso);
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);
                    SqlDataReader objReader = cmd.ExecuteReader();
                    while (objReader.Read())
                    {
                        mConsulta consultaObj = new mConsulta();
                        if (objReader["Id"] != DBNull.Value)
                        {
                            consultaObj.Id = Convert.ToInt32(objReader["Id"]);
                        }
                        if (objReader["NumeroRadicadoUnico"] != DBNull.Value)
                        {
                            consultaObj.NumeroRadicadoUnico = objReader["NumeroRadicadoUnico"].ToString();
                        }
                        if (objReader["NumeroProceso"] != DBNull.Value)
                        {
                            consultaObj.NumeroProceso = objReader["NumeroProceso"].ToString();
                        }

                        if (objReader["FechaRadicado"] != DBNull.Value)
                        {
                            consultaObj.FechaRadicado = Convert.ToDateTime(objReader["FechaRadicado"]);
                        }
                        if (objReader["IdEstado"] != DBNull.Value)
                        {
                            consultaObj.IdEstado = Convert.ToInt32((objReader["IdEstado"]));
                        }
                        if (objReader["Observaciones"] != DBNull.Value)
                        {
                            consultaObj.Observaciones = objReader["Observaciones"].ToString();
                        }

                        if (objReader["Activo"] != DBNull.Value)
                        {
                            consultaObj.Activo = Convert.ToInt32((objReader["Activo"]));
                        }
                        if (objReader["EscritoCiudadano"] != DBNull.Value)
                        {
                            consultaObj.EscritoCiudadano = (objReader["EscritoCiudadano"].ToString());
                        }
                        if (objReader["AdjuntoInsistencia"] != DBNull.Value)
                        {
                            consultaObj.AdjuntoInsistencia = (objReader["AdjuntoInsistencia"].ToString());
                        }
                        if (objReader["AdjuntoEscrito"] != DBNull.Value)
                        {
                            consultaObj.AdjuntoEscrito = (objReader["AdjuntoEscrito"].ToString());
                        }
                        if (objReader["Fallo"] != DBNull.Value)
                        {
                            consultaObj.Fallo = (objReader["Fallo"].ToString());
                        }

                        if (objReader["Insistencia"] != DBNull.Value)
                        {
                            consultaObj.Insistencia = (objReader["Insistencia"].ToString());
                        }

                        ListaRadicados.Add(consultaObj);
                    }
                }
                return ListaRadicados;
            }
        }


        #region InformeExpedienteDespacho
        public string ins_InformeExpedienteDespacho(mInformeExpedienteDespacho objeto, int NumProceso)
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_insInformeExpedienteDespacho", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("err_message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("idEstado", objeto.IdEstado);
                    cmd.Parameters.AddWithValue("idRadicado", objeto.IdRadicado);
                    cmd.Parameters.AddWithValue("idDespacho", objeto.IdDespacho);
                    cmd.Parameters.AddWithValue("idUsuario", objeto.IdUsuario);
                    cmd.Parameters.AddWithValue("impedimento", objeto.Impedimento);
                    cmd.Parameters.AddWithValue("temaGeneral", objeto.TemaGeneral);
                    cmd.Parameters.AddWithValue("titulo", objeto.Titulo);
                    cmd.Parameters.AddWithValue("juezRPI", objeto.JuezPrimeraInstancia);
                    cmd.Parameters.AddWithValue("juezRSI", objeto.JuezSegundaInstancia);
                    cmd.Parameters.AddWithValue("argumentosInsistencia", objeto.ArgumentosInsistencia);
                    cmd.Parameters.AddWithValue("observaciones", objeto.Observaciones);
                    cmd.Parameters.AddWithValue("altoRiesgo", objeto.AltoRiesgo);
                    cmd.Parameters.AddWithValue("procedenteAmparo", objeto.ProcedenteAmparo);
                    cmd.Parameters.AddWithValue("perjuicioIrremediable", objeto.PerjuicioIrremediable);
                    cmd.Parameters.AddWithValue("violoUnDerecho", objeto.VioloUnDerecho);
                    cmd.Parameters.AddWithValue("seleccionado", objeto.Seleccionado);
                    cmd.Parameters.AddWithValue("NumProceso", NumProceso);

                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["err_message"].Value.ToString();
                }
            }
        }
        #endregion

        public List<mEstadoProcesoSeleccion> get_VotosProcesoSelec()
        {
            List<mEstadoProcesoSeleccion> estadoProcesosSelecccion = new List<mEstadoProcesoSeleccion>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getVotosProcesoSelec", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    SqlDataReader objReader = cmd.ExecuteReader();

                    bool votacionCompleta = false;

                    while (objReader.Read())
                    {
                        votacionCompleta = Convert.ToBoolean(objReader["Votacion completa"]);//.ToString()

                        string numeroProceso = objReader["NumeroProceso"].ToString();
                        mEstadoProcesoSeleccion estadoProcesoSeleccion = estadoProcesosSelecccion.Where(x => x.NumeroProceso == numeroProceso).FirstOrDefault();

                        if (estadoProcesoSeleccion != null)
                        {
                            estadoProcesoSeleccion.Votos.Add(new mVotoProcesoSeleccion { Despacho = objReader["NombreDespacho"].ToString(), Voto = objReader["Voto"].ToString() });
                        }
                        else
                        {
                            mEstadoProcesoSeleccion nuevoEPS = new mEstadoProcesoSeleccion
                            {

                                IdRadicado = objReader["IdRadicado"].ToString(),
                                NumeroRadicado = objReader["NumeroRadicadoUnico"].ToString(),
                                FechaRadicado = objReader["FechaRadicado"].ToString(),
                                NumeroProceso = objReader["NumeroProceso"].ToString(),
                                Estado = objReader["NombreEstado"].ToString(),
                            };

                            nuevoEPS.Votos.Add(new mVotoProcesoSeleccion { Despacho = objReader["NombreDespacho"].ToString(), Voto = objReader["Voto"].ToString() });

                            estadoProcesosSelecccion.Add(nuevoEPS);
                        }
                    }

                    foreach (var estadoProceso in estadoProcesosSelecccion)
                    {
                        //bool votacionCompleta = true;
                        estadoProceso.VotacionUnanime = false;

                        //if (estadoProceso.Votos.Count < 2)
                        //    votacionCompleta = false;
                        //else
                        //{
                        //    if (estadoProceso.Votos[0].Voto == estadoProceso.Votos[1].Voto)
                        //        estadoProceso.VotacionUnanime = true;
                        //}

                        if (votacionCompleta == false)
                        {
                            estadoProceso.Votos.Clear();
                        }
                        else
                        {
                            if (estadoProceso.Votos[0].Voto == estadoProceso.Votos[1].Voto)
                                estadoProceso.VotacionUnanime = true;
                        }

                        estadoProceso.VotacionCompleta = votacionCompleta;
                    }

                    //foreach(var estadoProceso in estadoProcesosSelecccion)
                    //{
                    //    bool votacionCompleta = true;
                    //    estadoProceso.VotacionUnanime = false;

                    //    if (estadoProceso.Votos.Count < 2)
                    //        votacionCompleta = false;
                    //    else
                    //    {
                    //        if(estadoProceso.Votos[0].Voto == estadoProceso.Votos[1].Voto)
                    //            estadoProceso.VotacionUnanime = true;
                    //    }

                    //    if (votacionCompleta == false)
                    //        estadoProceso.Votos.Clear();

                    //    estadoProceso.VotacionCompleta = votacionCompleta;
                    //}

                    return estadoProcesosSelecccion;
                }
            }
        }

        public List<mProcesoSeleccion> get_ProcesosSeleccionados()
        {
            List<mProcesoSeleccion> procesosSeleccion = new List<mProcesoSeleccion>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getProcesosSeleccionados", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    SqlDataReader objReader = cmd.ExecuteReader();

                    while (objReader.Read())
                    {
                        mProcesoSeleccion procesoSeleccion = new mProcesoSeleccion();
                        procesoSeleccion.Id = objReader["Id"].ToString();
                        procesoSeleccion.NumeroRadicado = objReader["NumeroRadicado"].ToString();
                        procesoSeleccion.FechaRadicado = objReader["FechaRadicado"].ToString();
                        procesoSeleccion.NumeroProceso = objReader["NumeroProceso"].ToString();

                        procesosSeleccion.Add(procesoSeleccion);
                    }

                    return procesosSeleccion;
                }
            }
        }

        public string upd_VotoProcesoSelec(string idRadicado, string idDespacho, string IdUsuario, int NumProceso)
        {

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_updVotoProcesoSelec", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("err_message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("idRadicado", idRadicado);
                    cmd.Parameters.AddWithValue("idDespacho", idDespacho);
                    cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("NumProceso", NumProceso);
                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["err_message"].Value.ToString();
                }
            }
        }



        //public string get_AutoSalaSeleccion_test()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("NumRad");
        //    table.Columns.Add("NumProc");
        //    table.Columns.Add("Estado");
        //    DataRow row = table.NewRow();
        //    row["NumRad"] = "1";
        //    row["NumProc"] = "AFF555";
        //    row["Estado"] = "No seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "2";
        //    row["NumProc"] = "BFF666";
        //    row["Estado"] = "Seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "3";
        //    row["NumProc"] = "CFF777";
        //    row["Estado"] = "Seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "4";
        //    row["NumProc"] = "DFF888";
        //    row["Estado"] = "No seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "5";
        //    row["NumProc"] = "EFF999";
        //    row["Estado"] = "No seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "6";
        //    row["NumProc"] = "FFF101";
        //    row["Estado"] = "No seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "7";
        //    row["NumProc"] = "GFF111";
        //    row["Estado"] = "Seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "8";
        //    row["NumProc"] = "HFF121";
        //    row["Estado"] = "Seleccionado";
        //    table.Rows.Add(row);


        //    row = table.NewRow();
        //    row["NumRad"] = "9";
        //    row["NumProc"] = "IFF131";
        //    row["Estado"] = "No seleccionado";
        //    table.Rows.Add(row);

        //    row = table.NewRow();
        //    row["NumRad"] = "10";
        //    row["NumProc"] = "JFF141";
        //    row["Estado"] = "Seleccionado";
        //    table.Rows.Add(row);


        //    bool fila = false;
        //    string temp = string.Empty;
        //    List<string> range = new List<string>();

        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        if (table.Rows[i]["Estado"].ToString() != "Seleccionado" && fila == false)
        //        {
        //            temp += table.Rows[i]["NumRad"].ToString();
        //            fila = true;
        //        }


        //        if (table.Rows[i]["Estado"].ToString() != "No seleccionado" && fila == true)
        //        {
        //            if (temp != table.Rows[i - 1]["NumRad"].ToString())
        //            {
        //                temp += "-" + table.Rows[i - 1]["NumRad"].ToString();
        //            }

        //            range.Add(temp);
        //            fila = false;
        //            temp = string.Empty;
        //        }

        //        if (table.Rows[i]["Estado"].ToString() == "No seleccionado" && i == (table.Rows.Count - 1))
        //        {

        //            if (table.Rows[i]["NumRad"].ToString() != temp)
        //                temp += "-" + table.Rows[i]["NumRad"].ToString();

        //            range.Add(temp);
        //        }

        //    }

        //    return "";
        //}

        public object get_AutoSalaSeleccion()
        {
            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("get_UltimoEstadoRadicados", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);

                    bool fila = false;
                    string temp = string.Empty;
                    List<object> seleccionados = new List<object>();
                    List<string> rangos = new List<string>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Estado"].ToString() == "No seleccionado" && fila == false)
                        {
                            temp += dt.Rows[i]["NumeroRadicadoUnico"].ToString();
                            fila = true;
                        }

                        if (dt.Rows[i]["Estado"].ToString() == "No seleccionado" && i == (dt.Rows.Count - 1))
                        {

                            if (dt.Rows[i]["NumeroRadicadoUnico"].ToString() != temp)
                                temp += "-" + dt.Rows[i]["NumeroRadicadoUnico"].ToString();

                            rangos.Add(temp);
                        }

                        if (dt.Rows[i]["Estado"].ToString() == "Seleccionado")
                        {
                            seleccionados.Add(new
                            {
                                Id = dt.Rows[i]["Id"].ToString(),
                                NumeroRadicadoUnico = dt.Rows[i]["NumeroRadicadoUnico"].ToString(),
                                NumeroProceso = dt.Rows[i]["NumeroProceso"].ToString()
                            });
                        }


                        if (dt.Rows[i]["Estado"].ToString() == "Seleccionado" && fila == true)
                        {
                            if (temp != dt.Rows[i - 1]["NumeroRadicadoUnico"].ToString())
                            {
                                temp += " - " + dt.Rows[i - 1]["NumeroRadicadoUnico"].ToString();
                            }

                            rangos.Add(temp);
                            fila = false;
                            temp = string.Empty;
                        }
                    }
                    return new { NoSeleccionados = rangos, Seleccionado = seleccionados };
                }
            }
        }

        #region Auto sala de seleccion

        public List<mAutoProcesoSeleccionado> get_SeleccionadosParaRevision()
        {
            List<mAutoProcesoSeleccionado> procesosSelecionado = new List<mAutoProcesoSeleccionado>();

            using (Conexion objConn = new Conexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_getProcesosSeleccionados", objConn.cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    SqlDataAdapter objDataAdapter = new SqlDataAdapter(cmd);
                    objDataAdapter.Fill(dt);

                    mAutoProcesoSeleccionado proceso;

                    foreach (DataRow dr in dt.Rows)
                    {
                        proceso = new mAutoProcesoSeleccionado();
                        proceso.Expediente = dr["NumeroRadicado"].ToString();
                        proceso.NumeroDeProceso = dr["NumeroProceso"].ToString();
                        proceso.partesProcesales = getPartesProcesales(proceso.NumeroDeProceso);

                        procesosSelecionado.Add(proceso);
                    }
                }
            }

            return procesosSelecionado;
        }
        #endregion
    }

}


