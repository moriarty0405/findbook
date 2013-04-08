using System.Linq;
using findbook.Domain.Entities;
using System;

namespace findbook.Domain.Abstract {
    public interface IBookCommentsRepository {
        IQueryable<BookComments> BookComments { get; }

        bool Comment(string bookID, string bookName, string userID, string userName, string cBody);
    }
}
