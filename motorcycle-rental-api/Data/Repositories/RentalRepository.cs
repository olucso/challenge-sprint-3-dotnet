using Microsoft.EntityFrameworkCore;
using motorcycle_rental_api.Data.AppData;
using motorcycle_rental_api.Data.Repositories.Interfaces;
using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationContext _context;

        public RentalRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RentalEntity?> Add(RentalEntity entity)
        {
            _context.Rental.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RentalEntity?> Delete(int Id)
        {
            var result = await _context.Rental.FindAsync(Id);

            if (result is not null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PageResultModel<IEnumerable<RentalEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3)
        {
            var totalRecords = await _context.Rental.CountAsync();

            var result = await _context
                .Rental
                .Include(r => r.Client)
                .Include(r => r.Motorcycle)
                .OrderBy(x => x.Id)
                .Skip(Displacement)
                .Take(TotalRecords)
                .ToListAsync();

            return new PageResultModel<IEnumerable<RentalEntity>>
            {
                Data = result,
                Displacement = Displacement,
                ReturnedRecords = result.Count,
                TotalRecords = totalRecords
            };
        }

        public async Task<RentalEntity?> GetById(int id)
        {
            return await _context.Rental
                .Include(r => r.Client)
                .Include(r => r.Motorcycle)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RentalEntity?> Update(int Id, RentalEntity entity)
        {
            var result = await _context.Rental.FindAsync(Id);

            if (result is not null)
            {
                result.ClientId = entity.ClientId;
                result.MotorcycleId = entity.MotorcycleId;
                result.StartDate = entity.StartDate;
                result.EndDate = entity.EndDate;
                result.TotalValue = entity.TotalValue;
                result.Completed = entity.Completed;

                _context.Update(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
