using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DespachoController : Controller
    {
        DAL dal = new DAL();

        
        public ActionResult GetAllDespachos()
        {
            return Json(new { data = dal.get_AllDespachos() }, JsonRequestBehavior.AllowGet);
        }
    }
}