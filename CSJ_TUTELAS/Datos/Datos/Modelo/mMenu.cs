using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Datos.Modelo
{
    /// <summary>
    /// Clase de la capa de datos para la entidad de Menus
    /// </summary>
    public class mMenu
    {
        /// <summary>
        /// Gets or sets the identifier menu.
        /// </summary>
        /// <value>
        /// The identifier menu.
        /// </value>
        public string idMenu { get; set; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }
        /// <summary>
        /// Gets or sets the ruta menu.
        /// </summary>
        /// <value>
        /// The ruta menu.
        /// </value>
        public string rutaMenu { get; set; }
        /// <summary>
        /// Gets or sets the identifier padre.
        /// </summary>
        /// <value>
        /// The identifier padre.
        /// </value>
        public string idPadre { get; set; }
        /// <summary>
        /// Gets or sets the activo.
        /// </summary>
        /// <value>
        /// The activo.
        /// </value>
        public string Activo { get; set; }
    }
}
