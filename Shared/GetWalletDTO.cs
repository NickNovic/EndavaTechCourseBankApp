namespace EndavaTechCourseBankApp.Shared;

public class GetWalletDTO
{
    public Guid Id { get; set; }

    public DateTimeOffset CreateDate { get; set; }

    public string Type { get; set; }

    public decimal Amount { get; set; }
    public Guid CurrencyId { get; set; }        
}