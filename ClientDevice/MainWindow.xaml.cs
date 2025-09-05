using ClientDevice.Classes;
using System.Windows;

namespace ClientDevice;

public partial class MainWindow : Window {
    private readonly UserInterfaceManager _viewModel;

    public MainWindow (UserInterfaceManager viewModel){
        InitializeComponent();

        _viewModel = viewModel;

        DataContext = _viewModel;
    }
}