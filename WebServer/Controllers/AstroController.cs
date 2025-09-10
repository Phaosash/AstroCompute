using ErrorLogging;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebServer.ApiModels;
using WebServer.Services;

namespace WebServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstroController(IAstroContract astro) : ControllerBase {
    private readonly IAstroContract _astro = astro;

    [HttpPost("velocity")]
    public async Task<IActionResult> Velocity ([FromBody] StarVelocityRequest req){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        try {
            double result = await _astro.CalculateStarVelocityAsync(req.ObservedWavelength, req.RestWavelength);

            return Ok(result);
        } catch (ArgumentOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Velocity Method.");

            return BadRequest(new { error = ex.ParamName, message = ex.Message });
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Velocity Method.");

            return StatusCode(500, new { error = "internal_error", message = ex.Message });
        }
    }

    [HttpPost("distance")]
    public async Task<IActionResult> Distance ([FromBody] StarDistanceRequest req){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        try {
            double result = await _astro.CalculateStarDistanceParsecsAsync(req.ParallaxAngle);

            return Ok(result);
        } catch (ArgumentOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Distance Method.");

            return BadRequest(new { error = ex.ParamName, message = ex.Message });
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Distance Method.");

            return StatusCode(500, new { error = "internal_error", message = ex.Message });
        }
    }

    [HttpPost("temperature")]
    public async Task<IActionResult> Temperature ([FromBody] TemperatureRequest req){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        try {
            double result = await _astro.ConvertCelsiusToKelvinAsync(req.Celsius);

            return Ok(result);
        } catch (ArgumentOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Temperature Method.");

            return BadRequest(new { error = ex.ParamName, message = ex.Message });
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Temperature Method.");

            return StatusCode(500, new { error = "internal_error", message = ex.Message });
        }
    }

    [HttpPost("eventhorizon")]
    public async Task<IActionResult> EventHorizon ([FromBody] EventHorizonRequest req){
        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        try {
            double result = await _astro.CalculateEventHorizonAsync(req.Mass);
            return Ok (result);
        } catch (ArgumentOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Event Horizon Method.");

            return BadRequest(new { error = ex.ParamName, message = ex.Message });
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Event Horizon Method.");

            return StatusCode(500, new { error = "internal_error", message = ex.Message });
        }
    }
}