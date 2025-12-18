using System.Windows.Controls;
using TestWPF.ViewModel;

namespace TestWPF.View
{
    public partial class WelcomePage : Page
    {
        private MainViewModel _viewModel;
        public WelcomePage(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            WelcomeTextBlock.Text = $"Добро пожаловать, {_viewModel.EnteredLogin}, Ваша роль - {_viewModel.CurrentUserRole}";
        }
    }
}
