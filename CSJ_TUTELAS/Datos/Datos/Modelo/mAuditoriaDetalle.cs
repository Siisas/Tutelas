using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    /// <summary>
    /// Clase de la capa de datos para la entidad del detalle las auditorias
    /// </summary>
    public class mAuditoriaDetalle
    {
        /// <summary>
        /// Gets or sets the campo.
        /// </summary>
        /// <value>
        /// The campo.
        /// </value>
        public string Campo { get; set; }
        /// <summary>
        /// Gets or sets the valor anterior.
        /// </summary>
        /// <value>
        /// The valor anterior.
        /// </value>
        public string ValorAnterior { get; set; }
        /// <summary>
        /// Gets or sets the valor nuevo.
        /// </summary>
        /// <value>
        /// The valor nuevo.
        /// </value>
        public string ValorNuevo { get; set; }
    }
}
