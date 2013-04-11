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
                //在确定接收用的情况下，选出每个发送用户发送的最晚那条记录
                //Privates =  from x in pr.Privates
                //            group x by x.sUserID into g
                //            let y = (from z in g
                //                     orderby z.sTime descending
                //                     select z).FirstOrDefault()
                //            select y

                //找到所有与该用户相关的私信组
                GPrivates = gpr.GPrivates.Where(g => g.rUserID.Equals(userID))
                                         .OrderByDescending(g => g.lTime)

            };

            return View(pv);
        }

        public ViewResult DetailList(string rUserID, string sUserID) {
            //传入选中用户对当前用户的私信,按时间排列
            PrivateView pv = new PrivateView() {
                Privates = pr.Privates.Where(p => p.rUserID == rUserID && p.sUserID == sUserID
                                                || p.sUserID == rUserID && p.rUserID == sUserID)
                                      .OrderByDescending(p => p.sTime)
            };

            return View(pv);
        }

        [HttpPost]
        public ActionResult SendPrivate(string sUserID, string rUserID) {


            return RedirectToAction("Index", "Home");
        }

    }
}
