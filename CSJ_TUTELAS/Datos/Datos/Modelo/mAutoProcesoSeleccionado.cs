using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class mAutoProcesoSeleccionado
    {
        public string Expediente { get; set; }

        public string NumeroDeProceso { get; set; }

        public List<mPartesProcesales> partesProcesales { get; set; }
    }
}
