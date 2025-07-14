using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts
{
    #region https://echarts.apache.org/en/option.html#toolbox
    public class ToolboxOption
    {
        public ToolboxFeatureOption? feature { get; set; }
    }

    #region https://echarts.apache.org/en/option.html#toolbox.feature
    public class ToolboxFeatureOption
    {
        public object? saveAsImage { get; set; }
    }  
    #endregion
    #endregion
}
