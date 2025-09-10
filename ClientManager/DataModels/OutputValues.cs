using System.Collections.ObjectModel;

namespace ClientManager.DataModels;

public class OutputValues {
    public ObservableCollection<Measurement> Velocities { get; set; } = [];
    public ObservableCollection<Measurement> Distances { get; set; } = [];
    public ObservableCollection<Measurement> Temperatures { get; set; } = [];
    public ObservableCollection<Measurement> Horizons { get; set; } = [];
}