using finisher_service.api.model;
using Microsoft.AspNetCore.Mvc;

namespace finisher_service.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinishController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(FinishEvent finish) => Ok();
    }
}
