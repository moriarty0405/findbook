using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFPrivatesRepository : IPrivatesRepository {
        private EFDbContext context = new EFDbContext();
        
        public IQueryable<Privates> Privates {
            get {
                return context.Privates;
            }
        }
    }
}
