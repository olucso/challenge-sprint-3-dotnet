using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using motorcycle_rental_api.MachineLearning;
using motorcycle_rental_api.MachineLearning.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace motorcycle_rental_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PredictionController : ControllerBase
    {
        private readonly RentalPredictionService _predictionService;

        public PredictionController(RentalPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpPost("rental")]
        [EnableRateLimiting("rateLimitePolicy")]
        [SwaggerOperation(
            Summary = "Predição de valor de aluguel",
            Description = "Usa ML.NET para prever o valor total de um aluguel com base nos dias, valor diário e fidelidade do cliente.")]
        [SwaggerResponse(statusCode: 200, description: "Predição realizada com sucesso.")]
        public IActionResult PredictRental([FromBody] RentalInputModel input)
        {
            var predictedValue = _predictionService.Predict(input);

            var hateoas = new
            {
                input,
                predictedValue,
                links = new
                {
                    self = Url.Action(nameof(PredictRental), "Prediction", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}
