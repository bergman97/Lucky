using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TimeBooker.Models
{
    public class TimeContext : DbContext
    {
        public TimeContext() 
            : base()
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<TimeContext>());

        }

        public DbSet <AppointmentInfo> Appointments { get; set; }

    }
}