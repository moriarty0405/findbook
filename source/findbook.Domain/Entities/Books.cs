using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Books {
        [Key]
        public string bookID { get; set; }

        public string bookName { get; set; }

        public string imgID { get; set; }

        public string imgURL { get; set; }

        public DateTime upTime { get; set; }

        public string upUserID { get; set; }

        public string upUserNickName { get; set; }

        public int recNumber { get; set; }

        public string tagID { get; set; }

        public int remNumber { get; set; }

        public string bookIntr { get; set; }

        public int bookSale { get; set; }

        public decimal bookPrice { get; set; }

        public string author { get; set; }

        public string pub {get; set;}

        public string bookXY { get; set; }

        public string bookZY { get; set; }

        public int cNumber { get; set; }

        public string ZT { get; set; }
    }
}
