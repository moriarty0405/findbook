using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface IWantedRepository {
        IQueryable<Wanted> Wanted { get; }

        bool PostWanted(string bookName, string author, string pub, int num, string userID, string userName);
    }
}
