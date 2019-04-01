using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEvent.Common;
using DAL.Controller;
namespace BLL
{
    public class userBLL
    {
        public int createUser(userDTO newUser)
        {
            var userDal = new UserDAL();
            var userDetail = find(newUser.email);
            if (userDetail!=null)
            {
                return 0;
            }
            else {
                userDal.createUser(newUser);
                return 1;
            }
        }

        public userDTO find(string email)
        {
            var userDal = new UserDAL();
            return userDal.find(email);
        }
        //public List<userDTO> GetEvents(userDTO user)
        //{
        //    var userDal = new UserDAL();
        //    return userDal.GetEvents(user);
        //}
    }
}
