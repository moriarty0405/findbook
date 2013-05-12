using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface IBooksRepository {
        IQueryable<Books> Books { get; }

        void UpLoadBook(string bookName, string upUserID, string upUserNickName, string tagID, string bookInro, int bookSale, decimal bookPrice, string author, string pub, string bookXY, string bookZY);

        void SaveBook();

        void DeleteBook(Books book);
    }    
}
