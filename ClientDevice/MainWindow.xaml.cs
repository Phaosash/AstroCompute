using ClientDevice.Classes;
using ErrorLogging;
using System.Windows;

namespace ClientDevice;

public partial class MainWindow : Window {
    private readonly UserInterfaceManager? _viewModel;

    //  This constructor is used to initialise the Main Window, and sets its data context
    //  to the UserInterfaceManager. If it fails to initialise it logs the error.
    public MainWindow (UserInterfaceManager viewModel){
        try {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        } catch (Exception ex){ 
            LoggingManager.Instance.LogError(ex, "Failed to initialise the MainWindow");
        }
    }
}