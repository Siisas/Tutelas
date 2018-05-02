using Datos;
using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AuditoriaController : BrowseController
    {
        // GET: Auditoria


        #region Dependencia
        /// <summary>
        /// Auditorias this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Auditoria()
        {
            return ListaPermisos("5");
        }

        /// <summary>
        /// Vers the detalle auditoria.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult VerDetalleAuditoria(int id)
        {
            DAL verDetalleAuditoria = new DAL();

            Datos.Modelo.mAuditoria result = new Datos.Modelo.mAuditoria();
            result = verDetalleAuditoria.get_AuditoriaById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
            //return View(result);
        }

        /// <summary>
        /// Conectars the auditoria.
        /// </summary>
        /// <returns></returns>
        public ActionResult ConectarAuditoria()
        {
            DAL auditoria = new DAL();
            var result = auditoria.get_auditoria_all();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Conectars the auditoria by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult ConectarAuditoriaById(int id)
        {
            DAL auditoria = new DAL();
            var result = auditoria.get_auditoriaDetalleById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}