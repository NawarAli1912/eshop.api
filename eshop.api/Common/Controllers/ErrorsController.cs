using Microsoft.AspNetCore.Mvc;

namespace eshop.api.Common.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [HttpGet]
    public IActionResult Error()
    {
        // var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}
