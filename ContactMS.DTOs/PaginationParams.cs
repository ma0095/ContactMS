using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs
{
    public class PaginationParams
    {
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string? SearchTerm { get; set; }
    }
}
