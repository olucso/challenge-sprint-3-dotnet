using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using motorcycle_rental_api.Data.AppData;
using motorcycle_rental_api.Data.Repositories.Interfaces;
using motorcycle_rental_api.Dtos;
using motorcycle_rental_api.Mappers;
using motorcycle_rental_api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace motorcycle_rental_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista de Clientes",
            Description = "Retorna uma lista completa, com todos os clientes cadastrados.")]
        [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso.", type: typeof(IEnumerable<ClientEntity>))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Displacement = 0, int TotalRecords = 3)
        {
            var result = await _clientRepository.GetAll(Displacement, TotalRecords);

            if(!result.Data.Any())
                return NoContent();

            var Id = result.Data.FirstOrDefault()?.Id ?? 0;

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Client", null, Request.Scheme),
                    getById = Url.Action(nameof(Get), "Client", new { id = Id }, Request.Scheme),
                    post = Url.Action(nameof(Post), "Client", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Client", new { id = Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Client", new { id = Id }, Request.Scheme),
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
            Summary = "Busca de cliente por seu ID",
            Description = "Retorna um cliente específico, com base no ID informado.")]
        [SwaggerResponse(statusCode: 200, description: "Cliente encontrado.", type: typeof(IEnumerable<ClientEntity>))]
        [SwaggerResponse(statusCode: 404, description: "Cliente não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _clientRepository.GetById(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Client", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Client", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Client", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Client", new { id = result.Id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Client", new { id = result.Id }, Request.Scheme),
                }
            };

            return Ok(hateoas);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastro de Cliente",
            Description = "Cadastra um cliente no banco de dados.")]
        [SwaggerResponse(statusCode: 200, description: "Cliente cadastrado com sucesso.", type: typeof(ClientEntity))]
        [SwaggerResponse(statusCode: 400, description: "Erro ao cadastrar o cliente.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Post(ClientDto entity)
        {
            try
            {
                var result = _clientRepository.Add(entity.ToClientEntity());

                var hateoas = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Client", new { id = result.Id }, Request.Scheme),
                        getAll = Url.Action(nameof(Get), "Client", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Client", new { id = result.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Client", new { id = result.Id }, Request.Scheme)
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
            Summary = "Alteração de Cliente",
            Description = "Atualiza o cadastro de um cliente no banco de dados.")]
        [SwaggerResponse(statusCode: 200, description: "Cliente atualizado com sucesso.", type: typeof(ClientEntity))]
        [SwaggerResponse(statusCode: 404, description: "Cliente não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Put(int id, ClientDto entity)
        {
            var result = await _clientRepository.Update(id, entity.ToClientEntity());

            if (result is null)
                return NotFound();
                        
            var hateoas = new
            {
                data = result,
                links = new
                {
                    self = Url.Action(nameof(Get), "Client", new { id = result.Id }, Request.Scheme),
                    getAll = Url.Action(nameof(Get), "Client", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Client", null, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Client", new { id = result.Id }, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleção de Cliente",
            Description = "Deleta um cliente a partir de seu ID.")]
        [SwaggerResponse(statusCode: 200, description: "Cliente deletado com sucesso.")]
        [SwaggerResponse(statusCode: 404, description: "Cliente não encontrado.")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clientRepository.Delete(id);

            if (result is null)
                return NotFound();

            var hateoas = new
            {
                message = "Cliente deletado com sucesso.",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Client", null, Request.Scheme),
                    post = Url.Action(nameof(Post), "Client", null, Request.Scheme)
                }
            };

            return Ok(hateoas);
        }
    }
}
