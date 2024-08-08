using Microsoft.Extensions.DependencyInjection;
using MongoDb_Repo.Domain.Interface.Service;
using MongoDb_Repo.Domain.Interface.Repository;
using MongoDb_Repo.Domain.Repository;
using MongoDb_Repo.Application.Service;


namespace MongoDb_Repo.Application.Extension
{
    public static class ServiceCollectionExtension
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
