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
            context.Conventions.AddOrUpdate(c=> new Convention
            {
                IsActive = true,
                Description = "<b>Lol</b>",
                Activities = "<ul><li>Foo</li><li>Bar</li></ul>",
                ContactInfo = "Nope",
                Name = "MyCon",
                PlaceName = "Kvolna",
                GpsLatitude = 40,
                GpsLongitude = 18,
                DateCreated = DateTime.UtcNow
            });
        }
    }
}
