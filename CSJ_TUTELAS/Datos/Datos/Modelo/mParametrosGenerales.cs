using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    /// <summary>
    /// Clase de la capa de datos para la entidad de los parametros generales
    /// </summary>
    public class mParametrosGenerales
    {
        /// <summary>
        /// Gets or sets the identifier parametro.
        /// </summary>
        /// <value>
        /// The identifier parametro.
        /// </value>
        public int idParametro { get; set; }
        /// <summary>
        /// Gets or sets the parametro.
        /// </summary>
        /// <value>
        /// The parametro.
        /// </value>
        public string Parametro { get; set; }
        /// <summary>
        /// Gets or sets the fl estado.
        /// </summary>
        /// <value>
        /// The fl estado.
        /// </value>
        public int flEstado { get; set; }
        /// <summary>
        /// Gets or sets the identifier tipo parametro.
        /// </summary>
        /// <value>
        /// The identifier tipo parametro.
        /// </value>
        public int idTipoParametro { get; set; }
        /// <summary>
        /// Gets or sets the descripcion.
        /// </summary>
        /// <value>
        /// The descripcion.
        /// </value>
        public string Descripcion { get; set; }
    }
}
