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
        // IQueryable: you can order by, make where, etc etc
       Task<IEnumerable<City>> GetCities();
    }
}
