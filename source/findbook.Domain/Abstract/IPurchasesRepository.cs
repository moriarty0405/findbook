using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface IPurchasesRepository {
        IQueryable<Purchases> Purchases {get;}

        bool Purchase(string bookID, string pUserID, int bkNum);

        bool Deal(string pID, string type);
    }
}
