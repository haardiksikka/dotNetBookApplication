using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BookEvent.Common;
using BookReadingEventApplication.Filters;
using BookReadingEventApplication.Models;
namespace BookReadingEventApplication.Controllers
{
    public class HomeController : Controller
    {
        private BLL.EventBLL eventBll= new BLL.EventBLL();
        private BLL.userBLL userBll = new BLL.userBLL();
        private Interceptor logger = new Interceptor();
        private ApplicationMessages appMessages = new ApplicationMessages(); 
        
        public ActionResult Index()
        {
            logger.Info("Entering Home Page");
            var eventDetail = eventBll.GetEvents();
            logger.Info("Getting Public Events to display them on Home Page");
            EventsWrappper eventWrap = new EventsWrappper();
          
            foreach(var events in eventDetail)
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
         
             eventWrap.futureEvents= eventWrap.futureEvents.OrderByDescending(o => o.eventDate).ToList();
            eventWrap.pastEvents = eventWrap.pastEvents.OrderByDescending(o => o.eventDate).ToList();
            return View("Index",eventWrap);
        }

              
        public ActionResult Login()
        {
            logger.Info("Entering Login Page");
            return View("Login");
        }


        [HttpPost]
        public ActionResult Login([Bind(Include = "email,password")] User user)
        {

            userDTO userDto;
           
            userDto = userBll.find(user.email);
            if (userDto == null)
            {
                logger.Info(appMessages.userDoesNotExist);
               
                ModelState.AddModelError("", appMessages.userDoesNotExist);
                return View("Login");
            }
            else
            {
                if (string.Equals(userDto.password, user.password)){
                    Session["Authenticated"] = userDto.userName;
                    if (string.Equals(userDto.email, "myAdmin@event.com")){
                        logger.Info(appMessages.adminLoginSuccessMessage);
                        return RedirectToAction("Index", "Admin");
                    }
                   
                    logger.Info(appMessages.userLoginSuccessMessage);
                    return RedirectToAction("UserView");
                }
                else
                {
                    logger.Info(appMessages.loginFailed);
                    ModelState.AddModelError("", appMessages.loginFailed);
                    return View("Login");
                }
            }
          
        }

        public ActionResult Logout()
        {
            logger.Info(string.Format("Logging Out User {0}", Session["Authenticated"].ToString()));
            Session.Remove("Authenticated");
            return RedirectToAction("Index");
        }



        public ActionResult CreateUser()
        {
            logger.Info(appMessages.creatingNewUser);
            return View();
        }



        [HttpPost]
        public ActionResult CreateUser([Bind(Include = "userName,email,password")] User user)
        {
            logger.Info(appMessages.creatingNewUser);
            var userDto = new BookEvent.Common.userDTO();
            userDto.userName = user.userName;
            userDto.email = user.email;
            userDto.password = user.password;
            int status = userBll.createUser(userDto);
            if (status == 0)
            {
                logger.Info(appMessages.userAlreadyExist);
                ModelState.AddModelError("",appMessages.userAlreadyExist);
                return View();
            }
            logger.Info(appMessages.newUserCreated);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            
                logger.Info("Fetching Event Details");
                var eventInfo = eventBll.GetEventDetails(id);
            if (eventInfo != null)
            {
                logger.Info("fetching comments associated with event");
                var eventComments = eventBll.GetComments(id);
                var eventCommentModel = new EventCommentWrapper();
                if (eventComments != null)
                {
                    
                    List<Comment> commentPosted = new List<Comment>();
                    foreach (var comments in eventComments)
                    {
                        var commentModel = new Comment()
                        {
                            comment = comments.comments,
                           
                        };

                        eventCommentModel.commentList.Add(commentModel);
                    }
                }
                else
                {
                    eventCommentModel.commentList.Add(new Comment { comment = "No comments on this post" });
                }
                var eventDetail = new Event();
                eventDetail.bookTitle = eventInfo.bookTitle;
                eventDetail.eventDate = eventInfo.eventDate;
                eventDetail.description = eventInfo.description;
                eventDetail.eventType = eventInfo.eventType;
                eventDetail.eventDuration = eventInfo.eventDuration;
                eventDetail.location = eventInfo.location;
                eventDetail.startTime = eventInfo.startTime;
                
                eventDetail.noOfInvites = eventInfo.eventInvites.Split(',').Length;
                eventDetail.userName = eventInfo.userName;
                eventCommentModel.eventModel = eventDetail;
                return View(eventCommentModel);
            }
            else
            {
                return View("Index");
            }
        }
        [UserAuthenticationFilter]
        public ActionResult UserView()
        {
            try
            {
                var Eventbll = new BLL.EventBLL();
                var eventDetail = Eventbll.GetAllUserEvents(Session["Authenticated"].ToString());
                EventsWrappper eventWrap = new EventsWrappper();

                foreach (var events in eventDetail)
                {
                    if (DateTime.Compare(events.eventDate, DateTime.Now) > 0)
                    {
                        // eventWrap.futureEvents.Add(events);
                        eventWrap.futureEvents.Add(events);
                    }
                    else
                    {
                        eventWrap.pastEvents.Add(events);
                    }
                }



                return View(eventWrap);
            }
            catch (Exception)
            {
                logger.Info("Exception handled");
                ModelState.AddModelError("", "Session not set/ unauthorize access");
                return View("Index");
            }
        }
        [HttpPost,ActionName("Details")]
        public ActionResult PostComment(int id, string postComment)
        {
            logger.Info("Posting new comment");
            
            var commentModel = new Comment();
            commentModel.eventId = id;
            commentModel.comment = postComment;
            var commentDTO = new CommentDTO()
            {
                eventID = commentModel.eventId,
                comments = commentModel.comment
            };
            eventBll.PostComment(commentDTO);
            return RedirectToAction("Details",new { eventId=id});
        }
       
        
    }
}