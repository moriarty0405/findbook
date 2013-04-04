using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class LeaveComments : TimeHelper {
        [Key]
        public string leaComID { get; set; }

        public string lUserID { get; set; }

        public string lUserName { get; set; }

        public string hUserID { get; set; }

        public string hUserName { get; set; }

        public DateTime lcTime { get; set; }

        public string ZT { get; set; }

        public string lBody { get; set; }
    }
}
