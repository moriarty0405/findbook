using System.Web.Mvc;
using findbook.Domain.Entities;
using System.Web;
using findbook.Domain.Abstract;

namespace findbook.WebUI.Controllers {
    public class InfoController : Controller {
        private IBooksRepository br;

        public InfoController(IBooksRepository bookRepository) {
            br = bookRepository;
        }

        public ViewResult UpLoad() {
            return View();
        }

        [HttpPost]
        public ActionResult UpLoad(string userID, Books model) {
            int status = 0;

            if (ModelState.IsValid) {
                //从cookie中获取userName
                HttpCookie cookie = Request.Cookies["user"];
                string userName = cookie["userName"].ToString();

                br.UpLoadBook(model.bookName, userID, userName, "111", model.bookIntr, model.bookSale, model.bookPrice, model.author, model.pub, model.bookXY, model.bookZY);
                status = 1;

                if (status == 1) {
                    //如果发布成功则跳转到Page中的list界面
                    return RedirectToAction("List", "Page");
                }
            }

            return View(model);
        }
    }
}
