using Riganti.Utils.Infrastructure.Core;
using SnowFur.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class Service : IEntity<int>, IGuardedEntity
    {
        public int Id { get; set; }

        public int ConventionId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ServiceType Type { get; set; }
        public DateTime? ChargeAfter { get; set; }

        public DateTime? DateDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(ConventionId))]
        public Convention Convention { get; set; }
    }
}
