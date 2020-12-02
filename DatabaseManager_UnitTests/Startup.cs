using DatabaseManager;
using DatabaseManager.Repository.Contracts;
using DatabaseManager.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests_MockDatabase;

namespace DatabaseManager_UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Database access
            services.AddScoped((Func<IServiceProvider, DbContextBase>)((serviceProvider) =>
                new UnitTests_MockDatabase.MockDbFactory().CreateMockDbContext()));

            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
