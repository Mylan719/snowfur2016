using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Repositories;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Facades
{
    public class PersonalProfileFacade : ApplicationFacadeBase
    {
        public PersonalProfileRepository PersonalProfileRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public PersonalProfileDto GetById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = PersonalProfileRepository.GetById(id);
                return Mapper.Map<PersonalProfileDto>(entity);
            }
        }

        public string GetUserNameById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var user = UserRepository.GetById(id);
                return user?.UserName ?? "<null>";
            }
        }

        public PersonalProfileDto GetPersonalProfile(int userId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var user = UserRepository.GetById(userId);
                if (user == null) { return null; }
                return GetById(user.PersonalProfile?.Id ?? 0);
            }
        }

        public void Insert(PersonalProfileDto profile, string userName)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var user = UserRepository.GetByUserName(userName);

                if (user == null)
                {
                    throw new UIException("Neexistuje meno užívateľa, profil sa nedá uložiť");
                }

                var entity = PersonalProfileRepository.InitializeNew();
                Mapper.Map(profile, entity);

                entity.User = user;
                PersonalProfileRepository.Insert(entity);

                uow.Commit();
            }
        }

        public void Update(PersonalProfileDto profile, int userId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var entity = PersonalProfileRepository.GetByUserId(userId);
                Mapper.Map(profile, entity);
                uow.Commit();
            }
        }

        public void Remove(int userId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (UserRepository.GetById(userId) == null) { throw new UIException("Neexistuje Id užívateľa, profil sa nedá zmazať"); }

                var profile = PersonalProfileRepository.GetByUserId(userId);

                if (UserRepository.GetById(userId) == null) { throw new UIException("Nenašiel sa profil, nedá sa zmazať"); }

                PersonalProfileRepository.Delete(profile);
            }
        }

        public bool ProfileExists(string userName)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var user = UserRepository.GetByUserName(userName);

                return user!= null && PersonalProfileRepository.GetByUserId(user.Id) != null;
            }
        }
    }
}
