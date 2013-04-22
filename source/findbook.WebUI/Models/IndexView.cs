using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class IndexView {
        public IEnumerable<Wanted> Wanted { get; set; }

        public IEnumerable<Books> NewBook { get; set; }

        public IEnumerable<Books> PopularBook { get; set; }

        public XYView XYView { get; set; }
    }
}