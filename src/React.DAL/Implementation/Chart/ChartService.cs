using React.DAL.Interface.Chart;
using React.Domain.Common;
using React.Domain.DTOs.ECharts;
using React.Domain.DTOs.ECharts.ChartsDto;
using React.Domain.DTOs.ECharts.Series;
using React.Domain.DTOs.ECharts.Series.SeriesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Implementation.Chart
{
    public class ChartService : IChartService
    {
        #region Linechart https://echarts.apache.org/examples/en/editor.html?c=line-stack
        public async Task<APIBaseResponse<LineChart>> GetLineChartDataAsync(FilterDto filter)
        {
            var option = new LineChart
            {
                title = new TitleOption { text = "Stacked Line" },
                tooltip = new TooltipOption { trigger = TooltipOption.Triggers.Axis },
                grid = new GridOption
                {
                    left = "3%",
                    right = "4%",
                    bottom = "3%",
                    containLabel = true
                },
                toolbox = new ToolboxOption
                {
                    feature = new ToolboxFeatureOption { saveAsImage = new { } }
                },
                xAxis = new XAxisOption
                {
                    type = XAxisOption.Types.Category,
                    boundaryGap = false,
                    data = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" }
                },
                yAxis = new YAxisOption { type = YAxisOption.Types.Value },
                series = new List<LineSeriesOption>
        {
            new LineSeriesOption { name = "Email", type = SeriesOption.Types.Line, stack = "Total", data = new List<object> { 120, 132, 101, 134, 90, 230, 210 } },
            new LineSeriesOption { name = "Union Ads", type = SeriesOption.Types.Line, stack = "Total", data = new List<object> { 220, 182, 191, 234, 290, 330, 310 } },
            new LineSeriesOption { name = "Video Ads", type = SeriesOption.Types.Line, stack = "Total", data = new List<object> { 150, 232, 201, 154, 190, 330, 410 } },
            new LineSeriesOption { name = "Direct", type = SeriesOption.Types.Line, stack = "Total", data = new List<object> { 320, 332, 301, 334, 390, 330, 320 } },
            new LineSeriesOption { name = "Search Engine", type = SeriesOption.Types.Line, stack = "Total", data = new List<object> { 820, 932, 901, 934, 1290, 1330, 1320 } }
        }
            };

            return new APIBaseResponse<LineChart> { Data = option };
        }
        #endregion


        #region Barchart https://echarts.apache.org/examples/en/editor.html?c=bar-background&lang=ts
        public async Task<APIBaseResponse<BarChart>> GetBarChartDataAsync(FilterDto filter)
        {
            var option = new BarChart
            {
                tooltip = new List<TooltipOption>
                {
                    new TooltipOption
                    {
                        trigger = TooltipOption.Triggers.Axis,
                        axisPointer = new AxisPointerOption
                        {
                            type = AxisPointerOption.Types.Shadow
                        }
                    }
                },
                grid = new List<GridOption>
                {
                    new GridOption
                    {
                        left = "3%",
                        right = "4%",
                        bottom = "3%",
                        containLabel = true
                    }
                },
                xAxis = new List<XAxisOption>
                {
                    new XAxisOption
                    {
                        type = XAxisOption.Types.Category,
                        data = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
                        axisTick = new AxisTickOption { alignWithLabel = true }
                    }
                },
                yAxis = new List<YAxisOption>
                {
                    new YAxisOption { type = YAxisOption.Types.Value }
                },
                series = new List<BarSeriesOption>
                {
                    new BarSeriesOption
                    {
                        name = "Direct",
                        type = SeriesOption.Types.Bar,
                        barWidth = "60%",
                        data = new List<object> { 10, 52, 200, 334, 390, 330, 220 },
                        label = new LabelOption
                        {
                            show = true,
                            position = LabelOption.Positions.Inside // Or Top / Bottom etc.
                        }
                    }
                },
                dataZoom = new List<DataZoomOption>
                {
                    new DataZoomOption
                    {
                        type = "inside",
                        start = "0",
                        end = "100"
                    }
                }
            };

            return new APIBaseResponse<BarChart> { Data = option };
        }

        #endregion


        public async Task<APIBaseResponse<DonutChart>> GetDonutChartDataAsync(FilterDto filter)
        {
            var option = new DonutChart
            {
                tooltip = new TooltipOption
                {
                    trigger = TooltipOption.Triggers.Item
                },
                legend = new LegendOption
                {
                    top = "5%",
                    left = "center"
                },
                series = new List<DonutSeriesOption>
                {
                    new DonutSeriesOption
                    {
                        name = "Access From",
                        type = SeriesOption.Types.Pie,
                        radius = new List<string> { "40%", "70%" },
                        avoidLabelOverlap = false,
                        itemStyle = new ItemStyleOption
                        {
                            borderRadius = 10,
                            borderColor = "#fff",
                            borderWidth = 2
                        },
                        label = new LabelOption
                        {
                            show = true,
                            position =LabelOption.Positions.Inside,
                            formatter = "{d}%"
                        },
                        emphasis = new EmphasisOption
                        {
                            focus = EmphasisOption.Focuses.Self,
                            label = new LabelOption
                            {
                                show = true,
                                fontSize = 40,
                                fontWeight = "bold"
                            }
                        },
                        labelLine = new LabelLineOption
                        {
                            show = true
                        },
                        data = new List<object>
                        {
                            new { value = 735, name = "Search Engine" },
                            new { value = 1048, name = "Direct" },
                            new { value = 580, name = "Email" },
                            new { value = 484, name = "Union Ads" },
                            new { value = 300, name = "Video Ads" }
                        }
                    }
                }
            };

            return new APIBaseResponse<DonutChart> { Data = option };
        }
        public async Task<APIBaseResponse<PieChart>> GetPieChartDataAsync(FilterDto filter)
        {
            var option = new PieChart
            {
                title = new TitleOption
                {
                    text = "Referer of a Website",
                    subtext = "Fake Data",
                    left = "center"
                },
                tooltip = new TooltipOption
                {
                    trigger = TooltipOption.Triggers.Item
                },
                legend = new LegendOption
                {
                    orient = LegendOption.Orients.Vertical,
                    left = "left"
                },
                series = new List<DonutSeriesOption>
                {
                    new DonutSeriesOption
                    {
                        name = "Access From",
                        type = SeriesOption.Types.Pie,
                        radius = "50%",
                        data = new List<object>
                        {
                            new { value = 735, name = "Direct" },
                            new { value = 1048, name = "Search Engine" },
                            new { value = 580, name = "Email" },
                            new { value = 484, name = "Union Ads" },
                            new { value = 300, name = "Video Ads" }
                        },
                        label = new LabelOption
                        {
                            show = true,
                            formatter = "{c}",
                            position= LabelOption.Positions.Inside
                        },
                        emphasis = new EmphasisOption
                        {
                            itemStyle = new ItemStyleOption
                            {
                                shadowBlur = 10,
                                shadowOffsetX = 0,
                                shadowColor = "rgba(0, 0, 0, 0.5)"
                            }
                        }
                    }
                }
            };

            return new APIBaseResponse<PieChart> { Data = option };
        }


        public async Task<APIBaseResponse<StackedBarChart>> GetStackedBarChartDataAsync(FilterDto filter)
        {
            var option = new StackedBarChart
            {
                tooltip = new TooltipOption
                {
                    trigger = TooltipOption.Triggers.Axis,
                    axisPointer = new AxisPointerOption
                    {
                        type = AxisPointerOption.Types.Shadow
                    }
                },
                legend = new LegendOption(),
                grid = new GridOption
                {
                    left = "3%",
                    right = "4%",
                    bottom = "3%",
                    containLabel = true
                },
                xAxis = new List<XAxisOption>
        {
            new XAxisOption
            {
                type = "category",
                data = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" }
            }
        },
                yAxis = new List<YAxisOption>
        {
            new YAxisOption
            {
                type = "value"
            }
        },
                series = new List<BarSeriesOption>
                {
                    new BarSeriesOption { name = "Direct", type = "bar", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 320, 332, 301, 334, 390, 330, 320 }},
                    new BarSeriesOption { name = "Email", type = "bar", stack = "Ad", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 120, 132, 101, 134, 90, 230, 210 }},
                    new BarSeriesOption { name = "Union Ads", type = "bar", stack = "Ad", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 220, 182, 191, 234, 290, 330, 310 }},
                    new BarSeriesOption { name = "Video Ads", type = "bar", stack = "Ad", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 150, 232, 201, 154, 190, 330, 410 }},
                    new BarSeriesOption { name = "Search Engine",type = "bar",emphasis = new EmphasisOption { focus = "series" },data = new List<object> { 862, 1018, 964, 1026, 1679, 1600, 1570 } },
                    new BarSeriesOption { name = "Baidu", type = "bar", barWidth = "5", stack = "Search Engine", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 620, 732, 701, 734, 1090, 1130, 1120 }},
                    new BarSeriesOption { name = "Google", type = "bar", stack = "Search Engine", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 120, 132, 101, 134, 290, 230, 220 }},
                    new BarSeriesOption { name = "Others", type = "bar", stack = "Search Engine", emphasis = new EmphasisOption { focus = "series" }, data = new List<object> { 62, 82, 91, 84, 109, 110, 120 }},
                },
                dataZoom = new List<DataZoomOption>
                {
                    new DataZoomOption
                    {
                        type = "inside",
                        start = "0",
                        end = "100"
                    }
                }
            };

            return new APIBaseResponse<StackedBarChart> { Data = option };
        }


    }

}
