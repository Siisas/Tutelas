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
    public class mResena
    {
        [Display(Name = "Id Reseña")]
        public string Id { get; set; }

        [Display(Name = "Id Radicado")]
        public string IdRadicado { get; set; }

        [Display(Name = "Fecha radicado")]
        public string FechaRadicado { get; set; }

        [Display(Name = "Expediente")]
        public string Expediente { get; set; }

        [Display(Name = "Demandante")]
        public string Demandante { get; set; }

        [Display(Name = "Orientación sexual")]
        public string OrientacionSexual { get; set; }

        [Display(Name = "Legitimacion")]
        public string Legitimacion { get; set; }

        [Display(Name = "Demandado")]
        public string Demandado { get; set; }

        [Display(Name = "Despacho primera instancia")]
        public string Despacho1raInstancia { get; set; }

        [Display(Name = "Despacho segunda instancia")]
        public string Despacho2daInstancia { get; set; }

        [Display(Name = "Derechos vulnerados")]
        public string DerechosVulnerados { get; set; }

        [Display(Name = "Pretensiones")]
        public string Pretensiones { get; set; }

        [Display(Name = "Hechos")]
        public string Hechos { get; set; }

        [Display(Name = "Primera instancia")]
        public string PrimeraInstancia { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones1raInstancia { get; set; }

        [Display(Name = "Impugnación")]
        public string Impugnacion { get; set; }

        [Display(Name = "Observaciones impugnación")]
        public string ObservacionesImpugnacion { get; set; }

        [Display(Name = "Segunda instancia")]
        public string SegundaInstancia { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones2daInstancia { get; set; }

        [Display(Name = "Especial protección")]
        public string Especialproteccion { get; set; }

        [Display(Name = "Tipo sujeto especial protección")]
        public string TipoSujetoEspecialproteccion { get; set; }

        [Display(Name = "Origen sujeto")]
        public string OrigenSujeto { get; set; }

        [Display(Name = "Extranjero sujeto")]
        public string ExtranjeroSujeto { get; set; }

        [Display(Name = "Condicion personal sujeto")]
        public string CondicionPersonalSujeto { get; set; }

        [Display(Name = "Condicion discapacidad sujeto")]
        public string CondicionDiscapacidadSujeto { get; set; }

        [Display(Name = "Condicion social sujeto")]
        public string CondicionSocialSujeto { get; set; }

        [Display(Name = "Restitucion de tierras sujeto")]
        public string RestitucionDeTierrasSujeto { get; set; }

        [Display(Name = "Criterios objetivos")]
        public string CriteriosObjetivos { get; set; }

        [Display(Name = "Criterios subjetivos")]
        public string CriteriosSubjetivos { get; set; }

        [Display(Name = "Criterios complementarios")]
        public string CriteriosComplementarios { get; set; }

        [Display(Name = "Criterios adicionales")]
        public string CriteriosAdicionales { get; set; }

        [Display(Name = "Problema juridico")]
        public string ProblemaJuridico { get; set; }

        [Display(Name = "Observaciones")]
        public string ObservacionesProblemaJuridico { get; set; }

        [Display(Name = "Fecha reseña")]
        public string FechaResena { get; set; }

    }
}
