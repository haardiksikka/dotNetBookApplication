using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Event
    {
        [Key]
        public int eventID { get; set; }
        [Required]
        public string bookTitle { get; set; }
        [Required]
        public string location { get; set; }       
        [Required]
        public DateTime eventDate { get; set; }
        
        public string startTime { get; set; }
       
        public int eventDuration { get; set; }
        public string description { get; set; }
        public string eventType { get; set; }
        public string eventInvites { get; set; }
        public virtual string userName { get; set; }
    }
}
