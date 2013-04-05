using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFUsersRepository : IUsersRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Users> Users {
            get { return context.Users; }
        }

        public bool ValidateUser(string userName, string passWord) {
            Users u = this.Users.FirstOrDefault(p => p.userName.Equals(userName));

            return u.userPw.Equals(passWord);
        }

        public Users GetByNameAndPassword(string name, string password) {
            return this.Users
                .FirstOrDefault(u => u.userName.Equals(name) && u.userPw.Equals(password));
        }

        public void CreateUser(string userName, string userPw, string mail, string sex, string XQ, string XY, string ZY) {
            #region 定义存储过程参数
            SqlParameter[] parameters = new SqlParameter[7];
                        
            parameters[0] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "userName",
                    Value = userName
            };

            parameters[1] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "userPw",
                    Value = userPw
            };

            parameters[2] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "mail",
                    Value = mail
            };

            if (sex.Equals("男")) {
                sex = "0";
            } else {
                sex = "1";
            }

            parameters[3] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "sex",
                    Value = sex
            };

            parameters[4] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "XQ",
                    Value = XQ
            };

            parameters[5] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "XY",
                    Value = XY
            };

            parameters[6] = new SqlParameter {
                    DbType = DbType.String,
                    ParameterName = "ZY",
                    Value = ZY
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec [dbo].[sp_create_user] @userName, @userPw, @mail, @sex, @XQ, @XY, @ZY", parameters);
        }

        public void SaveUser() {
            context.SaveChanges();
        }

    }
}
