using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class GPrivates {
        [Key]
        public string GPID { get; set; }

        public string sUserID { get; set; }

        public string rUserID { get; set; }

        public string sUserName { get; set; }

        public string rUserName { get; set; }

        public string lMes { get; set; }

        public DateTime lTime { get; set; }

        public string ZT { get; set; }

        public int pNumber { get; set; }
    }
}
