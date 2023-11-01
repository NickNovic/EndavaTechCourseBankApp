using EndavaTechCourseBankApp.Domain.Common;
using EndavaTechCourseBankApp.Domain.Models;

namespace EndavaTechCourseBankApp.Shared;

public class GetWalletDTO : BaseEntity
{
    public Guid WalletId {  get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public int Pincode { get; set; }
    public string Type { get; set; }
    public DateTime LastActivity { get; set; }
    public Guid CurrencyId { get; set; }
}