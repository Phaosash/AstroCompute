using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class StarDistanceRequest {
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Parallax must be > 0.")]
    public double ParallaxArcseconds { get; set; }
}