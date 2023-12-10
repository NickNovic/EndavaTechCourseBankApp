using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetFavorites
{
    public class GetFavoritesHandler : IRequestHandler<GetFavoritesQuery, List<string>>
    {
        private readonly ApplicationDbContext _context;

        public GetFavoritesHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            _context = context;
        }

        public async Task<List<string>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == request.UserId);
            var userFavorites = user.Favorites;

            var result = new List<FavoriteWalletCode>();

            if(userFavorites is not null) 
            {
                result = userFavorites.ToList();
            }

            List<string> favorites = new();
            foreach(var c in result) 
            {
                favorites.Add(c.WalletCode);
            }

            return favorites;
        }
    }
}
