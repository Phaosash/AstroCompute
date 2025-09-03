using System.Windows.Media;

namespace ClientDevice.Classes;

internal class FontSettings {
    public required FontFamily TitleFont { get; set; }
    public required FontFamily SubtitleFont { get; set; }
    public required FontFamily BodyFont { get; set; }

    public int TitleFontSize { get; set; }
    public int SubtitleFontSize { get; set; }
    public int BodyFontSize { get; set; }
}