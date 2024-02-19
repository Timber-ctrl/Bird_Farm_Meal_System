﻿using AutoMapper;
using Common.Helpers;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Creates;
using Domain.Models.Updates;
using Domain.Models.Views;

namespace Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Data type
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Guid?, Guid>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            // Staff
            CreateMap<Staff, AuthModel>();
            CreateMap<Staff, StaffViewModel>();
            CreateMap<StaffRegistrationModel, Staff>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StaffStatuses.Active))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));

            // Manager
            CreateMap<Manager, AuthModel>();
            CreateMap<Manager, ManagerViewModel>();
            CreateMap<ManagerRegistrationModel, Manager>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StaffStatuses.Active))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));

            // Cage
            CreateMap<Cage, CageViewModel>();
            CreateMap<CageCreateModel, Cage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<CageUpdateModel, Cage>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Area
            CreateMap<Area, AreaViewModel>();

            // Species
            CreateMap<Species, SpeciesViewModel>();

            // CareMode
            CreateMap<CareMode, CareModeViewModel>();
        }
    }
}
