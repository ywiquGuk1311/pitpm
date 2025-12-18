using System.Windows.Controls;
using TestWPF.ViewModel;

namespace TestWPF.View
{
    public partial class AuthorizationPage : Page
    {
        private MainViewModel _viewModel;
        public event Action SignInEvent;

        public AuthorizationPage(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
        }

        private void SignInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.EnteredLogin = LoginTextBox.Text;
            _viewModel.EnteredPassword = PasswordBox.Password;

            SignInEvent?.Invoke();
        }
    }
}
