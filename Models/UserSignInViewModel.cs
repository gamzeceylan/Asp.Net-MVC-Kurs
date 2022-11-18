using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adı giriniz")]
        public string username { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı şifre giriniz")]
        public string password { get; set; }
    }
}
