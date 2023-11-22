using EndavaTechCourseBankApp.Domain.Common;
using EndavaTechCourseBankApp.Domain.Models;

namespace EndavaTechCourseBankApp.Shared;

public class GetWalletDTO : BaseEntity
{
    public Guid WalletId {  get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; }
    public decimal ChangeRate { get; set; }
    public string CurrencyName { get; set; }
    public int Pincode { get; set; }
    public string Type { get; set; }
    public DateTime LastActivity { get; set; }
    public Guid CurrencyId { get; set; }
    public string Code { get; set; }
}