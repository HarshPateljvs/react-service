using React.Domain.Common;
using React.Domain.DTOs.ECharts;
using React.Domain.DTOs.ECharts.ChartsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.Chart
{
    //https://echarts.apache.org/en/option.html#title
    public interface IChartService
    {
        Task<APIBaseResponse<LineChart>> GetLineChartDataAsync(FilterDto filter);
        Task<APIBaseResponse<BarChart>> GetBarChartDataAsync(FilterDto filter);
        Task<APIBaseResponse<DonutChart>> GetDonutChartDataAsync(FilterDto filter);
        Task<APIBaseResponse<PieChart>> GetPieChartDataAsync(FilterDto filter);
    }
}
