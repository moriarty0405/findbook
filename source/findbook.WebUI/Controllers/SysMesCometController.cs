using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using findbook.Domain.Concrete;
using System.Web.Security;

namespace findbook.WebUI.Controllers {
    public class SysMesCometController : AsyncController {
        //LongPolling Action 1 - 处理客户端发起的请求
        public void LongPollingAsync() {
            //计时器，60秒种触发一次Elapsed事件
            System.Timers.Timer timer = new System.Timers.Timer(60000);

            //告诉ASP.NET接下来将进行一个异步操作
            AsyncManager.OutstandingOperations.Increment();

            //订阅计时器的Elapsed事件
            timer.Elapsed += (sender, e) => {

                    //告诉ASP.NET异步操作已完成，进行LongPollingCompleted方法的调用
                    AsyncManager.OutstandingOperations.Decrement();
                };
            //启动计时器
            timer.Start();
        }

        
        //LongPolling Action 2 - 异步处理完成，向客户端发送响应
        public ActionResult LongPollingCompleted() {
            int mesNum = 0;
            string userID = "";

            //从cookie中获取userID
            HttpCookie cookie = Request.Cookies["user"];

            if (cookie == null) {
                //如果用户未登陆
                return Json(new { mesNum = mesNum },
                JsonRequestBehavior.AllowGet);
            } else {
                userID = cookie["userID"].ToString();
            }
            

            //获取用户的未读消息数
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {

                    String selectSql = String.Format("select count(1) from SystemMessages where sta = '1' and userID = '{0}'", userID);
                    cmd.CommandText = selectSql;
                    mesNum = (int)cmd.ExecuteScalar();
                }
            }

            return Json(new { mesNum = mesNum },
                JsonRequestBehavior.AllowGet);
        }
    }
}
