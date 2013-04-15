﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class SRecords {
        [Key]
        public string sRecordID { get; set; }

        public string sBody { get; set; }

        public string userID { get; set; }

        public DateTime cTime { get; set; }

        public string ZT { get; set; }

    }
}
