using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly ApplicationDbContext _context;

        public GetUserByEmailHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user is null)
                return new User();

            return user;
        }
    }
}