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

            return Ok(new ValueResponse { Value = result, Formatted = FormatScientific(result), Unit = "m/s" });
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

            return Ok(new ValueResponse { Value = result, Formatted = FormatScientific(result), Unit = "pc" });
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

            return Ok(new ValueResponse { Value = result, Formatted = FormatScientific(result), Unit = "K" });
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
            return Ok (new ValueResponse { Value = result, Formatted = FormatScientific(result), Unit = "m" });
        } catch (ArgumentOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Event Horizon Method.");

            return BadRequest(new { error = ex.ParamName, message = ex.Message });
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Web Server: Astro Controller - Event Horizon Method.");

            return StatusCode(500, new { error = "internal_error", message = ex.Message });
        }
    }

    private static string FormatScientific (double value, int significantDigits = 4){
        if (double.IsNaN(value) || double.IsInfinity(value)){
            return value.ToString(CultureInfo.InvariantCulture);
        }

        //  Use "G" or custom formatting to produce scientific notation with n significant digits.
        //  Will format as: 1.234E+05
        string fmt = "E" + (significantDigits - 1).ToString();
        return value.ToString(fmt, CultureInfo.InvariantCulture);
    }
}