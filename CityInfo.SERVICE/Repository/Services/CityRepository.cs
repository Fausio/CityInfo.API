using CityInfo.DATA.DbContext;
using CityInfo.DOMAIN.Helpers;
using CityInfo.DOMAIN.Models;
using CityInfo.SERVICE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.SERVICE.Repository.Services
{
    public class CityRepository : ICityRepository
    {
        private AppDbContext _db;

        public CityRepository(AppDbContext appDb)
        {
            this._db = appDb;
        }

        public async Task CreatePointsOfInterest(int cityId, PointsOfInterest model)
        {

            if (!await ReadExists(cityId))
            {
                throw new Exception($"City with id {cityId} Not Found When create Points Of Interest");
            }

            var modelToSave = await Read(cityId, false);
            modelToSave.PointsOfInterests.Add(model);
            await _db.SaveChangesAsync();
        }
        public async Task UpdatePointsOfInterest(int cityId, int pointsOfInterestId, PointsOfInterest model)
        {
            if (!await ReadExists(cityId))
            {
                throw new Exception($"City with id {cityId} Not Found When update Points Of Interest");
            }

            var result = await ReadPointsOfInterestForCity(cityId, pointsOfInterestId);

            if (result is null)
            {
                throw new Exception("PointsOfInterests NotFound");
            }

            result.Name = model.Name;
            result.Description = model.Description;

            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<City>> Read()
        {
            return await _db.Cities.OrderBy(c => c.Name)
                                   .ToListAsync();
        }

        public async Task<City?> Read(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _db.Cities.Include(c => c.PointsOfInterests)
                                       .FirstOrDefaultAsync(c => c.Id == cityId);
            }

            return await _db.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<bool> ReadExists(int cityId) => await _db.Cities.AnyAsync(c => c.Id == cityId);

        public async Task<IEnumerable<PointsOfInterest>> ReadPointsOfInterestForCity(int cityId)
        {
            return await _db.PointsOfInterests.Where(p => p.CityId == cityId)
                                              .OrderBy(c => c.Name)
                                              .ToListAsync();
        }

        public async Task<PointsOfInterest?> ReadPointsOfInterestForCity(int cityId, int pointsOfInterestId)
        {
            return await _db.PointsOfInterests.FirstOrDefaultAsync(p => p.CityId == cityId && p.Id == pointsOfInterestId);

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task DeletePointsOfInterests(int cityId, int includePointsOfInterestId)
        {

            if (!await ReadExists(cityId))
            {
                throw new Exception($"City with id {cityId} Not Found When delete Points Of Interest");
            }

            var result = await ReadPointsOfInterestForCity(cityId, includePointsOfInterestId);
            if (result is null)
            {
                throw new Exception("PointsOfInterests NotFound");
            }

            _db.PointsOfInterests.Remove(result);
            await SaveChangesAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> Read(string? name, string? search, int PAGE_NUMBER, int PAGE_SIZE)
        {

            var result = _db.Cities as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                result = result.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                result = result.Where(c => c.Name.Contains(search) || c.Description != null && c.Description.Contains(search));
            }

            var totalItemCount = await _db.Cities.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, PAGE_SIZE, PAGE_NUMBER);


            var CiiesResult = await result.OrderBy(c => c.Name)
                               .Skip(PAGE_SIZE * (PAGE_NUMBER - 1))
                               .Take(PAGE_SIZE)
                               .ToListAsync();

            return (CiiesResult, paginationMetadata);
        }
    }
}
