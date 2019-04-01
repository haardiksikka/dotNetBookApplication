using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
