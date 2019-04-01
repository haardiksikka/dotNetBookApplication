using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReadingEventApplication.Models
{
    public class Event
    {

        [Required(AllowEmptyStrings =false)]
        public string bookTitle { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string location { get; set; }
        [Required(AllowEmptyStrings =false)]
        [DataType(DataType.Date)]
        public DateTime eventDate { get; set; }

        public string startTime { get; set; }
        [Range(1,4,ErrorMessage ="Value Cannot be greater than 4 and less than 1")]
        public int eventDuration { get; set; }
        [MaxLength(50,ErrorMessage ="Maximum Permitted Length, 50 Characters")]
        public string description { get; set; }
        public int noOfInvites { get; set; }
        public string eventType { get; set; }
        public string eventInvites { get; set; }
        public string userName { get; set; }
    }
}