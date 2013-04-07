using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace findbook.Domain.Helpers {
    public class TimeHelper {
        //计算时差
        public string DateDiff(DateTime upTime) {
            string dateDiff = null;
            //计算当前时间和传入时间的时间差
            TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan tsUp = new TimeSpan(upTime.Ticks);
            TimeSpan ts = tsNow.Subtract(tsUp).Duration();
            double totalSecond = ts.Seconds + ts.Minutes * 60 + ts.Hours * 3600;

            if (totalSecond <= 60)
                dateDiff = ts.Seconds.ToString() + "秒前";
            else if (totalSecond <= 3600)
                dateDiff = ts.Minutes.ToString() + "分钟前";
            else if (totalSecond <= 86400)
                dateDiff = ts.Hours.ToString() + "小时前";
            else if (ts.Days <= 30)
                dateDiff = ts.Days.ToString() + "天前";
            else if (ts.Days / 30 <= 12)
                dateDiff = (ts.Days / 30).ToString() + "个月前";
            else dateDiff = (ts.Days / 365).ToString() + "年前";

            return dateDiff;
        }
    }
}
