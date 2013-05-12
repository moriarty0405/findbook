﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using findbook.Domain.Abstract;
using findbook.WebUI.Models;
using findbook.Domain.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace findbook.WebUI.Controllers
{
    public class SysMessageController : Controller
    {
        private ISystemMessagesRepository smr;

        public SysMessageController(ISystemMessagesRepository systemMessageRepository) {
            smr = systemMessageRepository;
        }

        public ViewResult UnRead(string userID, int page = 1) {
            int PageSize = 10;
            
            //未读的消息
            SysMesView smv = new SysMesView {
                SystemMessages = smr.SystemMessages
                            .Where(s => s.userID.Equals(userID) && s.sta.Equals("0"))
                            .OrderByDescending(s => s.sTime)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                sm = new SystemMessages(),

                //分页信息
                smpg = new PageInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = smr.SystemMessages
                            .Where(s => s.userID.Equals(userID) && s.sta.Equals("0"))
                            .Count()
                }
            };

            return View(smv);
        }

        public ViewResult Read(string userID, int page = 1) {
            int PageSize = 10;
            
            //未读的消息
            SysMesView smv = new SysMesView {
                SystemMessages = smr.SystemMessages
                            .Where(s => s.userID.Equals(userID) && s.sta.Equals("1"))
                            .OrderByDescending(s => s.sTime)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                //分页信息
                smpg = new PageInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = smr.SystemMessages
                            .Where(s => s.userID.Equals(userID) && s.sta.Equals("1"))
                            .Count()
                }
            };

            return View(smv);
        }

        

    }
}
