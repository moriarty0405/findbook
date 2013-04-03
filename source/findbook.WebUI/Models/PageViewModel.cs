using System.Collections.Generic;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models {
    public class PageViewModel {
        //用于显示用户信息
        public Users Users { get; set; }

        //用于显示主页信息
        public Pages Pages { get; set;}

        //留言信息
        public IEnumerable<LeaveComments> LeaveComments { get; set; }
    }
}