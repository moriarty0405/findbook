using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFLeaveCommentsRepository : ILeaveCommentsRepository{
        private EFDbContext context = new EFDbContext();

        public IQueryable<LeaveComments> LeaveComments {
            get {
                return context.LeaveComments;
            }
        }

        public bool LeaveComment(string lUserID, string lUserName, string hUserID, string hUserName, string lBody) {
            bool status = false;

            #region 初始化参数
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "lUserID",
                Value = lUserID
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "lUserName",
                Value = lUserName
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "hUserID",
                Value = hUserID
            };

            parameters[3] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "hUserName",
                Value = hUserName
            };

            parameters[4] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "lBody",
                Value = lBody
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec dbo.sp_leave_comment @lUserID, @lUserName, @hUserID, @hUserName, @lBody", parameters);
            status = true;
            
            return status;
        }
    }
}
