namespace WebServer.Services;

public interface IAstroContract {
    Task<double> CalculateEventHorizonAsync (double a);
    Task<double> CalculateKelvinAsync (double a);
    Task<double> CalculateStarDistance (double a);
    Task<double> CalculateStarVelocity (double a, double B);
}