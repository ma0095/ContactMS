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
    public class EditContactMapper : APIDataMapper<IContact, EditContactDTO>
    {
        public EditContactMapper(IServiceProvider Services) : base(Services)
        {
        }

        public override IContact ToEntity(EditContactDTO value)
        {
            IContact entity = this.CreateEntity();
            entity.Id = value.Id;
            entity.Name = value.Name;
            entity.ActiveStatus = value.ActiveStatus;
            entity.EditedUserId = value.EditedUserId;
            return entity;
        }

        public override EditContactDTO ToObject(IContact? entity)
        {
            EditContactDTO value = new EditContactDTO();
            value.Id = entity.Id;
            value.Name = entity.Name;
            value.ActiveStatus = entity.ActiveStatus;
            value.EditedUserId = entity.EditedUserId;
            return value;
        }
    }
}
