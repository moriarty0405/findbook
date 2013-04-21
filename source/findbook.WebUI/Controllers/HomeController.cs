using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers
{
    public class HomeController : Controller {
        private IBooksRepository br;
        private IWantedRepository wr;

        public HomeController(IBooksRepository bookRepository, IWantedRepository wantedRepository) {
            br = bookRepository;
            wr = wantedRepository;
        }

        public ActionResult Index() {
            IndexView iv = new IndexView() {
                Wanted = wr.Wanted,
                NewBook = br.Books.OrderByDescending(b => b.upTime)
                                  .Take(6),
                FreeBook = br.Books.Where(b => b.bookPrice == 0)
                                   .Take(6)


            };

            return View();
        }

    }
}
