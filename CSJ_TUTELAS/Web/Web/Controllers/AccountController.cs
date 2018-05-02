using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using Datos;
using Datos.Modelo;
using System.Collections.Generic;
using System.Web.Routing;
using System.Collections;
using System.Configuration;
using static Web.Controllers.ManageController;

namespace Web.Controllers
{
    /// <summary>
    /// Clase Controlador para el manejo de las cuentas
    /// </summary>
    /// <seealso cref="Web.BrowseController" />
    [Authorize]
    public class AccountController : BrowseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            context = new ApplicationDbContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        /// <value>
        /// The sign in manager.
        /// </value>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = UserManager.FindByName(model.Email);
            if (user != null)
            {

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

                if (user.UserEnabled == "1")
                {
                    var roles = UserManager.Find(model.Email, model.Password);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            var userDespacho = roles.UserDespacho;
                            Session["UserDespacho"] = userDespacho;

                            var IdUsuario = roles.Id;
                            Session["IdUsuario"] = IdUsuario;

                                var rol = roles.Roles;
                                if (rol.Count > 0)
                                {
                                    foreach (var i in rol)
                                    {

                                        Session["Rol"] = i.RoleId;
                 //Creo La variable de sesion del usuario que se ha logeado ejemplo:Radicador1 tiene le id = "024858c4-f98b-4df2-9017-aeca5a246e3d"
                                        Session["UserId"] = i.UserId;
                                    DAL dal = new DAL();
                                        Session["Permisos"] = dal.get_permissions_by_role(i.RoleId, 0);    
                                    }
                                }

                                Session["nombreUsuario"] = roles.first_name + " " + roles.last_name;
                                //Session["SeccionalUsuario"] = roles.UserSeccional;

                                var s = UserManager.GetRoles(identity.GetUserId());
                                if (s.Count > 0)
                                {
                                    Session["RolName"] = s[0].ToString();
                                    return RedirectToLocal(returnUrl);
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Usuario sin rol.");
                                    return View(model);
                                }

                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Intentos de inicio de sesión no válidos");
                            return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no existe.");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            DAL clase = new DAL();
            IEnumerable Despachos = clase.getAllDespacho().ToList();

            ViewBag.UserRoles = new SelectList(context.Roles.Where(u => !u.Name.Contains("Administrador")).ToList(), "Id", "Name");
            ViewBag.UserDespacho = new SelectList(Despachos, "Id", "NombreDespacho");

            return View();
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            DAL clase = new DAL();

            if (ModelState.IsValid)
            {
                bool valid = true;
                var result=  new IdentityResult();
                ApplicationUser us = null;
                us = UserManager.FindByName(model.UserName);

                //Nombre de Usuario existe
                if (us != null)
                {
                    ModelState.AddModelError("", "UserName '" + model.UserName + "' ya existe en la base de datos.");
                    valid = false;
                }

                us = null;
                us = UserManager.FindByEmail(model.Email);
                //Si Email de Usuario existe
                if (us != null)
                {
                    ModelState.AddModelError("", "Email '"+ model.Email + "' ya existe en la base de datos.");
                    valid = false;
                }

                if (valid) {
                    var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, first_name = model.first_name, last_name = model.last_name, UserDespacho = model.UserDespacho, created_date = DateTime.Now, last_activity = DateTime.Now, UserEnabled = "1" };

                    // Configure validation logic for passwords
                    UserManager.PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = int.Parse(ConfigurationManager.AppSettings["PasswordRequiredLength"]),
                        RequireNonLetterOrDigit = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireNonLetterOrDigit"]),
                        RequireDigit = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireDigit"]),
                        RequireLowercase = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireLowercase"]),
                        RequireUppercase = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireUppercase"])
                    };

                    result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        clase.Insert_usuario_rol_new(user.Id, model.UserRoles);


                        return RedirectToAction("ManageUsers", "Account");
                    }
                }

                //IEnumerable Secc = clase.getAllSeccionales().ToList();
                
                ViewBag.UserRoles = new SelectList(context.Roles.Where(u => !u.Name.Contains("Administrador")).ToList(), "Id", "Name");
                //ViewBag.UserSeccional = new SelectList(Secc, "scCodigo", "scNombre");

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        /// <summary>
        /// Forgots the password confirmation.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        /// <summary>
        /// Resets the password confirmation.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        /// <summary>
        /// Externals the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        /// <summary>
        /// Externals the login callback.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        /// <summary>
        /// Externals the login confirmation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        /// <summary>
        /// Externals the login failure.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Usuarios

        /// <summary>
        /// Manages the users.
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageUsers()
        {
            return ListaPermisos("2");
        }

        /// <summary>
        /// Lists the users.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListUsers()
        {
            DAL dal = new DAL();

            var result = dal.get_all_users();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public ActionResult EditUser(string uid)
        {
            DAL dal = new DAL();
            var lista = dal.get_user_info(uid);
            IEnumerable Despachos = dal.getAllDespacho().ToList();

            ViewBag.UserDespacho = new SelectList(Despachos, "Id", "NombreDespacho", lista.UserDespacho);
            ViewBag.User_Role = new SelectList(context.Roles.Where(u => !u.Name.Contains("Administrador")).ToList(), "Id", "Name", lista.User_Role);

            return View(lista);
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(MembershipModel model, FormCollection form)
        {
            var idUser = form["idusuario"].ToString();

            try
            {
                if (ModelState.IsValid)
                {
                    DAL dal = new DAL();

                    var result = dal.Update_user_info(idUser, model.User_NickName, model.User_Name, model.User_LastName, model.User_Mail, model.User_Role, model.UserDespacho);

                    UserManager.PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = int.Parse(ConfigurationManager.AppSettings["PasswordRequiredLength"]),
                        RequireNonLetterOrDigit = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireNonLetterOrDigit"]),
                        RequireDigit = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireDigit"]),
                        RequireLowercase = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireLowercase"]),
                        RequireUppercase = bool.Parse(ConfigurationManager.AppSettings["PasswordRequireUppercase"])
                    };

                    if(form["Password"].ToString() != "")
                    {
                        var token = UserManager.GeneratePasswordResetToken(idUser);                        
                        var res = UserManager.ResetPassword(idUser, token, form["Password"].ToString());

                        var i = res.Errors;

                        if (result)
                        {
                            if (!res.Succeeded)
                            {
                                AddErrors(res);

                                DAL dalc = new DAL();
                                var lista = dalc.get_user_info(idUser);
                                //IEnumerable Secc = dalc.getAllSeccionales().ToList();

                                ViewBag.User_Role = new SelectList(context.Roles.Where(u => !u.Name.Contains("Administrador")).ToList(), "Id", "Name", lista.User_Role);
                                //ViewBag.UserSeccional = new SelectList(Secc, "scCodigo", "scNombre");
                                return View(lista);
                            }
                        }
                    }
                    

                    return RedirectToAction("ManageUsers", "Account");
                }

                ViewBag.User_Role = new SelectList(context.Roles.Where(u => !u.Name.Contains("Administrador")).ToList(), "Id", "Name", model.User_Role);

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ManageUsers", "Account");
            }
        }

        /// <summary>
        /// Updates the user enabled.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="est">The est.</param>
        /// <returns></returns>
        public ActionResult UpdateUserEnabled(string UserId, string est)
        {
            bool resultado = false;
            DAL dal = new DAL();
            resultado = dal.set_status_user(UserId, est);

            return Json(new
            {
                resultado = resultado
            },
               JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Roles

        /// <summary>
        /// llama la vista Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Roles()
        {
            return ListaPermisos("3");
        }

        /// <summary>
        /// Obtiene todas los Ciudades
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public ActionResult getRoles(jQueryDataTableParams param)
        {
            DAL dal = new DAL();
            string user_owner = User.Identity.GetUserId();

            IQueryable<IdentityRole> listRoles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)).Roles.AsQueryable();

            int totalCount = listRoles.Count();

            IEnumerable<IdentityRole> filtroMembers = listRoles;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtroMembers = listRoles
                       .Where(m => m.Id.ToString().Contains(param.sSearch.ToUpper()) ||
                          m.Name.ToUpper().Contains(param.sSearch.ToUpper()));
            }
            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc  

            Func<IdentityRole, string> orderingFunction = ( m => sortIdx == 0 ? m.Id : m.Name );

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
                             a.Id,
                             a.Name
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
        /// Saves the roles.
        /// </summary>
        /// <param name="rl">The rl.</param>
        /// <returns></returns>
        public ActionResult saveRoles(IdentityRole rl)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var IdentityResult = new IdentityResult();

            var role = new IdentityRole();
            role.Name = rl.Name;
            IdentityResult = roleManager.Create(role);

            return Json(new { data = IdentityResult.Success.Succeeded, msgError = string.Join(", ", IdentityResult.Success.Errors) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edits the roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult editRoles(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var result = roleManager.Roles.Where(s => s.Id == id).First();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Upds the roles.
        /// </summary>
        /// <param name="rl">The rl.</param>
        /// <returns></returns>
        public ActionResult updRoles(IdentityRole rl)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var IdentityResult = new IdentityResult();
            var role = new IdentityRole();

            role = roleManager.Roles.Where(s => s.Id == rl.Id).First();
            role.Name = rl.Name;

            IdentityResult = roleManager.Update(role);

            return Json(new { data = IdentityResult.Success.Succeeded, msgError = string.Join(", ", IdentityResult.Success.Errors) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deletes the roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult delRoles(string id)
        {
            bool respuesta = false;
            string msg = "";

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var users = roleManager.FindById(id).Users;

            if (users.Count > 0)
            {
                respuesta = false;
         
                msg = "No se puede eliminar este rol, porque existen usuarios asociados.";
            }
            else
            {
                var result = roleManager.Roles.Where(s => s.Id == id).First();

                var IdentityResult = new IdentityResult();

                IdentityResult = roleManager.Delete(result);

                respuesta = IdentityResult.Success.Succeeded;

                msg = string.Join(", ", IdentityResult.Success.Errors);
            }

            return Json(new { data = respuesta, msgError = msg }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Menu

        /// <summary>
        /// Obtiene todas las opciones de Menú
        /// </summary>
        /// <returns></returns>
        public ActionResult getMenu(string idRol)
        {
            DAL dal = new DAL();

            List<mMenu> listmenu = dal.get_all_menu(idRol);

            //Se devuelven los resultados por json
            return Json( listmenu , JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Permisos

        /// <summary>
        /// Manages the permissions.
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagePermissions()
        {
            var listRoles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)).Roles.ToList();
            ViewBag.DdlPermisions = new SelectList(listRoles, "Id", "Name", "");

            return ListaPermisos("4");
        }

        /// <summary>
        /// Gets the permissions by role.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public ActionResult get_permissions_by_role(jQueryDataTableParams param)
        {
            DAL dal = new DAL();
            string user_owner = User.Identity.GetUserId();
            string idRol = "";
            int idMenu = 0;

            if (Request.QueryString["idRol"] != "")
            {
                idRol = Request.QueryString["idRol"];
            }
            if (Request.QueryString["idMenu"] != "")
            {
                idMenu = Convert.ToInt32(Request.QueryString["idMenu"]);
            }

            IQueryable<PermissionModel> listPermisos = dal.get_permissions_by_role(idRol, idMenu).AsQueryable();
            int totalCount = listPermisos.Count();
            IEnumerable<PermissionModel> filtroMembers = listPermisos;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtroMembers = listPermisos
                       .Where(m => m.Name.ToUpper().Contains(param.sSearch.ToUpper()) ||
                          m.Opciones.ToUpper().Contains(param.sSearch.ToUpper()));
            }
            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"]; // asc or desc 

            if(sortIdx == 0)
            {
                Func<PermissionModel, int> orderingFunction = (m => m.Orden);

                if (sortDirection == "asc")
                    filtroMembers = filtroMembers.OrderBy(orderingFunction);
                else
                    filtroMembers = filtroMembers.OrderByDescending(orderingFunction);
            }
            else
            {
                Func<PermissionModel, string> orderingFunction = (m => sortIdx == 1 ? m.Name : m.Opciones);

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
                             a.Id,
                             a.Name,
                             a.Opciones,
                             a.Permisos,
                             a.Enable,
                             a.Orden 
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
        /// Adds the permissions by role.
        /// </summary>
        /// <param name="idRol">The identifier rol.</param>
        /// <param name="Permisos">The permisos.</param>
        /// <returns></returns>
        public ActionResult add_permissions_by_role(string idRol, string Permisos)
        {
            try
            {
                DAL dal = new DAL();
                var result = dal.add_permissions_by_role(idRol, Permisos);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>
        /// The authentication manager.
        /// </value>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /// <summary>
        /// Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Clase ChallengeResult
        /// </summary>
        /// <seealso cref="System.Web.Mvc.HttpUnauthorizedResult" />
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}