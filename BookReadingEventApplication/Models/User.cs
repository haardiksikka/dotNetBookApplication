using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReadingEventApplication.Models
{
    public class User
    {
        [Required(ErrorMessage = "Name is Required")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Enter Password!! ")]
        [RegularExpression("^((?=.*?[A-Za-z0-9])(?=.*?[#?!@$%^&*-])).{5,}$", ErrorMessage = "Password must be atleast 5 char long and contains a special character")]
        public string password { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string email { get; set; }
    }
}