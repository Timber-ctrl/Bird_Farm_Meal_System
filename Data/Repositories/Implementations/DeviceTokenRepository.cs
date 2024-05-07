using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class DeviceTokenRepository : Repository<DeviceToken>, IDeviceTokenRepository
    {
        public DeviceTokenRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
