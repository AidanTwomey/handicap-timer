using finisher_service.api.model;
using finisher_service.lib;
using Microsoft.AspNetCore.Mvc;

namespace finisher_service.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinishController : ControllerBase
    {
        private readonly IPersister persister;

        public FinishController(IPersister persister)
        {
            this.persister = persister;
        }

        [HttpPost]
        public IActionResult Post(FinishEvent finish)
        {
            persister.Persist(0);

            return Ok();
        }
    }
}
