using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class PrivateView {
        public IEnumerable<Privates> Privates { get; set; }

        public IEnumerable<GPrivates> GPrivates { get; set; }

        public Privates PrivateDetail { get; set; }
    }
}