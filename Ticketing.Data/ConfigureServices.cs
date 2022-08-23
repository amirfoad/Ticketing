using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities.Users;
using Ticketing.Data.Persistence;
using Ticketing.Data.Persistence.Interceptors;

namespace Ticketing.Data
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TicketingDbContext>(options =>
            {
                //options.UseSqlServer(configuration.GetConnectionString("TicketingConnection"));
                options.UseInMemoryDatabase("TicketingDb");
            });
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();



            return services;
        }
    }
}
