using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PosReversalNIBBS_API.Models.DTO;
using PosReversalNIBBS_API.Repositories.IRepository;
using PosReversalNIBBS_API.Utilities;

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
            //var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            loginRequest.Username = loginRequest.Username.Replace("@ubagroup.com", "");
            var checkAdRequest = await AuthADService.ValidateUserOnADAsync(loginRequest);
            if (checkAdRequest)
            {

                //var user = await AuthADService.GetUserDetails(loginRequest.Username); ;


               
                    // Generate a JWT Token
                    var token = await tokenHandler.CreateTokenAsync(loginRequest);
                    return Ok(new { responseData=token,
                    message="Successfully"});
                
            }
            else
            return BadRequest("Username or Password is incorrect.");

        }
    }
}
