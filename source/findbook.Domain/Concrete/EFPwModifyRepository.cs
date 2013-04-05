using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFPwModifyRepository : IPwModifyRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<PwModify> PwModify {
            get { return context.PwModify; }
        }

        public bool Modify(string userID, string forePw, string newPw) {
            bool status = false;

            #region 定义存储过程参数
            SqlParameter[] parameters = new SqlParameter[3];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "userID",
                Value = userID
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "forePw",
                Value = forePw
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "newPw",
                Value = newPw
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec dbo.sp_pw_modify @userID, @forePw, @newPw", parameters);
            status = true;

            return status;
        }
    }
}
