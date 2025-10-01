using motorcycle_rental_api.Models;
using motorcycle_rental_api.Dtos;

namespace motorcycle_rental_api.Mappers
{
    public static class MotorcycleMapper
    {
        public static MotorcycleEntity ToMotorcycleEntity(this MotorcycleDto obj)
        {
            return new MotorcycleEntity
            {
                Brand = obj.Brand,
                Model = obj.Model,
                Plate = obj.Plate,
                ManufacturingYear = obj.ManufacturingYear,
                DailyValue = obj.DailyValue,
                Availability = obj.Availability
            };
        }

        public static MotorcycleDto ToMotorcycleDto(this MotorcycleEntity entity)
        {
            return new MotorcycleDto(
                entity.Brand,
                entity.Model,
                entity.Plate,
                entity.ManufacturingYear,
                entity.DailyValue,
                entity.Availability
            );
        }
    }
}
