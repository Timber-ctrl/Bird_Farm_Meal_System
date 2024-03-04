namespace Domain.Models.Authentications
{
    public class AuthResponseModel
    {
        public string Access_token { get; set; } = null!;
        public UserDataModel User { get; set; } = null!;
    }

    public class UserDataModel
    {
        public Guid Uuid { get; set; }
        public string Role { get; set; } = null!;
        public InfoManager Data { get; set; } = null!;
    }

    public class InfoManager
    {
        public string DisplayName { get; set; } = null!;
        public string? PhotoURL { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
