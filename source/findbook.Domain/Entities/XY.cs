using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class XY {
        [Key]
        public string XYDM { get; set; }

        public string XYMC { get; set; }

        public string ZT { get; set; }
    }
}
