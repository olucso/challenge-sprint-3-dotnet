using motorcycle_rental_api.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace motorcycle_rental_api.Tests.App
{
    public class RentalEntityTest
    {
        // Método auxiliar para validar o objeto e retornar erros
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        // Teste de aluguel válido
        [Fact]
        public void Rental_IsValid()
        {
            var rental = new RentalEntity
            {
                ClientId = 1,
                MotorcycleId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                TotalValue = 350.00m,
                Completed = false
            };

            var validationResults = ValidateModel(rental);

            Assert.Empty(validationResults);
        }

        // Teste específico para StartDate (DateTime, Required) - valor padrão
        [Fact]
        public void Rental_StartDate_DefaultValue()
        {
            var rental = new RentalEntity
            {
                ClientId = 1,
                MotorcycleId = 1,
                StartDate = DateTime.MinValue, // Valor padrão
                EndDate = DateTime.Now.AddDays(7),
                TotalValue = 350.00m,
                Completed = false
            };

            var validationResults = ValidateModel(rental);

            // DateTime.MinValue pode ser considerado inválido; ajustar conforme necessidade
            Assert.Empty(validationResults); // Ou Assert.Contains se quiser invalidar
        }

        // Teste para EndDate (DateTime?, opcional) - null é válido
        [Fact]
        public void Rental_EndDate_Null_IsValid()
        {
            var rental = new RentalEntity
            {
                ClientId = 1,
                MotorcycleId = 1,
                StartDate = DateTime.Now,
                EndDate = null,
                TotalValue = 350.00m,
                Completed = false
            };

            var validationResults = ValidateModel(rental);

            Assert.Empty(validationResults);
        }

        // Teste para Completed (bool, Required) - ambos true e false são válidos
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Rental_Completed_IsValid(bool completed)
        {
            var rental = new RentalEntity
            {
                ClientId = 1,
                MotorcycleId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                TotalValue = 350.00m,
                Completed = completed
            };

            var validationResults = ValidateModel(rental);

            Assert.Empty(validationResults);
        }
    }
}