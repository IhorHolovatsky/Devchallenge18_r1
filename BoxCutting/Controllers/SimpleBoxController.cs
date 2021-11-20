using System.Threading.Tasks;
using BoxCutting.Core.Dtos;
using BoxCutting.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoxCutting.Api.Controllers
{
    [ApiController]
    public class SimpleBoxController : Controller
    {
        private readonly ISimpleBoxService _simpleBoxService;

        public SimpleBoxController(ISimpleBoxService simpleBoxService)
        {
            _simpleBoxService = simpleBoxService;
        }

        [HttpPost("api/simple_box")]
        public async Task<IActionResult> SimpleBox([FromBody] SimpleBoxRequestDto request)
        {
            var result = await _simpleBoxService.GetCuttingProgramAsync(request);

            if (result.Success)
            {
                return Json(result);
            }

            return UnprocessableEntity(new
            {
                success = result.Success,
                error = result.Error
            });
        }
    }
}
