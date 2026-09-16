using ContactMS.Data.Contract;
using ContactMS.DTOs.ContactDetail;
using ContactMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTO.Mappers.ContactDetail
{
    public class ContactDetailCreateMapper : APIDataMapper<IContactDetail, ContactDetailCreateDTO>
    {
        public ContactDetailCreateMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContactDetail ToEntity(ContactDetailCreateDTO value)
        {
            IContactDetail entity = this.CreateEntity();
            entity.ContactNumber = value.ContactNumber;
            return entity;
        }

        public override ContactDetailCreateDTO ToObject(IContactDetail? entity)
        {
            ContactDetailCreateDTO value = new ContactDetailCreateDTO();
            value.ContactNumber = entity.ContactNumber;
            return value;
        }
    }
}
