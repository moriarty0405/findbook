using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;
using findbook.Domain.Entities;

namespace findbook.WebUI.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchasesRepository pr;
        private IDealsRepository dr;
        private IBooksRepository br;

        public PurchaseController(IPurchasesRepository purchaseRepository, IDealsRepository dealRepository,
            IBooksRepository bookRepository) {
            pr = purchaseRepository;
            dr = dealRepository;
            br = bookRepository;
        }

        #region 对于卖家
        //已完成的交易
        public ActionResult Deal(string userID) {
            //查找用户完成的交易，按时间顺序降序排列
            List<Books> books = br.Books.ToList();
            List<Deals> deals = dr.Deals
                                      .Where(d => d.sUserID == userID)
                                      .ToList();

            var db = (from d in deals join b in books
                                           on d.bookID equals b.bookID
                      orderby d.dTime descending
                      select new DealBook {
                          dID = d.dID,
                          bookID = d.bookID,
                          bookName = d.bookName,
                          pUserID = d.pUserID,
                          pUserName = d.pUserName,
                          dTime = d.dTime,
                          ZT = d.ZT,
                          sUserID = d.sUserID,
                          sUserName = d.sUserName,
                          price = d.price,
                          number = d.number,
                          author = b.author,
                          pub = b.pub
                      }).ToList();

            return View(db);
        }

        //未完成的交易
        public ActionResult UnDeal(string userID) {
            //查找用户的购买信息,其中不包括已完成的交易，按时间顺序降序排列
            List<Books> books = br.Books.ToList();
            List<Purchases> purchases = (pr.Purchases
                                      .Where(p => p.sUserID == userID
                                          && p.sta != "3")).ToList();
                                            //不在此界面中显示已完成的交易                            

            var pb = (from d in purchases join b in books
                                           on d.bookID equals b.bookID
                      orderby d.pTime descending
                      select new PurchaseBook {
                          pID = d.pID,
                          bookID = d.bookID,
                          bookName = d.bookName,
                          pUserID = d.pUserID,
                          pUserName = d.pUserName,
                          sta = d.sta,
                          pTime = d.pTime,
                          ZT = d.ZT,
                          sUserID = d.sUserID,
                          sUserName = d.sUserName,
                          price = d.price,
                          number = d.number,
                          author = b.author,
                          pub = b.pub
                      }).ToList();

            return View(pb);

        }
        #endregion

        #region 买家
        //已完成的交易
        public ActionResult Purchased(string userID) {
            //查找用户完成的交易，按时间顺序降序排列
            List<Books> books = br.Books.ToList();
            List<Deals> deals = dr.Deals
                                      .Where(d => d.pUserID == userID)
                                      .ToList();

            var db = (from d in deals join b in books
                                           on d.bookID equals b.bookID
                      orderby d.dTime descending
                      select new DealBook {
                          dID = d.dID,
                          bookID = d.bookID,
                          bookName = d.bookName,
                          pUserID = d.pUserID,
                          pUserName = d.pUserName,
                          dTime = d.dTime,
                          ZT = d.ZT,
                          sUserID = d.sUserID,
                          sUserName = d.sUserName,
                          price = d.price,
                          number = d.number,
                          author = b.author,
                          pub = b.pub
                      }).ToList();

            return View(db);
        }

        //未完成的交易
        public ActionResult UnPurchase(string userID) {
            //查找用户的购买信息,其中不包括已完成的交易，按时间顺序降序排列
            List<Books> books = br.Books.ToList();
            List<Purchases> purchases = (pr.Purchases
                                      .Where(p => p.pUserID == userID
                                          && p.sta != "3" && p.sta != "1")).ToList();
                                        //不在此界面中显示，已完成交易和已取消交易的图书

            var pb = (from d in purchases join b in books
                                           on d.bookID equals b.bookID
                      orderby d.pTime descending
                      select new PurchaseBook {
                          pID = d.pID,
                          bookID = d.bookID,
                          bookName = d.bookName,
                          pUserID = d.pUserID,
                          pUserName = d.pUserName,
                          sta = d.sta,
                          pTime = d.pTime,
                          ZT = d.ZT,
                          sUserID = d.sUserID,
                          sUserName = d.sUserName,
                          price = d.price,
                          number = d.number,
                          author = b.author,
                          pub = b.pub
                      }).ToList();

            return View(pb);
        }

        #endregion

        [HttpPost]
        public ActionResult PurchaseBox() { 
            //从session中获取用户信息
            string pUserID = Session["logOnUserID"].ToString();

            //从request中获取购买图书信息
            string bookID = HttpContext.Request["wantBookID"].ToString();
            int bookNum =  Int32.Parse(HttpContext.Request["wantbookNum"].ToString());

            //调用存储过程
            if (pr.Purchase(bookID, pUserID, bookNum)) {
                //跳转到原先的界面
                string url = HttpContext.Request.UrlReferrer.ToString();

                return Redirect(url);
            }

            return RedirectToAction("Index", "Home"); 

        }

        [HttpPost]
        public ActionResult PurchasePro(string pID, string type) { 
            //调用存储过程
            if (pr.Deal(pID, type)) {
                //跳转到原先的界面
                string url = HttpContext.Request.UrlReferrer.ToString();

                return Redirect(url);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
