using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.Series
{
    #region https://echarts.apache.org/en/option.html#series-pie.emphasis
    public class EmphasisOption
    {
        public ItemStyleOption? itemStyle { get; set; }
        public LabelOption? label { get; set; }
    }

    public class ItemStyleOption
    {
        public int? borderRadius { get; set; }
        public string? borderColor { get; set; }
        public int? borderWidth { get; set; }
    }
    public class LabelOption
    {
        public bool? show { get; set; }
        public string? position { get; set; }
        public int? fontSize { get; set; }
        public string? fontWeight { get; set; }
        public string? formatter { get; set; }
        public static class Positions
        {
            public const string Inside = "inside";
            public const string Top = "top";
            public const string Left = "left";
            public const string Right = "right";
            public const string Bottom = "bottom";
        }
    }
    #endregion
}
