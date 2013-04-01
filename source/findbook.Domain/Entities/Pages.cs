using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Pages {
        [Key]
        public string pageID { get; set; }

        public int visitTimes { get; set; }

        public string userName { get; set; }

        public string userNickName { get; set; }

        public string pageDesc { get; set; }
    }
}
