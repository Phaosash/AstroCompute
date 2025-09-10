namespace ClientManager.Classes;

public interface IAstroContract {
     //  Calculates star velocity (m/s) from observed and rest wavelengths (nm).
    Task<double> CalculateStarVelocityAsync(double observedWavelengthNm, double restWavelengthNm);

    //  Calculates distance in parsecs from parallax angle in arcseconds.
    Task<double> CalculateStarDistanceParsecsAsync(double parallaxArcseconds);

    //  Converts Celsius to Kelvin.
    Task<double> ConvertCelsiusToKelvinAsync(double celsius);

    //  Calculates Schwarzschild radius (event horizon) in metres for given mass (kg).
    Task<double> CalculateEventHorizonAsync(double massKg);
}