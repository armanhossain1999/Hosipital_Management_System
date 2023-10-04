using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}