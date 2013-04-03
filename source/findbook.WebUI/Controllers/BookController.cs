using System.Linq;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBooksRepository bookRepository;
        private IBookCommentsRepository bookCommentRepository;

        public BookController(IBooksRepository br, IBookCommentsRepository bcr) {
            bookRepository = br;
            bookCommentRepository = bcr;
        }

        public ActionResult List(string bookID)
        {
            BookViewModel bv = new BookViewModel {
                //当前图书
                Books = bookRepository.Books.FirstOrDefault(u => u.bookID.Equals(bookID)),

                //对当前图书的评论，并按时间降序排序
                BookComments = bookCommentRepository.BookComments
                            .Where(b => b.bookID.Equals(bookID))
                            .OrderByDescending(b => b.cTime)
            };

            return View(bv);
        }

    }
}
