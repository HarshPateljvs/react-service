using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React.DAL.Interface.Chart;
using React.Domain.Common;

namespace React.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartService _chartService;

        public ChartController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpPost("GetLineChartData")]
        public async Task<IActionResult> GetLineChartData([FromBody] FilterDto filter)
        {
            var result = await _chartService.GetLineChartDataAsync(filter);
            return Ok(result);
        }

        [HttpPost("GetBarChartData")]
        public async Task<IActionResult> GetBarChartData([FromBody] FilterDto filter)
        {
            var response = await _chartService.GetBarChartDataAsync(filter);
            return Ok(response);
        }

        [HttpPost("GetDonutChartData")]
        public async Task<IActionResult> GetDonutChartData([FromBody] FilterDto filter)
        {
            var response = await _chartService.GetDonutChartDataAsync(filter);
            return Ok(response);
        }

        [HttpPost("GetPieChartData")]
        public async Task<IActionResult> GetPieChartData([FromBody] FilterDto filter)
        {
            var result = await _chartService.GetPieChartDataAsync(filter);
            return Ok(result);
        }
    }
}
