using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class SearchView {
        //显示搜索到的用户
        public List<Users> Users { get; set; }

        //显示搜索到的书籍
        public List<Books> Books { get; set; }
    }
}