using System.Linq;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;
using System.Data;
using System.Data.SqlClient;

namespace findbook.Domain.Concrete {
    public class EFSRecordsRepository : ISRecordsRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<SRecords> SRecords {
            get {
                return context.SRecords;
            }
        }

        //收集热词
        public void Collect(string userID, string sBody) {
            if (!string.IsNullOrEmpty(sBody)) {
                #region 初始化存储过程参数

                SqlParameter[] parameters = new SqlParameter[11];

                parameters[0] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "userID",
                    Value = userID
                };

                parameters[1] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "sBody",
                    Value = sBody
                };
                #endregion

                context.Database.ExecuteSqlCommand("exec dbo.sp_add_srecord @userID, @sBody", parameters);

            }
        }


    }
}
