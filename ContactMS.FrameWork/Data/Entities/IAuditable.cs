using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework.Data.Entities
{
    /// <summary>
    /// Represents Interface IAuditable
    /// </summary>
    public interface IAuditable
    {
        #region Member Variable
        /// <summary>
        /// get or set Created User Id
        /// </summary>
        long? CreatedUserId { get; set; }
        /// <summary>
        /// get or set Edited User Id
        /// </summary>
        long? EditedUserId { get; set; }
        #endregion
    }
}
