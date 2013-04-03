using System.Linq;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers
{
    public class PageController : Controller
    {
        private IPagesRepository pageRepository;
        private IUsersRepository userRepository;
        private ILeaveCommentsRepository leaveCommentsRepository;

        public PageController(IUsersRepository ur, IPagesRepository pr, ILeaveCommentsRepository lcr) {
            userRepository = ur;
            pageRepository = pr;
            leaveCommentsRepository = lcr;
        }

        public ActionResult List(string userID)
        {
            PageViewModel pvm = new PageViewModel {
                //传入View中的是用户点击查看主页的当前用户
                Users = userRepository.Users.FirstOrDefault(u => u.userID.Equals(userID)),
                Pages = pageRepository.Pages.FirstOrDefault(p => p.pageID.Equals(userID)),

                //传入对当前用户的留言，按时间降序排列
                LeaveComments = leaveCommentsRepository.LeaveComments
                            .Where(l => l.hUserID.Equals(userID))
                            .OrderByDescending(l => l.lcTime)
            };

            return View(pvm);
        }

    }
}
