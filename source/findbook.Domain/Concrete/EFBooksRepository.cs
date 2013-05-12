using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace findbook.Domain.Concrete {
    public class EFBooksRepository : IBooksRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Books> Books {
            get { return context.Books; }
        }

        public void UpLoadBook(string bookName, string upUserID, string upUserNickName, string tagID, string bookIntr, int bookSale, decimal bookPrice, string author, string pub, string bookXY, string bookZY) {
            #region 初始化存储过程参数
            SqlParameter[] parameters = new SqlParameter[11];

            parameters[0] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookName",
                Value = bookName
            };

            parameters[1] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "upUserID",
                Value = upUserID
            };

            parameters[2] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "upUserNickName",
                Value = upUserNickName
            };

            parameters[3] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "tagID",
                Value = tagID
            };

            parameters[4] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookIntr",
                Value = bookIntr
            };

            parameters[5] = new SqlParameter {
                DbType = DbType.Int32,
                ParameterName = "bookSale",
                Value = bookSale
            };

            parameters[6] = new SqlParameter {
                DbType = DbType.Currency,
                ParameterName = "bookPrice",
                Value = bookPrice
            };

            parameters[7] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "author",
                Value = author
            };

            parameters[8] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "pub",
                Value = pub
            };

            parameters[9] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookXY",
                Value = bookXY
            };

            parameters[10] = new SqlParameter {
                DbType = DbType.String,
                ParameterName = "bookZY",
                Value = bookZY
            };
            #endregion

            context.Database.ExecuteSqlCommand("exec [dbo].[sp_post_book] @bookName, @upUserID, @upUserNickName, @tagID, @bookIntr, @bookSale, @bookPrice, @author, @pub, @bookXY, @bookZY", parameters);
        }

        public void SaveBook(Books book, Books deleteBook) {
            DeleteBook(deleteBook);
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void DeleteBook(Books book) {
            context.Books.Remove(book);
            context.SaveChanges();
        }
    }
}
