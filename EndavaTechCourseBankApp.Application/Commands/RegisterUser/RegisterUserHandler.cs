using EndavaTechCourseBankApp.Domain.Enums;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;


        public RegisterUserHandler(ApplicationDbContext context, UserManager<User> userManager) 
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(userManager);

            this.context = context;
            this.userManager = userManager;
        }

        public async Task<CommandStatus> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await context.Users.AnyAsync(user => user.Email == request.Email, cancellationToken);

            if (userExists)
                return CommandStatus.Failed("Utilizatorul deja exista");

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            var createResult = await userManager.CreateAsync(user, request.Password);

            IdentityResult roleResult;

            if (await context.Users.AnyAsync(cancellationToken))
                roleResult = await userManager.AddToRoleAsync(user, UserRole.User.ToString());
            else
                roleResult = await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            if (!roleResult.Succeeded || !createResult.Succeeded)
                return CommandStatus.Failed("Utilizatorul nu a putut fi inregistrat");

            return new CommandStatus();
        }
    }
}
