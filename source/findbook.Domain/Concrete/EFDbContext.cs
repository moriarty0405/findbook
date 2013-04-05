using System.Data.Entity;
using findbook.Domain.Entities;

namespace findbook.Domain.Concrete {
    public class EFDbContext : DbContext {
        public DbSet<Users> Users { get; set; }

        public DbSet<Pages> Pages { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<BookComments> BookComments { get; set; }

        public DbSet<LeaveComments> LeaveComments { get; set; }

        public DbSet<PwModify> PwModify { get; set; }

        public DbSet<SystemMessages> SystemMessages { get; set; }

        public DbSet<Wanted> Wanted { get; set; }

        public DbSet<Privates> Privates { get; set; }
    }
}
