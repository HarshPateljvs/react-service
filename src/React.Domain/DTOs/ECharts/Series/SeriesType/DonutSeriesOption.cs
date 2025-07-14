using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.ECharts.Series.SeriesType
{
    public class DonutSeriesOption : SeriesOption
    {
        public bool? avoidLabelOverlap { get; set; }
        public List<string>? radius { get; set; }
        public EmphasisOption? emphasis { get; set; }
        public LabelOption? label { get; set; }
        public ItemStyleOption? itemStyle { get; set; }
        public LabelLineOption? labelLine { get; set; }
    }
}
