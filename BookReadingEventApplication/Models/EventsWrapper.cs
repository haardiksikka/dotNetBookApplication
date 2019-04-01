using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReadingEventApplication.Models
{
    public class EventsWrappper
    {
      public  List<BookEvent.Common.EventDTO> pastEvents = new List<BookEvent.Common.EventDTO>();
       public List<BookEvent.Common.EventDTO> futureEvents = new List<BookEvent.Common.EventDTO>();
    }
}