using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFWantsRepository : IWantsRepository {
        private EFDbContext context = new EFDbContext();
        
        public IQueryable<Wants> Wants {
            get { return context.Wanted; }
        }

        public bool PostWanted(string bookName, string author, string pub, int num, string userID, string userName) {
            bool status = false;

            #region 定义存储过程参数
            SqlParameter[] parameters = new SqlParameter[6];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookName",
                Value = bookName
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "author",
                Value = author
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "pub",
                Value = pub
            };

            parameters[3] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "num",
                Value = num
            };

            parameters[4] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "userID",
                Value = userID
            };

            parameters[5] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "userName",
                Value = userName
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec dbo.sp_post_wanted @bookName, @author, @pub, @num, @userID, @userName", parameters);
            status = true;

            return status;
        }

    }
}
