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

        private ConventionFacade conventionFacade;

        public GridViewDataSet<ConventionListDto> Conventions { get; set; } = new GridViewDataSet<ConventionListDto>()
        {
            SortExpression = nameof(ConventionListDto.IsActive),
            PageSize = 4
        };

        public ConventionsViewModel(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
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
            });
        }

        public void EditConvention(int id)
        {
            ReportErrors(() =>
            {
                ConventionDetail = new ConventionDetailForm(conventionFacade.GetConventionDetail(id));
            });
        }
    }
}

