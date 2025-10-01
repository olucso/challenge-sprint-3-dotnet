using motorcycle_rental_api.Dtos;
using motorcycle_rental_api.Models;

namespace motorcycle_rental_api.Mappers
{
    public static class ClientMapper
    {
        public static ClientEntity ToClientEntity(this ClientDto obj)
        {
            return new ClientEntity()
            {
                Name = obj.Name,
                CPF = obj.CPF,
                Street = obj.Street,
                HouseNumber = obj.HouseNumber,
                Address2 = obj.Address2,
                District = obj.District,
                City = obj.City,
                State = obj.State,
                CEP = obj.CEP,
                Fone = obj.Fone,
                Email = obj.Email,
            };
        }
    }
}
