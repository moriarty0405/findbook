using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFPurchasesRepository : IPurchasesRepository {
        private EFDbContext context = new EFDbContext();
        
        public IQueryable<Purchases> Purchases{
            get
            {
            	return context.Purchases;
            }
        }

        public bool Purchase(string bookID, string pUserID, int bkNum) {
            bool status = false;

            #region 初始化参数
            SqlParameter[] parameters = new SqlParameter[3];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookID",
                Value = bookID
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "pUserID",
                Value = pUserID
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.Int16,
                ParameterName = "bkNum",
                Value = bkNum
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec dbo.sp_book_purchases @bookID, @pUserID, @bkNum", parameters);

            status = true;

            return status;
        }

        public bool Deal(string pID, string type) {
            bool status = false;

            SqlParameter[] parameters = new SqlParameter[2];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "pID",
                Value = pID
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "type",
                Value = type
            };

            context.Database.ExecuteSqlCommand("exec dbo.sp_book_deal @pID, @type", parameters);

            status = true;

            return status;
        }
    }
}
