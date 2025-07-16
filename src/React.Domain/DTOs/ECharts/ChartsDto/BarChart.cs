using React.Domain.DTOs.ECharts.Series.SeriesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.ChartsDto
{
    public class BarChart
    {
        public List<TooltipOption>? tooltip { get; set; }
        public List<GridOption>? grid { get; set; }
        public List<XAxisOption>? xAxis { get; set; }
        public List<YAxisOption>? yAxis { get; set; }
        public List<BarSeriesOption>? series { get; set; }
        public List<DataZoomOption>? dataZoom { get; set; }

    }
}
