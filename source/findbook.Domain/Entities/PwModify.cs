using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class PwModify {
        [Key]
		public string modifyId{get; set;}

	    public string userID{get ; set;}

		public string forePw{get; set;}

		public string newPw{get; set;}

		public DateTime mTime{get; set;}

        public string sta { get; set; }

        public string ZT { get; set; }
    }
}
