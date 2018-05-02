using Datos.Modelo;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleAntiforgeryTokenErrorAttribute()
                            { ExceptionType = typeof(HttpAntiForgeryException) }
                       );
        }
    }

    public class HandleAntiforgeryTokenErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { action = "Login", controller = "Account" }));
        }
    }

    public class BrowseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)










        {
            if (filterContext.ActionDescriptor.ActionName == "Login" && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Account")
                return;
                
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var sessions = filterContext.HttpContext.Session;
                if (sessions["nombreUsuario"] != null)
                {
                    return;
                }
                else
                {
                    filterContext.Result = new JsonResult { Data = 401, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                    //xhr status code 401 to redirect
                    filterContext.HttpContext.Response.StatusCode = 401;

                    return;
                }
            }

            var session = filterContext.HttpContext.Session;
            if (session["nombreUsuario"] != null)
                return;

            //Redirect to login page.
            var redirectTarget = new RouteValueDictionary { { "action", "Login" }, { "controller", "Account" } };
            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }

        #region Utilidades

        public ActionResult ListaPermisos(string IdMenu)
        {
            Session["tienepermisos"] = false;
            List<PermissionModel> ListSort = new List<PermissionModel>();
            if (Session["Permisos"] != null)
            {
                List<PermissionModel> ListPermisos = (List<PermissionModel>)Session["Permisos"];
                ListSort = ListPermisos.Where(s => s.Id == IdMenu).ToList();
            }


            if (ListSort != null)
            {
                if (ListSort.Count > 0)
                {
                    var permisos = ListSort.First();
                    var permite = false;

                    if (permisos.Opciones.Contains("1-") && permisos.Permisos.Contains("1,"))
                    {
                        permite = true;
                    }

                    if (permisos.Opciones.Contains("2-") && permisos.Permisos.Contains("2,"))
                    {
                        permite = true;
                    }

                    if (permite)
                    {
                        ViewBag.Permisos = ListSort.First().Permisos;
                        Session["tienepermisos"] = true;
                        return View();
                    }
                    else
                    {
                        if (!permisos.Opciones.Contains("1-") && !permisos.Opciones.Contains("2-"))
                        {
                            ViewBag.Permisos = ListSort.First().Permisos;
                            Session["tienepermisos"] = true;
                            return View();
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
