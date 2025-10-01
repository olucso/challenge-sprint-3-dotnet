using motorcycle_rental_api.Models;
using motorcycle_rental_api.Dtos;

namespace motorcycle_rental_api.Mappers
{
    public static class RentalMapper
    {
        public static RentalEntity ToRentalEntity(this RentalDto obj)
        {
            return new RentalEntity
            {
                ClientId = obj.ClientId,
                MotorcycleId = obj.MotorcycleId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                TotalValue = obj.TotalValue,
                Completed = obj.Completed
            };
        }

        public static RentalDto ToRentalDto(this RentalEntity entity)
        {
            return new RentalDto(
                entity.ClientId,
                entity.MotorcycleId,
                entity.StartDate,
                entity.EndDate,
                entity.TotalValue,
                entity.Completed
            );
        }
    }
}
