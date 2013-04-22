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
    public class HomeController : Controller {
        private IBooksRepository br;
        private IWantedRepository wr;
        private IXYRepository xyr;
        private IZYRepository zyr;

        public HomeController(IBooksRepository bookRepository, IWantedRepository wantedRepository, 
            IXYRepository xyRepository, IZYRepository zyRepository) {
            br = bookRepository;
            wr = wantedRepository;
            xyr = xyRepository;
            zyr = zyRepository;
        }

        public ActionResult Index() {
            IndexView iv = new IndexView() {
                Wanted = wr.Wanted,
                NewBook = br.Books.OrderByDescending(b => b.upTime)
                                  .Take(12),

                PopularBook = br.Books.OrderByDescending(b => b.cNumber)
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
            IEnumerable<Books> Books = br.Books.Where(b => b.bookPrice == 0)
                                               .OrderByDescending(b => b.upTime);

            return View(Books);
        }

        public ActionResult Recommend() {
            IEnumerable<Books> Books = br.Books.Where(b => b.recNumber > 0 || b.cNumber > 0)
                                               .OrderByDescending(b => b.upTime);

            return View(Books);
        }

        public ActionResult Find() {


            return View();
        }

    }
}
