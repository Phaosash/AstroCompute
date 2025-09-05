using ClientDevice.DataModels;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClientDevice.Classes;

public class AstroService (HttpClient httpClient): IAstroContract {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<double> CalculateStarVelocityAsync (double observedWavelengthNm, double restWavelengthNm){
        var response = await _httpClient.PostAsJsonAsync("api/astro/velocity", new {
            observedWavelength = observedWavelengthNm,
            restWavelength = restWavelengthNm
        });

        response.EnsureSuccessStatusCode();

        var velocityResult = await response.Content.ReadFromJsonAsync<double>();
        return velocityResult;
    }

    public async Task<double> CalculateStarDistanceParsecsAsync (double parallaxArcseconds){
        var response = await _httpClient.PostAsJsonAsync("api/astro/distance", new {
            parallaxAngle = parallaxArcseconds
        });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<double>();
    }

    public async Task<double> ConvertCelsiusToKelvinAsync (double celsius){
        var response = await _httpClient.PostAsJsonAsync("api/astro/temperature", new {
            celsius
        });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<double>();
    }

    public async Task<double> CalculateEventHorizonAsync (double massKg){
        var response = await _httpClient.PostAsJsonAsync("api/astro/eventhorizon", new {
            mass = massKg
        });

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<double>();
    }
}