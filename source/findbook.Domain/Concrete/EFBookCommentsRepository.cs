using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFBookCommentsRepository : IBookCommentsRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<BookComments> BookComments {
            get { return context.BookComments; }
        }

        public bool Comment(string bookID, string bookName, string userID, string userName, string cBody) {
            bool status = false;

            #region 初始化存储过程参数
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookID",
                Value = bookID
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookName",
                Value = bookName
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "userID",
                Value = userID
            };

            parameters[3] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "userName",
                Value = userName
            };

            parameters[4] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "cBody",
                Value = cBody
            };

            #endregion

            context.Database.ExecuteSqlCommand("exec dbo.sp_books_comment @bookID, @bookName, @userID, @userName, @cBody", parameters);
            status = true;

            return status;
        }
    }
}
