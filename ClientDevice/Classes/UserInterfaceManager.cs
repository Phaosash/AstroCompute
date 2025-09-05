using ClientDevice.DataModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErrorLogging;
using System.Collections.ObjectModel;

namespace ClientDevice.Classes;

public partial class UserInterfaceManager : ObservableObject {
    private readonly IAstroContract _apiInterface;
    private enum Languages { English, French, German }
    private enum Themes { Day, Night }
    private enum CalculationTypes { Velocity, Temperature, Distance, Mass }
    private Languages _language = Languages.English;
    private Themes _theme = Themes.Night;
    private CalculationTypes _lastCalculation;

    private readonly Dictionary<Themes, string> _themePaths = new(){
        { Themes.Day, "Resources/DayTheme.xaml" },
        { Themes.Night, "Resources/NightTheme.xaml" }
    };

    private readonly Dictionary<Languages, string> _languagePaths = new(){
        { Languages.English, "Resources/EnglishDictionary.xaml" },
        { Languages.French, "Resources/FrenchDictionary.xaml" },
        { Languages.German, "Resources/GermanDictionary.xaml" }
    };

    [ObservableProperty] private FontSettings? _fontSettings = new();
    [ObservableProperty] private ColourSettings? _colourSettings = new();
    [ObservableProperty] private DataValues? _inputedValue = new();
    [ObservableProperty] private OutputValues? _calculationResults = new();
    [ObservableProperty] private bool _isNightTheme = true;

    public UserInterfaceManager (IAstroContract contract){
        _apiInterface = contract;
        UpdateTheme();
    }

    [RelayCommand]
    private void SwitchThemes (){       
        IsNightTheme = !IsNightTheme;
        _theme = IsNightTheme ? Themes.Night : Themes.Day;

        UpdateColours();   
    }

    private void UpdateTheme (){
        var uri = new Uri(_themePaths[_theme], UriKind.Relative);
        UISettingsService.UpdateResourceDictionary(uri);
        UpdateColours();
        ChangeLanguage(_language);
    }

    private void UpdateColours (){
        //  Tried moving this into the ColourSetting.cs, for some reason in broke the theme switching, so
        //  moved it back to fix the issue
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

    private void ChangeLanguage (Languages language){
        _language = language;
        var languageUri = new Uri(_languagePaths[_language], UriKind.Relative);
        UISettingsService.UpdateResourceDictionary(languageUri);
    }

    [RelayCommand] private void SwitchToFrench() => ChangeLanguage(Languages.French);
    [RelayCommand] private void SwitchToGerman() => ChangeLanguage(Languages.German);
    [RelayCommand] private void SwitchToEnglish() => ChangeLanguage(Languages.English);

    [RelayCommand] private async Task CalculateVelocity() => await AddResult(CalculationTypes.Velocity);
    [RelayCommand] private async Task CalculateDistance() => await AddResult(CalculationTypes.Distance);
    [RelayCommand] private async Task ConvertTemperature() => await AddResult(CalculationTypes.Temperature);
    [RelayCommand] private async Task CalculateEventHorizon() => await AddResult(CalculationTypes.Mass);

    private async Task<double> GetResultAsync (CalculationTypes types){
        if (InputedValue == null){
            LoggingManager.Instance.LogWarning("Input is null.");
            return -1;
        }

        _lastCalculation = types;

        try {
            return types switch {
                CalculationTypes.Distance => await _apiInterface.CalculateStarDistanceParsecsAsync(InputedValue.ParalaxAngle),
                CalculationTypes.Temperature => await _apiInterface.ConvertCelsiusToKelvinAsync(InputedValue.Temperature),
                CalculationTypes.Mass => await _apiInterface.CalculateEventHorizonAsync(InputedValue.Mass),
                CalculationTypes.Velocity => await _apiInterface.CalculateStarVelocityAsync(InputedValue.ObservedWL, InputedValue.RestWL),
                _ => -1
            };
        } catch (Exception ex) {
            LoggingManager.Instance.LogError(ex, "Error during calculation.");
            return -1;
        }
    }

    private async Task AddResult (CalculationTypes types){       
        var result = await GetResultAsync(types);
        
        if (result >= 0) {
            var list = GetResultList(types);
            list?.Add(new Measurement { Timestamp = DateTime.Now, Value = result });
        }
    }

    private ObservableCollection<Measurement>? GetResultList (CalculationTypes type){
        if (CalculationResults == null) return null;

        return type switch {
            CalculationTypes.Distance => CalculationResults.Distances,
            CalculationTypes.Temperature => CalculationResults.Temperatures,
            CalculationTypes.Mass => CalculationResults.Horizons,
            CalculationTypes.Velocity => CalculationResults.Velocities,
            _ => null
        };
    }
}