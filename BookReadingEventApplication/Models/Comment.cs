using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReadingEventApplication.Models
{
    public class Comment
    {
        public int eventId { get; set; }
        public string comment { get; set; }
    }
}