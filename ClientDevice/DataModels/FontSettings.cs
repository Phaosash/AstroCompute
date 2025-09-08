using System.Windows.Media;

namespace ClientDevice.DataModels;

public class FontSettings {
    public FontFamily TitleFont { get; set; } = new FontFamily("Segoe UI");
    public FontFamily SubtitleFont { get; set; } = new FontFamily("Segoe UI");
    public FontFamily BodyFont { get; set; } = new FontFamily("Segoe UI");

    public int TitleFontSize { get; set; } = 24;
    public int SubtitleFontSize { get; set; } = 16;
    public int BodyFontSize { get; set; } = 12;
}