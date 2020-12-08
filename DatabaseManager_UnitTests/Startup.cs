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
            services.AddScoped(serviceProvider => new InMemoryDbFactory().CreateDbContext());

            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
