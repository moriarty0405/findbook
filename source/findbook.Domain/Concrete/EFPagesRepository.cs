using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFPagesRepository : IPagesRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Pages> Pages {
            get { return context.Pages; }
        }
    }
}
