using BlazorStack.Server.Data;
using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterModel request)
        {
            var response = await _authRepository.Register(new User
            {
                Username = request.Username,
                Email = request.Email,
                Points = request.Points,
                DateOfBirth = request.DateOfBirth,
                AcceptedTermsAgreements = request.AcceptedTermsAgreements,
            }, request.Password, request.StartUnitId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel request)
        {
            var response = await _authRepository.Login(request.Email, request.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
