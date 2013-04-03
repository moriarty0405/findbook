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

            if (ts.Seconds <= 60)
                dateDiff = ts.Seconds.ToString() + "秒";
            else if (ts.Minutes <= 60)
                dateDiff = ts.Minutes.ToString() + "分";
            else if (ts.Hours <= 24)
                dateDiff = ts.Hours.ToString() + "小时";
            else if (ts.Days <= 30)
                dateDiff = ts.Days.ToString() + "天";
            else if (ts.Days / 30 <= 12)
                dateDiff = (ts.Days / 30).ToString() + "月";
            else dateDiff = (ts.Days / 365).ToString() + "年";

            return dateDiff;
        }
    }
}
