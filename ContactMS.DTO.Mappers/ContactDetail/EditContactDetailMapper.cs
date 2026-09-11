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
    public class EditContactDetailMapper : APIDataMapper<IContactDetail, EditContactDetailDTO>
    {
        public EditContactDetailMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContactDetail ToEntity(EditContactDetailDTO value)
        {
            IContactDetail entity = this.CreateEntity();
            entity.Id = value.Id;
            entity.ContactId = value.ContactId;
            entity.ContactNumber = value.ContactNumber;
            entity.EditedUserId = value.EditedUserId;
            return entity;
        }

        public override EditContactDetailDTO ToObject(IContactDetail? entity)
        {
            EditContactDetailDTO value = new EditContactDetailDTO();
            value.Id = entity.Id;
            value.ContactId = entity.ContactId;
            value.ContactNumber = entity.ContactNumber;
            value.EditedUserId = entity.EditedUserId;
            return value;
        }
    }
}
