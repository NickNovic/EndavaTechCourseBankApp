using EndavaTechCourseBankApp.Application.Commands.LoginUser;
using EndavaTechCourseBankApp.Application.Commands.RegisterUser;
using EndavaTechCourseBankApp.Application.Queries.GetUserDetails;
using EndavaTechCourseBankApp.Server.Common.JwtToken;
using EndavaTechCourseBankApp.Shared;
using MediatR;
using EndavaTechCourseBankApp.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourseBankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly JwtService jwtService;

        public AccountController(IMediator mediator, JwtService jwtService) 
        {
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(jwtService);
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register) 
        {
            var registerUserCommand = new RegisterUserCommand()
            {
                Username = register.Username,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Password = register.Password,
                Email = register.Email
            };

            var result = await mediator.Send(registerUserCommand);

            return result.IsSuccessful ? Ok() : BadRequest(new {result.Error });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var loginCommand = new LoginUserCommand()
            {
                Username = dto.Username,
                Password = dto.Password
            };

            var result = await mediator.Send(loginCommand);

            if (!result.IsSuccessful)
                return BadRequest(result.Error);

            var userDetailsQuery = new GetUserDetailsQuery()
            {
                Username = dto.Username
            };
            var userDetails = await mediator.Send(userDetailsQuery);

            string jwtToken = jwtService.CreateAuthToken(userDetails.Id, userDetails.Username, userDetails.Roles);

            Response.Cookies.Append(Constants.TokenCookieName, jwtToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.MaxValue
            });

            return Ok(jwtToken);
        }
    }
}
