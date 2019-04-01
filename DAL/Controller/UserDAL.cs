using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEvent.Common;
using DAL.Models;
namespace DAL.Controller
{
    public class UserDAL
    {
        private EventContext userContext;

        public UserDAL()
        {
            userContext = new EventContext();
        }


        public void createUser(userDTO newUser)
        {
           
            var userEntity = new User();
            userEntity.userName = newUser.userName;
            userEntity.email = newUser.email;
            userEntity.password = newUser.password;
            userContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            userContext.User.Add(userEntity);
            userContext.SaveChanges();

        }


        public userDTO find(string email)
        {
            userContext.Database.Log = s => { System.Diagnostics.Debug.WriteLine(s); };
            var userEntity = userContext.User.SingleOrDefault(user => string.Equals(user.email, email));
            
            if (userEntity == null)
            {
                return null;
            }
            else
            {
                var userDto = new userDTO();
                userDto.userName = userEntity.userName;
                userDto.password = userEntity.password;
                userDto.email = userEntity.email;
                return userDto;
            }
        }
        //public List<userDTO> GetEvents(userDTO user)
        //{
        //    var userContext = new EventContext();
        //    var eventEntities = userContext.Event.Where(events => events.eventInvites);
        //}
 
    }
}

