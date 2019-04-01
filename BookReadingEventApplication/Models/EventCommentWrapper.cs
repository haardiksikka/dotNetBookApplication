using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReadingEventApplication.Models
{
    public class EventCommentWrapper
    {
        public Event eventModel = new Event();
        public List<Comment> commentList = new List<Comment>();
    }
}