namespace EndavaTechCourseBankApp.Server.Common.JwtToken
{
    public class TokenSettings
    {
        public string SecretKey { get; set; } = null!;
        public int ExpirationInMinutes { get; set; }
    }
}
