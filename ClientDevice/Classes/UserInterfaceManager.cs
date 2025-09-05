using ClientDevice.DataModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErrorLogging;
using System.Windows;

namespace ClientDevice.Classes;

public partial class UserInterfaceManager : ObservableObject {
    private readonly IAstroContract _apiInterface;
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
    [ObservableProperty] private DataValues? _inputedValue;
    [ObservableProperty] private OutputValues? _calculationResults;

    public UserInterfaceManager (IAstroContract contract){
        _apiInterface = contract;
        CalculationResults = new OutputValues();
        InputedValue = new DataValues();
        FontSettings = new FontSettings();
        currentLanguage = AvailableLanguages.English;
        currentTheme = DefaultThemes.Night;
        isNightTheme = true;

        UpdateThemeColours();
    }

    [RelayCommand]
    private void SwitchThemes (){
        isNightTheme = !isNightTheme;
        currentTheme = isNightTheme ? DefaultThemes.Night : DefaultThemes.Day;

        ApplyTheme();
        UpdateThemeColours();
        ChangeLanguage();        
    }

    //  Tried moving this into the ColourSetting.cs, for some reason in broke the theme switching, so
    //  moved it back to fix the issue
    private void UpdateThemeColours (){
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

    [RelayCommand]
    private async Task CalculateVelocity (){
        try {
            if (CalculationResults?.Velocities == null){
                LoggingManager.Instance.LogWarning("CalculationResults.Velocities is null.");
                return;
            }

            if (InputedValue == null){
                LoggingManager.Instance.LogWarning("InputedValue is null.");
                return;
            }
            double velocity = await _apiInterface.CalculateStarVelocityAsync(InputedValue.ObservedWL, InputedValue.RestWL);

            if (velocity > 0){
                CalculationResults.Velocities.Add(new Measurement {
                    Timestamp = DateTime.Now,
                    Value = velocity
                });
            }
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the Velocity!");
        }
    }

    [RelayCommand]
    private async Task CalculateEventHorizon (){
        try {
            if (CalculationResults?.Horizons == null){
                LoggingManager.Instance.LogWarning("CalculationResults.Velocities is null.");
                return;
            }

            if (InputedValue == null){
                LoggingManager.Instance.LogWarning("InputedValue is null.");
                return;
            }

            double mass = await _apiInterface.CalculateEventHorizonAsync(InputedValue.Mass);

            if (mass > 0){
                CalculationResults.Horizons.Add(new Measurement {
                    Timestamp = DateTime.Now,
                    Value = mass
                });
            }
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the Event Horizon!");
        }
    }

    [RelayCommand]
    private async Task CalculateDistance (){
        try {
            if (CalculationResults?.Distances == null){
                MessageBox.Show("Encountered an unexpected problem while trying to calculate the star distance.");
                LoggingManager.Instance.LogWarning("CalculationResults.Velocities is null.");
                return;
            }

            if (InputedValue == null){
                MessageBox.Show("Inputed Value cannot be null when calculating the mass of the event horizon.");
                LoggingManager.Instance.LogWarning("InputedValue is null.");
                return;
            }

            double distance = await _apiInterface.CalculateStarDistanceParsecsAsync(InputedValue.ParalaxAngle);

            if (distance > 0){
                CalculationResults.Distances.Add(new Measurement {
                    Timestamp = DateTime.Now,
                    Value = distance
                });
            }
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the star distance!");
        }
    }

    [RelayCommand]
    private async Task ConvertTemperature (){
        try {
            if (CalculationResults?.Temperatures == null){
                LoggingManager.Instance.LogWarning("CalculationResults.Temperatures is null.");
                return;
            }

            if (InputedValue == null){
                LoggingManager.Instance.LogWarning("InputedValue is null.");
                return;
            }

            double result = await _apiInterface.ConvertCelsiusToKelvinAsync(InputedValue.Temperature);

            if (result > 0){                
                CalculationResults.Temperatures.Add(new Measurement {
                    Timestamp = DateTime.Now,
                    Value = result
                });
            }
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to convert the temperature!");
        }
    }
}