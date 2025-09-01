using AstroMath;
using Microsoft.AspNetCore.Mvc;
using WebServer.Services;

namespace WebServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AstroServerController : ControllerBase, IAstroContract {
    [HttpGet("calculate-horizon")]
    public async Task<double> CalculateEventHorizonAsync (double a){
        return await Task.FromResult(AstroMathFunctions.CalculateEventHorizon(a));
    }

    [HttpGet("convert-kelvin")]
    public async Task<double> CalculateKelvinAsync (double a){
        return await Task.FromResult(AstroMathFunctions.ConvertCelsiusToKelvin(a));
    }

    [HttpGet("calculate-distance")]
    public async Task<double> CalculateStarDistance (double a){
        return await Task.FromResult(AstroMathFunctions.CalculateStarDistance(a));
    }

    [HttpGet("calculate-velocity")]
    public async Task<double> CalculateStarVelocity (double a, double B){
        return await Task.FromResult(AstroMathFunctions.CalculateStarVelocity(a, B));
    }
}
