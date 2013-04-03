using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFBooksRepository : IBooksRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Books> Books {
            get { return context.Books; }
        }
    }
}
