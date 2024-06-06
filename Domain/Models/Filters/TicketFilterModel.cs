using Microsoft.AspNetCore.Http;

namespace Domain.Models.Filters
{
    public class TicketFilterModel
    {
        public string? TicketCategory { get; set; }
        public string? Status { get; set; }
        public Guid? FarmId { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
