using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class ApplicationDbContextContainer : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContextContainer() : base("ApplicationContext")
        {
        }

        public IDbSet<PersonalProfile> Profiles { get; set; }
        public IDbSet<RoomMateOffer> RoomMates { get; set; }
        public IDbSet<RoomReservation> RoomReservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<RoomMateOffer>()
               .HasRequired(f => f.Receiver)
               .WithRequiredDependent()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomMateOffer>()
               .HasRequired(f => f.Sender)
               .WithRequiredDependent()
               .WillCascadeOnDelete(false);
        }
    }
}
