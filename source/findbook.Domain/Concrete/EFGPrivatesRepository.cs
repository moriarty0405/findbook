using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFGPrivatesRepository : IGPrivatesRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<GPrivates> GPrivates {
            get {
                return context.GPrivates;
            }
        }

        public void Delete(GPrivates gp) {
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "gpID",
                Value = gp.GPID
            };

            context.Database.ExecuteSqlCommand("exec [dbo].[sp_delete_gprivates] @gpID", parameters);
        }
    }
}
