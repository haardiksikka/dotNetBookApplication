using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Controller;
using DAL.Models;

namespace DAL
{
    public class EventInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EventContext>
    {
        protected override void Seed(EventContext context)
        {
            //var events = new List<Event>
            //{
            //new Event{bookTitle="book1",location="Gurgoan",eventDate=DateTime.Parse("2019-01-01"), eventType="Public",eventDuration=1},
            //new Event{bookTitle="book2",location="Gurgoan",eventDate=DateTime.Parse("2019-02-16"), eventType="Public",eventDuration=1},
            //new Event{bookTitle="book3",location="Gurgoan",eventDate=DateTime.Parse("2019-03-05"), eventType="Private",eventDuration=3},
            //new Event{bookTitle="book4",location="Delhi",eventDate=DateTime.Parse("2019-04-10"), eventType="Public",eventDuration=2},
            //new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            //new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            //new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            //};

            //events.ForEach(s => context.Event.Add(s));
            //context.SaveChanges();
        }
    }
}