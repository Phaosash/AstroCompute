using ErrorLogging;
using Microsoft.AspNetCore.Mvc;
using WebServer.ApiModels;
using WebServer.Services;

namespace WebServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstroController(IAstroContract astro) : ControllerBase {
    private readonly IAstroContract _astro = astro;

    //  This endpoint handles HTTP POST requests to calculate the velocity of a star based on observed and rest wavelengths.
    //  It validates the input model, calls an asynchronous velocity calculation method, and returns the result or appropriate
    //  error responses for invalid data or unexpected exceptions.
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

    //  This POST endpoint calculates the distance to a star in parsecs using the provided parallax angle.
    //  It validates the input, performs the calculation asynchronously, and handles errors by returning
    //  appropriate HTTP responses with logged details.
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

    //  This POST endpoint converts a temperature from Celsius to Kelvin asynchronously.
    //  It validates the input, performs the conversion, and returns the result or appropriate
    //  error responses with logging for any exceptions.
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

    //  This POST endpoint calculates the event horizon radius based on the given mass.
    //  It validates the input, performs the calculation asynchronously, and returns the result
    //  or appropriate error responses with error logging.
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