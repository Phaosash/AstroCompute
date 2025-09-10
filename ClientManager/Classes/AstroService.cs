using ErrorLogging;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClientManager.Classes;

public class AstroService (HttpClient httpClient): IAstroContract {
    private readonly HttpClient _httpClient = httpClient;

    //  This method sends an HTTP POST request with the observed and rest wavelengths to calculate the star's velocity,
    //  handling any errors by logging them and returning a default value of 0.0 in case of failure.
    public async Task<double> CalculateStarVelocityAsync (double observedWavelengthNm, double restWavelengthNm){
        try {
            var response = await _httpClient.PostAsJsonAsync("api/astro/velocity", new {
                observedWavelength = observedWavelengthNm,
                restWavelength = restWavelengthNm
            });

            response.EnsureSuccessStatusCode();

            var velocityResult = await response.Content.ReadFromJsonAsync<double>();
            return velocityResult;
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the star velocity");
            return 0.0;
        }
    }

    //  This method sends an HTTP POST request with the parallax angle to calculate the star's distance in parsecs,
    //  handling errors by logging them and returning -1 if the calculation fails.
    public async Task<double> CalculateStarDistanceParsecsAsync (double parallaxArcseconds){
        try {
            var response = await _httpClient.PostAsJsonAsync("api/astro/distance", new { 
                parallaxAngle = parallaxArcseconds 
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<double>();
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the star distance");
            return -1;
        }
    }

    //  This method sends an HTTP POST request with a Celsius value to convert it to Kelvin, handling
    //  any errors by logging them and returning -1 if the conversion fails.
    public async Task<double> ConvertCelsiusToKelvinAsync (double celsius){
        try {
            var response = await _httpClient.PostAsJsonAsync("api/astro/temperature", new { 
                celsius 
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<double>();
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to convert Celsius to Kelvin");
            return -1;
        }
    }

    //  This method sends an HTTP POST request with a mass value to calculate the event horizon,
    //  handling errors by logging them and returning -1 if the calculation fails.
    public async Task<double> CalculateEventHorizonAsync (double massKg){
        try {
            var response = await _httpClient.PostAsJsonAsync("api/astro/eventhorizon", new { 
                mass = massKg 
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<double>();
        } catch(Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the Event Horizon");
            return -1;
        }
    }
}