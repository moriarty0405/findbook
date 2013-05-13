using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Wants {
        [Key]
        public string WantedID { get; set; }

        public string bookName { get; set; }

        public string author { get; set; }

        public string pub { get; set; }

        public int num { get; set; }

        public string userID { get; set; }

        public string userName { get; set; }

        public DateTime wTime { get; set; }
    }
}
