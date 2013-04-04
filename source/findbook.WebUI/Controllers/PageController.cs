using System.Linq;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers
{
    public class PageController : Controller
    {
        private IPagesRepository pr;
        private IUsersRepository ur;
        private ILeaveCommentsRepository lcr;

        public PageController(IUsersRepository userRepository, IPagesRepository pageRepository, ILeaveCommentsRepository leaveCommentsRepository) {
            ur = userRepository;
            pr = pageRepository;
            lcr = leaveCommentsRepository;
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
                            .OrderByDescending(l => l.lcTime)
            };

            return View(pvm);
        }

    }
}
