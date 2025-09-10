using ClientManager.DataModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErrorLogging;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClientManager.Classes;

public partial class UserInterfaceManager: ObservableObject {
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

    //  This method toggles between night and day themes, updating the theme accordingly,
    //  and logs an error if the theme switch operation fails.
    [RelayCommand]
    private void SwitchThemes (){       
        try {
            IsNightTheme = !IsNightTheme;
            _theme = IsNightTheme ? Themes.Night : Themes.Day;

            UpdateTheme();
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to Switch Themes!");
        }
    }

    //  This method updates the theme by loading the corresponding resource dictionary, refreshing the UI colors,
    //  and changing the language, while handling errors by logging them.
    private void UpdateTheme (){
        try {
            var uri = new Uri($"pack://application:,,,/ClientManager;component/{_themePaths[_theme]}", UriKind.Absolute);
            UISettingsService.UpdateResourceDictionary(uri);
            UpdateColours();
            ChangeLanguage(_language);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to Update the theme");
        }
    }

    //  This method retrieves color values from the application's resources and updates the
    //  ColourSettings object, handling any errors by logging them.
    private void UpdateColours (){
        try {
            ColourSettings = new ColourSettings {
                BackgroundColour = UISettingsService.GetColorFromResources("WindowBackground"),
                PanelBackgroundColour = UISettingsService.GetColorFromResources("PanelBackground"),
                TitleTextColour = UISettingsService.GetColorFromResources("TextForeground"),
                SubtitleTextColour = UISettingsService.GetColorFromResources("TextForeground"),
                BodyTextColour = UISettingsService.GetColorFromResources("TextForeground"),
                TitleBackgroundColour = UISettingsService.GetColorFromResources("WindowBackground"),
                ButtonBackgroundColour = UISettingsService.GetColorFromResources("ButtonBackground")
            };
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to Update Colours");
        }
    }

    //  This method updates the application's language by loading the appropriate resource dictionary
    //  based on the selected language, and logs an error if the language change fails.
    private void ChangeLanguage (Languages language){
        try {
            _language = language;
            var languageUri = new Uri($"pack://application:,,,/ClientManager;component/{_languagePaths[_language]}", UriKind.Absolute);
            UISettingsService.UpdateResourceDictionary(languageUri);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to change to to either English, French or German");
        }
    }

    //  This method is a command that switches the application's language to French by calling the
    //  ChangeLanguage method with the Languages.French parameter.
    [RelayCommand] private void SwitchToFrench() => ChangeLanguage(Languages.French);

    //  This method is a command that switches the application's language to German by calling the
    //  ChangeLanguage method with the Languages.German parameter.
    [RelayCommand] private void SwitchToGerman() => ChangeLanguage(Languages.German);

    //  This method is a command that switches the application's language to English by calling the
    //  ChangeLanguage method with the Languages.English parameter.
    [RelayCommand] private void SwitchToEnglish() => ChangeLanguage(Languages.English);

    //  This method is a command that asynchronously calculates the velocity by calling the
    //  AddResult method with the CalculationTypes.Velocity parameter.
    [RelayCommand] private async Task CalculateVelocity() => await AddResult(CalculationTypes.Velocity);

    //  This method is a command that asynchronously calculates the distance by calling the
    //  AddResult method with the CalculationTypes.Distance parameter.
    [RelayCommand] private async Task CalculateDistance() => await AddResult(CalculationTypes.Distance);

    //  This method is a command that asynchronously converts the temperature by calling the
    //  AddResult method with the CalculationTypes.Temperature parameter.
    [RelayCommand] private async Task ConvertTemperature() => await AddResult(CalculationTypes.Temperature);

    //  This method is a command that asynchronously calculates the event horizon by calling the
    //  AddResult method with the CalculationTypes.Mass parameter.
    [RelayCommand] private async Task CalculateEventHorizon() => await AddResult(CalculationTypes.Mass);

    //  This method attempts to retrieve a calculation result based on the provided CalculationTypes,
    //  ensuring that input is valid and handling errors with appropriate logging and user notifications.
    //  It calculates the result by calling corresponding API methods and handles negative results (except
    //  for velocity) by displaying warnings.
    private async Task<double> GetResultAsync (CalculationTypes types){
        try {
            if (InputedValue == null){
                LoggingManager.Instance.LogWarning("Input is null.");
                MessageBox.Show((string)Application.Current.Resources["InputWarning"], (string)Application.Current.Resources["ErrorMessage"], MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

            _lastCalculation = types;

            try {
                double result = types switch {
                    CalculationTypes.Distance => await _apiInterface.CalculateStarDistanceParsecsAsync(InputedValue.ParalaxAngle),
                    CalculationTypes.Temperature => await _apiInterface.ConvertCelsiusToKelvinAsync(InputedValue.Temperature),
                    CalculationTypes.Mass => await _apiInterface.CalculateEventHorizonAsync(InputedValue.Mass),
                    CalculationTypes.Velocity => await _apiInterface.CalculateStarVelocityAsync(InputedValue.ObservedWL, InputedValue.RestWL),
                    _ => -1
                };

                if (result < 0 && types != CalculationTypes.Velocity){
                    LoggingManager.Instance.LogWarning($"Negative result for {types} calculation.");
                    MessageBox.Show("Calculation result cannot be negative.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return -1;
                }

                return result;
            } catch (Exception ex) {
                LoggingManager.Instance.LogError(ex, "Error during calculation.");
                MessageBox.Show((string)Application.Current.Resources["CalculationFailure"], (string)Application.Current.Resources["ErrorMessage"], MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to get a result");
            return -1;
        }
    }

    //  This method calculates a result using GetResultAsync, displays a success message with the result
    //  if the calculation is successful, logs the result, and adds it to a list. If the calculation fails,
    //  it shows an error message and logs a warning.
    private async Task AddResult (CalculationTypes types){       
        try {
            var result = await GetResultAsync(types);
        
            if (result >= 0) {
                MessageBox.Show((string)Application.Current.Resources["CalculationSuccessful"] + " " + result, (string)Application.Current.Resources["InformationMessage"], MessageBoxButton.OK, MessageBoxImage.None);
                LoggingManager.Instance.LogInformation($"The Calculation was successfully completed, resulting value {result}");
                var list = GetResultList(types);
                list?.Add(new Measurement { Timestamp = DateTime.Now, Value = result });
            } else {
                MessageBox.Show((string)Application.Current.Resources["CalculationFailure"], (string)Application.Current.Resources["ErrorMessage"], MessageBoxButton.OK, MessageBoxImage.Error);
                LoggingManager.Instance.LogWarning("The Calculation was unccessful!");
            }
        } catch(Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to AddResult");
        }
    }

    //  This method retrieves the appropriate ObservableCollection<Measurement> based on the specified
    //  CalculationTypes, handling null values for CalculationResults and logging any errors that occur
    //  during the process.
    private ObservableCollection<Measurement>? GetResultList (CalculationTypes type){
        try {
            if (CalculationResults == null){
                MessageBox.Show((string)Application.Current.Resources["NullCalculation"], (string)Application.Current.Resources["ErrorMessage"], MessageBoxButton.OK, MessageBoxImage.Error);
                LoggingManager.Instance.LogWarning("The Calculation Results is null!");
                return null;
            }

            return type switch {
                CalculationTypes.Distance => CalculationResults.Distances,
                CalculationTypes.Temperature => CalculationResults.Temperatures,
                CalculationTypes.Mass => CalculationResults.Horizons,
                CalculationTypes.Velocity => CalculationResults.Velocities,
                _ => null
            };
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to get the result list!");
            return null;
        }
    }
}