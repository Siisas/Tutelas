using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    /// <summary>  
    ///  Clase jQueryDataTableParams:
    /// <Author> </Author>
    ///Clase que encapsula los parámetros más comunes enviados por el complemento DataTables
    /// </summary>  

    public class jQueryDataTableParams
    {
        /// <summary>  
        /// Solicitar número de secuencia enviado por DataTable,
        /// Mismo valor debe ser devuelto en respuesta
        /// </summary>      
        public string sEcho { get; set; }
        /// <summary>  
        /// Texto utilizado para filtrar
        /// </summary>  
        public string sSearch { get; set; }
        /// <summary>  
        /// Número de registros que deben mostrarse en la tabla
        /// </summary>  
        public int iDisplayLength { get; set; }
        /// <summary>  
        /// Primer registro que se debe mostrar (utilizado para la paginación)
        /// </summary>  
        public int iDisplayStart { get; set; }
        /// <summary>  
        /// Número de columnas de la tabla
        /// </summary>  
        public int iColumns { get; set; }
        /// <summary>  
        /// Número de columnas que se utilizan en la busqueda  
        /// </summary>  
        public int iSortingCols { get; set; }
        /// <summary>  
        /// Lista de nombres de columnas separada por comas 
        /// </summary>  
        public string sColumns { get; set; }       
    }
}