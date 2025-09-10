using System.ComponentModel.DataAnnotations;

namespace WebServer.ApiModels;

public class EventHorizonRequest {
    [Required]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Mass must be > 0.")]
    public double Mass { get; set; }
}