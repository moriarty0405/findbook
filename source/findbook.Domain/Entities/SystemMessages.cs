using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class SystemMessages : TimeHelper{
        [Key]
        public string sysMesID { get; set; }

        public string smBody { get; set; }

        public string userID { get; set; }

        public string sysType { get; set; }

        public string ZT { get; set; }

        public string sta { get; set; }

        public DateTime sTime { get; set; }
    }
}
