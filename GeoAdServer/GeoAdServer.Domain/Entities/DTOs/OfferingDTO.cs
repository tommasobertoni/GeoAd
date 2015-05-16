﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoAdServer.Domain.Entities.DTOs
{
    public class OfferingDTO : ReflectiveEquals
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public string Desc { get; set; }

        public long InsDateMillis { get; set; }

        public long ExpDateMillis { get; set; }
    }
}
