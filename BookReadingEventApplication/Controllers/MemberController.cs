using System;
using System.Web.Mvc;
using BookReadingEventApplication.Models;
using System.Net;
using System.Data;
using BookReadingEventApplication.Filters;
using BookEvent.Common;

namespace BookReadingEventApplication.Controllers
{
    [UserAuthenticationFilter]
    public class MemberController : Controller
    {
        // GET: Member
         Interceptor logger = new Interceptor();
         BLL.EventBLL eventBll = new BLL.EventBLL();
        EventDTO eventDto = new EventDTO();
        public ActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            logger.Info("Creating New Events");
            
           

            eventDto.bookTitle = form["bookTitle"].ToString();
            eventDto.location = form["location"].ToString();
            eventDto.eventDate = Convert.ToDateTime(form["eventDate"]);
            eventDto.startTime = form["startTime"];
            eventDto.eventDuration = Convert.ToInt32(form["eventDuration"]);
            eventDto.eventType = form["Event Type"].ToString();
            eventDto.eventInvites = form["eventInvites"];
            eventDto.description = form["description"];
            eventDto.userName = Session["Authenticated"].ToString();
            eventBll.CreateEvent(eventDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyEvents()
        {
           



            var eventDTO = eventBll.MyEvents(Session["Authenticated"].ToString());
            var eventWrap = new EventsWrappper();
            foreach (var events in eventDTO)
            {
                if (DateTime.Compare(events.eventDate, DateTime.Now) > 0)
                {

                    eventWrap.futureEvents.Add(events);
                }
                else
                {
                    eventWrap.pastEvents.Add(events);
                }
            }


            return View(eventWrap);
        }

        //[Authorize]
        public ActionResult EventsInvitedTo()
        {
           
            //To-Do
            var invites = eventBll.EventInvites(Session["Authenticated"].ToString());
            var eventWrap = new EventsWrappper();
            foreach (var events in invites)
            {
                if (DateTime.Compare(events.eventDate, DateTime.Now) > 0)
                {
                    
                    eventWrap.futureEvents.Add(events);
                }
                else
                {
                    eventWrap.pastEvents.Add(events);
                }
            }


            return View(eventWrap);
        }

        //Get:Edit
        public ActionResult Edit(int? id)
        {

            
            var eventInfo = eventBll.GetEventDetails(id);
            if (eventInfo != null)
            {
                var eventDetail = new Event();
                eventDetail.bookTitle = eventInfo.bookTitle;
                eventDetail.eventDate = eventInfo.eventDate;
                eventDetail.description = eventInfo.description;
                eventDetail.eventType = eventInfo.eventType;
                eventDetail.eventDuration = eventInfo.eventDuration;
                eventDetail.location = eventInfo.location;
                eventDetail.startTime = eventInfo.startTime;
               
                eventDetail.eventInvites = eventInfo.eventInvites;
                return View(eventDetail);
            }
            else
            {
                return RedirectToAction("UserView", "Home");
            }

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           

            var eventDetail = eventBll.GetEventDetails(id);

            if (TryUpdateModel(eventDetail, "",
               new string[] { "bookTitle", "eventDate", "description", "eventType", "eventDuration", "location", "startTime", "eventInvites" }))
            {
               
                    try
                    {
                        eventBll.EditEvent(eventDetail);
                        if (string.Equals(Session["Authenticated"].ToString(), "Admin"))
                            return RedirectToAction("Index", "Admin");
                        else
                        {
                            return RedirectToAction("UserView", "Home");
                        }
                    }

                    catch (DataException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                
            }
            return RedirectToAction("UserView", "Home");
        }
    }
}