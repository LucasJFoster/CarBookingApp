using CarBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBookingApp.Repositories.Contracts
{
    public interface ICarsModelsRepository : IGenericRepository<CarModel>
    {
        Task<List<CarModel>> GetCarModelsByMake(int makeId);
        Task<CarModel> GetCarModelsWithDetail(int id);
    }
}
