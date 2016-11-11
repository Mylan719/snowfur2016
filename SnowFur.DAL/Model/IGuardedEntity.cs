﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public interface IGuardedEntity
    {
        DateTime? DateDeleted { get; set; }
        DateTime? DateUpdated { get; set; }
        DateTime DateCreated { get; set; }
    }
}