using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class mFormularioEDP
    {
        public string Tipo { get; set; }

        public string Expediente { get; set; }

        public string Acionante { get; set; }

        public string Acionado { get; set; }

        public string Solicitante { get; set; }

        public string DespachoFicha { get; set; }

        public string Responsable { get; set; }

        public string TemaGeneral { get; set; }

        public string Objetivo { get; set; }

        public string Subjetivo { get; set; }

        public string Complementario { get; set; }

        public string Titulo { get; set; }

        public string DerechosInvocados { get; set; }

        public string HechosPruebas { get; set; }

        public string JuezResumenPrimeraI { get; set; }

        public string JuezResumenSegundaI { get; set; }

        public int AgumentosInsistencia { get; set; }

        public string Observaciones { get; set; }

        public int asoAltoRiesgo { get; set; }

        public int RazonesAmparo { get; set; }

        public int RazonesIrremediable { get; set; }

        public int PerjuicioIrremediable { get; set; }

        public string DerechoViolentado { get; set; }

        public string Seleccionado { get; set; }
    }       
}
