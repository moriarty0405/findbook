using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Deals {
        [Key]
        public string dID { get; set; }

        public string bookID { get; set; }

        public string bookName { get; set; }

        public string pUserID { get; set; }

        public string pUserName { get; set; }

        public DateTime dTime { get; set; }

        public string ZT { get; set; }

        public decimal  price { get; set; }

        public string sUserID { get; set; }

        public string sUserName { get; set; }

        public int number { get; set; }
    }
}
