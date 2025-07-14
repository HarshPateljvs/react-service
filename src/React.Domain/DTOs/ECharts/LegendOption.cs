using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{
    #region https://echarts.apache.org/en/option.html#legend
    public class LegendOption
    {
        public string? orient { get; set; }
        public string? left { get; set; }
        public string? top { get; set; }
        public static class Orients
        {
            public const string Vertical = "vertical";
            public const string Horizontal = "horizontal";
        }
    } 
    #endregion
}
