using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using DAL.Models;

namespace DAL.Controller
{
    public class EventContext : DbContext
    {
        public EventContext() : base("EventContext")
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventInviteMapper> EventInviteMapper { get; set; }
        public DbSet<Comments> CommentsPosted { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
