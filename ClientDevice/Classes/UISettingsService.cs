using System.Windows;
using System.Windows.Media;

namespace ClientDevice.Classes;

internal class UISettingsService {
    public static Color GetColorFromResources (string key){
        return (Color)Application.Current.Resources[key];
    }

    public static void UpdateResourceDictionary (Uri resourceUri){
        try {
            var dictionary = new ResourceDictionary { Source = resourceUri };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        } catch (Exception ex) {
            MessageBox.Show($"Error loading resource: {resourceUri} - {ex.Message}");
        }
    }
}