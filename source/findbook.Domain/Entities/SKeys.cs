﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class SKeys {
        [Key]
        public string sKeysID { get; set; }

        public string sKeysBody { get; set; }

        public string ZT { get; set; }

        public int cNumber { get; set; }
    }
}
