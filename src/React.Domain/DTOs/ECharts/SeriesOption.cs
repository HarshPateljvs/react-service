using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{

    #region https://echarts.apache.org/en/option.html#series
    public partial class SeriesOption
    {
        public string? name { get; set; }
        public string? type { get; set; }
        public string? stack { get; set; }
        public List<object>? data { get; set; }
        public static class Types
        {
            public const string Line = "line";
            public const string Bar = "bar";
            public const string Pie = "pie";
        }
    }
    #endregion

}
