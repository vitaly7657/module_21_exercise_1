﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace module_21.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
