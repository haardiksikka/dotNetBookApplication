using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEvent.Common;

namespace BLL
{
    public class EventBLL
    {
        private DAL.Controller.EventDAL eventDal;
        public EventBLL()
        {
            eventDal = new DAL.Controller.EventDAL();
        }
        public List<EventDTO> GetEvents()
        {
            
            var events = eventDal.GetEvents();
            return events;
        }

        public List<EventDTO> GetAllUserEvents(string userName)
        {
            var eventDal = new DAL.Controller.EventDAL();
            var events = eventDal.GetAllUserEvents(userName);
            return events;
        }
        public List<EventDTO> GetAllEvents()
        {
           
             return eventDal.GetAllEvents();

        }
        public void CreateEvent(EventDTO newEvent)
        {
            
            eventDal.CreateEvent(newEvent);
        }

        public List<EventDTO> MyEvents(string userName)
        {
            
            return eventDal.MyEvents(userName);
        }

        public List<EventDTO> EventInvites(string userName)
        {
           
            return eventDal.EventInvites(userName);

        }

        public EventDTO GetEventDetails(int? id)
        {
           
                return eventDal.GetEventDetails(id);
        }
        public void EditEvent(EventDTO eventDetail)
        {
           
            eventDal.EditEvent(eventDetail);
        }

        public void RemoveEvent(EventDTO eventDetail)
        {
           
            eventDal.RemoveEvent(eventDetail);
        }

        public List<CommentDTO> GetComments(int? eventId)
        {
            
            return eventDal.GetComments(eventId);
        }

        public void PostComment(CommentDTO postComment)
        {
            eventDal.PostComment(postComment);
        }
    }
}
