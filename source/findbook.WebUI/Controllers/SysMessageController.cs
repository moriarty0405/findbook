using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;

namespace findbook.WebUI.Controllers
{
    public class SysMessageController : Controller
    {
        private ISystemMessagesRepository smr;

        public SysMessageController(ISystemMessagesRepository systemMessageRepository) {
            smr = systemMessageRepository;
        }

        public ViewResult UnRead(string userID) {
            //未读的消息
            SysMesView smv = new SysMesView {
                SystemMessages = smr.SystemMessages
                            .Where(s => s.userID.Equals(userID) && s.sta.Equals("0"))
                            .OrderByDescending(s => s.sTime)
            };

            return View(smv);
        }

        public ViewResult Read(string userID) {
            //未读的消息
            SysMesView smv = new SysMesView {
                SystemMessages = smr.SystemMessages
                            .Where(s => s.userID.Equals(userID) && s.sta.Equals("1"))
                            .OrderByDescending(s => s.sTime)
            };

            return View(smv);
        }

    }
}
