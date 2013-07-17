using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.WebUI.Models;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

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
        public ViewResult DetailList(string anotherUserID, int page = 1) {
            int pageSize = 6;
            
            //发送用户为当前用户，故可从session中获取userName
            //string sUserID = Session["logOnUserID"].ToString();
            HttpCookie cookie = Request.Cookies["user"];

            string sUserID = cookie["userID"].ToString();


            //传入选中用户对当前用户的私信,按时间排列, 并获取分页后记录
            PrivateView pv = new PrivateView() {
                Privates = pr.Privates.Where(p => p.rUserID == anotherUserID && p.sUserID == sUserID
                                                  || p.sUserID == anotherUserID && p.rUserID == sUserID)
                                        .OrderByDescending(p => p.sTime)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize),

                //PrivateDetail分页信息
                PD = new PageInfo {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = pr.Privates.Where(p => p.rUserID == anotherUserID && p.sUserID == sUserID
                                                  || p.sUserID == anotherUserID && p.rUserID == sUserID)
                                            .Count()
                }
            };

            return View(pv);
        }

        [HttpPost]
        public ActionResult SendPrivate(string sUserID, string rUserID) {
            //发送用户为当前用户，故可从session中获取userName
            //string sUserName = Session["logOnUserName"].ToString();

            //从cookie中获取userID
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

        [HttpPost]
        public ActionResult SendPrivateBox() {
            //发送用户为当前用户，故可从session中获取user信息
            //string sUserID = Session["logOnUserID"].ToString();
            //string sUserName = Session["logOnUserName"].ToString();

            //从cookie中获取userID
            HttpCookie cookie = Request.Cookies["user"];

            string sUserID = cookie["userID"].ToString();
            string sUserName = cookie["userName"].ToString();

            //从表单中获取接收用户信息
            string rUserID = HttpContext.Request["userID"];
            string rUserName = HttpContext.Request["userName"];

            //从textarea中获得私信提取
            string pmBody = HttpContext.Request["privateBody"];

            //跳转到原先的界面
            string url = HttpContext.Request.UrlReferrer.ToString();

            //调用存储过程
            if (pr.SendPrivate(sUserID, sUserName, rUserID, rUserName, pmBody)) {

                return Redirect(url);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteGroup(string gpId) {
            GPrivates gp = gpr.GPrivates.FirstOrDefault(g => g.GPID.Equals(gpId));
            gpr.Delete(gp);

            string url = HttpContext.Request.UrlReferrer.ToString();

            return Redirect(url);
        }

        public ActionResult Delete(string pmID) {
            Privates pri = pr.Privates.FirstOrDefault(p => p.pmID.Equals(pmID));
            pr.Delete(pri);

            string url = HttpContext.Request.UrlReferrer.ToString();

            return Redirect(url);
        }

    }
}
