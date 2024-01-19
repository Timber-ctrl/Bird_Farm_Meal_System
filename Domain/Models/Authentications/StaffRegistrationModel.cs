namespace Domain.Models.Authentications
{
    public class StaffRegistrationModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public Guid FarmId { get; set; }
    }
}
