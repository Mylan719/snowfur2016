using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Extensions;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class ServicesAdmin : PageViewModelBase
    {
        private readonly ConventionFacade conventionFacade;

        public GridViewDataSet<ServiceDetailDto> Services { get; set; } = new GridViewDataSet<ServiceDetailDto>()
        {
            PageSize = 100,
            PrimaryKeyPropertyName = nameof(ServiceDetailDto.Id),
            SortExpression = nameof(ServiceDetailDto.Name)
        };

        public List<ServiceTypeDto> ServiceTypes { get; set; }

        public int? DeletePropmptId { get; set; }

        public ServiceDetailDto NewDetail { get; set; }

        [Bind(Direction.ServerToClient)]
        public string ConventionName => conventionFacade.ActiveConventionName;

        public ServicesAdmin(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            LogoText = "Convention Admin";
            SubpageTitle = "Cenník";
        }

        public override Task Init()
        {
            conventionFacade.InitializeActiveConvention();
            NewDetail = GetNewService();
            ServiceTypes = conventionFacade.GetServiceTypes();
            return base.Init();
        }

        public override Task PreRender()
        {
            conventionFacade.FillServices(Services);
            return base.PreRender();
        }

        public void Edit(int serviceId)
        {
            ReportErrors(() =>
            {
                Services.EditRowId = serviceId;
            });
        }

        public void Add()
        {
            ReportErrors(() =>
            {
                HandleDatePicker(NewDetail);
                conventionFacade.Save(NewDetail);
                NewDetail = GetNewService();
            });
        }

        public void SaveEdit(ServiceDetailDto service)
        {
            ReportErrors(() =>
            {
                HandleDatePicker(service);
                conventionFacade.Save(service);
                Services.EditRowId = null;
            });
        }

        private static void HandleDatePicker(ServiceDetailDto service)
        {
            if (service.IsChargeAfterSet)
            {
                service.ChargeAfter = service.ChargeAfterString.FromHtmlPickerFormatDate();
            }
            else
            {
                service.ChargeAfter = null;
            }
        }

        public void CancelEdit()
        {
            Services.EditRowId = null;
        }

        public void Delete(int serviceId, bool force)
        {
            ReportErrors(() =>
            {
                if (!force && conventionFacade.GetOrderCount(serviceId) > 0)
                {
                    DeletePropmptId = serviceId;
                    return;
                }
                conventionFacade.RemoveService(serviceId);
                DeletePropmptId = null;
            });
        }


        private ServiceDetailDto GetNewService() => new ServiceDetailDto { ConventionId = conventionFacade.ActiveConventionId };
    }
}