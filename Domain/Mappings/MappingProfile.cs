using AutoMapper;
using Domain.Entities;
using Domain.Models.Authentications;

namespace Domain.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Data type
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Guid?, Guid>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            // Staff
            CreateMap<Staff, AuthModel>();

            // Manager
            CreateMap<Manager, AuthModel>();
        }
    }
}
