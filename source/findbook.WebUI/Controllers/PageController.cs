using System.Linq;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Controllers
{
    public class PageController : Controller
    {
        private IPagesRepository pr;
        private IUsersRepository ur;
        private ILeaveCommentsRepository lcr;
        private IBooksRepository br;

        public PageController(IUsersRepository userRepository, IPagesRepository pageRepository, 
            ILeaveCommentsRepository leaveCommentsRepository, IBooksRepository bookRepository) {
            ur = userRepository;
            pr = pageRepository;
            lcr = leaveCommentsRepository;
            br = bookRepository;
        }

        public ActionResult List(string userID)
        {
            PageViewModel pvm = new PageViewModel {
                //传入View中的是用户点击查看主页的当前用户
                Users = ur.Users.FirstOrDefault(u => u.userID.Equals(userID)),
                Pages = pr.Pages.FirstOrDefault(p => p.pageID.Equals(userID)),

                //传入对当前用户的留言，按时间降序排列
                LeaveComments = lcr.LeaveComments
                            .Where(l => l.hUserID.Equals(userID))
                            .OrderByDescending(l => l.lcTime),
                
                //传入当前用户上传的图书
                Books = br.Books.Where(b => b.upUserID.Equals(userID))
                                .OrderByDescending(b => b.upTime)
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult LeaveComment() {
            //从cookie中取出lUser的信息
            HttpCookie cookie = Request.Cookies["user"];
            string lUserID = cookie["userID"].ToString();
            string lUserName = cookie["userName"].ToString();

            //从Request中取出信息
            string lBody = Request["lBody"];
            string hUserID = Session["hUserID"].ToString();

            //利用ur，取出hUserName
            Users user = ur.Users.FirstOrDefault(u => u.userID.Equals(hUserID));
            string hUserName = user.userName;

            if (lcr.LeaveComment(lUserID, lUserName, hUserID, hUserName, lBody)) {
                return RedirectToAction("List", "Page");
            }
            
            return View();
        }



    }
}
