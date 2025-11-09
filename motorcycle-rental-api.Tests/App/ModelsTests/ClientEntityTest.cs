using motorcycle_rental_api.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace motorcycle_rental_api.Tests.App.ModelsTests
{
    public class ClientEntityTest
    {
        // Método auxiliar para validar o objeto e retornar erros
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        // Teste de cliente válido
        [Fact]
        public void Client_IsValid()
        {
            var client = new ClientEntity
            {
                Name = "João Silva",
                CPF = "12345678901",
                Street = "Rua das Flores",
                HouseNumber = 123,
                Address2 = "Apto 45",
                District = "Centro",
                City = "São Paulo",
                State = "SP",
                CEP = "01234567",
                Fone = "11987654321",
                Email = "joao.silva@example.com"
            };

            var validationResults = ValidateModel(client);

            Assert.Empty(validationResults);
        }

        // Testes para propriedades obrigatórias (Required) - null
        [Theory]
        [InlineData("Name")]
        [InlineData("CPF")]
        [InlineData("Street")]
        [InlineData("District")]
        [InlineData("City")]
        [InlineData("State")]
        [InlineData("CEP")]
        [InlineData("Fone")]
        [InlineData("Email")]
        public void Client_RequiredProperties_Null(string propertyName)
        {
            var client = new ClientEntity
            {
                Name = "João Silva",
                CPF = "12345678901",
                Street = "Rua das Flores",
                HouseNumber = 123,
                District = "Centro",
                City = "São Paulo",
                State = "SP",
                CEP = "01234567",
                Fone = "11987654321",
                Email = "joao.silva@example.com"
            };

            // Define o valor null para a propriedade específica
            typeof(ClientEntity).GetProperty(propertyName)?.SetValue(client, null);

            var validationResults = ValidateModel(client);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para propriedades obrigatórias (Required) - empty
        [Theory]
        [InlineData("Name")]
        [InlineData("CPF")]
        [InlineData("Street")]
        [InlineData("District")]
        [InlineData("City")]
        [InlineData("State")]
        [InlineData("CEP")]
        [InlineData("Fone")]
        [InlineData("Email")]
        public void Client_RequiredProperties_Empty(string propertyName)
        {
            var client = new ClientEntity
            {
                Name = "João Silva",
                CPF = "12345678901",
                Street = "Rua das Flores",
                HouseNumber = 123,
                District = "Centro",
                City = "São Paulo",
                State = "SP",
                CEP = "01234567",
                Fone = "11987654321",
                Email = "joao.silva@example.com"
            };

            // Define o valor empty para a propriedade específica
            typeof(ClientEntity).GetProperty(propertyName)?.SetValue(client, "");

            var validationResults = ValidateModel(client);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para comprimento de string (StringLength) - exceder limite
        [Theory]
        [InlineData("Name", 201)]
        [InlineData("CPF", 12)] // CPF tem erro customizado, mas testa o limite
        [InlineData("Street", 201)]
        [InlineData("Address2", 201)]
        [InlineData("District", 201)]
        [InlineData("City", 201)]
        [InlineData("State", 3)] // State tem erro customizado
        [InlineData("CEP", 9)] // CEP tem erro customizado
        [InlineData("Fone", 12)] // Fone tem erro customizado
        [InlineData("Email", 201)]
        public void Client_StringLength_Exceeds(string propertyName, int length)
        {
            var client = new ClientEntity
            {
                Name = "João Silva",
                CPF = "12345678901",
                Street = "Rua das Flores",
                HouseNumber = 123,
                District = "Centro",
                City = "São Paulo",
                State = "SP",
                CEP = "01234567",
                Fone = "11987654321",
                Email = "joao.silva@example.com"
            };

            // Define uma string com o comprimento especificado
            typeof(ClientEntity).GetProperty(propertyName)?.SetValue(client, new string('A', length));

            var validationResults = ValidateModel(client);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("maximum length") ||
                                                     vr.ErrorMessage.Contains("deve conter"));
        }

        // Teste para HouseNumber (int, Required) - assumindo que 0 é válido
        [Fact]
        public void Client_HouseNumber_IsValid()
        {
            var client = new ClientEntity
            {
                Name = "João Silva",
                CPF = "12345678901",
                Street = "Rua das Flores",
                HouseNumber = 0,
                District = "Centro",
                City = "São Paulo",
                State = "SP",
                CEP = "01234567",
                Fone = "11987654321",
                Email = "joao.silva@example.com"
            };

            var validationResults = ValidateModel(client);

            Assert.Empty(validationResults);
        }
    }
}
