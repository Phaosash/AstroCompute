using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace ClientDevice.DataModels;

public partial class ColourSettings : ObservableObject {
    public Color BackgroundColour { get; set; }
    public Color PanelBackgroundColour { get; set; }
    public Color TitleTextColour { get; set; }
    public Color SubtitleTextColour { get; set; }
    public Color BodyTextColour { get; set; }
    public Color TitleBackgroundColour { get; set; }
    public Color ButtonBackgroundColour { get; set; }
}