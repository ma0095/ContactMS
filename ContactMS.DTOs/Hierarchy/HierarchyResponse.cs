using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Hierarchy
{
    public class HierarchyResponse : Response,IHierarchyResponse
    {
         public long Id { get; set; }
        /// <summary>
        /// Get or Set AgentCode
        /// </summary>
        public string AgentCode { get; set; }
        /// <summary>
        /// Get or Set Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Get or Set Hierarchy Type ID
        /// </summary>
        public long HierarchyTypeId { get; set; }
        /// <summary>
        /// Get or Set SolutionPartnerId
        /// </summary>
        public long? SolutionPartnerId { get; set; }
        /// <summary>
        /// Get or Set ParentId
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// Get or Set External System Number
        /// </summary>
        public string? ExternalSystemNumber { get; set; }
        /// <summary>
        /// Get or Set Short Code
        /// </summary>
        public string? ShortCode { get; set; }

        /// <summary>
        /// Get or Set ActiveStatus
        /// </summary>
        public int ActiveStatus { get; set; }
        /// <summary>
        /// get or set Created User Id
        /// </summary>
        public long? CreatedUserId { get; set; }
        /// <summary>
        /// get or set Edited User Id
        /// </summary>
        public long? EditedUserId { get; set; }
        /// <summary>
        /// get or set Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// get or set Edited Date
        /// </summary>
        public DateTime? EditedDate { get; set; }
    }
}
