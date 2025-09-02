using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDevice.ViewModels;

internal partial class TextControlManager: ObservableObject {
    [ObservableProperty] private string? _titleText = "Text Options";
    [ObservableProperty] private string? _titleSubText = "Titles";
    [ObservableProperty] private string? _labelTitle = "Labels";
    [ObservableProperty] private string? _bodyTitle = "Body";
    [ObservableProperty] private string? _selectFontText = "Select Font";
    [ObservableProperty] private string? _fontSizeText = "Font Size";
    [ObservableProperty] private string? _fontColourText = "Font Colour";
    [ObservableProperty] private string? _buttonText = "Apply";

    [ObservableProperty] private int _titleFontSize = 24;
    [ObservableProperty] private int _bodyFontSize = 12;
    [ObservableProperty] private int _labelFontSize = 14;
}