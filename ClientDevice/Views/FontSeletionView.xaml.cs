using ClientDevice.ViewModels;
using System.Windows.Controls;

namespace ClientDevice.Views;

public partial class FontSeletionView : UserControl {
    private readonly FontSelectionViewModel _viewModel;
    
    public FontSeletionView (){
        InitializeComponent();
        _viewModel = new FontSelectionViewModel();

        DataContext = _viewModel;
    }
}