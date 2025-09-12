using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class StarDistanceRequest {
    //  This property represents the parallax angle and must be a positive value greater than zero.
    //  Validation restricts it to the range from double.Epsilon to double.MaxValue.
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Parallax must be > 0.")]
    public double ParallaxAngle { get; set; }
}