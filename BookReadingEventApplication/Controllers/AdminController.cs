using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReadingEventApplication.Filters;
using BookReadingEventApplication.Models;

namespace BookReadingEventApplication.Controllers
{
    [UserAuthenticationFilter]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var Eventbll = new BLL.EventBLL();
            var eventDetail = Eventbll.GetAllEvents();
            EventsWrappper eventWrap = new EventsWrappper();
            
            foreach (var events in eventDetail)
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventBll = new BLL.EventBLL();

            var eventDetail = eventBll.GetEventDetails(id);
            var eventInfo = new Event();
            eventInfo.bookTitle = eventDetail.bookTitle;
            eventInfo.eventDate = eventDetail.eventDate;
            eventInfo.description = eventDetail.description;
            eventInfo.eventType = eventDetail.eventType;
            eventInfo.eventDuration = eventDetail.eventDuration;
            eventInfo.location = eventDetail.location;
            eventInfo.startTime = eventDetail.startTime;
            eventInfo.userName = eventDetail.userName;
            eventInfo.eventInvites = eventDetail.eventInvites;
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventInfo);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eventBll = new BLL.EventBLL();

            var eventDetail = eventBll.GetEventDetails(id);
            eventBll.RemoveEvent(eventDetail);
            return RedirectToAction("Index");

        }
    }
}