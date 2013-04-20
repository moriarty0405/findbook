using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;

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
            return View();
        }

    }
}
