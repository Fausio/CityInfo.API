using AutoMapper;
using CityInfo.DOMAIN.DTOs;
using CityInfo.DOMAIN.Models;

namespace CityInfo.API.Profiles
{
    public class PointsOfInteresProdile : Profile
    {
        public PointsOfInteresProdile()
        {
            CreateMap<PointsOfInterest, PointsOfInterestDTO>();
            CreateMap<PointsOfInterest, PointsOfInterestUpdateDTO>();
            CreateMap<PointsOfInterestCreateDTO, PointsOfInterest>();
            CreateMap<PointsOfInterestUpdateDTO, PointsOfInterest>();
        }
    }
}
