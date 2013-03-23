using System.ComponentModel.DataAnnotations;

namespace findbook.WebUI.Models.Account {
        public class RegisterModel {
                [Required]
                public string UserName { get; set; }

                [Required]
                [DataType(DataType.Password)]
                public string NewPassword { get; set; }

                [Required]
                [DataType(DataType.Password)]
                public string ConfirmPassword { get; set; }

                [Required]
                [DataType(DataType.EmailAddress)]
                public string Email { get; set; }

                [Required]
                public string Sex { get; set; }

                [Required]
                public string XQ { get; set; }

                [Required]
                public string XY { get; set; }

                [Required]
                public string ZY { get; set; }

                [Required]
                public bool read { get; set; }
        }
}