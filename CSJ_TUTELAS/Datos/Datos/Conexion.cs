using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    /// <summary>
    /// Clase que permite la conexión a la base de datos
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    class Conexion : IDisposable
    {
        public SqlConnection cnn;
        protected string strConnString;

        /// <summary>
        /// Initializa una nueva instancia de la clase <see cref="Conexion"/> .
        /// </summary>
        public Conexion()
        {
            if ((cnn = _cnnT()) == null)
            {
                this.Dispose();
            }
        }

       

        /// <summary>
        /// Valida si esta establecida la conexión.
        /// </summary>
        /// <returns></returns>
        private SqlConnection _cnnT()
        {
            SqlConnection conn = null;
            strConnString = ConfigurationManager.ConnectionStrings["connect"].ToString();

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = strConnString;
                conn.Open();
                return conn;
            }
            catch
            {
                conn.Dispose();
                return null;
            }
        }

        /// <summary>
        /// Realiza tareas definidas por la aplicación asociadas con la liberación, liberación o restablecimiento de recursos no administrados.
        /// </summary>
        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
            }
        }
    }

    class ConexionCC : IDisposable
    {
        public SqlConnection cnn;
        protected string strConnString;

        public ConexionCC()
        {
            if ((cnn = _cnnCC()) == null)
            {
                this.Dispose();
            }
        }
                
        private SqlConnection _cnnCC()
        {
            SqlConnection conn = null;
            strConnString = ConfigurationManager.ConnectionStrings["BdJ21Web"].ToString();

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = strConnString;
                conn.Open();
                return conn;
            }
            catch
            {
                conn.Dispose();
                return null;
            }
        }

        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
            }
        }

    }
}
