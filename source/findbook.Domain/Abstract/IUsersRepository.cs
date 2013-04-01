using System.Linq;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface IUsersRepository {
        IQueryable<Users> Users { get; }

        //验证用户名密码
        bool ValidateUser(string userName, string passWord);

        //创建用户
        void CreateUser(string userName, string userPw, string mail, string sex, string XQ, string XY, string ZY);

        //用于验证用户，并写入cookie
        Users GetByNameAndPassword(string name, string password);
    }
}
