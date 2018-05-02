using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class mInformeExpedienteDespacho
    {
        public int IdEstado { get; set; }

        public int IdRadicado { get; set; }

        public int IdDespacho { get; set; }

        public string IdUsuario { get; set; }

        public string Impedimento { get; set; }

        public string TemaGeneral { get; set; }

        public string Titulo { get; set; }

        public string JuezPrimeraInstancia { get; set; }

        public string JuezSegundaInstancia { get; set; }

        public string ArgumentosInsistencia { get; set;  }

        public string Observaciones { get; set; }

        public bool AltoRiesgo { get; set; }

        public bool ProcedenteAmparo { get; set; }

        public bool PerjuicioIrremediable { get; set; }

        public bool VioloUnDerecho { get; set; }

        public string Seleccionado { get; set; }
    }
}
