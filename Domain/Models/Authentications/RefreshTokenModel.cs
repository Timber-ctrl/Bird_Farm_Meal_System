namespace Domain.Models.Authentications
{
    public class RefreshTokenModel
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
