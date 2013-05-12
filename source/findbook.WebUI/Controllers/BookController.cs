using System.Linq;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System;
using findbook.Domain.Entities;

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

        public ActionResult List(string bookID, int page = 1) {
            int PageSize = 4;

            BookViewModel bv = new BookViewModel {
                //当前图书
                Books = br.Books.FirstOrDefault(u => u.bookID.Equals(bookID)),

                //对当前图书的评论，并按时间降序排序
                BookComments = bcr.BookComments
                            .Where(b => b.bookID.Equals(bookID))
                            .OrderByDescending(b => b.cTime)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                BK = new PageInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = bcr.BookComments
                            .Where(l => l.bookID.Equals(bookID))
                            .Count()
                }
            };

            bv.HisBooks = br.Books.Where(b => b.upUserID == bv.Books.upUserID 
                                                    && b.bookID != bv.Books.bookID);

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

        public ActionResult Agree(string bcID) {
            int agreement = 0;

            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {
                    //先更新
                    string updateSql = String.Format("update BookComments set agreement = agreement + 1 where bCommentID = '{0}'", bcID);
                    cmd.CommandText = updateSql;
                    cmd.ExecuteNonQuery();

                    //再查询
                    String selectSql = String.Format("select agreement from BookComments where bCommentID = '{0}'", bcID);
                    cmd.CommandText = selectSql;
                    agreement = (int)cmd.ExecuteScalar();
                }
            }

            //跳转到原先的界面
            string url = HttpContext.Request.UrlReferrer.ToString();

            return Redirect(url);
        }

        public ActionResult Recommend(string bookID) {
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {

                    String updateSql = String.Format("update Books set recNumber = recNumber + 1 where bookID = '{0}'", bookID);
                    cmd.CommandText = updateSql;
                    cmd.ExecuteNonQuery();
                }
            }

            //跳转到原先的界面
            string url = HttpContext.Request.UrlReferrer.ToString();

            return Redirect(url);
        }

        public ActionResult Edit(string bookID) {
            Books book = br.Books.FirstOrDefault(b => b.bookID.Equals(bookID));

            return View(book);   
        }

        [HttpPost]
        public ActionResult Edit(Books model) {
            if (ModelState.IsValid) {
                Books originalModel = br.Books
                                .FirstOrDefault(b => b.bookID.Equals(model.bookID));

                //根据model中的信息修改数据库对应的信息
                originalModel.bookName = model.bookName;
                originalModel.author = model.author;
                originalModel.pub = model.pub;
                originalModel.remNumber = model.remNumber;
                originalModel.bookPrice = model.bookPrice;
                originalModel.bookIntr = model.bookIntr;

                br.SaveBook();

                string url = HttpContext.Request.UrlReferrer.ToString();

                return Redirect(url);
            }

            return View();
        }

        public ActionResult Delete(string bookID) {
            Books book = br.Books.FirstOrDefault(b => b.bookID.Equals(bookID));
            br.DeleteBook(book);

            string url = HttpContext.Request.UrlReferrer.ToString();

            return Redirect(url);
        }

    }
}
