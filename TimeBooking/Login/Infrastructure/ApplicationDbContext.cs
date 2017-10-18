using Login.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Login.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

          public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)

        {
            Configuration.ProxyCreationEnabled = false;

            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public DbSet<Bookings> Booking { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                IList<Bookings> defaultbooking = new List<Bookings>();

                defaultbooking.Add(new Bookings() { Description = "Bokad1", StartDate = new DateTime(2017, 10, 02), EndDate = new DateTime(2017, 10, 03) });
                defaultbooking.Add(new Bookings() { Description = "Bokad2", StartDate = new DateTime(2017, 10, 10), EndDate = new DateTime(2017, 10, 13) });
                defaultbooking.Add(new Bookings() { Description = "Bokad3", StartDate = new DateTime(2017, 10, 15), EndDate = new DateTime(2017, 10, 19) });
                defaultbooking.Add(new Bookings() { Description = "Bokad4", StartDate = new DateTime(2017, 10, 20), EndDate = new DateTime(2017, 10, 21) });


                foreach (Bookings i in defaultbooking)
                {
                    context.Booking.Add(i);
                }

                base.Seed(context);
            }
        }

    }
}