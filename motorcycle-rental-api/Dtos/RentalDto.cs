namespace motorcycle_rental_api.Dtos
{
    public record RentalDto(
        int ClientId,
        int MotorcycleId,
        DateTime StartDate,
        DateTime? EndDate,
        decimal TotalValue,
        bool Completed
    );
}
