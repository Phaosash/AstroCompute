using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class EventHorizonRequest {
    //  This property represents the mass of an object and must be a positive value greater than zero.
    //  Validation ensures it falls within the range from double.Epsilon to double.MaxValue.
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Mass must be > 0.")]
    public double Mass { get; set; }
}