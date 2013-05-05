using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFPurchasesRepository : IPurchasesRepository {
        private EFDbContext context = new EFDbContext();
        
        public IQueryable<Purchases> Purchases{
            get
            {
            	return context.Purchases;
            }
        }
    }
}
