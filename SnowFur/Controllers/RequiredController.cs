using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;

namespace SnowFur.Controllers
{
    [System.Web.Mvc.Route("api/[controller]/[action]")]
    public class RequiredController : ApiController
    {
        public ConventionFacade ConventionFacade { get; set; }

        [System.Web.Http.HttpGet]
        public IEnumerable<AttendeeListItemDto> GetAttendees()
        {
            ConventionFacade.InitializeActiveConvention();
            return ConventionFacade.GetAtendees();
        }

        //TODO: FIXME Never in production just test
        [HttpPost]
        [Route("convention/activate/{conventionId}")]
        public void Put(int conventionId)
        {
            ConventionFacade.InitializeActiveConvention();
            ConventionFacade.ActivateConvention(conventionId);
        }
    }
}