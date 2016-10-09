using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class ServiceOrder : IEntity<int>
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ServiceId { get; set; }
       
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

    }
}
