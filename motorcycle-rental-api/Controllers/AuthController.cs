using Microsoft.AspNetCore.Mvc;
using motorcycle_rental_api.Dtos;
using motorcycle_rental_api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace motorcycle_rental_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Autenticação do usuário",
            Description = "Realiza login e retorna um token JWT válido.")]
        [SwaggerResponse(statusCode: 200, description: "Login bem-sucedido.")]
        [SwaggerResponse(statusCode: 401, description: "Credenciais inválidas.")]
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            if (login.Username == "admin" && login.Password == "123456")
            {
                var token = _jwtService.GenerateToken(login.Username);

                return Ok(new
                {
                    user = login.Username,
                    token,
                    expiresIn = 60 * 60,
                    type = "Bearer"
                });
            }

            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }
    }
}
