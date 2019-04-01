using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class EventInviteMapper
    {
        [Key]
        public int inviteID { get; set; }
        public int eventID { get; set; }
        public string emails { get; set; }
    }
}
