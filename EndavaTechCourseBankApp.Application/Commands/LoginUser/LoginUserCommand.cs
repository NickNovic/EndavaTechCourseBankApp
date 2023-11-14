using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<CommandStatus>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
