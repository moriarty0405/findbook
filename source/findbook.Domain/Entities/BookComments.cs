using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using findbook.Domain.Helpers;

namespace findbook.Domain.Entities {
    public class BookComments : TimeHelper {
        [Key]
        public string bCommentID { get; set; }

        public string bookID { get; set; }

        public string bookName { get; set; }

        public string userID { get; set; }

        public string userName { get; set; }

        public DateTime cTime { get; set; }

        public string sta { get; set; }

        public string ZT { get; set; }

        public int agreement { get; set; }

        public string cBody { get; set; }
    }
}
