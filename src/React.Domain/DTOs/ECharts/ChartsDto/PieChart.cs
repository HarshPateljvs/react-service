using React.Domain.DTOs.ECharts.Series.SeriesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.ChartsDto
{
    public class PieChart
    {
        public TitleOption? title { get; set; }
        public TooltipOption? tooltip { get; set; }
        public LegendOption? legend { get; set; }
        public List<DonutSeriesOption>? series { get; set; }
    }
}
