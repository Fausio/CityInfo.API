﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.DOMAIN.DTOs
{
    public class PointsOfInterestCreateDTO
    { 
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
