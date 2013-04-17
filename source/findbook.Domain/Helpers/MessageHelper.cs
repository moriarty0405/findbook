using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace findbook.Domain.Helpers {
    public class MessageHelper {
        //获取用户未读信息数
        public static int Count(string userID) {
            int count = 0;

            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {
                    String Sql = String.Format("select count(1) from SystemMessages where userID = '{0}'", userID);
                    cmd.CommandText = Sql;
                    count = (int)cmd.ExecuteScalar();
                }

                return count;
            }
        }
    }
}
