using System.Linq;
using System.Windows;
using PhoneLinesApp.UI.ViewModels;

namespace PhoneLinesApp.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                if (DataContext is MainViewModel vm)
                    vm.LoadCommand.Execute(null);
            };
        }
    }
}
