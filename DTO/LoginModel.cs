using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class LoginModel
    {
        [StringLength(50, MinimumLength=3)]
        [Required]
        public string Username {get; set;}
        [StringLength(20, MinimumLength=3)]
        [Required]
        public string Password {get; set;}

        public LoginModel ()
        {

        }
    }
}