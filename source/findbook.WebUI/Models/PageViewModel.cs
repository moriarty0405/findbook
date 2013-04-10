﻿using System.Collections.Generic;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class PageViewModel {
        //用于显示用户信息
        public Users Users { get; set; }

        //用于显示主页信息
        public Pages Pages { get; set;}

        //留言信息
        public IEnumerable<LeaveComments> LeaveComments { get; set; }

        //上传图书信息
        public IEnumerable<Books> Books { get; set; }

        //对留言的分页
        public PageInfo LC { get; set; }

        //对上传图书的分页
        public PageInfo BK { get; set; }
    }
}