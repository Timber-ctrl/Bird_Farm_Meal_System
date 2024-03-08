using Microsoft.AspNetCore.Http;

namespace Domain.Models.Filters
{
    public class TicketFilterModel
    {
        public string? TicketCategory { get; set; } = null!;
        public Guid? CageId { get; set; }
    }
}
