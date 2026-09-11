using ContactMS.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Business
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IContactService, ContactService>();
            _ = services.AddScoped<IContactDetailService, ContactDetailService>();
            return services;
        }
    }
}
