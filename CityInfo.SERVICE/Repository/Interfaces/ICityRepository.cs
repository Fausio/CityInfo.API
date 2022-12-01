﻿using CityInfo.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.SERVICE.Repository.Interfaces
{
    public interface ICityRepository
    {
        // IQueryable: you can order by, make where, etc etc
        Task<IEnumerable<City>> Read();
        Task<bool> ReadExists(int cityId);
        Task<City?> Read(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointsOfInterest>> ReadPointsOfInterestForCity(int cityId);
        Task<PointsOfInterest?> ReadPointsOfInterestForCity(int cityId, int pointsOfInterestId);
        Task CreatePointsOfInterest(int cityId, PointsOfInterest model);
    }
}