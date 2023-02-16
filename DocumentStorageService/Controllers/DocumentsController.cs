using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorageService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DocumentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
            => Ok("Hello");
    }
}
