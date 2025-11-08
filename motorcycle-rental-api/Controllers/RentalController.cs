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
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista de Aluguéis",
            Description = "Retorna uma lista completa de aluguéis com paginação.")]
        [SwaggerResponse(200, "Lista retornada com sucesso.", typeof(IEnumerable<RentalEntity>))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Displacement = 0, int TotalRecords = 3)
        {
            var result = await _rentalRepository.GetAll(Displacement, TotalRecords);

            if (!result.Data.Any())
                return NoContent();

            var Id = result.Data.FirstOrDefault()?.Id ?? 0;

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Rental", null, Request.Scheme),
                    getById = Url.Action(nameof(Get), "Rental", new { id = Id }, Request.Scheme),
                    post = Url.Action(nameof(Post), "Rental", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Rental", new { id = Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Rental", new { id = Id }, Request.Scheme),
                },
                page = new
                {
                    result.Displacement,
                    result.ReturnedRecords,
                    result.TotalRecords
                }
            };

            return Ok(hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Busca de Aluguel por ID",
            Description = "Retorna um aluguel específico, com base no ID informado.")]
        [SwaggerResponse(200, "Aluguel encontrado.", typeof(RentalEntity))]
        [SwaggerResponse(404, "Aluguel não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _rentalRepository.GetById(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Rental", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Rental", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Rental", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Rental", new { id = result.Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Rental", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastro de Aluguel",
            Description = "Cadastra um novo aluguel no sistema.")]
        [SwaggerResponse(200, "Aluguel cadastrado com sucesso.", typeof(RentalEntity))]
        [SwaggerResponse(400, "Erro ao cadastrar o aluguel.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Post(RentalDto entity)
        {
            try
            {
                var result = await _rentalRepository.Add(entity.ToRentalEntity());

                var hateoas = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Rental", new { id = result.Id }, Request.Scheme),
                        getAll = Url.Action(nameof(Get), "Rental", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Rental", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Rental", new { id = result.Id }, Request.Scheme)
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualização de Aluguel",
            Description = "Atualiza os dados de um aluguel.")]
        [SwaggerResponse(200, "Aluguel atualizado com sucesso.", typeof(RentalEntity))]
        [SwaggerResponse(404, "Aluguel não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, RentalDto entity)
        {
            var result = await _rentalRepository.Update(id, entity.ToRentalEntity());

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Rental", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Rental", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Rental", null, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Rental", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleção de Aluguel",
            Description = "Remove um aluguel com base no ID.")]
        [SwaggerResponse(200, "Aluguel deletado com sucesso.")]
        [SwaggerResponse(404, "Aluguel não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rentalRepository.Delete(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                message = "Aluguel deletado com sucesso.",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Rental", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Rental", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}
