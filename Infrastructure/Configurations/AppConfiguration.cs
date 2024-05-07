using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Configurations
{
    public static class AppConfiguration
    {
        public static void AddDependenceInjection(this IServiceCollection services)
        {
            services.AddScoped<IHangfireService, HangfireService>();
            services.AddScoped<ICloudStorageService, CloudStorageService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ICageService, CageService>();
            services.AddScoped<IBirdService, BirdService>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<ICareModeService, CareModeService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<ISpeciesService, SpeciesService>(); 
            services.AddScoped<IFoodService, FoodService>(); 
            services.AddScoped<IFoodCategoryService, FoodCategoryService>(); 
            services.AddScoped<IBirdCategoryService, BirdCategoryService>(); 
            services.AddScoped<IUnitOfMeasurementService, UnitOfMeasurementService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskCheckListService, TaskCheckListService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IDeviceTokenService, DeviceTokenService>();
            // day bi loi
            services.AddScoped<ITaskCheckListReportService, TaskCheckListReportService>();
            services.AddScoped<ITaskCheckListReportItemService, TaskCheckListReportItemService>();
            services.AddScoped<ITaskSampleService, TaskSampleService>();
            services.AddScoped<ITaskSampleCheckListService, TaskSampleCheckListService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IMealItemService, MealItemService>();
            services.AddScoped<IMealItemSampleService, MealItemSampleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuMealService, MenuMealService>();
            services.AddScoped<IMenuMealSampleService, MenuMealSampleService>();
            services.AddScoped<IMenuSampleService, MenuSampleService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IRepeatService, RepeatService>();
            services.AddScoped<IFoodReportService, FoodReportService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddFirebase(this IServiceCollection services)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(Path.Combine(currentDirectory, "firebase-adminsdk.json")),
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.Net 6.0 - Bird Farm Meal System", Description = "APIs Service", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                 });
            });
        }

        public static void UseJwt(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtMiddleware>();
        }

        public static void UseHangfireService(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var hangfireService = services.GetService<IHangfireService>();

                if (hangfireService != null)
                {
                    // Task
                }
            }
        }

    }
}
