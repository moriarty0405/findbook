﻿using System.Linq;
using System.Web.Mvc;
using findbook.WebUI.Models.Account;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Web.Security;

namespace findbook.WebUI.Controllers
{
        public class AccountController : Controller {
                private IUsersRepository userRepository;

                public AccountController(IUsersRepository ur) {
                        userRepository = ur;
                }

                public bool ValidateUser(string userName, string passWord) {
                        Users u = userRepository.Users.FirstOrDefault(p => p.userName.Equals(userName));

                        return u.userPw.Equals(passWord);
                }

                public ViewResult LogOn() {
                        return View();
                }

                [HttpPost]
                public ActionResult LogOn(LogOnModel model, string returnUrl) {
                        if (ModelState.IsValid) {
                                if (this.ValidateUser(model.UserName, model.Password)) {
                                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\")) {
                                                return Redirect(returnUrl);
                                        } else {
                                                return RedirectToAction("Index", "Home");
                                        }
                                } else {
                                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                                }
                        }

                        return View(model);
                }

                public ViewResult Register() {
                        return View();
                }
        }
}
