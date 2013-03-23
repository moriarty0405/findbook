using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.WebUI.Infrastructure.Abstract;
using findbook.WebUI.Models.Account;

namespace findbook.WebUI.Controllers
{
        public class AccountController : Controller {
                IAuthProvider authProvider;

                public AccountController(IAuthProvider auth) {
                        authProvider = auth;
                }

                public ViewResult LogOn() {
                        return View();
                }

                [HttpPost]
                public ActionResult LogOn(LogOnModel model, string returnUrl) {
                        if (ModelState.IsValid) {
                                if (authProvider.Authenticate(model.UserName, model.Password)) {
                                        return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                                } else {
                                        ModelState.AddModelError("", "Incorrect username or password");
                                        return View();
                                }
                        } else {
                                return View();
                        }
                }
        }
}
