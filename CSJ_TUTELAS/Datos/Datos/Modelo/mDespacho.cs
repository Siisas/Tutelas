using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class mDespacho
    {

        public int Id { get; set; }

        public string NombreDespacho { get; set; }

        public string DescripcionDespacho { get; set; }

        public bool Activo { get; set; }
    }
}
