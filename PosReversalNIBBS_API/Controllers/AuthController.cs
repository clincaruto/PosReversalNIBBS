using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PosReversalNIBBS_API.Models.DTO;
using PosReversalNIBBS_API.Repositories.IRepository;

namespace PosReversalNIBBS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandlerRepo tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandlerRepo tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            // Validate the incoming request

            // Check if user is authenticated
            // Check username and Password
            var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                // Generate a JWT Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is incorrect.");

        }
    }
}
