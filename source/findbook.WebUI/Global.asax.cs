using System.Web.Mvc;
using System.Web.Routing;
using findbook.WebUI.Infrastructure;
using System;
using System.Security.Principal;
using findbook.Domain.Concrete;
using System.Web.Security;

namespace findbook.WebUI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //无参构造函数，用户验证用户权限
        public MvcApplication() {
            AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "",
                "Page/Agree/{lcID}",
                new { controller = "Page", action = "Agree", lcID = "" }
            );

            routes.MapRoute(
                "",
                "Page/{userID}/{page}",
                new { controller = "Page", action = "List", userID = "" }
            );

            routes.MapRoute(
                "Page", 
                "Page/{userID}",
                new { controller = "Page", action = "List", userID = "" } 
            );

            routes.MapRoute(
                "UnSysMessage",
                "UnReadMes/{userID}",
                new { controller = "SysMessage", action = "UnRead", userID = "" }
            );

            routes.MapRoute(
                "RSysMessage",
                "ReadMes/{userID}",
                new { controller = "SysMessage", action = "Read", userID = "" }
            );

            routes.MapRoute(
                "Info",                                         
                "Info/{userID}/{action}",                            
                new { controller = "Info", action = "", userID = "" }  
            );

            routes.MapRoute(
                "Modify",
                "Book/Edit/{bookID}",
                new { controller = "Book", action = "Edit", bookID = "" }
            );

            routes.MapRoute(
                "",
                "Book/Delete/{bookID}",
                new { controller = "Book", action = "Delete", bookID = "" }
            );

            routes.MapRoute(
                "Book",
                "Book/Recommend/{bookID}",
                new { controller = "Book", action = "Recommend", bookID = "" }
            );

            routes.MapRoute(
                "",
                "Book/Agree/{bcID}",
                new { controller = "Book", action = "Agree", bcID = "" }
            );

            routes.MapRoute(
                "",
                "Book/{bookID}/{page}",
                new { controller = "Book", action = "List", bookID = "" }
            );

            routes.MapRoute(
                "",
                "Book/{bookID}",
                new { controller = "Book", action = "List", bookID = "" }
            );

            routes.MapRoute(
                "Private-Detail-paging",
                "Private-Detail/{anotherUserID}/{page}",
                new { controller = "Private", action = "DetailList", anotherUserID = "" }
            );

            routes.MapRoute(
                "Private-Detail",
                "Private-Detail/{anotherUserID}",
                new { controller = "Private", action = "DetailList", anotherUserID = "" }
            );

            routes.MapRoute(
                "Private-List",
                "Private-List/{userID}",
                new { controller = "Private", action = "List", userID = "" }
            );

            routes.MapRoute(
                "",
                "Find/{userID}",
                new { controller = "Home", action = "Find", userID = "" }
            );

            routes.MapRoute(
                "",
                "PurchasePro/{pID}/{type}",
                new { controller = "Purchase", action = "PurchasePro", pID = "", type = "" }
            );

            routes.MapRoute(
                "",
                "UnPurchase/{userID}",
                new { controller = "Purchase", action = "UnPurchase", userID = "" }
            );

            routes.MapRoute(
                "",
                "Purchased/{userID}",
                new { controller = "Purchase", action = "Purchased", userID = "" }
            );

            routes.MapRoute(
                "",
                "UnDeal/{userID}",
                new { controller = "Purchase", action = "UnDeal", userID = "" }
            );

            routes.MapRoute(
                "",
                "Deal/{userID}",
                new { controller = "Purchase", action = "Deal", userID = "" }
            );

            routes.MapRoute(
                "",
                "Search/{kw}",
                new { controller = "Search", action = "Show", kw = "" }
            );

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}", // 带有参数的 URL
                new { controller = "Home", action = "Index" } // 参数默认值
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        //确定登录用户的角色权限
        void MvcApplication_AuthorizeRequest(object sender, EventArgs e) {
            var id = Context.User.Identity as FormsIdentity;
            if (id != null && id.IsAuthenticated) {
                var roles = id.Ticket.UserData.Split(',');
                Context.User = new GenericPrincipal(id, roles);
            }
        }
    }
}