using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Data.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? EditedDate { get; set; }
    }
}
