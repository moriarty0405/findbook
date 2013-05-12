using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace findbook.Domain.Helpers {
    //用于翻译状态字段
    public class TranslateHelper {
        public string purchaseTran(string sta) {
            string result = "";

            switch (sta) { 
                case "0" : result = "发出购买申请"; break;
                case "1" : result = "取消交易"; break;
                case "2": result = "取消交易"; break;
                case "3": result = "交易完成"; break;
            }

            return result;
        }

        public string dealTran(string sta) {
            string result = "";

            switch (sta) {
                case "1": result = "交易成功"; break;
                case "2": result = "申请退货"; break;
                case "3": result = "退货成功"; break;
            }

            return result;
        }

    }
}
