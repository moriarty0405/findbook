using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFPrivatesRepository : IPrivatesRepository {
        private EFDbContext context = new EFDbContext();
        
        public IQueryable<Privates> Privates {
            get {
                return context.Privates;
            }
        }

        public bool SendPrivate(string sUserID, string sUserName, string rUserID, string rUserName, string pmBody) {
            bool status = false;

            #region 初始化存储过程参数
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "sUserID",
                Value = sUserID
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "sUserName",
                Value = sUserName
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "rUserID",
                Value = rUserID
            };

            parameters[3] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "rUserName",
                Value = rUserName
            };

            parameters[4] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "pmBody",
                Value = pmBody
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec dbo.sp_send_private @sUserID, @sUserName, @rUserID, @rUserName, @pmBody", parameters);

            status = true;
            return status;
        }

        public void Delete(Privates p) {
            context.Privates.Remove(p);
            context.SaveChanges();
        }
    }
}
