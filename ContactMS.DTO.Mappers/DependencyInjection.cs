using ContactMS.Data.Contract;
using ContactMS.DTO.Mappers.Contact;
using ContactMS.DTOs.Contact;
using ContactMS.Framework.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTO.Mappers
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddDTOMappers(this IServiceCollection services)
        {
            services.AddScoped<APIDataMapper<IContact, ContactDTO>, ContactMapper>();
            services.AddScoped<APIDataMapper<IContact, CreateContactDTO>, CreateContactMapper>();
            services.AddScoped<APIDataMapper<IContact, EditContactDTO>, EditContactMapper>();

            return services;
        }
    }
}
