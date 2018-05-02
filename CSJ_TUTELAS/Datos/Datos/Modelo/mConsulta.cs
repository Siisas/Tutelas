using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    /// <summary>
    /// 
    /// </summary>
    public class mConsulta
    {
        #region Variables

        public mConsulta()
        {
            Consulta = new List<mConsulta>();
        }

        public List<mConsulta> Consulta;

        public int IdEstado { get; set; }
        public string Observaciones { get; set; }
        public string EscritoCiudadano { get; set; }
        public string AdjuntoInsistencia { get; set; }
        public string Insistencia { get; set; }
        public string AdjuntoEscrito { get; set; }
        public string Fallo { get; set; }

        public string numeroRadicado { get; set; }

        public DateTime fechaRadicacion { get; set; }

        public string NumeroProceso { get; set; }

        public string Accion { get; set; }

        public int IdReparto { get; set; }

        public string IdUsuario { get; set; }

        public string ProcesoRadicado { get; set; }

        public DateTime Fecha { get; set; }

        public int TipoReparto { get; set; }

        public int Activo { get; set; }

        public string NumeroRadicadoUnico { get; set; }

        public DateTime FechaRadicado { get; set; }

        public string fechaRadicado { get; set; }

        public string name { get; set; }

        public string IdRol { get; set; }

        public string first_name { get; set; }

        public string Nombre { get; set; }

        public int Id { get; set; }

        public string NumeroRadicado { get; set; }

        public string Estado { get; set; }

        public bool VotacionCompleta { get; set; }

        public bool VotacionUnanime { get; set; }

        public string IdRadicado { get; set; }

        public string uploadFoto { get; set; }

        public byte[] uploadFoto1 { get; set; }

        public object Content { get; set; }

        public bool Adjunto { get; set; }


        #endregion

    }
}
