using System.Collections.Generic;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class PageViewModel {
        public IEnumerable<Users> Users { get; set; }

        public IEnumerable<Pages> Pages { get; set;}
    }
}