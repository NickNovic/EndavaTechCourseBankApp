using EndavaTechCourseBankApp.Domain.Models;

namespace EndavaTechCourseBankApp.Shared
{
    public class CreateWalletDTO
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
