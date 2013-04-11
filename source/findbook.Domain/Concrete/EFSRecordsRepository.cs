using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;

namespace findbook.Domain.Concrete {
    public class EFSRecordsRepository : ISRecordsRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<SRecords> SRecords {
            get {
                return context.SRecords;
            } 
        }
    }
}
