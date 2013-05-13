using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;
using findbook.Domain.Entities;
using findbook.WebUI.Models.Account;
using System.Drawing;

namespace findbook.WebUI.Controllers
{
    public class HomeController : Controller {
        private IBooksRepository br;
        private IWantsRepository wr;
        private IXYRepository xyr;
        private IZYRepository zyr;
        private IUsersRepository ur;
        private IBookCommentsRepository bcr;

        public HomeController(IBooksRepository bookRepository, IWantsRepository wantedRepository, 
            IXYRepository xyRepository, IZYRepository zyRepository, 
            IUsersRepository userRepository, IBookCommentsRepository bookCommentRepository) {
            br = bookRepository;
            wr = wantedRepository;
            xyr = xyRepository;
            zyr = zyRepository;
            ur = userRepository;
            bcr = bookCommentRepository;
        }

        public ActionResult Index() {
            IndexView iv = new IndexView() {
                Wants = wr.Wants.OrderByDescending(w => w.wTime),
                NewBook = br.Books.Where(b => b.remNumber > 0)
                                  .OrderByDescending(b => b.upTime)
                                  .Take(12),

                PopularBook = br.Books.Where(b => b.remNumber > 0)
                                      .OrderByDescending(b => b.cNumber)
                                      .Take(12),

                //获取学院和专业信息
                XYView = new XYView() { 
                    XY = xyr.XY,
                    ZY = zyr.ZY
                }
            };

            return View(iv);
        }

        public ActionResult Free() {
            IEnumerable<Books> Books = br.Books.Where(b => b.bookPrice == 0 && b.remNumber > 0)
                                               .OrderByDescending(b => b.upTime);

            return View(Books);
        }

        public ActionResult Recommend() {
            IEnumerable<Books> Books = br.Books.Where(b => (b.recNumber > 0 || b.cNumber > 0) && b.remNumber > 0)
                                               .OrderByDescending(b => b.upTime);

            return View(Books);
        }

        public ActionResult Find(string userID) {
            FindView fv = new FindView() {
                //显示购买数量最多和销售数量最多的用户
                Users = ur.Users.Where(u => u.userID != userID)
                                .OrderBy(u => u.userRegDate)
                                .Take(6),

                //显示相同学院或专业的用户的动态
                //图书上传
                Books = br.Books.Where(b => b.upUserID != userID)
                                .OrderByDescending(b => b.upTime)
                                .Take(6),

                //图书评论
                BookComments = bcr.BookComments.Where(bc => bc.userID != userID)
                                               .OrderByDescending(bc => bc.cTime)
                                               .Take(6)
                
            };

            return View(fv);
        }

    }
}
