using ContactMS.Data.Contract;
using ContactMS.Data.Entities;
using ContactMS.FrameWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductMS.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEntities(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ContactMSContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            _ = services.AddTransient<IContact, Contact>();
            _ = services.AddTransient<IContactDetail, ContactDetail>();

            return services;
        }
    }
}
