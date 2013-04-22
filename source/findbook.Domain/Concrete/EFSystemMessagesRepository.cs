using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;
using findbook.Domain.Abstract;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace findbook.Domain.Concrete {
    public class EFSystemMessagesRepository : ISystemMessagesRepository{
        private EFDbContext context = new EFDbContext();

        public IQueryable<SystemMessages> SystemMessages {
            get { return context.SystemMessages; }
        }
        
    }
}
