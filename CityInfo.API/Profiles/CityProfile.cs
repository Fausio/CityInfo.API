using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.DOMAIN.DTOs;
using CityInfo.DOMAIN.Models;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTOWithoutPointsOfInterest>();
            CreateMap<City, CityDTO>();
        }
    }
}
