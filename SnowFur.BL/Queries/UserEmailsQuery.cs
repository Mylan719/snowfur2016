using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Queries
{
    public class UserEmailsQuery : ApplicationQueryBase<UserEmailListDto>
    {
        public UserEmailFilter Filter { get; set; } = new UserEmailFilter { MailConfirmed = true };

        public UserEmailsQuery(IUnitOfWorkProvider provider) : base (provider)
        {}
        protected override IQueryable<UserEmailListDto> GetQueryable()
        {
            var query = Context.Users.AsQueryable();
            

            if (!Filter.MailConfirmed)
            {
                query = query.Where(user => !user.EmailConfirmed);
            }

            if (!Filter.MailUnconfirmed)
            {
                query = query.Where(user => user.EmailConfirmed);
            }

            return query.ProjectTo<UserEmailListDto>();
        }
    }
}
