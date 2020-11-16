﻿using DatabaseManager.Repository.Contracts;
using DatabaseManager.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopService.Exceptions.Authentication;
using ShopService.Purchase;
using ShopService.UserServ;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests_MockDatabase;

namespace ShopService_UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Database access
            services.AddScoped<DbContext>((serviceProvider) =>
                new DbContextFactory().CreateMockDbContext());

            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // ShopService
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
