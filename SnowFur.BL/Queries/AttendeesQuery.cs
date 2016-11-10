﻿using SnowFur.BL.Dtos;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Filters;

namespace SnowFur.BL.Queries
{
    public class AttendeesQuery : ApplicationQueryBase<AttendeeListItemDto>
    {
        public ConventionFilter Filter { get; set; }

        public AttendeesQuery(IUnitOfWorkProvider provider)
            : base(provider)
        { }

        protected override IQueryable<AttendeeListItemDto> GetQueryable()
            => Context.RoomReservations
                .Include("User")
                .Where(rr => rr.Room.Convention.Id == Filter.ConventionId)
                .Select(rr => rr.User)
                .ProjectTo<AttendeeListItemDto>();

        protected override void PostProcessResults(IList<AttendeeListItemDto> results)
        {
            int ordinalNumber = 1;
            foreach (var attendee in results)
            {
                attendee.Count = ordinalNumber++;
            }
        }
    }
}
