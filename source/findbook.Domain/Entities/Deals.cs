using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace findbook.Domain.Entities {
    public class Deals {
        public string dID { get; set; }

        public string bookID { get; set; }

        public string bookName { get; set; }

        public string pUserID { get; set; }

        public string pUserName { get; set; }

        public DateTime dTime { get; set; }

        public string ZT { get; set; }

        public double  price { get; set; }

        public string sUserID { get; set; }

        public string sUserName { get; set; }

        public int number { get; set; }
    }
}
