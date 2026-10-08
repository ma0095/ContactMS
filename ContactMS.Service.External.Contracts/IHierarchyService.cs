using ContactMS.DTOs.Hierarchy;
using ContactMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Service.External.Contracts
{
    public interface IHierarchyService
    {
        Task<ActionStatus<HierarchyDTO>> GetHierarchyDetails(string token, long id);
        Task<ActionStatus<IHierarchyResponse>> CreateHierarchy(string token, HierarchyCreateRequestDTO request);

    }
}
