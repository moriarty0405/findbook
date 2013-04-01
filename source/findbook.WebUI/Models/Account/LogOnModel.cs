using System.ComponentModel.DataAnnotations;
using findbook.Domain.Abstract;
using findbook.Domain.Entities;

namespace findbook.WebUI.Models.Account {
    public class LogOnModel {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}