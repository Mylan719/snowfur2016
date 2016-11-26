using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;
using SnowFur.ViewModels.Controls;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class ConventionsViewModel : PageViewModelBase
    {
        public ConventionDetailForm ConventionDetail { get; set; }

        private readonly ConventionFacade conventionFacade;

        public GridViewDataSet<ConventionListDto> Conventions { get; set; } = new GridViewDataSet<ConventionListDto>()
        {
            SortExpression = nameof(ConventionListDto.IsActive),
            SortDescending = true,
            PageSize = 100
        };

        public ConventionsViewModel(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            LogoText = "Convention Admin";
            SubpageTitle = "Conventions";
        }

        public override Task Load()
        {
            ConventionDetail = ConventionDetail ?? new ConventionDetailForm();
            ConventionDetail.Load(conventionFacade, this);
            return base.Load();
        }

        public override Task PreRender()
        {
            conventionFacade.LoadList(Conventions);
            return base.PreRender();
        }

        public void ActivateConvention(int id)
        {
            ReportErrors(() =>
            {
                conventionFacade.ActivateConvention(id);
                var con = conventionFacade.GetConventionDetail(id);
                SuccessMessage = $"Convention {con.Name} aktivovaná.";
            });
        }

        public void EditConvention(int id)
        {
            ReportErrors(() =>
            {
                ConventionDetail.Detail = conventionFacade.GetConventionDetail(id);
            });
        }

        public void AddConvention()
        {
            ReportErrors(() =>
            {
                ConventionDetail.Detail = new ConventionDetailDto();
            });
        }
    }
}

