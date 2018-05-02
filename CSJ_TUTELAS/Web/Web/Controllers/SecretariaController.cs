using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Datos;
using Datos.Modelo;

namespace Web.Controllers
{
    public class SecretariaController : Controller
    {
        DAL dal = new DAL();

        // GET: Secretaria
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SorteoSeleccion()
        {
            return View();
        }

        public ActionResult LimiteReparto()
        {
            return View();
        }



        [HttpPost]
        public ActionResult GuardarLimiteReparto(int data)
        {
            return Json(new {data = dal.ins_LimiteProcessoRadicador(data)}, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ConsultarLimiteReparto()
        {
            return Json(new { data = dal.ConsultarLimiteReparto() }, JsonRequestBehavior.AllowGet);
        }





        public ActionResult GetSorteosSeleccion(jQueryDataTableParams param)
        {
            var sorteos = dal.get_AllSorteosSeleccion();

            // Manejador de Filtros

            int totalCount = sorteos.Count();
            IEnumerable<mSorteo> filteredSorteos = sorteos;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredSorteos = sorteos
                .Where(m => m.Ano.ToString().Contains(param.sSearch) ||
                    m.Mes.ToString().Contains(param.sSearch) ||
                    m.MesString.Contains(param.sSearch) ||
                    m.Dia.ToString().Contains(param.sSearch) ||
                    m.Despacho1.Contains(param.sSearch) || 
                    m.Despacho2.Contains(param.sSearch));
            }

            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mSorteo, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.Ano.ToString() :
                    sortIdx == 1 ? m.Ano.ToString() :
                    sortIdx == 2 ? m.Mes.ToString() :
                    sortIdx == 3 ? m.Dia.ToString() :
                    sortIdx == 4 ? m.Despacho1 :
                    sortIdx == 5 ? m.Despacho2 : 
                    m.Ano.ToString()
                );

            var sortDirection = Request["sSortDir_0"]; // asc or desc  

            if (sortDirection == "asc")
                filteredSorteos = filteredSorteos.OrderBy(orderingFunction);
            else
                filteredSorteos = filteredSorteos.OrderByDescending(orderingFunction);
            var displayedMembers = filteredSorteos
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.Ano,
                             a.Mes,
                             a.MesString,
                             a.Dia,
                             a.Despacho1,
                             a.Despacho2
                         };

            //Se devuelven los resultados por json
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredSorteos.Count(),
                aaData = result
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerarNuevoSorteoSeleccion()

        {
            return Json( new { data = dal.generarNuevoSorteoSeleccion() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SorteoRevision()
        {
            return View();
        }

        public ActionResult GetSorteosRevision(jQueryDataTableParams param)
        {

            var sorteos = dal.get_AllSorteosRevision();

            // Manejador de Filtros

            int totalCount = sorteos.Count();
            IEnumerable<mSorteoRevision> filteredSorteos = sorteos;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredSorteos = sorteos
                .Where(m => m.Ano.ToString().Contains(param.sSearch) ||
                    m.MesString.Contains(param.sSearch) ||
                    m.Dia.ToString().Contains(param.sSearch) ||
                    m.Despacho.NombreDespacho.Contains(param.sSearch));
            }

            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mSorteoRevision, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.Ano.ToString() :
                    sortIdx == 1 ? m.Mes.ToString() :
                    sortIdx == 2 ? m.Dia.ToString() :
                    sortIdx == 4 ? m.Despacho.NombreDespacho :
                    m.Id.ToString()
                );

            var sortDirection = Request["sSortDir_0"]; // asc or desc  

            if (sortDirection == "asc")
                filteredSorteos = filteredSorteos.OrderBy(orderingFunction);
            else
                filteredSorteos = filteredSorteos.OrderByDescending(orderingFunction);
            var displayedMembers = filteredSorteos
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.Id,
                             a.Ano,
                             a.Mes,
                             a.MesString,
                             a.Dia,
                             Despacho = a.Despacho.NombreDespacho
                         };

            //Se devuelven los resultados por json
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredSorteos.Count(),
                aaData = result
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegistrarSorteoRevision(int despacho_1, int despacho_2, int despacho_3)
        {
            return Json( new { data = dal.ins_SorteoRevision(despacho_1, despacho_2, despacho_3) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult RegistrarSorteoSeleccionManual(int despacho_1, int despacho_2)
        {
            return Json(new { data = dal.ins_SorteoSeleccionManual(despacho_1, despacho_2) }, JsonRequestBehavior.AllowGet);
        }

    }
}