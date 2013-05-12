using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class SysMesView {
        //传入当前用户的系统消息
        public IEnumerable<SystemMessages> SystemMessages { get; set; }

        //用户调用MarkReaded方法
        public SystemMessages sm { get; set; }

        //对系统信息的分页
        public PageInfo smpg { get; set; }


    }
}