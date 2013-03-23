using findbook.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace findbook.WebUI.Infrastructure.Concrete {
        public class FormsAuthProvider : IAuthProvider {
                public bool Authenticate(string username, string password) {
                        bool result = FormsAuthentication.Authenticate(username, password);
                        if (result) {
                                FormsAuthentication.SetAuthCookie(username, false);
                        }
                        return result;
                }
        }
}