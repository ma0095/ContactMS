using ContactMS.Data.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Services
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IContactDataService, ContactDataService>();
            return services;
        }
    }
}
