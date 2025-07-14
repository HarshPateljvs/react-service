using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.Series.SeriesType
{
    public class BarSeriesOption : SeriesOption
    {
        public string? barWidth { get; set; }
        public LabelOption? label { get; set; }
    }
}
