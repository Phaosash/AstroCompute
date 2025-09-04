using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;

namespace ClientDevice.Classes;

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

    private readonly Dictionary<DefaultThemes, string> ThemeMappings = new(){
        { DefaultThemes.Day, "Resources/DayTheme.xaml" },
        { DefaultThemes.Night, "Resources/NightTheme.xaml" }
    };

    private readonly Dictionary<AvailableLanguages, string> LanguageMappings = new(){
        { AvailableLanguages.English, "Resources/EnglishDictionary.xaml" },
        { AvailableLanguages.French, "Resources/FrenchDictionary.xaml" },
        { AvailableLanguages.German, "Resources/GermanDictionary.xaml" }
    };

    [ObservableProperty] private FontSettings? _fontSettings;
    [ObservableProperty] private ColourSettings? _colourSettings;

    public UserInterfaceManager (){
        InitialiseSettings();
        InitialiseFontSettings();
        InitialiseColorSettings();
    }

    private void InitialiseFontSettings (){
        FontSettings = new FontSettings {
            TitleFont = new FontFamily("Segoe UI"),
            SubtitleFont = new FontFamily("Segoe UI"),
            BodyFont = new FontFamily("Segoe UI"),
            TitleFontSize = 24,
            SubtitleFontSize = 16,
            BodyFontSize = 12
        };
    }

    private void InitialiseSettings (){
        currentLanguage = AvailableLanguages.English;
        currentTheme = DefaultThemes.Night;
        isNightTheme = true;
    }

    private void InitialiseColorSettings (){
        ColourSettings = new ColourSettings {
            BackgroundColour = UISettingsService.GetColorFromResources("WindowBackground"),
            PanelBackgroundColour = UISettingsService.GetColorFromResources("PanelBackground"),
            TitleTextColour = UISettingsService.GetColorFromResources("TextForeground"),
            SubtitleTextColour = UISettingsService.GetColorFromResources("TextForeground"),
            BodyTextColour = UISettingsService.GetColorFromResources("TextForeground"),
            TitleBackgroundColour = UISettingsService.GetColorFromResources("WindowBackground"),
            ButtonBackgroundColour = UISettingsService.GetColorFromResources("ButtonBackground")
        };
    }

    [RelayCommand]
    private void SwitchThemes (){
        isNightTheme = !isNightTheme;
        currentTheme = isNightTheme ? DefaultThemes.Night : DefaultThemes.Day;

        ApplyTheme();
        InitialiseColorSettings();
        ChangeLanguage();
    }

    private void ApplyTheme (){
        var themeUri = new Uri(ThemeMappings[currentTheme], UriKind.Relative);
        UISettingsService.UpdateResourceDictionary(themeUri);
    }

    private void ChangeLanguage (){
        var languageUri = new Uri(LanguageMappings[currentLanguage], UriKind.Relative);
        UISettingsService.UpdateResourceDictionary(languageUri);
    }

    private void SwitchLanguage (AvailableLanguages language){
        currentLanguage = language;
        ChangeLanguage();
    }

    [RelayCommand]
    private void SwitchToFrench() => SwitchLanguage(AvailableLanguages.French);

    [RelayCommand]
    private void SwitchToGerman() => SwitchLanguage(AvailableLanguages.German);

    [RelayCommand]
    private void SwitchToEnglish() => SwitchLanguage(AvailableLanguages.English);
}