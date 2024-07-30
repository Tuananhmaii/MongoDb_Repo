using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Infrastructure.Service;

namespace MongoDb_Repo.Application.Extension
{
    public static class DIExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IFileUploadService, FileUploadService>();
            return services;
        }
    }
}
