
using AutoMapper;
using Common.Helpers;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Creates;
using Domain.Models.Updates;
using Domain.Models.Views;
using Task = Domain.Entities.Task;

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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StaffStatuses.ACTIVE))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));

            // Manager
            CreateMap<Manager, AuthModel>().ReverseMap();
            CreateMap<Manager, ManagerViewModel>();
            CreateMap<ManagerRegistrationModel, Manager>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StaffStatuses.ACTIVE))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));

            // Farm
            CreateMap<Farm, FarmViewModel>();
            CreateMap<FarmCreateModel, Farm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<FarmUpdateModel, Farm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Bird
            CreateMap<Bird, BirdViewModel>();
            CreateMap<BirdCreateModel, Bird>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<BirdUpdateModel, Bird>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Cage
            CreateMap<Cage, CageViewModel>();
            CreateMap<CageCreateModel, Cage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<CageUpdateModel, Cage>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Area
            CreateMap<Area, AreaViewModel>();
            CreateMap<AreaCreateModel, Area>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<AreaUpdateModel, Area>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Species
            CreateMap<Species, SpeciesViewModel>();
            CreateMap<SpeciesCreateModel, Species>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<SpeciesUpdateModel, Species>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // CareMode
            CreateMap<CareMode, CareModeViewModel>();
            CreateMap<CareModeCreateModel, CareMode>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<CareModeUpdateModel, CareMode>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Food
            CreateMap<Food, FoodViewModel>();
            CreateMap<FoodCreateModel, Food>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => FoodStatuses.AVAILABLE));
            CreateMap<FoodUpdateModel, Food>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Food Category
            CreateMap<FoodCategory, FoodCategoryViewModel>();
            CreateMap<FoodCategoryCreateModel, FoodCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<FoodCategoryUpdateModel, FoodCategory>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Bird Category
            CreateMap<BirdCategory, BirdCategoryViewModel>();
            CreateMap<BirdCategoryCreateModel, BirdCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<BirdCategoryUpdateModel, BirdCategory>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Unit Of Measurement
            CreateMap<UnitOfMeasurement, UnitOfMeasurementViewModel>();
            CreateMap<UnitOfMeasurementCreateModel, UnitOfMeasurement>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<UnitOfMeasurementUpdateModel, UnitOfMeasurement>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task
            CreateMap<Task, TaskViewModel>();
            CreateMap<TaskCreateModel, Task>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<TaskUpdateModel, Task>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task Check List
            CreateMap<TaskCheckList, TaskCheckListViewModel>();
            CreateMap<TaskCheckListCreateModel, TaskCheckList>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<TaskCheckListUpdateModel, TaskCheckList>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task Check List Report 
            CreateMap<TaskCheckListReport, TaskCheckListReportViewModel>();
            CreateMap<TaskCheckListReportCreateModel, TaskCheckListReport>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.FinishAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<TaskCheckListReportUpdateModel, TaskCheckListReport>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task Check List Report Item
            CreateMap<TaskCheckListReportItem, TaskCheckListReportItemViewModel>();
            CreateMap<TaskCheckListReportItemCreateModel, TaskCheckListReportItem>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<TaskCheckListReportItemUpdateModel, TaskCheckListReportItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task Sample
            CreateMap<TaskSample, TaskSampleViewModel>();
            CreateMap<TaskSampleCreateModel, TaskSample>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<TaskSampleUpdateModel, TaskSample>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task Sample Check List
            CreateMap<TaskSampleCheckList, TaskSampleCheckListViewModel>();
            CreateMap<TaskSampleCheckListCreateModel, TaskSampleCheckList>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<TaskSampleCheckListUpdateModel, TaskSampleCheckList>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Ticket
            CreateMap<Ticket, TicketViewModel>();
            CreateMap<TicketCreateModel, Ticket>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<TicketUpdateModel, Ticket>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Meal Item
            CreateMap<MealItem, MealItemViewModel>();
            CreateMap<MealItemCreateModel, MealItem>()
               .ForMember(dest => dest.MenuMealId, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<MealItemUpdateModel, MealItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Meal Item
            CreateMap<MealItemSample, MealItemSampleViewModel>();
            CreateMap<MealItemSampleCreateModel, MealItemSample>()
               .ForMember(dest => dest.MenuMealSammpleId, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<MealItemSampleUpdateModel, MealItemSample>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Menu
            CreateMap<Menu, MenuViewModel>();
            CreateMap<MenuCreateModel, Menu>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<MenuUpdateModel, Menu>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Menu Meal
            CreateMap<MenuMeal, MenuMealViewModel>();
            CreateMap<MenuMealCreateModel, MenuMeal>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<MenuMealUpdateModel, MenuMeal>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Menu Meal Sample
            CreateMap<MenuMealSample, MenuMealSampleViewModel>();
            CreateMap<MenuMealSampleCreateModel, MenuMealSample>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<MenuMealSampleUpdateModel, MenuMealSample>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Menu Sample
            CreateMap<MenuSammple, MenuSampleViewModel>();
            CreateMap<MenuSampleCreateModel, MenuSammple>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<MenuSampleUpdateModel, MenuSammple>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Plan
            CreateMap<Plan, PlanViewModel>();
            CreateMap<PlanCreateModel, Plan>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTimeHelper.VnNow));
            CreateMap<PlanUpdateModel, Plan>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Repeat
            CreateMap<Repeat, RepeatViewModel>();
            CreateMap<RepeatCreateModel, Repeat>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<RepeatUpdateModel, Repeat>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
