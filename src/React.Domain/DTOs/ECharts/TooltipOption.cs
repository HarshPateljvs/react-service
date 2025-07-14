using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{
    #region https://echarts.apache.org/en/option.html#tooltip
    public class TooltipOption
    {
        public string? trigger { get; set; }
        public static class Triggers
        {
            public const string Item = "item";
            public const string Axis = "axis";
            public const string None = "none";
        }
        public AxisPointerOption? axisPointer { get; set; }
    }

    #region https://echarts.apache.org/en/option.html#tooltip.axisPointer
    public class AxisPointerOption
    {
        public string? type { get; set; }

        public static class Types
        {
            public const string Line = "line";
            public const string Shadow = "shadow";
            public const string Cross = "cross";
            public const string None = "none";
        }
    }  
    #endregion
    #endregion
}
