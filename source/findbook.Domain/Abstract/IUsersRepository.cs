using System.Linq;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
        public interface IUsersRepository {
                IQueryable<Users> Users { get; }

        }
}
