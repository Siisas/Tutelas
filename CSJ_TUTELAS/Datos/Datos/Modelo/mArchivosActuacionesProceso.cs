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
    public class mArchivosActuacionesProceso
    {

        public string id { get; set; }
        public string idproceso { get; set; }
        public string Nombreactuacion { get; set; }
        public byte[] Archivo { get; set; }
        public string NombreArchivo { get; set; }
        public string Codigo_Hash { get; set; }
        public string Tipo { get; set; }

    }
}
