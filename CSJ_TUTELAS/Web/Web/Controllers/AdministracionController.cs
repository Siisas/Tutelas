using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datos;
using Datos.Modelo;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Clase controlador del modulo de Administración
    /// </summary>
    /// <seealso cref="Web.BrowseController" />
    public class AdministracionController : BrowseController
    {

        // GET: Administracion
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region ParametrosGenerales

        /// <summary>
        /// Parametroses the generales.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ParametrosGenerales()
        {
            return ListaPermisos("18");
        }

        /// <summary>
        /// Gets the parametros generales.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public ActionResult getParametrosGenerales(jQueryDataTableParams param)
        {
            int id = -1;

            if (Request.QueryString["ppadre"].ToString() != "")
            {
                id = Convert.ToInt32(Request.QueryString["ppadre"]);
            }


            DAL dal = new DAL();
            IQueryable<mParametrosGenerales> list = dal.getParametrosGenerales(id);
            int totalCount = list.Count();
            IEnumerable<mParametrosGenerales> filtroMembers = list;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtroMembers = list
                       .Where(m => m.idParametro.ToString().ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Parametro.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Descripcion.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.idTipoParametro.ToString().ToUpper().Contains(param.sSearch.ToUpper())
                          );
            }

            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc  

            if (sortIdx == 0 || sortIdx == 3)
            {
                Func<mParametrosGenerales, int> orderingFunction =
                (m => sortIdx == 0 ? m.idParametro :
                      sortIdx == 3 ? m.idTipoParametro :
                   m.idParametro
                 );

                if (sortDirection == "asc")
                    filtroMembers = filtroMembers.OrderBy(orderingFunction);
                else
                    filtroMembers = filtroMembers.OrderByDescending(orderingFunction);
            }
            else
            {
                Func<mParametrosGenerales, string> orderingFunction = (m => sortIdx == 1 ? m.Parametro :
                                                                            sortIdx == 2 ? m.Descripcion :
                                                           m.Parametro
                                                         );

                if (sortDirection == "asc")
                    filtroMembers = filtroMembers.OrderBy(orderingFunction);
                else
                    filtroMembers = filtroMembers.OrderByDescending(orderingFunction);
            }

            var displayedMembers = filtroMembers
                     .Skip(param.iDisplayStart)
                     .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.idParametro,
                             a.Parametro,
                             a.Descripcion,
                             a.idTipoParametro
                         };

            //Se devuelven los resultados por json
            return Json(new
            {
                draw = param.sEcho,
                recordsTotal = totalCount,
                recordsFiltered = filtroMembers.Count(),
                data = result
            },
               JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the parameter generales.
        /// </summary>
        /// <returns></returns>
        public ActionResult saveParamGrales()
        {

            int id = -1;

            if (Request.QueryString["ppadre"].ToString() != "")
            {
                id = Convert.ToInt32(Request.QueryString["ppadre"]);
            }

            DAL dal = new DAL();
            string usuario = User.Identity.GetUserName();
            string mensajerror = "";
            List<mDataDdl> arrayparam = listaParametros();

            arrayparam.Add(new mDataDdl { N_ID = "@idUsuario", N_VALOR = usuario });
            arrayparam.Add(new mDataDdl { N_ID = "@idTipoParametro", N_VALOR = Convert.ToString(id) });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });
            arrayparam.RemoveAt(0);
            var result = dal.Ejecutar("sp_insParamGrales", ref mensajerror, "sp", arrayparam);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// Edits the parameter generales.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //public ActionResult editParamGrales(string id)
        //{
        //    DAL dal = new DAL();
        //    mMetas result = new mMetas();
        //    result = dal.getMetasById(id);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Upds the parameter generales.
        /// </summary>
        /// <returns></returns>
        //public ActionResult updParamGrales()
        //{
        //    DAL dal = new DAL();
        //    string usuario = User.Identity.GetUserName();
        //    string mensajerror = "";

        //    List<mDataDdl> arrayparam = listaParametros();

        //    arrayparam.Add(new mDataDdl { N_ID = "@idUsuario", N_VALOR = usuario });
        //    arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

        //    var result = dal.Ejecutar("sp_updParamGrales", ref mensajerror, "sp", arrayparam);
        //    return JsonResult(new { data = result, msgError = mensajerror }, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Deletes the parameter generales.
        /// </summary>
        /// <param name="idParametro">The identifier parametro.</param>
        /// <returns></returns>
        public ActionResult delParamGrales(int idParametro)
        {
            DAL dal = new DAL();
            string usuario = User.Identity.GetUserName();
            string mensajerror = "";

            List<mDataDdl> arrayparam = listaParametros();

            arrayparam.Add(new mDataDdl { N_ID = "@idUsuario", N_VALOR = usuario });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = dal.Ejecutar("sp_delParamGrales", ref mensajerror, "sp", arrayparam);
            return Json(new { data = result, msgError = mensajerror }, JsonRequestBehavior.AllowGet);

            //DAL dal = new DAL();
            //string usuario = User.Identity.GetUserName();
            //var result = dal.delParamGrales(usuario, id);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the parametros generales by tipo.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult getParametrosGeneralesByTipo(int id)
        {
            DAL dal = new DAL();
            var result = dal.getParametrosGeneralesByTipo(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the parametros generales by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult getParametrosGeneralesById(int id)
        {
            DAL dal = new DAL();
            var result = dal.getParamGralesById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the data DDL.
        /// </summary>
        /// <param name="tabla">The tabla.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="valor">The valor.</param>
        /// <param name="iddefault">The iddefault.</param>
        /// <param name="valordefault">The valordefault.</param>
        /// <param name="wherecolumn">The wherecolumn.</param>
        /// <param name="wherevalue">The wherevalue.</param>
        /// <returns></returns>
        public ActionResult getDataDdl(string tabla, string id, string valor, string iddefault = "", string valordefault = "", string wherecolumn = "", string wherevalue = "")
        {
            DAL dal = new DAL();
            var result = dal.getDataDdl(tabla, id, valor, iddefault, valordefault, wherecolumn, wherevalue);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Utilidades

        /// <summary>
        /// Listas the parametros.
        /// </summary>
        /// <returns></returns>
        public List<mDataDdl> listaParametros()
        {
            List<mDataDdl> arrayparam = new List<mDataDdl>();
            for (int i = 0; i <= Request.Form.Count - 1; i++)
            {
                mDataDdl p = new mDataDdl();
                p.N_ID = "@" + Request.Form.AllKeys[i];
                p.N_VALOR = Request.Form[i];
                arrayparam.Add(p);
            }

            return arrayparam;
        }

        #endregion

        #region Reportes

        /// <summary>
        /// Vista de Reportes.
        /// </summary>
        /// <returns></returns>
        public ActionResult Reporte()
        {
            ViewBag.reporte = Request.Params["reporte"];
            return View();
        }

        #endregion

    }
}
