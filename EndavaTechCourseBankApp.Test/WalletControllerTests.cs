using AutoFixture.Idioms;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using EndavaTechCourseBankApp.Server.Controllers;
using EndavaTechCourseBankApp.Test.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourseBankApp.Test
{
    public class WalletControllerTests
    {
        [Test, ApplicationData]
        public async Task ShouldGetWallets(
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
            result.Count.Should().Be( 2 );
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
    }
}
