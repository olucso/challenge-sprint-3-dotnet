using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<PageResultModel<IEnumerable<ClientEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3);
        Task<ClientEntity?> GetById(int id);
        Task<ClientEntity?> Add(ClientEntity entity);
        Task<ClientEntity?> Update(int Id, ClientEntity entity);
        Task<ClientEntity?> Delete(int Id);
    }
}
