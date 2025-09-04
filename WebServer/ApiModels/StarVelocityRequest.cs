using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class StarVelocityRequest {
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "ObservedWavelength must be > 0.")]
    public double ObservedWavelength { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "RestWavelength must be > 0.")]
    public double RestWavelength { get; set; }
}