using ClientDevice.ViewModels;
using System.Windows.Controls;

namespace ClientDevice.Views;

public partial class TextController : UserControl {
    private readonly TextControlManager textControlManager;

    public TextController (){
        InitializeComponent();

        textControlManager = new TextControlManager();

        DataContext = textControlManager;
    }

    //private void UpdateText (ApplicationLanguages languages){
    //    ControlTitle.Text = languages switch {
    //        ApplicationLanguages.English => "Text Options",
    //        ApplicationLanguages.German => "Textoptionen",
    //        ApplicationLanguages.French => "Options de texte",
    //        _ => "Text Options",
    //    };
    //}
}