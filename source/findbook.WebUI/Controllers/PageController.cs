using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers
{
    public class PageController : Controller
    {
        private IPagesRepository pageRepository;
        private IUsersRepository userRepository;

        public PageController(IUsersRepository ur, IPagesRepository pr) {
            userRepository = ur;
            pageRepository = pr;
        }

        public ActionResult List(string userID)
        {
            PageViewModel pvm = new PageViewModel {
                //传入View中的是用户点击查看主页的当前用户
                Users = userRepository.Users.FirstOrDefault(u => u.userID.Equals(userID)),
                
                Pages = pageRepository.Pages.FirstOrDefault(p => p.pageID.Equals(userID))
            };

            return View(pvm);
        }

    }
}
