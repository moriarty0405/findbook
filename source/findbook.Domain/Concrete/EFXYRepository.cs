using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;

namespace findbook.Domain.Concrete {
    public class EFXYRepository : IXYRepository {
        private EFDbContext context { get; set; }

        public IQueryable<XY> XY { 
            get { return context.XY; } 
        }
    }
}
