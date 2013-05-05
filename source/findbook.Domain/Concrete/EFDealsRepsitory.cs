using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFDealsRepsitory : IDealsRepository{
        private EFDbContext context = new EFDbContext();

        public IQueryable<Deals> Deals {
            get {
                return context.Deals;
            }
        }
    }
}
