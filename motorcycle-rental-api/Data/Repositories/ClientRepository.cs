using Microsoft.EntityFrameworkCore;
using motorcycle_rental_api.Data.AppData;
using motorcycle_rental_api.Data.Repositories.Interfaces;
using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ClientEntity?> Add(ClientEntity entity)
        {
            _context.Client.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<ClientEntity?> Delete(int Id)
        {
            var result = await _context.Client.FindAsync(Id);

            if (result is not null)
            {
                _context.Remove(result);
                _context.SaveChanges();

                return result;
            }

            return null;
        }

        public async Task<PageResultModel<IEnumerable<ClientEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3)
        {
            var totalRecords = await _context.Client.CountAsync();
            
            var result = await _context
                .Client
                .OrderBy(x => x.Id)
                .Skip(Displacement)
                .Take(TotalRecords)
                .ToListAsync();
                        
            return new PageResultModel<IEnumerable<ClientEntity>>
            {
                Data = result,
                Displacement = Displacement,
                ReturnedRecords = TotalRecords,
                TotalRecords = totalRecords
            };
        }

        public async Task<ClientEntity?> GetById(int id)
        {
            var result = await _context.Client.FindAsync(id);

            return result;
        }

        public async Task<ClientEntity?> Update(int Id, ClientEntity entity)
        {
            var result = await _context.Client.FindAsync(Id);

            if (result is not null)
            {
                result.Name = entity.Name;
                result.CPF = entity.CPF;
                result.Street = entity.Street;
                result.HouseNumber = entity.HouseNumber;
                result.Address2 = entity.Address2;
                result.District = entity.District;
                result.City = entity.City;
                result.State = entity.State;
                result.CEP = entity.CEP;
                result.Fone = entity.Fone;
                result.Email = entity.Email;


                _context.Update(result);
                _context.SaveChanges();

                return result;
            }

            return null;
        }
    }
}
