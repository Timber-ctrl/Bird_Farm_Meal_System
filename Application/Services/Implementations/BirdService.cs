using Application.Services.Interfaces;
using AutoMapper;
using Data;

namespace Application.Services.Implementations
{
    public class BirdService : BaseService, IBirdService
    {
        public BirdService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
