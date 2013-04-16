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

        public ActionResult List(string userID, int page = 1)
        {
            int PageSize = 4;

            PageViewModel pvm = new PageViewModel {
                //传入View中的是用户点击查看主页的当前用户
                Users = ur.Users.FirstOrDefault(u => u.userID.Equals(userID)),
                Pages = pr.Pages.FirstOrDefault(p => p.pageID.Equals(userID)),

                //传入对当前用户的留言，按时间降序排列,并获取分页后当前页面的评论数
                LeaveComments = lcr.LeaveComments
                            .Where(l => l.hUserID.Equals(userID))
                            .OrderByDescending(l => l.lcTime)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                //传入当前用户上传的图书
                Books = br.Books.Where(b => b.upUserID.Equals(userID))
                                .OrderByDescending(b => b.upTime),

                //分页信息
                LC = new PageInfo { 
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lcr.LeaveComments
                            .Where(l => l.hUserID.Equals(userID))        
                            .Count()
                }
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult List(string userID, string userName) {
            //从cookie中获取userID
            HttpCookie cookie = Request.Cookies["user"];
            string lUserID = cookie["userID"].ToString();
            string lUserName = cookie["userName"].ToString();

            string lBody = HttpContext.Request["lBody"];

            if (lcr.LeaveComment(lUserID, lUserName, userID, userName, lBody)) {
                return RedirectToRoute(new {
                    Controller = "Page",
                    Action = "List",
                    userID = userID
                });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
