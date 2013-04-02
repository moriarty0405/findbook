using System.Web.Mvc;
using findbook.WebUI.Models.Account;
using findbook.Domain.Abstract;
using System.Web.Security;
using findbook.Domain.Entities;
using System;
using System.Web;

namespace findbook.WebUI.Controllers
{
    public class AccountController : Controller {
        private IUsersRepository userRepository;

        public AccountController(IUsersRepository ur) {
            userRepository = ur;
        }

        public ViewResult LogOn() {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl) {
            if (ModelState.IsValid) {
                Users user = userRepository.GetByNameAndPassword(model.UserName, model.Password);

                if (user != null) {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        user.userName,
                        DateTime.Now,
                        DateTime.Now.Add(FormsAuthentication.Timeout),
                        model.RememberMe,
                        user.userRole
                    );
                    HttpCookie cookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket));
                    Response.Cookies.Add(cookie);
                    Session["userID"] = user.userID;

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

        public ActionResult LogOff() {
            //删除用户验证信息
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ViewResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            int status = 0;

            if (ModelState.IsValid) {
                //尝试创建用户
                userRepository.CreateUser(model.UserName, model.NewPassword, model.Email, model.Sex, model.XQ, model.XY, model.ZY);
                status = 1;

                if (status == 1) {
                        FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                        return RedirectToAction("LogOn", "Account");
                } else {
                        //ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
