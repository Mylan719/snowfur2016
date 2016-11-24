using AutoMapper;
using SnowFur.BL.Installers;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Queries
{
    public class AttendeeAdminListDto : IMapperInstaller
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPayed { get; set; }
        public string DatePaidFormated { get; set; }
        public decimal AmountPayed { get; set; }
        public string Note { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, AttendeeAdminListDto>()
                .ForMember(m => m.FirstName, a => a.MapFrom(s => s.PersonalProfile.FirstName))
                .ForMember(m => m.FirstName, a => a.MapFrom(s => s.PersonalProfile.FirstName));
        }
    }
}