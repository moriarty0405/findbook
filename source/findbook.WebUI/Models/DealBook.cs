using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class DealBook {
        IEnumerable<Books> Books { get; set; }

        IEnumerable<Deals> Deals { get; set; }
    }
}