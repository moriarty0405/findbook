using System;
using findbook.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;

namespace findbook.Domain.Entities {
    public class SystemMessages : TimeHelper {
        [Key]
        public string sysMesID { get; set; }

        public string smBody { get; set; }

        public string userID { get; set; }

        public string sysType { get; set; }

        public string ZT { get; set; }

        public string sta { get; set; }

        public DateTime sTime { get; set; }

        public string href { get; set; }

        //标记为已读
        public static int MarkReaded(string sysMesID) {
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {

                    String updateSql = String.Format("update SystemMessages set sta = '1' where sysMesID = '{0}'", sysMesID);
                    cmd.CommandText = updateSql;
                    cmd.ExecuteNonQuery();
                }

                return 0;
            }
        }
    }
}
