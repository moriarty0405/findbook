using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;

namespace findbook.Domain.Concrete {
        public class EFUsersRepository : IUsersRepository {
                private EFDbContext context = new EFDbContext();

                public IQueryable<Users> Users {
                        get { return context.Users; }
                }

        }
}
