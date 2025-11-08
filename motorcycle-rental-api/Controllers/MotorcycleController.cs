using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using motorcycle_rental_api.Data.Repositories.Interfaces;
using motorcycle_rental_api.Dtos;
using motorcycle_rental_api.Mappers;
using motorcycle_rental_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace motorcycle_rental_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleController(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista de Motos", Description = "Retorna todas as motos cadastradas.")]
        [SwaggerResponse(200, "Lista retornada com sucesso.", typeof(IEnumerable<MotorcycleEntity>))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Displacement = 0, int TotalRecords = 3)
        {
            var result = await _motorcycleRepository.GetAll(Displacement, TotalRecords);

            if (!result.Data.Any())
                return NoContent();

            var Id = result.Data.FirstOrDefault()?.Id ?? 0;

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Motorcycle", null, Request.Scheme),
                    getById = Url.Action(nameof(Get), "Motorcycle", new { id = Id }, Request.Scheme),
                    post = Url.Action(nameof(Post), "Motorcycle", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Motorcycle", new { id = Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Motorcycle", new { id = Id }, Request.Scheme),
                }
            };

            return Ok(hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca Moto por ID", Description = "Retorna uma moto pelo ID.")]
        [SwaggerResponse(200, "Moto encontrada.", typeof(MotorcycleEntity))]
        [SwaggerResponse(404, "Moto não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _motorcycleRepository.GetById(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Motorcycle", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Motorcycle", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Motorcycle", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Motorcycle", new { id = result.Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Motorcycle", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastro de Moto", Description = "Cadastra uma nova moto.")]
        [SwaggerResponse(200, "Moto cadastrada com sucesso.", typeof(MotorcycleEntity))]
        [SwaggerResponse(400, "Erro ao cadastrar moto.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post(MotorcycleDto entity)
        {
            try
            {
                var result = await _motorcycleRepository.Add(entity.ToMotorcycleEntity());

                var hateoas = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Motorcycle", new { id = result.Id }, Request.Scheme),
                        getAll = Url.Action(nameof(Get), "Motorcycle", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Motorcycle", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Motorcycle", new { id = result.Id }, Request.Scheme)
                    }
                };

                return Ok(hateoas);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualização de Moto", Description = "Atualiza o cadastro da moto.")]
        [SwaggerResponse(200, "Moto atualizada com sucesso.", typeof(MotorcycleEntity))]
        [SwaggerResponse(404, "Moto não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, MotorcycleDto entity)
        {
            var result = await _motorcycleRepository.Update(id, entity.ToMotorcycleEntity());

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Motorcycle", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Motorcycle", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Motorcycle", null, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Motorcycle", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleção de Moto", Description = "Deleta uma moto pelo ID.")]
        [SwaggerResponse(200, "Moto deletada com sucesso.")]
        [SwaggerResponse(404, "Moto não encontrada.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _motorcycleRepository.Delete(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                message = "Moto deletada com sucesso.",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Motorcycle", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Motorcycle", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}
