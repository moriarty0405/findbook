using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface IPwModifyRepository {
        IQueryable<PwModify> PwModify { get; }

        bool Modify(string userID, string forePw, string newPw);
    }
}
