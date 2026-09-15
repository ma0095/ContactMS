using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Data.Entities
{
    public interface IAuditable
    {
        long? CreatedUserId { get; set; }
        long? EditedUserId { get; set; }
    }
}
