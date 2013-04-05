using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Privates {
        [Key]
        public string pmID { get; set; }

        public string sUserID { get; set; }

        public string sUserName { get; set; }

        public string rUserID { get; set; }

        public string rUserName { get; set; }

        public string pmBody { get; set; }

        public DateTime sTime { get; set; }

        public string sta { get; set; }

        public string ZT { get; set; }

    }
}
