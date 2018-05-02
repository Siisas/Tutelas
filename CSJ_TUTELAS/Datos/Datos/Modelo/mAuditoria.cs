using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    /// <summary>
    /// Clase de la capa de datos para la entidad de las auditorias
    /// </summary>
    public class mAuditoria
    {
        /// <summary>
        /// Gets or sets the identifier auditoria.
        /// </summary>
        /// <value>
        /// The identifier auditoria.
        /// </value>
        public int IdAuditoria { get; set; }
        /// <summary>
        /// Gets or sets the identifier usuario.
        /// </summary>
        /// <value>
        /// The identifier usuario.
        /// </value>
        public string IdUsuario { get; set; }
        /// <summary>
        /// Gets or sets the tabla.
        /// </summary>
        /// <value>
        /// The tabla.
        /// </value>
        public string Tabla { get; set; }
        /// <summary>
        /// Gets or sets the identifier registro.
        /// </summary>
        /// <value>
        /// The identifier registro.
        /// </value>
        public string IdRegistro { get; set; }
        /// <summary>
        /// Gets or sets the fecha accion.
        /// </summary>
        /// <value>
        /// The fecha accion.
        /// </value>
        public string FechaAccion { get; set; }
        /// <summary>
        /// Gets or sets the accion.
        /// </summary>
        /// <value>
        /// The accion.
        /// </value>
        public string Accion { get; set; }
    }
}
