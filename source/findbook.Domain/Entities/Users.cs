using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace findbook.Domain.Entities {
    public class Users {
        [Key]
        public string userID { get; set; }

        public string userPw { get; set; }

        public string userMail { get; set; }

        public string userName { get; set; }

        public DateTime userRegDate { get; set; }

        public string imgID { get; set; }

        public string userRole { get; set; }

        public string userXY { get; set; }

        public string userZY { get; set; }

        public string userNickName { get; set; }

        public string userSex { get; set; }

        public string userQQ { get; set; }

        public string tagID { get; set; }

        public string userPhone { get; set; }

        public string userGrade { get; set; }

        public string XQ { get; set; }

        public string ZT { get; set; }

        public string intro { get; set; }

    }
}
