using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.WebUI.Models;
using findbook.Domain.Abstract;

namespace findbook.WebUI.Controllers
{
    public class PrivateController : Controller
    {
        private IPrivatesRepository pr;
        private IGPrivatesRepository gpr;

        public PrivateController(IPrivatesRepository privateRepository, IGPrivatesRepository gprivateRepository) {
            pr = privateRepository;
            gpr = gprivateRepository;
        }

        public ViewResult List(string userID) {
            PrivateView pv = new PrivateView() {
                //找到所有与该用户相关的私信组
                GPrivates = gpr.GPrivates.Where(g => g.rUserID.Equals(userID))
                                         .OrderByDescending(g => g.lTime)

            };

            return View(pv);
        }

        //传入参数为另一位用户的ID
        public ViewResult DetailList(string anotherUserID) {
            //发送用户为当前用户，故可从cookie中获取userName
            HttpCookie cookie = Request.Cookies["user"];
            string sUserID = cookie["userID"].ToString();
            
            //传入选中用户对当前用户的私信,按时间排列
            PrivateView pv = new PrivateView() {
                Privates = pr.Privates.Where(p => p.rUserID == anotherUserID && p.sUserID == sUserID
                                                || p.sUserID == anotherUserID && p.rUserID == sUserID)
                                      .OrderByDescending(p => p.sTime)
            };

            return View(pv);
        }

        [HttpPost]
        public ActionResult SendPrivate(string sUserID, string rUserID) {
            //发送用户为当前用户，故可从cookie中获取userName
            HttpCookie cookie = Request.Cookies["user"];
            string sUserName = cookie["userName"].ToString();

            //从session中获取接收用户的userName
            string rUserName = HttpContext.Session["rUserName"].ToString();

            //从textarea中获得私信提
            string pmBody = HttpContext.Request["pmBody"];

            //调用存储过程
            if (pr.SendPrivate(sUserID, sUserName, rUserID, rUserName, pmBody)) {
                return RedirectToRoute(new {
                                    Controller = "Private",
                                    Action = "DetailList",
                                    anotherUserID = rUserID
                                });
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
