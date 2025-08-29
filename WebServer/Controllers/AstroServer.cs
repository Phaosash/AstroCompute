using AstroMath;
using WebServer.Services;

namespace WebServer.Controllers;

public class AstroServer : IAstroContract {

    public double EventHorizon (double a){
        return AstroMathFormulas.CalculateEventHorizon(a);
    }

    public double Kelvin (double a){
        return AstroMathFormulas.ConvertCelsiusToKelvin(a);
    }

    public double StarDistance (double a){
        return AstroMathFormulas.CalculateStarDistance(a);
    }

    public double StarVelocity (double a, double B){
        return AstroMathFormulas.CalculateStarVelocity(a, B);
    }
}
