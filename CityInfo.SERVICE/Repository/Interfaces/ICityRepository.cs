using CityInfo.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.SERVICE.Repository.Interfaces
{
    public interface ICityRepository
    {
        Task DeletePointsOfInterests(int cityId, int includePointsOfInterestId);

        // IQueryable: you can order by, make where, etc etc
        Task<IEnumerable<City>> Read();
        Task<IEnumerable<City>> Read(string? name);
        Task<bool> ReadExists(int cityId);
        Task<City?> Read(int cityId, bool includePointsOfInterest);

        Task<IEnumerable<PointsOfInterest>> ReadPointsOfInterestForCity(int cityId);
        Task<PointsOfInterest?> ReadPointsOfInterestForCity(int cityId, int pointsOfInterestId);
        Task CreatePointsOfInterest(int cityId, PointsOfInterest model);
        Task UpdatePointsOfInterest(int cityId, int pointsOfInterestId, PointsOfInterest model);
        Task<int> SaveChangesAsync();
    }
}