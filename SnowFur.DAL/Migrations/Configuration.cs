using System.Collections.Generic;
using System.Configuration;
using SnowFur.DAL.Model;

namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SnowFur.DAL.Model.ApplicationDbContextContainer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SnowFur.DAL.Model.ApplicationDbContextContainer";
        }

        protected override void Seed(SnowFur.DAL.Model.ApplicationDbContextContainer context)
        {
            context.Roles.AddOrUpdate(new Role
            {
                Id = 1,
                Name = "admin",
            });

            context.Users.AddOrUpdate(new User
            {
                Id = 1,
                UserName = "admin",
                DateCreated = DateTime.UtcNow,
                Email = "admin@aa.aa",
                EmailConfirmed = true,
                Roles = {
                    new UserRole
                    {
                        RoleId = 1
                    }
                },
                SecurityStamp = "08058364-ce39-44c9-8650-d5b44de3c087",
                PasswordHash = "AJdgbg6YJdYKpNokDKo01uzURfGz1Qwt2q8naJc+Hz2l/LEgkLmxy/L6RfR8vWVSUQ=="
            });

            context.Conventions.AddOrUpdate(new Convention
            {
                IsActive = true,
                Description = "<b>Lol</b>",
                Activities = "<ul><li>Foo</li><li>Bar</li></ul>",
                ContactInfo = "Nope",
                Name = "MyCon",
                PlaceName = "Kvolna",
                GpsLatitude = 40,
                GpsLongitude = 18,
                DateCreated = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(-6)
            });

            context.SaveChanges();
        }
    }
}
