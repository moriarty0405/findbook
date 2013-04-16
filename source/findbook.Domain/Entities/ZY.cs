using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class ZY {
        [Key]
        public string ZYDM { get; set; }

        public string ZYMC { get; set; }

        public string XYDM { get; set; }

        public string XYMC { get; set; }

        public string ZT { get; set; }
    }
}
