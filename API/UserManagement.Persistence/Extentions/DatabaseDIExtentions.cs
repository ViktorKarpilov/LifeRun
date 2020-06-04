using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UserManagement.Persistence.Contexts;
using UserManagement.Persistence.Repositories;
using UserManagement.Domain.Repositories;

namespace UserManagement.Persistence.Extentions
{
    public static class DatabaseDIExtensions
    {
        private const string MySqlConnection = nameof(MySqlConnection);

        public static void RegisterPersistence(this IServiceCollection collection, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString(MySqlConnection);
            collection.AddDbContextPool<UserManagementContext>(builder => builder.UseMySql(connection,
                mySqlOptions => mySqlOptions
                .ServerVersion(new Version(10, 3, 11), ServerType.MySql)));

            collection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}