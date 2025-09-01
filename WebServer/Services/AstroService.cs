
using AstroMath;

namespace WebServer.Services;

//  Implementation of the IAstroContract that delegates to AstroMath DLL.
public class AstroService : IAstroContract {
    public Task<double> CalculateEventHorizonAsync (double massKg){
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(massKg);

        double r = AstroMathFunctions.CalculateEventHorizon(massKg);
        return Task.FromResult(r);
    }

    public Task<double> CalculateStarDistanceParsecsAsync (double parallaxArcseconds){
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(parallaxArcseconds);

        double d = AstroMathFunctions.CalculateStarDistance(parallaxArcseconds);
            
        return Task.FromResult(d);
    }

    public Task<double> CalculateStarVelocityAsync (double observedWavelengthNm, double restWavelengthNm){
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(observedWavelengthNm);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(restWavelengthNm);

        double velocity = AstroMathFunctions.CalculateStarVelocity(observedWavelengthNm, restWavelengthNm);
        return Task.FromResult(velocity);
    }

    public Task<double> ConvertCelsiusToKelvinAsync (double celsius){
        ArgumentOutOfRangeException.ThrowIfLessThan(celsius, -273.15);

        double k = AstroMathFunctions.ConvertCelsiusToKelvin(celsius);
        return Task.FromResult(k);
    }
}