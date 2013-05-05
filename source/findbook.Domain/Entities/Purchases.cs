using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Purchases {
        [Key]
        public string pID { get; set; }

        public string bookID { get; set; }

        public string bookName { get; set; }

        public string pUserID { get; set; }

        public string pUserName { get; set; }

        public string sta { get; set; }

        public DateTime pTime { get; set; }

        public string ZT { get; set; }

        public string sUserID { get; set; }

        public string sUserName { get; set; }

        public double price { get; set; }

        public int number { get; set; }
    }
}
