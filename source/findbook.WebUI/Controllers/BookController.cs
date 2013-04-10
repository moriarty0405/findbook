using System.Linq;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;
using System.Web;

namespace findbook.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBooksRepository br;
        private IBookCommentsRepository bcr;

        public BookController(IBooksRepository bookRepository, IBookCommentsRepository bookCommentRepository) {
            br = bookRepository;
            bcr = bookCommentRepository;
        }

        public ActionResult List(string bookID) {
            BookViewModel bv = new BookViewModel {
                //当前图书
                Books = br.Books.FirstOrDefault(u => u.bookID.Equals(bookID)),

                //对当前图书的评论，并按时间降序排序
                BookComments = bcr.BookComments
                            .Where(b => b.bookID.Equals(bookID))
                            .OrderByDescending(b => b.cTime)
            };

            return View(bv);
        }

        [HttpPost]
        public ActionResult List(string bookID, string userID) {
            //从cookie中获取userID
            HttpCookie cookie = Request.Cookies["user"];
            string userName = cookie["userName"].ToString();

            string bookName = br.Books.FirstOrDefault(b => b.bookID.Equals(bookID)).bookName;
            string cBody = HttpContext.Request["cBody"];

            if (bcr.Comment(bookID, bookName, userID, userName, cBody)) {
                return RedirectToRoute(new {
                                            Controller = "Book",
                                            Action = "List",
                                            bookID = bookID
                                        });
            }
            
            return RedirectToAction("Index", "Home");
        }

    }
}
