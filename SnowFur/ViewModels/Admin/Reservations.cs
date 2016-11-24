using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Facades;
using SnowFur.BL.Dtos;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Queries;
using SnowFur.ViewModels.Controls;
using SnowFur.BL.Repositories;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class Reservations : PageViewModelBase
    {
        public PaimentConfirmationForm PaimentConfirmationForm { get; set; }

        public GridViewDataSet<AttendeeAdminListDto> Attendees { get; set; }
          = new GridViewDataSet<AttendeeAdminListDto>()
          {
              SortExpression = nameof(AttendeeAdminListDto.UserName),
              PageSize = 60
          };


        private ConventionFacade conventionFacade;

        public Reservations(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            conventionFacade.InitializeActiveConvention();

            PaimentConfirmationForm = new PaimentConfirmationForm(conventionFacade);

            SubpageTitle = "Rezervácie";
        }

        public override Task Init()
        {
            if (PaimentConfirmationForm != null)
            {
                PaimentConfirmationForm.ParentViewModel = this;
            }

            return base.Init();
        }

        public override Task PreRender()
        {
            conventionFacade.FillAdminAtendees(Attendees);

            return base.PreRender();
        }

        public void ConfirmPayment(int userId)
        {
            PaimentConfirmationForm.Show(userId);
        }
    }
}
