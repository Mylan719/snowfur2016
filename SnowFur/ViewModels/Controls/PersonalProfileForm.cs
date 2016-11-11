using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowFur.BL.Installers;
using AutoMapper;
using SnowFur.BL.Dtos;

namespace SnowFur.ViewModels.Controls
{
    public class PersonalProfileForm : OwinViewModelBase, IMapperInstaller
    {
        [MaxLength(100, ErrorMessage = "Maximálna dĺžka je 100 znakov.")]
        [Required(ErrorMessage = "Zadajte prosím meno")]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Maximálna dĺžka je 100 znakov.")]
        [Required(ErrorMessage = "Zadajte prosím priezvysko")]
        public string LastName { get; set; }

        [MaxLength(200, ErrorMessage = "Maximálna dĺžka je 200 znakov.")]
        [Required(ErrorMessage = "Zadajte prosím ulicu")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "Maximálna dĺžka je 100 znakov.")]
        [Required(ErrorMessage = "Zadajte prosím mesto")]
        public string City { get; set; }

        [MaxLength(6, ErrorMessage = "Maximálna dĺžka je 6 znakov.")]
        [Required(ErrorMessage = "Zadajťe prosím PSČ.")]
        [RegularExpression(@"^[0-9\s]+$", ErrorMessage = "PSČ nie je v správnom tvare.")]
        public string ZipCode { get; set; }

        [MaxLength(100, ErrorMessage = "Maximálna dĺžka je 100 znakov.")]
        public string CountryName { get; set; }

        public bool Adult { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PersonalProfileForm, PersonalProfileDto>().ReverseMap();
        }
    }
}
