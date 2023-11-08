using EndavaTechCourseBankApp.Application.Commands.RegisterUser;
using EndavaTechCourseBankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourseBankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        public AccountController(IMediator mediator) 
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
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
    }
}
