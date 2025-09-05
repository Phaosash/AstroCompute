namespace ClientDevice.DataModels;

public class DataValues {
    public double ObservedWL { get; set; }
    public double RestWL { get; set; }
    public double ParalaxAngle { get; set; }
    public double Temperature { get; set; }
    public double Mass { get; set; }

    public DataValues (){
        ObservedWL = 0;
        RestWL = 0;
        ParalaxAngle = 0;
        Temperature = 0;
        Mass = 0;
    }
}