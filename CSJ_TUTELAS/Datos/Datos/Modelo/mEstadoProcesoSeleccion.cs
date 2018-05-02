using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class mEstadoProcesoSeleccion
    {
        public mEstadoProcesoSeleccion()
        {
           Votos = new List<mVotoProcesoSeleccion>();
        }

        public string IdRadicado { get; set; }

        public string NumeroRadicado { get; set; }

        public string FechaRadicado { get; set; }

        public string NumeroProceso { get; set; }

        public string Estado { get; set; }

        public bool VotacionCompleta { get; set; }

        public bool VotacionUnanime { get; set; }

        public List<mVotoProcesoSeleccion> Votos;
    }

    public class mVotoProcesoSeleccion
    {
        public string Despacho { get; set; }
        public string Voto { get; set; }
    }
}
