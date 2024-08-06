using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Repository;
using MongoDb_Repo.Infrastructure.Interface;
using MongoDb_Repo.Infrastructure.Service;

namespace MongoDb_Repo.Application.Extension
{
    public static class DIExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IFileUploadService,FileUploadService>();
            services.AddScoped<IExcelService,ExcelService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSkillRepository, UserSkillRepository>();
            services.AddScoped<ISkillPropertiesRepository, SkillPropertiesRepository>();
            return services;
        }
    }
}
