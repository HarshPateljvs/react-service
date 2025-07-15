using React.Domain.DTOs.ECharts.Series.SeriesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.ChartsDto
{
    public class StackedBarChart
    {
        public TooltipOption? tooltip { get; set; }
        public LegendOption? legend { get; set; }
        public GridOption? grid { get; set; }
        public List<XAxisOption>? xAxis { get; set; }
        public List<YAxisOption>? yAxis { get; set; }
        public List<BarSeriesOption>? series { get; set; }
    }
}
