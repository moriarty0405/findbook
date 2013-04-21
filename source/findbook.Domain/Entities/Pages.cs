using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace findbook.Domain.Entities {
    public class Pages {
        [Key]
        public string pageID { get; set; }

        public string visitTimes { get; set; }

        public string userName { get; set; }

        public string userNickName { get; set; }

        public string pageDesc { get; set; }

        public int lNumber { get; set; }

        public string ZT { get; set; }

        //将访问数加1
        public int Increase(string pageID) {
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {

                    String updateSql = String.Format("update Pages set visitTimes = visitTimes + 1 where pageID = '{0}'", pageID);
                    cmd.CommandText = updateSql;
                    cmd.ExecuteNonQuery();
                }

                return 0;
            }
        }
    }
}
