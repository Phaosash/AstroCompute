using ErrorLogging;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace ClientManager.Classes;

internal class UISettingsService {
    //  This method retrieves a Color from the application's resources using the provided key.
    public static Color GetColorFromResources (string key){
        return (Color)Application.Current.Resources[key];
    }

    //  This method attempts to load a new resource dictionary from the provided URI, replacing
    //  the existing merged dictionaries, and logs an error if the operation fails, displaying a
    //  message box with the error details.
    public static void UpdateResourceDictionary (Uri resourceUri){
        try {            
            var dictionary = new ResourceDictionary { Source = resourceUri };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        } catch (IOException ex){
            LoggingManager.Instance.LogError(ex, "IO Exception Occured");
            MessageBox.Show($"IOException Occured: {resourceUri} - {ex.Message}");
        } catch (Exception ex) {
            MessageBox.Show($"Error loading resource: {resourceUri} - {ex.Message}");
            LoggingManager.Instance.LogError(ex, "Failed to update the resource dictionary");
        }
    }
}