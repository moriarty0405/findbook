using findbook.Domain.Entities;
using System.Collections.Generic;
using System;
using findbook.Domain.Helpers;

namespace findbook.WebUI.Models {
    public class BookViewModel : TimeHelper {
        public Books Books { get; set; }

        public IEnumerable<BookComments> BookComments { get; set; }
    }
}