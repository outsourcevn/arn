using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aries.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn cần nhập Tên Đăng Nhập")]
        [Display(Name = "Tên Đăng Nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Mật Khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}