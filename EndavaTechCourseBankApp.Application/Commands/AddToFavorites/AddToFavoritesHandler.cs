using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.AddToFavorites
{
    public class AddToFavoritesHandler : IRequestHandler<AddToFavoritesCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public AddToFavoritesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandStatus> Handle(AddToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user is null)
                return CommandStatus.Failed("No user with this Id");

            var walletCode = new FavoriteWalletCode()
            {
                WalletCode = request.WalletCode
            };

            if (user.Favorites is null)
                user.Favorites = new List<FavoriteWalletCode>();

            user.Favorites.Add(walletCode);
            _context.favorites.Add(walletCode);
            _context.Update(user);

            _context.SaveChanges();
            
            return new();
        }
    }
}
