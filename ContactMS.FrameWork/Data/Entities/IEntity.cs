using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMS.Framework.Data.Entities
{
    /// <summary>
    /// Represents a Entity Interface
    /// </summary>
    public interface IEntity
    {
        #region Member Variable
        /// <summary>
        /// get or set Identifier
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// get or set Created Date
        /// </summary>
        DateTime? CreatedDate { get; set; }
        /// <summary>
        /// get or set Edited Date
        /// </summary>
        DateTime? EditedDate { get; set; }
        #endregion
    }
}
