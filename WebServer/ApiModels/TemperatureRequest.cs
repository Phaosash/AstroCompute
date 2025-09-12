using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class TemperatureRequest {
    //  This property represents temperature in Celsius and must be greater than or equal to -273.15, the absolute zero.
    //  Validation ensures it falls within the valid physical range.
    [Required]
    [Range(-273.15, double.MaxValue, ErrorMessage = "Celsius must be >= -273.15.")]
    public double Celsius { get; set; }
}