using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
