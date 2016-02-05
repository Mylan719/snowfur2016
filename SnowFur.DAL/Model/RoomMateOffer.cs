using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class RoomMateOffer : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string MessageForReceiver { get; set; }
        public bool IsConfirmed { get; set; }

        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
    }
}
