using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class StarVelocityRequest {
    //  This property represents the observed wavelength and must be a positive value greater than zero.
    //  Validation ensures it lies between double.Epsilon and double.MaxValue.
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "ObservedWavelength must be > 0.")]
    public double ObservedWavelength { get; set; }

    //  This property represents the rest wavelength and must be a positive value greater than zero.
    //  Validation ensures it falls within the range of double.Epsilon to double.MaxValue.
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "RestWavelength must be > 0.")]
    public double RestWavelength { get; set; }
}