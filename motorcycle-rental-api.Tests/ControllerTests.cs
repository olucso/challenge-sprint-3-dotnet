using Microsoft.AspNetCore.Mvc.Testing;
using motorcycle_rental_api; // Garanta que esta seja a namespace/assembly do seu Program.cs
using System.Net; // Para usar HttpStatusCode
using System.Threading.Tasks;
using Xunit; // Assumindo que você está usando xUnit

namespace motorcycle_rental_api.Tests
{
    // A fixture garante que a WebApplicationFactory seja criada uma única vez para todos os testes na classe.
    public class ControllerTests : IClassFixture<WebApplicationFactory<ApiMarker>>
    {
        private readonly WebApplicationFactory<ApiMarker> _factory;

        // O construtor recebe a factory injetada pelo xUnit (devido ao IClassFixture).
        public ControllerTests(WebApplicationFactory<ApiMarker> factory)
        {
            _factory = factory;
        }

        [Fact] // Exemplo de um teste simples
        public async Task Get_Motorcycles_ReturnsSuccessStatusCode()
        {
            // Arrange
            // 1. Cria um HttpClient que faz chamadas ao TestServer em memória.
            var client = _factory.CreateClient();

            // ATENÇÃO: Ajuste o caminho da URL conforme o endpoint real do seu MotorcycleController
            var requestUrl = "/api/v1/Motorcycle"; // Exemplo: /api/v1/Motorcycle, /api/motorcycles, etc.

            // Act
            // 2. Faz a chamada HTTP GET para o endpoint.
            var response = await client.GetAsync(requestUrl);

            // Assert
            // 3. Verifica o status code da resposta.
            response.EnsureSuccessStatusCode(); // Verifica se o status code é 2xx
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Opcional: Você também pode ler e validar o conteúdo retornado (o payload JSON).
            // var content = await response.Content.ReadAsStringAsync();
            // Assert.Contains("algum valor esperado no JSON", content);
        }

        // Você adicionaria outros métodos [Fact] para testar outros cenários e Controllers.
        // Por exemplo:
        // [Fact]
        // public async Task Post_NewMotorcycle_ReturnsCreatedStatus() { ... }
    }
}