using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using findbook.Domain.Entities;

namespace findbook.WebUI.Controllers
{
    public class PrivateCometController : AsyncController {

        //LongPolling Action 1 - 处理客户端发起的请求
        public void LongPollingAsync() {
            //计时器，3秒种触发一次Elapsed事件
            System.Timers.Timer timer = new System.Timers.Timer(3000);

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
            //收到的最新私信
            Privates newPrivate;
            
            //从session中获取用户ID
            string userID = Session["logOnUserID"].ToString();

            //获取用户的未读消息数
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();

                using (SqlCommand cmd = mycon.CreateCommand()) {
                    //找到用户发送时间与当前时间差不超过5秒中的第一条记录
                    String selectSql = String.Format("select * from Privates where rUserID = '{0}' and DATEDIFF(SECOND, sTime, GETDATE()) < 20  order by sTime desc", userID);
                    cmd.CommandText = selectSql;
                    newPrivate = (Privates)cmd.ExecuteScalar();
                }
            }

            return Json(newPrivate,
                JsonRequestBehavior.AllowGet);
        }
    }
}
