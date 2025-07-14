using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{
    #region https://echarts.apache.org/en/option.html#yAxis
    public class YAxisOption
    {
        public string? type { get; set; }
        public string? name { get; set; }
        public static class Types
        {
            public const string Category = "category";
            public const string Time = "time";
            public const string Value = "value";
        }
    } 
    #endregion
}
