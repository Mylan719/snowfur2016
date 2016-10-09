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

        public IDbSet<Convention> Conventions { get; set; }
        public IDbSet<PersonalProfile> Profiles { get; set; }

        public IDbSet<Service> Services { get; set; }
        public IDbSet<ServiceOrder> ServiceOrders { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<RoomReservation> RoomReservations { get; set; }

        public IDbSet<ConventionPayment> ConventionPaymens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
