using AutoFixture.Idioms;
using EndavaTechCourse.BankApp.Test.Common;
using EndavaTechCourseBankApp.Application.Commands.AddCurrency;
using EndavaTechCourseBankApp.Application.Commands.CreateWallet;
using EndavaTechCourseBankApp.Application.Commands.DeleteCurrencyById;
using EndavaTechCourseBankApp.Application.Commands.DeleteWalletById;
using EndavaTechCourseBankApp.Application.Commands.UpdateCurrency;
using EndavaTechCourseBankApp.Application.Commands.UpdateWallet;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencies;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourseBankApp.Application.Queries.GetWallets;
using EndavaTechCourseBankApp.Application.Queries.GetWalletsById;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using EndavaTechCourseBankApp.Server.Controllers;
using EndavaTechCourseBankApp.Shared;
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
        public async Task ShouldDeleteCurrency([Frozen] ApplicationDbContext context,
            [Greedy] CurrencyController currencyController,
            Currency currency,
            Currency currency2,
            Currency currency3) 
        {

            context.currencies.Add(currency);
            context.currencies.Add(currency2);
            context.currencies.Add(currency3);
            context.SaveChanges();

            await currencyController.DeleteCurrencyById(currency.Id);

            var res = await context.currencies.FirstOrDefaultAsync(c => c.Id == currency.Id);
        
            res.Should().BeNull();
        }

        [Test, ApplicationData]
        public async Task ShouldGetCurrencies([Frozen] ApplicationDbContext context,
            [Greedy] CurrencyController currencyController,
            Currency currency1,
            Currency currency2) 
        {
            context.currencies.AddRange(currency1, currency2);

            var currencies = await currencyController.GetCurrencyes();

            currencies.Should().NotBeNull();
        }

        [Test, ApplicationData]
        public async Task ShouldGetCurrencyById([Frozen] ApplicationDbContext context,
            [Greedy] CurrencyController currencyController,
            Currency currency1,
            Currency currency2)
        {
            context.AddRange(currency1, currency2);
            var currencies = await currencyController.GetCurrencyById(currency1.Id);

            currencies.Should().NotBeNull();
        }

        [Test, ApplicationData]
        public async Task ShouldUpdateCurrency(
            [Frozen] ApplicationDbContext context,
            [Greedy] CurrencyController currencyController,
            Currency currency1,
            Currency currency2) 
        {
            context.AddRange(currency1, currency2);

            var dto = new UpdateCurrencyDTO()
            {
                ChangeRate = currency1.ChangeRate,
                CurrencyCode = currency2.CurrencyCode,
                Name = currency2.Name,
                CurrencyId = currency1.Id
            };

            await currencyController.UpdateCurrency(dto);

            context.SaveChanges();
            var c = context.currencies.Find(dto.CurrencyId);

            Assert.IsNotNull(c);
        }

        [Test, ApplicationData]
        public async Task CanCreateAddCurrencyCommand(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(AddCurrencyCommand).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateDeleteCurrencyByIdCommand(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(DeleteCurrencyByIdCommand).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateUpdateCurrencyCommand(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(UpdateCurrencyCommand).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateGetCurrenciesQuery(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(GetCurrenciesQuery).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateGetCurrencyByIdQuery(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(GetCurrencyByIdQuery).GetConstructors());
    }
}
