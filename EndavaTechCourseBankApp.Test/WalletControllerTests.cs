using AutoFixture.Idioms;
using EndavaTechCourse.BankApp.Test.Common;
using EndavaTechCourseBankApp.Application.Commands.CreateWallet;
using EndavaTechCourseBankApp.Application.Commands.DeleteWalletById;
using EndavaTechCourseBankApp.Application.Commands.UpdateWallet;
using EndavaTechCourseBankApp.Application.Queries.GetWallets;
using EndavaTechCourseBankApp.Application.Queries.GetWalletsById;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using EndavaTechCourseBankApp.Server.Controllers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourseBankApp.Test
{
    public class WalletControllerTests
    {
        [Test, ApplicationData]
        public async void ShouldGetWallets(
            [Frozen] ApplicationDbContext context,
            [Greedy] WalletController walletController,
            Wallet firtWallet,
            Wallet secondWallet
            )
        {
            //Arrange
            context.wallets.AddRange(firtWallet, secondWallet);
            context.SaveChanges();
            context.ChangeTracker.Clear();

            //Act
            var result = await walletController.GetWallets();

            //Assert
            //result.Count.Should().Be( 2 );
            Assert.AreEqual( 2, result.Count);
        }


        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(WalletController).GetConstructors());

        [Test, ApplicationData]
        public async void ShouldDeleteWallet([Frozen] ApplicationDbContext context,
            [Greedy] WalletController walletController,
            Wallet wallet)
        {
            await walletController.DeleteWalletById(wallet.Id);

            var res = await context.wallets.FirstOrDefaultAsync(c => c.Id == wallet.Id);

            res.Should().BeNull();
        }

        [Test, ApplicationData]
        public async Task CanCreateCreateWalletCommand(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(CreateWalletCommand).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateDeleteWalletByIdCommand(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(DeleteWalletByIdCommand).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateUpdateWalletCommand(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(UpdateWalletCommand).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateGetWalletsQuery(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(GetWalletsQuery).GetConstructors());

        [Test, ApplicationData]
        public async Task CanCreateGetWalletByIdQuery(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(GetWalletByIdQuery).GetConstructors());
    }
}
