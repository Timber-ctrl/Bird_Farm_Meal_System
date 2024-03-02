using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
