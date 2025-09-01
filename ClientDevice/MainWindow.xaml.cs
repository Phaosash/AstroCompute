using ClientDevice.ViewModels;
using System.Windows;

namespace ClientDevice;

public partial class MainWindow : Window {
    private MainWindowViewModel viewModel;

    public MainWindow (){
        InitializeComponent();

        viewModel = new MainWindowViewModel();

        SetDataContext();
    }

    //  This method sets the data context for the Main Window, to the Main Window View Model,
    //  essentially this tells the Main Window to obtain its data from the associated View Model.
    private void SetDataContext (){
        this.DataContext = viewModel;
    }
}