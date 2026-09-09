using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
[Route("error")]
[ApiController]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;
    public ErrorController (ILogger<ErrorController> logger)
    {
        _logger = logger;
    }
    [Route("")]
    public IActionResult ManejarError()
    {
        var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = errorFeature?.Error;

        _logger.LogError(exception, "Ocurrió un error inesperado");
        return StatusCode (500, "Ha ocurrido un error, intenta más tarde");
    }
}