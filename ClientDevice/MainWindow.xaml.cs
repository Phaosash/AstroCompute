using ClientDevice.Classes;
using System.Windows;

namespace ClientDevice;

public partial class MainWindow : Window {
    private readonly UserInterfaceManager viewModel;

    public MainWindow (){
        InitializeComponent();

        viewModel = new UserInterfaceManager();

        SetDataContext();
    }

    //  This method sets the data context for the Main Window, to the User Interface Manager,
    //  essentially this tells the Main Window to obtain its data from the associated View Model.
    private void SetDataContext (){
        this.DataContext = viewModel;
    }
}