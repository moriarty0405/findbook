using System.Web.Mvc;
using System.Linq;
using System.Web;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Collections.Generic;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers {
    public class InfoController : Controller {
        private IBooksRepository br;
        private IWantedRepository wr;
        private IUsersRepository ur;
        private IPwModifyRepository pr;


        public InfoController(IBooksRepository bookRepository, IUsersRepository userRepository,
            IPwModifyRepository pwModifyRepository, IWantedRepository wantedRepository) {

            br = bookRepository;
            wr = wantedRepository;
            ur = userRepository;
            pr = pwModifyRepository;
        }

        #region 上传图书
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
        #endregion

        #region 我的资料
        public ViewResult Profile(string userID) {
            Users user = ur.Users.FirstOrDefault(u => u.userID.Equals(userID));

            return View(user);
        }

        [HttpPost]
        public ActionResult Profile() {
            if (ModelState.IsValid) {
                ur.SaveUser();

                return RedirectToAction("List", "Page");
            }

            return View();
        }
        #endregion

        #region 修改密码
        public ViewResult PassWord() {
            return View();
        }

        [HttpPost]
        public ActionResult PassWord(string userID, PwModify model) {
            
            if (ModelState.IsValid) {
                //如果修改成功就回到List
                if (pr.Modify(userID, model.forePw, model.newPw)) {
                    return RedirectToAction("List", "Page");
                }
            }
            
            return View();
        }
        #endregion

        #region 发布求购
        public ViewResult Want() {
            return View();
        }

        [HttpPost]
        public ActionResult Want(string userID, Wanted model) {
            //从cookie中获取userName
            HttpCookie cookie = Request.Cookies["user"];
            string userName = cookie["userName"].ToString();

            if (ModelState.IsValid) {
                if (wr.PostWanted(model.bookName, model.author, model.pub, model.num, userID, userName)) {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
        #endregion
    }
}
