using CityInfo.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.DOMAIN.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfPointsOfInterest { get { return PointsOfInterests.Count; } }

        public ICollection<PointsOfInterestDTO> PointsOfInterests { get; set; } = new List<PointsOfInterestDTO>();
    }
}
