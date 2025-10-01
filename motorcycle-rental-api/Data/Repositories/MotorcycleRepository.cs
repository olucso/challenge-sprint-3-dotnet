using Microsoft.EntityFrameworkCore;
using motorcycle_rental_api.Data.AppData;
using motorcycle_rental_api.Data.Repositories.Interfaces;
using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Data.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ApplicationContext _context;

        public MotorcycleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<MotorcycleEntity?> Add(MotorcycleEntity entity)
        {
            _context.Motorcycle.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MotorcycleEntity?> Delete(int Id)
        {
            var result = await _context.Motorcycle.FindAsync(Id);

            if (result is not null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<PageResultModel<IEnumerable<MotorcycleEntity>>> GetAll(int Displacement = 0, int TotalRecords = 3)
        {
            var totalRecords = await _context.Motorcycle.CountAsync();

            var result = await _context
                .Motorcycle
                .OrderBy(x => x.Id)
                .Skip(Displacement)
                .Take(TotalRecords)
                .ToListAsync();

            return new PageResultModel<IEnumerable<MotorcycleEntity>>
            {
                Data = result,
                Displacement = Displacement,
                ReturnedRecords = result.Count,
                TotalRecords = totalRecords
            };
        }

        public async Task<MotorcycleEntity?> GetById(int id)
        {
            return await _context.Motorcycle.FindAsync(id);
        }

        public async Task<MotorcycleEntity?> Update(int Id, MotorcycleEntity entity)
        {
            var result = await _context.Motorcycle.FindAsync(Id);

            if (result is not null)
            {
                result.Brand = entity.Brand;
                result.Model = entity.Model;
                result.Plate = entity.Plate;
                result.ManufacturingYear = entity.ManufacturingYear;
                result.DailyValue = entity.DailyValue;
                result.Availability = entity.Availability;

                _context.Update(result);
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
