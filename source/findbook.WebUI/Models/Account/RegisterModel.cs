using System.ComponentModel.DataAnnotations;

namespace findbook.WebUI.Models.Account {
    public class RegisterModel {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "性别")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "校区")]
        public string XQ { get; set; }

        [Required]
        [Display(Name = "学院")]
        public string XY { get; set; }

        [Required]
        [Display(Name = "专业")]
        public string ZY { get; set; }

        [Required]
        public bool read { get; set; }
    }
}