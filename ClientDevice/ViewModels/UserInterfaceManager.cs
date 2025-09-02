using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace ClientDevice.ViewModels;

internal partial class UserInterfaceManager : ObservableObject {
    [ObservableProperty] private int _titleFontSize;
    [ObservableProperty] private Color _titleFontColour;
    [ObservableProperty] private FontFamily? _titleFontFamily;

    [ObservableProperty] private int _labelFontSize;
    [ObservableProperty] private Color _labelFontColour;
    [ObservableProperty] private FontFamily? _labelFontFamily;

    [ObservableProperty] private int _bodyFontSize;
    [ObservableProperty] private Color _bodyFontColour;
    [ObservableProperty] private FontFamily? _bodyFontFamily;

    [ObservableProperty] private bool isNightMode = true;

    public UserInterfaceManager (){
        InitialiseFonts();
    }

    private void InitialiseFonts (){
        TitleFontSize = 24;
        TitleFontFamily = new FontFamily("Segoe UI");

        LabelFontSize = 14;
        LabelFontFamily = new FontFamily("Segoe UI");

        BodyFontSize = 12;
        BodyFontFamily = new FontFamily("Segoe UI");
    }
}