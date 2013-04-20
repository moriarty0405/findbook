using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class IndexView {
        IEnumerable<Wanted> Wanted { get; set; }

        IEnumerable<Books> RecommendBook { get; set; }

        IEnumerable<Books> PopularBook { get; set; }

        IEnumerable<Books> FreeBook { get; set; }
    }
}