using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class XYView {
        public IEnumerable<XY> XY { get; set; }

        public IEnumerable<ZY> ZY { get; set; }
    }
}