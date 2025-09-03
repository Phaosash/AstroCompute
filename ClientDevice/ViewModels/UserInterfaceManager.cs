using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Media;

namespace ClientDevice.ViewModels;

internal partial class UserInterfaceManager : ObservableObject {
    private enum AvailableLanguages {
        English,
        French,
        German
    }

    private enum DefaultThemes {
        Day,
        Night
    }

    private AvailableLanguages currentLanguage;
    private DefaultThemes currentTheme;
    private bool isNightTheme;

    [ObservableProperty] private int _titleFontSize;
    [ObservableProperty] private int _subtitleFontSize;
    [ObservableProperty] private int _bodyTextSize;

    [ObservableProperty] private Color _backgroundColour;
    [ObservableProperty] private Color _panelBackgroundColour;
    [ObservableProperty] private Color _labelBackgroundColour;
    [ObservableProperty] private Color _buttonBackgroundColour;
    [ObservableProperty] private Color _dataGridBackgroundColour;
    [ObservableProperty] private Color _bodyTextColour;
    [ObservableProperty] private Color _subtitleTextColour;
    [ObservableProperty] private Color _titleTextColour;

    [ObservableProperty] private FontFamily? _titleFonts;
    [ObservableProperty] private FontFamily? _subtitleFont;
    [ObservableProperty] private FontFamily? _bodyFont;

    public UserInterfaceManager (){
        InitialiseSettings();
        InitialiseFontSizes();
        InitialiseColours();
        InitialiseFontType();
    }

    private void InitialiseFontType (){
        TitleFonts = new FontFamily("Segoe UI");
        SubtitleFont = new FontFamily("Segoe UI");
        BodyFont = new FontFamily("Segoe UI");
    }

    private void InitialiseSettings (){
        currentLanguage = AvailableLanguages.English;
        currentTheme = DefaultThemes.Night;
        isNightTheme = true;
    }

    private void InitialiseColours (){
        var resources = Application.Current.Resources;

        BackgroundColour = (Color)resources["WindowBackground"];
        PanelBackgroundColour = (Color)resources["PanelBackground"];
        LabelBackgroundColour = (Color)resources["WindowBackground"];
        ButtonBackgroundColour = (Color)resources["ButtonBackground"];
        DataGridBackgroundColour = (Color)resources["PanelBackground"];
        BodyTextColour = (Color)resources["TextForeground"];
        SubtitleTextColour = (Color)resources["TextForeground"];
        TitleTextColour = (Color)resources["TextForeground"];
    }

    private void InitialiseFontSizes (){
        TitleFontSize = 24;
        SubtitleFontSize = 14;
        BodyTextSize = 12;
    }

    [RelayCommand]
    private void SwitchToFrench (){
        currentLanguage = AvailableLanguages.French;
        ChangeLanguage();
    }

    [RelayCommand]
    private void SwitchToGerman (){
        currentLanguage = AvailableLanguages.German;
        ChangeLanguage();
    }

    [RelayCommand]
    private void SwitchToEnglish (){
        currentLanguage = AvailableLanguages.English;
        ChangeLanguage();
    }

    [RelayCommand]
    private void SwitchThemes (){
        if (isNightTheme){
            currentTheme = DefaultThemes.Day;
            isNightTheme= false;
        } else {
            currentTheme = DefaultThemes.Night;
            isNightTheme = true;
        }

        ApplyTheme();
        InitialiseColours();
    }

    private void ApplyTheme (){
        Uri themeUri = currentTheme switch {
            DefaultThemes.Day => new Uri("Resources/DayTheme.xaml", UriKind.Absolute),
            DefaultThemes.Night => new Uri("Resources/NightTheme.xaml", UriKind.Absolute),
            _ => new Uri("Resources/DayTheme.xaml", UriKind.Absolute),
        };

        var themeDictionary = new ResourceDictionary { Source = themeUri };

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
    }

    private void ChangeLanguage (){
        Uri resourceUri = currentLanguage switch {
            AvailableLanguages.English => new Uri("Resources/EnglishDictionary.xaml", UriKind.Relative),
            AvailableLanguages.French => new Uri("Resources/FrenchDictionary.xaml", UriKind.Relative),
            AvailableLanguages.German => new Uri("Resources/GermanDictionary.xaml", UriKind.Relative),
            _ => new Uri("Resources/EnglishDictionary.xaml", UriKind.Relative),
        };

        var dictionary = new ResourceDictionary { Source = resourceUri };

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(dictionary);
    }
}