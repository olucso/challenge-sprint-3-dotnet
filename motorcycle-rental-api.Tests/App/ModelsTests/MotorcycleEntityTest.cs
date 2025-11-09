using motorcycle_rental_api.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace motorcycle_rental_api.Tests.App
{
    public class MotorcycleEntityTest
    {
        // Método auxiliar para validar o objeto e retornar erros
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        // Teste de motocicleta válida
        [Fact]
        public void Motorcycle_IsValid()
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = true
            };

            var validationResults = ValidateModel(motorcycle);

            Assert.Empty(validationResults);
        }

        // Testes para propriedades obrigatórias (Required) - null
        [Theory]
        [InlineData("Brand")]
        [InlineData("Model")]
        [InlineData("Plate")]
        public void Motorcycle_RequiredProperties_Null(string propertyName)
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = true
            };

            // Define o valor null para a propriedade específica
            typeof(MotorcycleEntity).GetProperty(propertyName)?.SetValue(motorcycle, null);

            var validationResults = ValidateModel(motorcycle);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para propriedades obrigatórias (Required) - empty
        [Theory]
        [InlineData("Brand")]
        [InlineData("Model")]
        [InlineData("Plate")]
        public void Motorcycle_RequiredProperties_Empty(string propertyName)
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = true
            };

            // Define o valor empty para a propriedade específica
            typeof(MotorcycleEntity).GetProperty(propertyName)?.SetValue(motorcycle, "");

            var validationResults = ValidateModel(motorcycle);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("required"));
        }

        // Testes para comprimento de string (StringLength) - exceder limite
        [Theory]
        [InlineData("Brand", 101)]
        [InlineData("Model", 101)]
        [InlineData("Plate", 8)] // Plate tem erro customizado
        public void Motorcycle_StringLength_Exceeds(string propertyName, int length)
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = true
            };

            // Define uma string com o comprimento especificado
            typeof(MotorcycleEntity).GetProperty(propertyName)?.SetValue(motorcycle, new string('A', length));

            var validationResults = ValidateModel(motorcycle);

            Assert.Contains(validationResults, vr => vr.ErrorMessage.Contains("maximum length") ||
                                                     vr.ErrorMessage.Contains("deve conter"));
        }

        // Teste para ManufacturingYear (int, Required) - assumindo que um valor positivo é válido
        [Fact]
        public void Motorcycle_ManufacturingYear_IsValid()
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = true
            };

            var validationResults = ValidateModel(motorcycle);

            Assert.Empty(validationResults);
        }

        // Teste para DailyValue (decimal, Required) - assumindo que um valor positivo é válido
        [Fact]
        public void Motorcycle_DailyValue_IsValid()
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = true
            };

            var validationResults = ValidateModel(motorcycle);

            Assert.Empty(validationResults);
        }

        // Teste para Availability (bool, Required) - ambos true e false são válidos
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Motorcycle_Availability_IsValid(bool availability)
        {
            var motorcycle = new MotorcycleEntity
            {
                Brand = "Honda",
                Model = "CB 300R",
                Plate = "ABC1234",
                ManufacturingYear = 2020,
                DailyValue = 50.00m,
                Availability = availability
            };

            var validationResults = ValidateModel(motorcycle);

            Assert.Empty(validationResults);
        }
    }
}
