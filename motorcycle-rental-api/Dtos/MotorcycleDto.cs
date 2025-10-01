namespace motorcycle_rental_api.Dtos
{
    public record MotorcycleDto(
        string Brand,
        string Model,
        string Plate,
        int ManufacturingYear,
        decimal DailyValue,
        bool Availability
    );
}
