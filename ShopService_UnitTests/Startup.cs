using DatabaseManager.Repository.Contracts;
using DatabaseManager.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopService;
using ShopService_UnitTests.MockDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService_UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Database access
            services.AddSingleton<DbContext>((serviceProvider) =>
                new DbContextFactory().CreateMockDbContext());

            services.AddTransient<IUserRepository, UserRepository>();

            // ShopService
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
        }
    }
}
