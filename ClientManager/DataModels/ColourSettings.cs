using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace ClientManager.DataModels;

public partial class ColourSettings: ObservableObject {
    private Color _backgroundColour;
    private Color _panelBackgroundColour;
    private Color _titleTextColour;
    private Color _subtitleTextColour;
    private Color _bodyTextColour;
    private Color _titleBackgroundColour;
    private Color _buttonBackgroundColour;

    public Color BackgroundColour { 
        get => _backgroundColour;
        set {
            if (_backgroundColour != value){
                _backgroundColour = value;
                OnPropertyChanged();
            }
        }
    }

    public Color PanelBackgroundColour { 
        get => _panelBackgroundColour; 
        set {
            if (_panelBackgroundColour != value){
                _panelBackgroundColour = value;
                OnPropertyChanged();
            }
        }
    }

    public Color TitleTextColour { 
        get => _titleTextColour; 
        set {
            if (_titleTextColour != value){
                _titleTextColour = value;
                OnPropertyChanged();
            }
        }
    }

    public Color SubtitleTextColour { 
        get => _subtitleTextColour; 
        set {
            if (_subtitleTextColour != value){
                _subtitleTextColour = value;
                OnPropertyChanged();
            }
        }
    }

    public Color BodyTextColour { 
        get => _bodyTextColour; 
        set {
            if (value != _bodyTextColour){
                _bodyTextColour = value;
                OnPropertyChanged();
            }
        }
    }

    public Color TitleBackgroundColour { 
        get => _titleBackgroundColour; 
        set {
            if (_titleBackgroundColour!= value){
                _titleBackgroundColour = value;
                OnPropertyChanged();
            }
        }
    }

    public Color ButtonBackgroundColour { 
        get => _buttonBackgroundColour; 
        set {
            if (_buttonBackgroundColour!= value){
                _buttonBackgroundColour = value;
                OnPropertyChanged();
            }
        }
    }
}