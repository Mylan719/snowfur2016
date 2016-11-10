using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowFur.BL.Dtos;
using SnowFur.BL.Repositories;
using SnowFur.BL.Services;

namespace SnowFur.BL.Facades
{
    public class PersonalProfileFacade
    {
        private readonly PersonalProfileService personalProfileService;

        public PersonalProfileFacade(PersonalProfileService personalProfileService)
        {
            this.personalProfileService = personalProfileService;
        }

        public int CurrentUserId { get; set; }

        public PersonalProfileDto GetPersonalProfile()
            => personalProfileService.GetPersonalProfile(CurrentUserId);

        public bool ProfileExists(string userName)
            => personalProfileService.ProfileExists(userName);

        public void Update(PersonalProfileDto profileDto)
            => personalProfileService.Update(profileDto, CurrentUserId);
    }
}
