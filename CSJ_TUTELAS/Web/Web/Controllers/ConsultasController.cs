using Datos;
using Datos.Modelo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Models;

namespace Web.Controllers
{
    public class ConsultasController : Controller
    {

        #region Consulta
        // GET: Consultas
        public ActionResult Consulta()
        {
            DAL consulta = new DAL();

            return ListaPermisos("7");
        }

        public ActionResult AutoSalaSeleccion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecibirPorcentajes(string stringifyJson, int IdFormulario)
        {
            DAL consulta = new DAL();
            JArray jsonArray = JArray.Parse(stringifyJson);
            List<mPorcentajes> ListaPorcentajes = new List<mPorcentajes>();
            int UserDespacho = Convert.ToInt32(Session["UserDespacho"]);
            if (IdFormulario == 1)
            {
                foreach (var lis in jsonArray)
                {



                    ListaPorcentajes.Add(
                  new mPorcentajes
                  {
                      indice = lis["indice"].ToString(),
                      valor = lis["valor"].ToString(),

                  });


                }
                consulta.getRepartoAdHonorem(ListaPorcentajes, UserDespacho);
                consulta.RepartoHonorem(UserDespacho);

            }                    
           
            if (IdFormulario == 2)
            {

                foreach (var lis in jsonArray)
                {



                    ListaPorcentajes.Add(
                  new mPorcentajes
                  {
                      adHonorem = lis["adHonorem"].ToString(),
                      tutor = lis["tutor"].ToString(),

                  });


                }


                consulta.getRepartoTutores(ListaPorcentajes, UserDespacho);
                //consulta.RepartoTutores(UserDespacho);
            }
            return Json(ListaPorcentajes);
        }

        [HttpPost]
        public ActionResult RolUsuario()
        {
            string Rol = Session["Rol"].ToString();
            return Json(Rol);
        }

        [HttpPost]
        public ActionResult GetAllAdhonoremTutorsByIdDespacho(int data)
        {
            DAL consulta = new DAL();
            int UserDespacho = Convert.ToInt32(Session["UserDespacho"]);
            if (data == 1)
            {

                return Json(consulta.GetAllAdhonoremTutorsByIdDespacho(data, UserDespacho));
            }
            else
            {

                return Json(new { data = consulta.GetAllAdhonoremTutorsByIdDespacho(data, UserDespacho), dato = consulta.GetAllAdhonoremTutorsByIdDespacho(1, UserDespacho) }, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        public ActionResult GetAllAdhonoremTutorsByIdDespachoA(int data)
        {
            DAL consulta = new DAL();
            int UserDespacho = Convert.ToInt32(Session["UserDespacho"]);
            return Json(consulta.GetAllAdhonoremTutorsByIdDespacho(data, UserDespacho));
        }


        public ActionResult GetProcesosParaInsistenciaYEscrito(jQueryDataTableParams param)
        {

            DAL consulta = new DAL();

            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = "ConsultaEscritosEInsistencias";

            var resultados = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = resultados.Count();

            IEnumerable<mConsulta> filteredConsulta = resultados;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredConsulta = filteredConsulta
                        .Where(m => m.ProcesoRadicado.Contains(param.sSearch)
                                || m.NumeroProceso.Contains(param.sSearch)
                                || m.Estado.Contains(param.sSearch));
            }

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.ProcesoRadicado :
                         sortIdx == 1 ? m.NumeroProceso :
                         sortIdx == 2 ? m.Estado :
                    m.NumeroRadicado
                );

            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredConsulta = filteredConsulta.OrderBy(orderingFunction);
            else
                filteredConsulta = filteredConsulta.OrderByDescending(orderingFunction);
            var displayedMembers = filteredConsulta
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from x in displayedMembers
                         select new
                         {
                             x.NumeroRadicadoUnico,
                             x.ProcesoRadicado,
                             x.Nombre
                         };

            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredConsulta.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult ConsultarSecretario(jQueryDataTableParams param, int IdEstado)
        {
            //var Json = "";
            var variableSesionIdRol = Session["Rol"];

            if (variableSesionIdRol != null)
            {

                // Secretario
                if (variableSesionIdRol.ToString() == "dc9a678b-f9c8-4567-b4bc-a6ef71f59483")
                {
                    return ConsultarSecretarioNuevo(param, IdEstado);
                }
                // Varios Roles 
                else
                {
                    return ConsultarVarios(param);
                }
            }
            else
            {
                return Json(new { data = "Session Cerrada" }, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpGet]
        public ActionResult Consultar(jQueryDataTableParams param)
        {
            //var Json = "";
            var variableSesionIdRol = Session["Rol"];
            //ROL Radicador
            if (variableSesionIdRol.ToString() == "bc600e11-ba76-4729-bc36-35eab571fee2")
            {
                return ConsultarRadicador(param);
            }
            //ROL Adhonorem
            if (variableSesionIdRol.ToString() == "4b365b55-eb91-4437-af19-5f00dd9bf55b")
            {
                return ConsultarAdhonorem(param);
            }
            //ROL  Tutor
            if (variableSesionIdRol.ToString() == "b76d800c-f34d-43a2-a583-549317968a4e")
            {
                return ConsultarTutor(param);
            }
            //ROL CoordinadorDespacho
            if (variableSesionIdRol.ToString() == "9e42ad3f-f449-4582-9b37-37343dfa5596")
            {
                return ConsultarCoordinadorDespacho(param);
            }
            // Coordinador Revision
            if (variableSesionIdRol.ToString() == "8cbd9e16-5dcf-4ebd-a2b4-7011fac4ed25")
            {
                return ConsultarCoordinadorRevision(param);
            }
            // Coordinador Seleccion
            if (variableSesionIdRol.ToString() == "cb06f9d4-13d6-4341-8b69-7ec6199c85ac")
            {
                return ConsultarCoordinadorSeleccion(param);
            }
            // Coordinador Revision
            if (variableSesionIdRol.ToString() == "cb06f9d4-13d6-4341-8b69-7ec6199c85ac")
            {
                return ConsultarRevision(param);
            }
            // Secretario
            if (variableSesionIdRol.ToString() == "dc9a678b-f9c8-4567-b4bc-a6ef71f59483")
            {
                return ConsultarSecretario(param);
            }
            // Varios Roles 
            else
            {
                return ConsultarVarios(param);
            }

        }

        #region Votos Proceso seleccion
        public ActionResult GetVotosProcesosSeleccion(jQueryDataTableParams param)
        {

            DAL consulta = new DAL();

            if (consulta.validDespachoSeleccion(Convert.ToInt32(Session["UserDespacho"])) == 1)
            {

                var votos = consulta.get_VotosProcesoSelec();
                int totalCount = votos.Count();

                IEnumerable<mEstadoProcesoSeleccion> filteredVotos = votos;


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filteredVotos = filteredVotos
                        .Where(m => m.NumeroRadicado.Contains(param.sSearch)
                                || m.FechaRadicado.Contains(param.sSearch)
                                || m.NumeroProceso.Contains(param.sSearch)
                                || m.Estado.Contains(param.sSearch));
                }

                var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
                Func<mEstadoProcesoSeleccion, string> orderingFunction =
                    (
                        m => sortIdx == 0 ? m.NumeroRadicado :
                             sortIdx == 1 ? m.FechaRadicado :
                             sortIdx == 2 ? m.NumeroProceso :
                             sortIdx == 3 ? m.Estado :
                        m.NumeroRadicado
                    );

                var sortDirection = Request["sSortDir_0"]; // asc or desc  
                if (sortDirection == "asc")
                    filteredVotos = filteredVotos.OrderBy(orderingFunction);
                else
                    filteredVotos = filteredVotos.OrderByDescending(orderingFunction);
                var displayedMembers = filteredVotos
                   .Skip(param.iDisplayStart)
                   .Take(param.iDisplayLength);

                //Manejardo de resultados
                var result = from x in displayedMembers
                             select new
                             {
                                 x.IdRadicado,
                                 x.NumeroRadicado,
                                 x.FechaRadicado,
                                 x.NumeroProceso,
                                 x.Estado,
                                 x.Votos,
                                 x.VotacionCompleta,
                                 x.VotacionUnanime
                             };

                //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalCount,
                    iTotalDisplayRecords = filteredVotos.Count(),
                    aaData = result
                },
               JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new
                    {
                        IdRadicado = "",
                        NumeroRadicado = "",
                        FechaRadicado = "",
                        NumeroProceso = "",
                        Estado = "",
                        Votos = "",
                        VotacionCompleta = "",
                        VotacionUnanime = ""
                    }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ActulizarVotoProcesoSeleccion(string idRadicado, int NumProceso)
        {
            DAL dal = new DAL();
            //var datico = idRadicado.Split('311', '.');
            var IdUsuario = Session["IdUsuario"];
            //var ValorUrl = Request.UrlReferrer.Query.Split('=', '&');
            // NumProceso = Convert.ToInt32(ValorUrl[5]);
            return Json(new { data = dal.upd_VotoProcesoSelec(idRadicado, Session["userDespacho"].ToString(), IdUsuario.ToString(), NumProceso) });
        }

        public ActionResult GetAutoSeleccion()
        {
            DAL dal = new DAL();
            return Json(dal.get_AutoSalaSeleccion(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Proceso revision
        public ActionResult ConsultarProcesosSeleccion(jQueryDataTableParams param)
        {
            DAL consulta = new DAL();

            if (consulta.validDespachosRevision(Convert.ToInt32(Session["UserDespacho"])) == 1)
            {
                var procesoSeleccion = consulta.get_ProcesosSeleccionados();
                int totalCount = procesoSeleccion.Count();

                IEnumerable<mProcesoSeleccion> filteredProSeleccion = procesoSeleccion;


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filteredProSeleccion = filteredProSeleccion
                        .Where(m => m.NumeroRadicado.Contains(param.sSearch)
                                || m.FechaRadicado.Contains(param.sSearch)
                                || m.NumeroProceso.Contains(param.sSearch));
                }

                var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
                Func<mProcesoSeleccion, string> orderingFunction =
                    (
                        m => sortIdx == 0 ? m.NumeroRadicado :
                             sortIdx == 1 ? m.FechaRadicado :
                             sortIdx == 2 ? m.NumeroProceso :
                        m.Id
                    );

                var sortDirection = Request["sSortDir_0"]; // asc or desc  
                if (sortDirection == "asc")
                    filteredProSeleccion = filteredProSeleccion.OrderBy(orderingFunction);
                else
                    filteredProSeleccion = filteredProSeleccion.OrderByDescending(orderingFunction);
                var displayedMembers = filteredProSeleccion
                   .Skip(param.iDisplayStart)
                   .Take(param.iDisplayLength);

                //Manejardo de resultados
                var result = from x in displayedMembers
                             select new
                             {
                                 x.Id,
                                 x.NumeroRadicado,
                                 x.FechaRadicado,
                                 x.NumeroProceso
                             };

                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalCount,
                    iTotalDisplayRecords = filteredProSeleccion.Count(),
                    aaData = result
                },
               JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new
                    {
                        Id = "",
                        NumeroRadicado = "",
                        FechaRadicado = "",
                        NumeroProceso = ""
                    }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SubirFalloRadicado(string idRadicado)
        {
            DAL consulta = new DAL();

            var fileContent = Request.Files[0];
            if (fileContent != null)
            {
                var fileExtencion = Path.GetExtension(fileContent.FileName);

                if (fileExtencion == ".doc" || fileExtencion == ".DOC"
                     || fileExtencion == ".pdf" || fileExtencion == ".PDF"
                     || fileExtencion == ".doc" || fileExtencion == ".DOC"
                     || fileExtencion == ".doc" || fileExtencion == ".DOC")
                {
                    var path = "/Archivos/Fallo/" + fileContent.FileName;
                    fileContent.SaveAs(Server.MapPath(path));

                    string result = consulta.ins_FalloRadicado(idRadicado, path);

                    if (result != "")
                    {
                        return Json(new { data = false, result = "Error al guardar Fallo" });
                    }

                    return Json(new { data = true, result = "" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = false, result = "Tipo de archivo invalido" });
            }

            return Json(new { data = false, result = "Error al subir al archivo" });
        }
        #endregion

        public ActionResult ConsultarRadicador(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);
            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarAdhonorem(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarCoordinadorDespacho(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarTutor(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarCoordinadorRevision(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarVarios(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.ProcesoRadicado.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.ProcesoRadicado :
                    m.ProcesoRadicado
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id,
                             a.NumeroProceso
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarCoordinadorSeleccion(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id,
                             a.NumeroProceso
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarRevision(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarSecretario(jQueryDataTableParams param)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = Session["Rol"];
            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            var user = consulta.getAllConsulta(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString());
            int totalCount = user.Count();
            IEnumerable<mConsulta> filteredUsers = user;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = user
                    .Where(m => m.NumeroProceso.Contains(param.sSearch));
            }
            //Manejador de orden         

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.NumeroProceso :
                    m.NumeroProceso
                );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredUsers = filteredUsers.OrderBy(orderingFunction);
            else
                filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.ProcesoRadicado,
                             a.NumeroRadicadoUnico,
                             a.fechaRadicacion,
                             a.Nombre,
                             a.Id
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }
        //public ActionResult ConsultarSecretarioNuevo(jQueryDataTableParams param, int IdEstado)
        //{
        //    //Capturo la variable de sesion y paso el rol
        //    var VariableSesionIdUsuario = Session["UserId"];
        //    var variableSesionIdRol = "ConsultaEscritosEInsistencias";
        //    DAL consulta = new DAL();
        //    //Paso el rol del Usuario en entero 
        //    var user = consulta.getAllConsultaSecretarioIdEstado(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString(), IdEstado);
        //    int totalCount = user.Count();
        //    IEnumerable<mConsulta> filteredUsers = user;


        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        filteredUsers = user
        //            .Where(m => m.NumeroProceso.Contains(param.sSearch));
        //    }
        //    //Manejador de orden         

        //    var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
        //    Func<mConsulta, string> orderingFunction =
        //        (
        //            m => sortIdx == 0 ? m.NumeroProceso :
        //            m.NumeroProceso
        //        );
        //    var sortDirection = Request["sSortDir_0"]; // asc or desc  
        //    if (sortDirection == "asc")
        //        filteredUsers = filteredUsers.OrderBy(orderingFunction);
        //    else
        //        filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
        //    var displayedMembers = filteredUsers
        //       .Skip(param.iDisplayStart)
        //       .Take(param.iDisplayLength);



        //    //Manejardo de resultados
        //    var result = from a in displayedMembers
        //                 select new
        //                 {
        //                     a.ProcesoRadicado,
        //                     a.NumeroRadicadoUnico,
        //                     a.fechaRadicacion,
        //                     a.Nombre,
        //                     a.Id
        //                 };
        //    //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        //    return Json(new
        //    {
        //        sEcho = param.sEcho,
        //        iTotalRecords = totalCount,
        //        iTotalDisplayRecords = filteredUsers.Count(),
        //        aaData = result
        //    },
        //   JsonRequestBehavior.AllowGet);
        //}
        public ActionResult ConsultarSecretarioNuevo(jQueryDataTableParams param, int IdEstado)
        {
            //Capturo la variable de sesion y paso el rol
            var VariableSesionIdUsuario = Session["UserId"];
            var variableSesionIdRol = "ConsultaEscritosEInsistencias";

            DAL consulta = new DAL();
            //Paso el rol del Usuario en entero 
            if (IdEstado == 9)
            {
                var user = consulta.getAllConsultaSecretarioIdEstadoNoSel(variableSesionIdRol);
                int totalCount = user.Count();
                IEnumerable<mConsulta> filteredUsers = user;


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filteredUsers = user
                        .Where(m => m.NumeroProceso.Contains(param.sSearch));
                }
                //Manejador de orden         

                var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
                Func<mConsulta, string> orderingFunction =
                    (
                        m => sortIdx == 0 ? m.NumeroProceso :
                        m.NumeroProceso
                    );
                var sortDirection = Request["sSortDir_0"]; // asc or desc  
                if (sortDirection == "asc")
                    filteredUsers = filteredUsers.OrderBy(orderingFunction);
                else
                    filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
                var displayedMembers = filteredUsers
                   .Skip(param.iDisplayStart)
                   .Take(param.iDisplayLength);



                //Manejardo de resultados
                var result = from a in displayedMembers
                             select new
                             {
                                 a.ProcesoRadicado,
                                 a.NumeroRadicadoUnico,
                                 a.fechaRadicacion,
                                 a.Nombre,
                                 a.Id,
                                 a.AdjuntoInsistencia,
                                 a.AdjuntoEscrito,
                             };
                //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalCount,
                    iTotalDisplayRecords = filteredUsers.Count(),
                    aaData = result
                },
               JsonRequestBehavior.AllowGet);

            }
            else
            {
                var user = consulta.getAllConsultaSecretarioIdEstado(variableSesionIdRol.ToString(), VariableSesionIdUsuario.ToString(), IdEstado);
                int totalCount = user.Count();
                IEnumerable<mConsulta> filteredUsers = user;


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filteredUsers = user
                        .Where(m => m.NumeroProceso.Contains(param.sSearch));
                }
                //Manejador de orden         

                var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
                Func<mConsulta, string> orderingFunction =
                    (
                        m => sortIdx == 0 ? m.NumeroProceso :
                        m.NumeroProceso
                    );
                var sortDirection = Request["sSortDir_0"]; // asc or desc  
                if (sortDirection == "asc")
                    filteredUsers = filteredUsers.OrderBy(orderingFunction);
                else
                    filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
                var displayedMembers = filteredUsers
                   .Skip(param.iDisplayStart)
                   .Take(param.iDisplayLength);



                //Manejardo de resultados
                var result = from a in displayedMembers
                             select new
                             {
                                 a.ProcesoRadicado,
                                 a.NumeroRadicadoUnico,
                                 a.fechaRadicacion,
                                 a.Nombre,
                                 a.Id,
                                 a.AdjuntoInsistencia,
                                 a.AdjuntoEscrito

                             };
                //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalCount,
                    iTotalDisplayRecords = filteredUsers.Count(),
                    aaData = result
                },
               JsonRequestBehavior.AllowGet);

            }

        }


        public ActionResult validDespachoSeleccion(int IdDespacho)
        {
            DAL dal = new DAL();
            var result = dal.validDespachoSeleccion(IdDespacho);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getProcesosPreSeleccionados(jQueryDataTableParams param)
        {
            int IdDespacho = 0;
            DAL consulta = new DAL();
            if (Request.QueryString["IdDespacho"] != null)
            {
                if (Request.QueryString["IdDespacho"].ToString() != "")
                {
                    IdDespacho = Convert.ToInt32(Request.QueryString["IdDespacho"]);
                }
            }

            var Process = consulta.getProcesosPreSeleccionados(IdDespacho);
            int totalCount = Process.Count();
            IEnumerable<string> filteredUsers = Process;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredUsers = Process
                    .Where(m => m.Contains(param.sSearch));
            }
            //Manejador de orden         

            //var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            //Func<mConsulta, string> orderingFunction =
            //    (
            //        m => sortIdx == 0 ? m.NumeroProceso :
            //        m.NumeroProceso
            //    );
            //var sortDirection = Request["sSortDir_0"]; // asc or desc  
            //if (sortDirection == "asc")
            //    filteredUsers = filteredUsers.OrderBy(orderingFunction);
            //else
            //    filteredUsers = filteredUsers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredUsers
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);



            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a
                         };
            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredUsers.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }

        public ActionResult getUsuariosSeleccion(int idDespacho)
        {
            DAL dal = new DAL();

            return Json(dal.getUsuariosSeleccion(idDespacho), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GuardarReparto(string Numproceso, string IdUsuario)
        {
            DAL dal = new DAL();
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@p_idUsuario", N_VALOR = IdUsuario });
            arrayparam.Add(new mDataDdl { N_ID = "@p_procesoRadicado", N_VALOR = Numproceso });
            arrayparam.Add(new mDataDdl { N_ID = "@p_tipoReparto", N_VALOR = "4" });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = dal.Ejecutar("sp_insReparto", ref mensajerror, "sp", arrayparam);

            return Json(new { data = result, msgError = mensajerror }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ConsultaDdl()
        {
            DAL consulta = new DAL();
            return Json(consulta.GetAllDDL(), JsonRequestBehavior.AllowGet);
        }

        //Consulta AdHonorem para el reparot de tutor
        [HttpGet]
        public ActionResult ConsultaAdHonorem()
        {
            DAL consulta = new DAL();
            return Json(consulta.ConsultaAdHonorem(), JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Consulta Detalle

        public ActionResult ConsultaDetalle()
        {
            DAL dal = new DAL();

            if (Request.QueryString["NumProceso"] != null)
            {
                if (Request.QueryString["NumProceso"].ToString() != "")
                {
                    ViewBag.NumProceso = Request.QueryString["NumProceso"].ToString().Replace("'", "");
                }
            }
            if (Request.QueryString["NumRadicado"] != null)
            {
                if (Request.QueryString["NumRadicado"].ToString() != "")
                {

                    ViewBag.NumRadicado = Request.QueryString["NumRadicado"].ToString();
                }
            }
            if (Request.QueryString["Rol"] != null)
            {
                if (Request.QueryString["Rol"].ToString() != "")
                {
                    switch (Request.QueryString["Rol"].ToString())
                    {
                        case "bc600e11-ba76-4729-bc36-35eab571fee2":
                            ViewBag.Rol = "Radicador";
                            break;
                        case "d823b9a8-9b38-417d-bf6a-dda0f4aceb8f":
                            ViewBag.Rol = "Abogado";
                            break;
                        case "4b365b55-eb91-4437-af19-5f00dd9bf55b":
                            ViewBag.Rol = "Adhonorem";
                            break;
                        case "d83bf7b1-5606-4d37-8af5-4ea53e767130":
                            ViewBag.Rol = "Administrador";
                            break;
                        case "9e42ad3f-f449-4582-9b37-37343dfa5596":
                            ViewBag.Rol = "Coordinador Despacho";
                            break;
                        case "8cbd9e16-5dcf-4ebd-a2b4-7011fac4ed25":
                            ViewBag.Rol = "Coordinador Revision";
                            break;
                        case "cb06f9d4-13d6-4341-8b69-7ec6199c85ac":
                            ViewBag.Rol = "Coordinador Seleccion";
                            break;
                        case "ebb6c4d1-d080-4822-8bc1-a5523a5f0fe1":
                            ViewBag.Rol = "Magistrado";
                            break;
                        case "dc9a678b-f9c8-4567-b4bc-a6ef71f59483":
                            ViewBag.Rol = "Secretario";
                            break;
                        case "b76d800c-f34d-43a2-a583-549317968a4e":
                            ViewBag.Rol = "Tutor";
                            break;
                        case "477bb66e-f523-487e-b6e5-08c80a4eb7da":
                            ViewBag.Rol = "Magistrado Auxiliar";
                            break;
                        case "6f8a9674-18ed-4d8b-9718-bdc53dc1b3fb":
                            ViewBag.Rol = "Profesional grado 33";
                            break;
                        case "eb581596-9767-4ad8-97b0-de7fffb6771a":
                            ViewBag.Rol = "Profesional grado 21";
                            break;
                        case "69902d61-4df4-4a41-abf1-e6cfb1251ed0":
                            ViewBag.Rol = "Abogado sustanciador";
                            break;
                        case "012da970-b31f-43f3-8b36-c89874aeb9fa":
                            ViewBag.Rol = "Auxiliar judicial grado 1";
                            break;
                        case "5be41259-98db-4fad-95e3-73dfac106432":
                            ViewBag.Rol = "Auxiliar judicial grado 2";
                            break;
                        case "9359a6ef-454c-44b9-b307-69f8e5504d37":
                            ViewBag.Rol = "Judicantes";
                            break;
                    }

                }
            }
            return ListaPermisos("7");
        }

        public ActionResult ConsultaDetalleRefac()
        {
            DAL dal = new DAL();

            if (Request.QueryString["NumProceso"] != null)
            {
                if (Request.QueryString["NumProceso"].ToString() != "")
                {

                    ViewBag.NumProceso = Request.QueryString["NumProceso"].ToString().Replace("'", "");
                }
            }
            if (Request.QueryString["NumRadicado"] != null)
            {
                if (Request.QueryString["NumRadicado"].ToString() != "")
                {
                    ViewBag.NumRadicado = Request.QueryString["NumRadicado"].ToString();
                }
            }
            if (Request.QueryString["Rol"] != null)
            {
                if (Request.QueryString["Rol"].ToString() != "")
                {
                    switch (Request.QueryString["Rol"].ToString())
                    {
                        case "bc600e11-ba76-4729-bc36-35eab571fee2":
                            ViewBag.Rol = "Radicador";
                            break;
                        case "d823b9a8-9b38-417d-bf6a-dda0f4aceb8f":
                            ViewBag.Rol = "Abogado";
                            break;
                        case "4b365b55-eb91-4437-af19-5f00dd9bf55b":
                            ViewBag.Rol = "Adhonorem";
                            break;
                        case "d83bf7b1-5606-4d37-8af5-4ea53e767130":
                            ViewBag.Rol = "Administrador";
                            break;
                        case "9e42ad3f-f449-4582-9b37-37343dfa5596":
                            ViewBag.Rol = "Coordinador Despacho";
                            break;
                        case "8cbd9e16-5dcf-4ebd-a2b4-7011fac4ed25":
                            ViewBag.Rol = "Coordinador Revision";
                            break;
                        case "cb06f9d4-13d6-4341-8b69-7ec6199c85ac":
                            ViewBag.Rol = "Coordinador Seleccion";
                            break;
                        case "ebb6c4d1-d080-4822-8bc1-a5523a5f0fe1":
                            ViewBag.Rol = "Magistrado";
                            break;
                        case "dc9a678b-f9c8-4567-b4bc-a6ef71f59483":
                            ViewBag.Rol = "Secretario";
                            break;
                        case "b76d800c-f34d-43a2-a583-549317968a4e":
                            ViewBag.Rol = "Tutor";
                            break;
                    }

                }
            }
            return ListaPermisos("7");
        }

        public ActionResult GetPartesProcesales(jQueryDataTableParams param)
        {
            string NumProceso = "";

            if (Request.QueryString["NumProceso"] != null)
            {
                if (Request.QueryString["NumProceso"].ToString() != "")
                {
                    NumProceso = Request.QueryString["NumProceso"].ToString();
                }
            }


            DAL dal = new DAL();
            IQueryable<mPartesProcesales> list = dal.getPartesProcesales(NumProceso).AsQueryable();
            int totalCount = list.Count();
            IEnumerable<mPartesProcesales> filtroMembers = list;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtroMembers = list
                       .Where(m => m.Numero_Identificacion.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Tipo_Identificacion.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Tipo_Sujeto.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Primer_Nombre.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Segundo_Nombre.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Primer_Apellido.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Segundo_Apellido.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Entidad.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Departamento_Contacto.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Ciudad_Contacto.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Correo_Electronico_Contacto.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Telefono_Contacto.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Celular_Contacto.ToString().ToUpper().Contains(param.sSearch.ToUpper())
                          );
            }

            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc  


            Func<mPartesProcesales, string> orderingFunction = (m => sortIdx == 0 ? m.Numero_Identificacion :
                                                                     sortIdx == 1 ? m.Tipo_Identificacion :
                                                                     sortIdx == 2 ? m.Tipo_Sujeto :
                                                                     sortIdx == 3 ? m.Primer_Nombre :
                                                                     sortIdx == 4 ? m.Segundo_Nombre :
                                                                     sortIdx == 5 ? m.Primer_Apellido :
                                                                     sortIdx == 6 ? m.Segundo_Apellido :
                                                                     sortIdx == 7 ? m.Entidad :
                                                                     sortIdx == 8 ? m.Departamento_Contacto :
                                                                     sortIdx == 9 ? m.Ciudad_Contacto :
                                                                     sortIdx == 10 ? m.Correo_Electronico_Contacto :
                                                                     sortIdx == 11 ? m.Telefono_Contacto :
                                                                     sortIdx == 12 ? m.Celular_Contacto :
                                                                     m.Celular_Contacto
                                                     );

            if (sortDirection == "asc")
                filtroMembers = filtroMembers.OrderBy(orderingFunction);
            else
                filtroMembers = filtroMembers.OrderByDescending(orderingFunction);

            var displayedMembers = filtroMembers
                     .Skip(param.iDisplayStart)
                     .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.idproceso,
                             a.Numero_Identificacion,
                             a.Tipo_Identificacion,
                             a.Tipo_Sujeto,
                             a.Primer_Nombre,
                             a.Segundo_Nombre,
                             a.Primer_Apellido,
                             a.Segundo_Apellido,
                             a.Entidad,
                             a.Departamento_Contacto,
                             a.Ciudad_Contacto,
                             a.Correo_Electronico_Contacto,
                             a.Telefono_Contacto,
                             a.Celular_Contacto
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

        public ActionResult GetArchivosActuaciones(jQueryDataTableParams param)
        {
            string NumProceso = "";

            if (Request.QueryString["NumProceso"] != null)
            {
                if (Request.QueryString["NumProceso"].ToString() != "")
                {
                    NumProceso = Request.QueryString["NumProceso"].ToString();
                }
            }

            DAL dal = new DAL();

            List<mArchivosActuacionesProceso> Lista = dal.getArchivosActuaciones(NumProceso);
            Session["Archivos"] = Lista;

            IQueryable<mArchivosActuacionesProceso> list = Lista.AsQueryable();
            int totalCount = list.Count();
            IEnumerable<mArchivosActuacionesProceso> filtroMembers = list;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtroMembers = list
                       .Where(m => m.idproceso.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.Nombreactuacion.ToUpper().Contains(param.sSearch.ToUpper())
                          );
            }

            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc  


            Func<mArchivosActuacionesProceso, string> orderingFunction = (m => sortIdx == 0 ? m.idproceso :
                                                                     sortIdx == 1 ? m.Nombreactuacion :
                                                                     m.idproceso
                                                     );

            if (sortDirection == "asc")
                filtroMembers = filtroMembers.OrderBy(orderingFunction);
            else
                filtroMembers = filtroMembers.OrderByDescending(orderingFunction);

            var displayedMembers = filtroMembers
                     .Skip(param.iDisplayStart)
                     .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.idproceso,
                             a.Nombreactuacion,
                             a.NombreArchivo
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

        public ActionResult GetArchivosProceso(jQueryDataTableParams param)
        {
            string NumProceso = "";

            if (Request.QueryString["NumProceso"] != null)
            {
                if (Request.QueryString["NumProceso"].ToString() != "")
                {
                    NumProceso = Request.QueryString["NumProceso"].ToString();
                }
            }

            DAL dal = new DAL();

            List<mArchivosActuacionesProceso> Lista = dal.getArchivosProceso(NumProceso);
            Session["ArchivosP"] = Lista;

            IQueryable<mArchivosActuacionesProceso> list = Lista.AsQueryable();
            int totalCount = list.Count();
            IEnumerable<mArchivosActuacionesProceso> filtroMembers = list;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtroMembers = list
                       .Where(m => m.id.ToUpper().Contains(param.sSearch.ToUpper()) ||
                                    m.NombreArchivo.ToUpper().Contains(param.sSearch.ToUpper())
                          );
            }

            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc  


            Func<mArchivosActuacionesProceso, string> orderingFunction = (m => sortIdx == 0 ? m.id :
                                                                     sortIdx == 1 ? m.NombreArchivo :
                                                                     m.id
                                                     );

            if (sortDirection == "asc")
                filtroMembers = filtroMembers.OrderBy(orderingFunction);
            else
                filtroMembers = filtroMembers.OrderByDescending(orderingFunction);

            var displayedMembers = filtroMembers
                     .Skip(param.iDisplayStart)
                     .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.id,
                             a.NombreArchivo
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

        public ActionResult VerPdf()
        {

            string PdfName = "";
            string TipoArchivo = "";

            if (Request.QueryString["FileName"] != null)
            {
                if (Request.QueryString["FileName"].ToString() != "")
                {
                    PdfName = Request.QueryString["FileName"].ToString();
                }
            }
            if (Request.QueryString["Tipo"] != null)
            {
                if (Request.QueryString["Tipo"].ToString() != "")
                {
                    TipoArchivo = Request.QueryString["Tipo"].ToString();
                }
            }

            List<mArchivosActuacionesProceso> Archivos = new List<mArchivosActuacionesProceso>();

            if (TipoArchivo == "Actuacion")
            {
                Archivos = (List<mArchivosActuacionesProceso>)Session["Archivos"];
            }
            else if (TipoArchivo == "Proceso")
            {
                Archivos = (List<mArchivosActuacionesProceso>)Session["ArchivosP"];
            }

            mArchivosActuacionesProceso Archivo = Archivos.Where(x => x.NombreArchivo == PdfName).FirstOrDefault();

            MemoryStream ms = new MemoryStream(Archivo.Archivo);

            Response.AppendHeader("Content-Disposition", string.Concat("inline; filename=\"",  "\""));
            return File(ms, "application/pdf");


            //Response.Clear();
           
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=" + PdfName);
            //Response.Buffer = true;
            //ms.WriteTo(Response.OutputStream);       
            //Response.End();         


            //return new     FileStreamResult(ms, "application/pdf");

        }

        public ActionResult Radicar(string NumProceso, string IdEstado, string Observaciones)
        {
            DAL dal = new DAL();

            
            string IdUsuario = Session["IdUsuario"].ToString();
            var result = dal.Insertar_Radicado(NumProceso, IdEstado, Observaciones, IdUsuario);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GuardarResena(mResena resena)
        {
            if (resena != null)
            {
                DAL dal = new DAL();
                string mensajerror = "";

        

                var ValorUrl = Request.UrlReferrer.Query.Split('=', '&');
                int  NumProceso = Convert.ToInt32(ValorUrl[5]);

                string IdUsuario = Session["IdUsuario"].ToString();

                List<mDataDdl> arrayparam = new List<mDataDdl>();

                arrayparam.Add(new mDataDdl { N_ID = "@p_IdRadicado", N_VALOR = resena.IdRadicado });
                arrayparam.Add(new mDataDdl { N_ID = "@p_NumRadicado", N_VALOR = resena.Expediente.Remove(0, 2) });
                arrayparam.Add(new mDataDdl { N_ID = "@p_demandado", N_VALOR = resena.Demandado });
                arrayparam.Add(new mDataDdl { N_ID = "@p_demandante", N_VALOR = resena.Demandante });
                arrayparam.Add(new mDataDdl { N_ID = "@p_legitimacion", N_VALOR = resena.Legitimacion });
                arrayparam.Add(new mDataDdl { N_ID = "@p_despacho1raInstancia", N_VALOR = resena.Despacho1raInstancia });
                arrayparam.Add(new mDataDdl { N_ID = "@p_despacho2daInstancia", N_VALOR = resena.Despacho2daInstancia });
                arrayparam.Add(new mDataDdl { N_ID = "@p_derechosVulnerados", N_VALOR = resena.DerechosVulnerados });
                arrayparam.Add(new mDataDdl { N_ID = "@p_pretensiones", N_VALOR = resena.Pretensiones });
                arrayparam.Add(new mDataDdl { N_ID = "@p_hechos", N_VALOR = resena.Hechos });
                arrayparam.Add(new mDataDdl { N_ID = "@p_primeraInstancia", N_VALOR = resena.PrimeraInstancia });
                arrayparam.Add(new mDataDdl { N_ID = "@p_observaciones1raInstancia", N_VALOR = resena.Observaciones1raInstancia });
                arrayparam.Add(new mDataDdl { N_ID = "@p_segundaInstancia", N_VALOR = resena.SegundaInstancia });
                arrayparam.Add(new mDataDdl { N_ID = "@p_observaciones2daInstancia", N_VALOR = resena.Observaciones2daInstancia });
                arrayparam.Add(new mDataDdl { N_ID = "@p_impugnacion", N_VALOR = resena.Impugnacion });
                arrayparam.Add(new mDataDdl { N_ID = "@p_especialProteccion", N_VALOR = resena.Especialproteccion });
                arrayparam.Add(new mDataDdl { N_ID = "@p_tipoSujetoEspecialProteccion", N_VALOR = resena.TipoSujetoEspecialproteccion });
                arrayparam.Add(new mDataDdl { N_ID = "@p_origenSujeto", N_VALOR = resena.OrigenSujeto });
                arrayparam.Add(new mDataDdl { N_ID = "@p_extranjeroSujeto", N_VALOR = resena.ExtranjeroSujeto });
                arrayparam.Add(new mDataDdl { N_ID = "@p_condicionPersonalSujeto", N_VALOR = resena.CondicionPersonalSujeto });
                arrayparam.Add(new mDataDdl { N_ID = "@p_condicionDiscapacidadSujeto", N_VALOR = resena.CondicionDiscapacidadSujeto });
                arrayparam.Add(new mDataDdl { N_ID = "@p_condicionSocialSujeto", N_VALOR = resena.CondicionSocialSujeto });
                arrayparam.Add(new mDataDdl { N_ID = "@p_restitucionDeTierrasSujeto", N_VALOR = resena.RestitucionDeTierrasSujeto });
                arrayparam.Add(new mDataDdl { N_ID = "@p_criteriosObjetivos", N_VALOR = resena.CriteriosObjetivos });
                arrayparam.Add(new mDataDdl { N_ID = "@p_criteriosSubjetivos", N_VALOR = resena.CriteriosSubjetivos });
                arrayparam.Add(new mDataDdl { N_ID = "@p_criteriosComplementarios", N_VALOR = resena.CriteriosComplementarios });
                arrayparam.Add(new mDataDdl { N_ID = "@p_criteriosAdicionales", N_VALOR = resena.CriteriosAdicionales });
                arrayparam.Add(new mDataDdl { N_ID = "@p_problemaJuridico", N_VALOR = resena.ProblemaJuridico });
                arrayparam.Add(new mDataDdl { N_ID = "@p_observacionesProblemaJuridico", N_VALOR = resena.ObservacionesProblemaJuridico });
                arrayparam.Add(new mDataDdl { N_ID = "@p_OrientacionSexual", N_VALOR = resena.OrientacionSexual });
                arrayparam.Add(new mDataDdl { N_ID = "@p_ObservacionesImpugnacion", N_VALOR = resena.ObservacionesImpugnacion });
                arrayparam.Add(new mDataDdl { N_ID = "@NumeroRadicados", N_VALOR = NumProceso.ToString()});
                arrayparam.Add(new mDataDdl { N_ID = "@p_IdUsuario", N_VALOR = IdUsuario });
                arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

                var result = dal.Ejecutar("sp_insResena", ref mensajerror, "sp", arrayparam);

                return Json(new { data = result, msgError = mensajerror }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { data = false, msgError = "El objeto reseña es nulo" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CambiarEstadoProceso(string NumProceso, string IdEstado)
        {
            DAL dal = new DAL();
            string mensajerror = "";

            List<mDataDdl> arrayparam = new List<mDataDdl>();

            arrayparam.Add(new mDataDdl { N_ID = "@p_numeroProceso", N_VALOR = NumProceso });
            arrayparam.Add(new mDataDdl { N_ID = "@p_idEstado", N_VALOR = IdEstado });
            arrayparam.Add(new mDataDdl { N_ID = "@err_message", N_VALOR = "" });

            var result = dal.Ejecutar("sp_updEstadoRadicado", ref mensajerror, "sp", arrayparam);

            return Json(new { data = result, msgError = mensajerror }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GuardarFormularioEDP(mInformeExpedienteDespacho camposFormulario)
        {
            DAL dal = new DAL();
            var ValorUrl = Request.UrlReferrer.Query.Split('=', '&');
            int NumProceso = Convert.ToInt32(ValorUrl[5]);
            return Json(new { data = dal.ins_InformeExpedienteDespacho(camposFormulario, NumProceso) });
        }

        #endregion

        #region ConsultaSeleccion

        public ActionResult ConsultaSeleccion()
        {
            return View();
        }
        #endregion

        [HttpPost]
        public ActionResult UploadFile(string peticionario)
        {
            DAL dal = new DAL();

            try
            {
                var fileContent = Request.Files[0];
                var ValorUrl = Request.UrlReferrer.Query.Split('=', '&');
                string NumProceso = ValorUrl[1];

                if (fileContent != null)
                {
                    var fileExtencion = Path.GetExtension(fileContent.FileName);

                    if (fileExtencion == ".doc" || fileExtencion == ".DOC"
                         || fileExtencion == ".pdf" || fileExtencion == ".PDF"
                         || fileExtencion == ".doc" || fileExtencion == ".DOC"
                         || fileExtencion == ".doc" || fileExtencion == ".DOC")
                    {
                        var path = "/Archivos/Insistencia" + fileContent.FileName;
                        fileContent.SaveAs(Server.MapPath(path));
                        return Json(new { data = dal.InsInsistencia(path, NumProceso, peticionario.ToString().Replace('"', ' ').Trim()) });
                    }
                    return Json(new { data = "Tipo de archivo no admitido" });
                }

            }
            catch (Exception Ex)
            {
                var dato = Ex;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { data = false, result = "" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = true, result = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllFiles(jQueryDataTableParams param)
        {


            var ValorUrl = Request.UrlReferrer.Query.Split('=', '&');
            string NumProceso = ValorUrl[1];
            DAL consulta = new DAL();
            var resultados = (consulta.GetAllArchivosAdjuntos(NumProceso));
            int totalCount = resultados.Count();

            IEnumerable<mConsulta> filteredConsulta = resultados;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredConsulta = filteredConsulta
                        .Where(m => m.NumeroProceso.Contains(param.sSearch)
                                || m.NumeroProceso.Contains(param.sSearch)
                                || m.Estado.Contains(param.sSearch));
            }

            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<mConsulta, string> orderingFunction =
                (
                    m => sortIdx == 0 ? m.ProcesoRadicado :
                         sortIdx == 1 ? m.NumeroProceso :
                         sortIdx == 2 ? m.NumeroRadicadoUnico :
                    m.NumeroRadicado
                );

            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredConsulta = filteredConsulta.OrderBy(orderingFunction);
            else
                filteredConsulta = filteredConsulta.OrderByDescending(orderingFunction);
            var displayedMembers = filteredConsulta
               .Skip(param.iDisplayStart)
               .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from x in displayedMembers
                         select new
                         {
                             x.NumeroRadicadoUnico,
                             x.ProcesoRadicado,
                             x.AdjuntoInsistencia,
                             x.AdjuntoEscrito,
                             x.EscritoCiudadano,
                             x.Insistencia,
                         };

            //return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredConsulta.Count(),
                aaData = result
            },
           JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult UploadFileEscrito(string peticionario)
        {
            DAL dal = new DAL();
            try
            {

                var fileContent = Request.Files[0];
                var ValorUrl = Request.UrlReferrer.Query.Split('=', '&');
                string NumProceso = ValorUrl[1];
                int NumRadicado = Convert.ToInt32(ValorUrl[5]);

                string IdUsuario = Session["IdUsuario"].ToString();

                if (fileContent != null)
                {
                    var fileExtencion = Path.GetExtension(fileContent.FileName);

                    if (fileExtencion == ".doc" || fileExtencion == ".DOC"
                         || fileExtencion == ".pdf" || fileExtencion == ".PDF"
                         || fileExtencion == ".doc" || fileExtencion == ".DOC"
                         || fileExtencion == ".doc" || fileExtencion == ".DOC")
                    {
                        var path = "/Archivos/EscritoCiudadano" + fileContent.FileName;
                        fileContent.SaveAs(Server.MapPath(path));

                        return Json(new
                        {
                            data = dal.InsEscritos(path, NumProceso, NumRadicado, IdUsuario, peticionario.ToString().Replace('"', ' ').Trim())
                        });
                    }
                    return Json(new { data = "Tipo de archivo no admitido" });
                }




            }
            catch (Exception Ex)
            {
                var dato = Ex;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { data = false, result = "" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = true, result = "" }, JsonRequestBehavior.AllowGet);
        }


        #region GetResenaByNumProceso
        public ActionResult GetResenaByNumProceso(string numProceso)
        {
            DAL dal = new DAL();
            return Json(dal.GetResenaByNumProceso(numProceso), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetEstadoByNumProceso
        public ActionResult GetEstadoByNumProceso(string numProceso)
        {
            DAL dal = new DAL();
            return Json(dal.GetEstadoByNumProceso(numProceso), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetDespachoById
        public ActionResult GetDespachoById(int id)
        {
            DAL dal = new DAL();
            return Json(dal.getDespachoById(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Auto sala de selección

        [HttpGet]
        public ActionResult GetAutoProcesosSeleccionados()
        {
            DAL dal = new DAL();
            return Json(dal.get_SeleccionadosParaRevision(), JsonRequestBehavior.AllowGet);
        }
        #endregion

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