using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Hierarchy
{
    public class HierarchyCreateRequestDTO
    {

        public string AgentCode { get; set; }
    
        public string Name { get; set; }

        public long HierarchyTypeId { get; set; }

        public long? SolutionPartnerId { get; set; }

        public string? ExternalSystemNumber { get; set; }

        public string? ShortCode { get; set; }

        public int ActiveStatus { get; set; }

        public long? CreatedUserId { get; set; }
    }
}
