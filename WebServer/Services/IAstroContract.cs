namespace WebServer.Services;

public interface IAstroContract {
    double EventHorizon (double a);
    double Kelvin (double a);
    double StarDistance (double a);
    double StarVelocity (double a, double B);
}