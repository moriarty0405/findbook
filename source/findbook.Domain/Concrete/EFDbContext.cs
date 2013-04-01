using System.Data.Entity;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFDbContext : DbContext {
        public DbSet<Users> Users { get; set; }

        public DbSet<Pages> Pages { get; set; }
    }
}
