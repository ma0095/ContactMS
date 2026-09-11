using ContactMS.Data.Contract;
using ContactMS.DTOs.Contact;
using ContactMS.DTOs.ContactDetail;
using ContactMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTO.Mappers.ContactDetail
{
    public class ContactDetailMapper : APIDataMapper<IContactDetail, ContactDetailDTO>
    {
        public ContactDetailMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContactDetail ToEntity(ContactDetailDTO value)
        {
            IContactDetail entity = this.CreateEntity();
            entity.Id = value.Id;
            entity.ContactId = value.ContactId;
            entity.ContactNumber = value.ContactNumber;
            entity.CreatedUserId = value.CreatedUserId;
            entity.EditedUserId = value.EditedUserId;
            return entity;
        }

        public override ContactDetailDTO ToObject(IContactDetail? entity)
        {
            ContactDetailDTO value = new ContactDetailDTO();
            value.Id = entity.Id;
            value.ContactId = entity.ContactId;
            value.ContactNumber = entity.ContactNumber;
            value.CreatedUserId = entity.CreatedUserId;
            value.EditedUserId = entity.EditedUserId;
            return value;
        }
    }
}
