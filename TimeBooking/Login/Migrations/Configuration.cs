namespace Login.Migrations
{
    using Login.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Login.Infrastructure.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Admin",
                Email = "markus.naslund@yaelev.se",
                EmailConfirmed = true,
                FirstName = "Markus",
                LastName = "Naslund",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)

            };

            manager.Create(user, "SuperUser@!");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("Admin");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
            manager.AddClaim(adminUser.Id, new System.Security.Claims.Claim(ClaimTypes.Role, "Admin"));
        }
    }
}
