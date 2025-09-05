using System.Windows.Media;

namespace ClientDevice.DataModels;

public class FontSettings {
    public FontFamily TitleFont { get; set; }
    public FontFamily SubtitleFont { get; set; }
    public FontFamily BodyFont { get; set; }

    public int TitleFontSize { get; set; }
    public int SubtitleFontSize { get; set; }
    public int BodyFontSize { get; set; }

    public FontSettings (){
        TitleFont = new FontFamily("Segoe UI");
        SubtitleFont = new FontFamily("Segoe UI");
        BodyFont = new FontFamily("Segoe UI");

        TitleFontSize = 24;
        SubtitleFontSize = 16;
        BodyFontSize = 12;
    }
}