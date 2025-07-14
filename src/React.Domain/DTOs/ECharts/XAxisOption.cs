using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{
    #region https://echarts.apache.org/en/option.html#xAxis
    public class XAxisOption
    {
        public string? type { get; set; }
        public string? name { get; set; }
        public bool? boundaryGap { get; set; }
        public List<string>? data { get; set; }
        public AxisTickOption? axisTick { get; set; }

        public static class Types
        {
            public const string Category = "category";
            public const string Time = "time";
            public const string Value = "value";
        }
    }

    #region https://echarts.apache.org/en/option.html#xAxis.axisTick
    public class AxisTickOption
    {
        public bool alignWithLabel { get; set; }
    } 
    #endregion

    #endregion

}
