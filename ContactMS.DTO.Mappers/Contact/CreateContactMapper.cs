using ContactMS.Data.Contract;
using ContactMS.DTOs.Contact;
using ContactMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTO.Mappers.Contact
{
    public class CreateContactMapper : APIDataMapper<IContact, CreateContactDTO>
    {
        public CreateContactMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContact ToEntity(CreateContactDTO value)
        {
            IContact entity = this.CreateEntity();
            entity.Name = value.Name;
            entity.ActiveStatus = value.ActiveStatus;
            entity.CreatedUserId = value.CreatedUserId;
            return entity;
        }

        public override CreateContactDTO ToObject(IContact? entity)
        {
            CreateContactDTO value = new CreateContactDTO();
            value.Name = entity.Name;
            value.ActiveStatus = entity.ActiveStatus;
            value.CreatedUserId = entity.CreatedUserId;
            return value;
        }
    }
}
