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
    public class CreateContactDetailMapper : APIDataMapper<IContactDetail, CreateContactDetailDTO>
    {
        public CreateContactDetailMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContactDetail ToEntity(CreateContactDetailDTO value)
        {
            IContactDetail entity = this.CreateEntity();
            entity.ContactId = value.ContactId;
            entity.ContactNumber = value.ContactNumber;
            entity.CreatedUserId = value.CreatedUserId;
            return entity;
        }

        public override CreateContactDetailDTO ToObject(IContactDetail? entity)
        {
            CreateContactDetailDTO value = new CreateContactDetailDTO();
            value.ContactId = entity.ContactId;
            value.ContactNumber = entity.ContactNumber;
            value.CreatedUserId = entity.CreatedUserId;
            return value;
        }
    }
}
