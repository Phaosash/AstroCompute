
using AstroMath;

namespace WebServer.Services;

//  Implementation of the IAstroContract that delegates to AstroMath DLL.
public class AstroService : IAstroContract {
    //  This method asynchronously calculates the event horizon radius for a given mass in kilograms,
    //  throwing an exception if the mass is zero or negative. It uses a helper function to perform the c
    //  alculation and returns the result as a completed task.
    public Task<double> CalculateEventHorizonAsync (double massKg){
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(massKg);

        double r = AstroMathFunctions.CalculateEventHorizon(massKg);
        return Task.FromResult(r);
    }

    //  This method asynchronously calculates the distance to a star in parsecs based on its parallax angle in arcseconds,
    //  throwing an exception if the input is zero or negative. It uses a helper function for the calculation and returns the
    //  result as a completed task.
    public Task<double> CalculateStarDistanceParsecsAsync (double parallaxArcseconds){
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(parallaxArcseconds);

        double d = AstroMathFunctions.CalculateStarDistance(parallaxArcseconds);
            
        return Task.FromResult(d);
    }

    //  This method asynchronously calculates the velocity of a star using the observed and rest wavelengths in nanometers,
    //  throwing an exception if either value is zero or negative. It delegates the calculation to a helper function and
    //  returns the result as a completed task.
    public Task<double> CalculateStarVelocityAsync (double observedWavelengthNm, double restWavelengthNm){
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(observedWavelengthNm);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(restWavelengthNm);

        double velocity = AstroMathFunctions.CalculateStarVelocity(observedWavelengthNm, restWavelengthNm);
        return Task.FromResult(velocity);
    }

    //  This method asynchronously converts a temperature from Celsius to Kelvin, throwing an exception if the Celsius value
    //  is below absolute zero (-273.15). It uses a helper function for the conversion and returns the result as a completed task.
    public Task<double> ConvertCelsiusToKelvinAsync (double celsius){
        ArgumentOutOfRangeException.ThrowIfLessThan(celsius, -273.15);

        double k = AstroMathFunctions.ConvertCelsiusToKelvin(celsius);
        return Task.FromResult(k);
    }
}