using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BookEvent.Common;
using DAL.Models;

namespace DAL.Controller
{
    public class EventDAL
    {
        private Interceptor logger;
        private EventContext eventContext;
        public EventDAL()
        {
            logger = new Interceptor();
            eventContext = new EventContext();
        }
        public List<EventDTO> GetEvents()
        {
            logger.Info("Inside GetEvents method");
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var eventEntities = eventContext.Event.Where(allEvents => string.Equals(allEvents.eventType,"Public"));
            
            List<EventDTO> events = new List<EventDTO>();
            foreach( var eventEntity in eventEntities){
                var eventDetail = new EventDTO()
                {
                    eventID = eventEntity.eventID,
                    bookTitle = eventEntity.bookTitle,
                    location = eventEntity.location,
                    eventDate = eventEntity.eventDate,
                    eventDuration = eventEntity.eventDuration,
                    startTime = eventEntity.startTime,
                    description = eventEntity.description,
                    eventType = eventEntity.eventType,
                };
                events.Add(eventDetail);
            }
            return events;
        }

        public void CreateEvent(EventDTO newEvent)
        {
            
            var eventEntity = new Event();
            eventEntity.bookTitle = newEvent.bookTitle;
            eventEntity.location = newEvent.location;
            eventEntity.eventDate = newEvent.eventDate;
            eventEntity.startTime = newEvent.startTime;
            eventEntity.eventDuration = newEvent.eventDuration;
            eventEntity.eventType = newEvent.eventType;
            eventEntity.eventInvites = newEvent.eventInvites;
            eventEntity.description = newEvent.description;
            eventEntity.userName = newEvent.userName;
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var eventCreated = eventContext.Event.Add(eventEntity);
            logger.Info(string.Format("Created new event with ID: {0}", 0));
            
            eventContext.SaveChanges();
            if (eventEntity.eventInvites != null)
            {
                string[] emails = eventEntity.eventInvites.Split(',');
                for (int i = 0; i < emails.Length; i++)
                {
                    emails[i] = emails[i].Trim();
                }

                var mapperEntity = new EventInviteMapper();
                
                foreach( var email in emails)
                {
                    mapperEntity.eventID = eventCreated.eventID;
                    mapperEntity.emails = email;
                    eventContext.EventInviteMapper.Add(mapperEntity);
                    eventContext.SaveChanges();
                }
            }
            

        }

        public List<EventDTO> MyEvents(string userName)
        {
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var eventEntities = eventContext.Event.Where(myEvents => string.Equals(myEvents.userName, userName));

            List<EventDTO> events = new List<EventDTO>();
            foreach (var eventEntity in eventEntities)
            {
                var eventDetail = new EventDTO()
                {
                    eventID = eventEntity.eventID,
                    bookTitle = eventEntity.bookTitle,
                    location = eventEntity.location,
                    eventDate = eventEntity.eventDate,
                    eventDuration = eventEntity.eventDuration,
                    startTime = eventEntity.startTime,
                    description = eventEntity.description,
                    eventType = eventEntity.eventType,
                };

                events.Add(eventDetail);
            }
            return events;
        }


        public List<EventDTO> EventInvites(string userName)
        {
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var userInfo = eventContext.User.SingleOrDefault(user => string.Equals(user.userName, userName));
            var email = userInfo.email;
            var eventMapperInfo = eventContext.EventInviteMapper.Where(emails => string.Equals(emails.emails, email));
            //var eventId = eventMapperInfo.eventID;
            List<Event> eventEntities = new List<Event>();
            foreach (var eventInfo in eventMapperInfo)
            {
               var eventsById = eventContext.Event.SingleOrDefault(e => (e.eventID==eventInfo.eventID));
                eventEntities.Add(eventsById);
                
            }
           

            List<EventDTO> events = new List<EventDTO>();
            foreach (var eventEntity in eventEntities)
            {
                var eventDetail = new EventDTO()
               {
                    eventID = eventEntity.eventID,
                    bookTitle = eventEntity.bookTitle,
                    location = eventEntity.location,
                    eventDate = eventEntity.eventDate,
                    eventDuration = eventEntity.eventDuration,
                    startTime = eventEntity.startTime,
                    description = eventEntity.description,
                    eventType = eventEntity.eventType,
                };
                eventContext.Database.Log = Console.Write;
                events.Add(eventDetail);
            }
            return events;
        }
        public EventDTO GetEventDetails(int? id)
        {
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            try
            {
                var eventEntity = eventContext.Event.Where(events => events.eventID == id).SingleOrDefault();
                var eventDetail = new EventDTO()
                {
                    eventID = eventEntity.eventID,
                    bookTitle = eventEntity.bookTitle,
                    location = eventEntity.location,
                    eventDate = eventEntity.eventDate,
                    eventDuration = eventEntity.eventDuration,
                    startTime = eventEntity.startTime,
                    description = eventEntity.description,
                    eventType = eventEntity.eventType,
                    userName = eventEntity.userName,
                    eventInvites = eventEntity.eventInvites,
                };
                return eventDetail;
            }
            catch(Exception )
            {
                return null;
            }
            
        }
        public List<EventDTO> GetAllUserEvents(string userName)
        {
            var eventEntities = eventContext.Event.Where(allEvents => string.Equals(allEvents.userName, userName));

            List<EventDTO> events = new List<EventDTO>();
            foreach (var eventEntity in eventEntities)
            {
                var eventDetail = new EventDTO()
                {
                    eventID = eventEntity.eventID,
                    bookTitle = eventEntity.bookTitle,
                    location = eventEntity.location,
                    eventDate = eventEntity.eventDate,
                    eventDuration = eventEntity.eventDuration,
                    startTime = eventEntity.startTime,
                    description = eventEntity.description,
                    eventType = eventEntity.eventType,
                };

                events.Add(eventDetail);
            }
            return events;
        }
        public void EditEvent(EventDTO eventDetail)
        {
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var eventEntity = eventContext.Event.Find(eventDetail.eventID);
            
            eventEntity.bookTitle = eventDetail.bookTitle;
            eventEntity.location = eventDetail.location;
            eventEntity.eventDate = eventDetail.eventDate;
            eventEntity.startTime = eventDetail.startTime;
            eventEntity.eventDuration = eventDetail.eventDuration;
            eventEntity.eventType = eventDetail.eventType;
            eventEntity.eventInvites = eventDetail.eventInvites;
            eventEntity.description = eventDetail.description;
            
            eventContext.Entry(eventEntity).State = EntityState.Modified;

            eventContext.SaveChanges();


        }

        public List<EventDTO> GetAllEvents()
        {
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var eventEntities = eventContext.Event;

            List<EventDTO> events = new List<EventDTO>();
            foreach (var eventEntity in eventEntities)
            {
                var eventDetail = new EventDTO()
                {
                    eventID = eventEntity.eventID,
                    bookTitle = eventEntity.bookTitle,
                    location = eventEntity.location,
                    eventDate = eventEntity.eventDate,
                    eventDuration = eventEntity.eventDuration,
                    startTime = eventEntity.startTime,
                    description = eventEntity.description,
                    eventType = eventEntity.eventType,
                };

                events.Add(eventDetail);
            }
            return events;
        }

        public void RemoveEvent(EventDTO eventDetail)
        {
            
            var eventEntity = new Event();
            eventEntity.eventID = eventDetail.eventID;
            eventEntity.bookTitle = eventDetail.bookTitle;
            eventEntity.location = eventDetail.location;
            eventEntity.eventDate = eventDetail.eventDate;
            eventEntity.startTime = eventDetail.startTime;
            eventEntity.eventDuration = eventDetail.eventDuration;
            eventEntity.eventType = eventDetail.eventType;
            eventEntity.eventInvites = eventDetail.eventInvites;
            eventEntity.description = eventDetail.description;
            eventEntity.userName = eventDetail.userName;
            eventContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var eventEntityMapper = eventContext.EventInviteMapper.Where(e => e.eventID == eventEntity.eventID);
            foreach (var eventMapper in eventEntityMapper) {
                eventContext.EventInviteMapper.Attach(eventMapper);
                eventContext.EventInviteMapper.Remove(eventMapper);
             }
            eventContext.Event.Attach(eventEntity);
            eventContext.Event.Remove(eventEntity);
            eventContext.SaveChanges();
        }

        public List<CommentDTO> GetComments(int? eventId)
        {
            List<CommentDTO> eventComments= new List<CommentDTO>();
          
            var commentEntity = eventContext.CommentsPosted.Where(comment => comment.eventID == eventId);
            if (commentEntity != null)
            {
                
                foreach (var comments in commentEntity)
                {
                    var getComment = new CommentDTO()
                    {
                        eventID = comments.eventID,
                        comments = comments.postComment,


                    };

                    eventComments.Add(getComment);
                }
              
            }
            return eventComments;
            
        }
        public void PostComment(CommentDTO postComment)
        {
            var commentDal = new Comments()
            {
                eventID = postComment.eventID,
                postComment = postComment.comments
            };
            eventContext.CommentsPosted.Add(commentDal);
            eventContext.SaveChanges();

        }
    }
}
