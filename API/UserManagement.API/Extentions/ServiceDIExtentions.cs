using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Helpers;
using UserManagement.Application.Services;
using UserManagement.Domain.Services;

namespace UserManagement.API.Extentions
{
    public static class ServiceDIExtensions
    {
        public static void RegisterAPI(this IServiceCollection collection)
        {
            collection.AddScoped<IUserService, UserService>();
           
            collection.AddScoped<AuthenticationSettings>();
        }
    }
}