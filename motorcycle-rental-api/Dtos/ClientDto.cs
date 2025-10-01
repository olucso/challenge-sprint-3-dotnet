namespace motorcycle_rental_api.Dtos
{
    public record ClientDto(string Name,
                            string CPF,
                            string Street,
                            int HouseNumber,
                            string Address2,
                            string District,
                            string City,
                            string State,
                            string CEP,
                            string Fone,
                            string Email);
}
