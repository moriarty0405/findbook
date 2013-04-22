using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFZYRepository : IZYRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<ZY> ZY { 
            get { return context.ZY; } 
        }
    }
}
