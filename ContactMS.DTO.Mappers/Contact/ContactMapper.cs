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
    public class ContactMapper : APIDataMapper<IContact, ContactDTO>
    {
        public ContactMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContact ToEntity(ContactDTO value)
        {
            IContact entity = this.CreateEntity();
            entity.Id = value.Id;
            entity.Name = value.Name;
            entity.ActiveStatus = value.ActiveStatus;
            entity.CreatedUserId = value.CreatedUserId;
            entity.EditedUserId = value.EditedUserId;
            return entity;
        }

        public override ContactDTO ToObject(IContact? entity)
        {
            ContactDTO value = new ContactDTO();
            value.Id = entity.Id;
            value.Name = entity.Name;
            value.ActiveStatus = entity.ActiveStatus;
            value.CreatedUserId = entity.CreatedUserId;
            value.EditedUserId = entity.EditedUserId;
            return value;
        }
    }
}
