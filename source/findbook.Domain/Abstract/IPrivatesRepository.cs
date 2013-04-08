using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface IPrivatesRepository {
        IQueryable<Privates> Privates { get; }

        bool SendPrivate(string sUserID, string sUserName, string rUserID, string rUserName, string pmBody);
    }
}
