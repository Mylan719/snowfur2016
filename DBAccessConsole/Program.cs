using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Queries;
using SnowFur.BL.Repositories;
using SnowFur.BL.UnitOfWork;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessConsole
{
    internal class Program
    {
        //Had to use mock because thread local does not work due to thread switching
        internal class MockRegistry : UnitOfWorkRegistryBase
        {
            private static Stack<IUnitOfWork> _stack = new Stack<IUnitOfWork>();

            protected override Stack<IUnitOfWork> GetStack() => _stack;
        }

        private static void Main(string[] args)
        {
            var provider = GetProviderInstance();
            var roomRepo = new RoomRepository(provider);
            var conventionRepo = new ConventionRepository(provider);
            var serviceRepo = new ServiceRepository(provider);

            var roomQuery = new RoomQuery(provider);

            using (var ouw = provider.Create())
            {
                var con = new Convention
                {
                    StartDate = new DateTime(2017, 1, 1),
                    EndDate = new DateTime(2017, 1, 4),
                    Name = "Comix Salon"
                };

                conventionRepo.Insert(con);

                serviceRepo.Insert(new Service { ChargeAfter = DateTime.UtcNow, Name = "1. Night", Price = 28, Type = SnowFur.DAL.Enums.ServiceType.AccomodationNight, Convention = con });
                serviceRepo.Insert(new Service { ChargeAfter = DateTime.UtcNow, Name = "VIP", Price = 40, Type = SnowFur.DAL.Enums.ServiceType.Additional, Convention = con });

                roomRepo.Insert(
                    new Room {
                        Capacity = 2,
                        Convention = con,
                        Name = "A1"
                    });

                roomRepo.Insert(
                    new Room
                    {
                        Capacity = 2,
                        Convention = con,
                        Name = "A2"
                    });

                roomRepo.Insert(
                    new Room
                    {
                        Capacity = 3,
                        Convention = con,
                        Name = "B1"
                    });

                ouw.Commit();
            }


            using (var uow = provider.Create())
            {
                roomQuery.ConventionName = "Comix Salon";
                roomQuery.Execute().ToList().ForEach(r =>
                {
                    Console.WriteLine($"{r.Name}: {r.BedCount} beds.");
                });
            }
        }

        private static ApplicationUnitOfWorkProvider GetProviderInstance()
        {
            return new ApplicationUnitOfWorkProvider(new MockRegistry(), () => new ApplicationDbContextContainer());
        }
    }
}
