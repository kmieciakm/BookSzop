using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService_UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register required services here
            // services.AddTransient<IDependency, DependencyClass>();
        }
    }
}
