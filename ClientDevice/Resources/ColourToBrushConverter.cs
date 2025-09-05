using ErrorLogging;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClientDevice.Resources;

public class ColourToBrushConverter: IValueConverter {
    //  This method is used to conviert a Colour into a Solid Colour Brush, so that it can be
    //  displayed correctly on the UI
    public object Convert (object value, Type targetType, object parameter, CultureInfo culture){
        try{
            if (value is Color color){
                return new SolidColorBrush(color);
            }

            return Brushes.Transparent;
        } catch (Exception ex){ 
            LoggingManager.Instance.LogError(ex, "Unable to convert Colour to Solid Colour Brush");
            return Brushes.Transparent; 
        }
    }

    //  This method isnt needed, as there is no need to convert back once converted, but is needed for script to compile
    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture){
        throw new NotImplementedException(); 
    }
}