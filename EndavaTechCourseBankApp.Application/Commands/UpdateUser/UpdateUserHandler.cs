using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;

        public UpdateUserHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandStatus> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            
            if(user is null)
                return CommandStatus.Failed("no such user");
            

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.Username;
            user.Email = request.Email;
            user.MainWalletId = request.MainWalletId;

            context.Users.Update(user);
            context.SaveChanges();

            return new CommandStatus();
        }
    }
}
