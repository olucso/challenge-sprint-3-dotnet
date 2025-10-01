using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.Repositories.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<PageResultModel<IEnumerable<MotorcycleEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3);
        Task<MotorcycleEntity?> GetById(int id);
        Task<MotorcycleEntity?> Add(MotorcycleEntity entity);
        Task<MotorcycleEntity?> Update(int Id, MotorcycleEntity entity);
        Task<MotorcycleEntity?> Delete(int Id);
    }
}
