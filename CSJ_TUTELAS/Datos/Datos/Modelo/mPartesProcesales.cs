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
    public class mPartesProcesales
    {

        public string idproceso { get; set; }
        public string Numero_Identificacion { get; set; }
        public string Tipo_Sujeto { get; set; }
        public string Tipo_Identificacion { get; set; }
        public string Primer_Nombre { get; set; }
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public string Entidad { get; set; }
        public string Departamento_Contacto { get; set; }
        public string Ciudad_Contacto { get; set; }
        public string Correo_Electronico_Contacto { get; set; }
        public string Telefono_Contacto { get; set; }
        public string Celular_Contacto { get; set; }

    }
}
