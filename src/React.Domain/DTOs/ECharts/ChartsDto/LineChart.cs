using React.Domain.DTOs.ECharts.Series.SeriesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.ChartsDto
{
    public class LineChart
    {
        public TitleOption? title { get; set; }
        public TooltipOption? tooltip { get; set; }
        public List<LegendOption>? legend { get; set; }
        public GridOption? grid { get; set; }
        public ToolboxOption? toolbox { get; set; }
        public XAxisOption? xAxis { get; set; }
        public YAxisOption? yAxis { get; set; }
        public List<LineSeriesOption>? series { get; set; }
    }

}
