using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        public int eventID { get; set; }
        public string postComment { get; set; }
    }
}
