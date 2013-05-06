using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using findbook.Domain.Helpers;

namespace findbook.WebUI.Models {
    public class DealBook :TranslateHelper {
        [Key]
        public string dID { get; set; }

        public string bookID { get; set; }

        public string bookName { get; set; }

        public string pUserID { get; set; }

        public string pUserName { get; set; }

        public DateTime dTime { get; set; }

        public string ZT { get; set; }

        public string sUserID { get; set; }

        public string sUserName { get; set; }

        public decimal price { get; set; }

        public int number { get; set; }

        public string author { get; set; }

        public string pub { get; set; }
    }
}