using EndavaTechCourseBankApp.Application.Commands.DeleteCurrencyById;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using EndavaTechCourseBankApp.Server.Controllers;
using EndavaTechCourseBankApp.Test.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Test
{
    public class CurrencyControllerTests
    {
        [Test, ApplicationData]
        public async void ShouldDeleteCurrency([Frozen] ApplicationDbContext context,
            [Greedy] CurrencyController currencyController,
            Currency currency) 
        {
            await currencyController.DeleteCurrencyById(currency.Id);

            var res = await context.currencies.FirstOrDefaultAsync(c => c.Id == currency.Id);
        
            res.Should().BeNull();
        }
    }
}
