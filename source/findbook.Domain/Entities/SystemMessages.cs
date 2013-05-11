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
            if (string.IsNullOrEmpty(sysMesID)) {
                return 0;
            }

            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {

                    String updateSql = String.Format("update SystemMessages set sta = '1' where sysMesID = '{0}'", sysMesID);
                    cmd.CommandText = updateSql;
                    cmd.ExecuteNonQuery();
                }
            }

            return 0;
        }

        public static int MesNumber(string userID) {
            int mesNum = 0;

            //获取用户的未读消息数
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {

                    String selectSql = String.Format("select count(1) from SystemMessages where sta = '0' and userID = '{0}'", userID);
                    cmd.CommandText = selectSql;
                    mesNum = (int)cmd.ExecuteScalar();
                }
            }

            return mesNum;
        }
    }
}
