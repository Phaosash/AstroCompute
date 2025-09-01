using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class TemperatureRequest {
    [Required]
    [Range(-273.15, double.MaxValue, ErrorMessage = "Celsius must be >= -273.15.")]
    public double Celsius { get; set; }
}