using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models.Account {
    public class FindView {
        public IEnumerable<Users> Users { get; set; }

        public IEnumerable<Purchases> Purchases { get; set; }

        public IEnumerable<BookComments> BookComments { get; set; }

        public IEnumerable<Books> Books { get; set; }
    }
}