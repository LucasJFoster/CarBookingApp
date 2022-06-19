using CarBookingApp.Data;
using CarBookingApp.Repositories.Contracts;
using CarBookingApp.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBookingApp.Repositories.Repositories
{
    public class CarModelsRepository : GenericRepository<CarModel>, ICarsModelsRepository
    {
        private readonly CarBookingAppDbContext _context;

       public CarModelsRepository(CarBookingAppDbContext context) : base(context)
        {
            this._context = context;
        }
        
         public async Task<List<CarModel>> GetCarModelsByMake(int makeId)
        {
            var models = await _context.CarModels
               .Where(q => q.MakeId == makeId)
               .ToListAsync();

            return models;
        }

        public Task<CarModel> GetCarModelsWithDetail(int id)
        {
            return _context.CarModels.Include(q => q.Make).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}

