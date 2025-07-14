using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{
    #region https://echarts.apache.org/en/option.html#grid
    public class GridOption
    {
        public string? left { get; set; }
        public string? right { get; set; }
        public string? bottom { get; set; }
        public bool containLabel { get; set; }
    } 
    #endregion
}
