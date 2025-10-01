using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task<PageResultModel<IEnumerable<RentalEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3);
        Task<RentalEntity?> GetById(int id);
        Task<RentalEntity?> Add(RentalEntity entity);
        Task<RentalEntity?> Update(int Id, RentalEntity entity);
        Task<RentalEntity?> Delete(int Id);
    }
}
