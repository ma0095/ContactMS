using ContactMS.Service.External.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Service.External
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddExternalService(this IServiceCollection services)
        {
            services.AddScoped<IHierarchyService, HierarchyService>();
            return services;
        }
    }

}
