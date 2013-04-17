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
        private IUsersRepository ur;

        public AccountController(IUsersRepository userRepository) {
            ur = userRepository;
        }

        #region 登录
        public ViewResult LogOn() {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl) {
            if (ModelState.IsValid) {

                #region 向cookie中写入信息
                Users user = ur.GetByNameAndPassword(model.UserName, model.Password);

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

                    //向cookie中写入用户信息
                    HttpCookie u = new HttpCookie("user");
                    u["userID"] = user.userID;
                    u["userName"] = user.userName;

                    Response.Cookies.Add(u);

                    Session["logOnUserID"] = user.userID;
                    Session["logOnUserName"] = user.userName;
                #endregion

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
        #endregion

        public ActionResult LogOff() {
            //删除用户验证信息
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #region 注册
        public ViewResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            int status = 0;

            if (ModelState.IsValid) {
                //尝试创建用户
                ur.CreateUser(model.UserName, model.NewPassword, model.Email, model.Sex, model.XQ, model.XY, model.ZY);
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
        #endregion
    }
}
