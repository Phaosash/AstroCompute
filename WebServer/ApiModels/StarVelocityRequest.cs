using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class StarVelocityRequest {
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "ObservedWavelength must be > 0.")]
    public double ObservedWavelengthNm { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "RestWavelength must be > 0.")]
    public double RestWavelengthNm { get; set; }
}