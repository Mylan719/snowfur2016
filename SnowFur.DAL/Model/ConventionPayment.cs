using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class ConventionPayment : IEntity<int>, IGuardedEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public int UserId { get; set; }
        public int ConventionId { get; set; }

        public DateTime? DateDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(ConventionId))]
        public Convention Convention { get; set; }
    }
}
