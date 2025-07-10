using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.Common
{
    public class FilterDto
    {
        public Dictionary<string, object> Predicates { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<SortModel>? SortModels { get; set; } = new List<SortModel>();
        public FilterDto()
        {
            this.Predicates = new Dictionary<string, object>();
        }
    }
    public class SortModel
    {
        public string Field { get; set; }
        public string Sort { get; set; } // "asc" or "desc"
    }
}
