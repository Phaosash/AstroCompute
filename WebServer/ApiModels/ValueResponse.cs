namespace WebServer.ApiModels;

public class ValueResponse {
    public double Value { get; set; }
    public string? Formatted { get; set; }
    public string? Unit { get; set; }
}