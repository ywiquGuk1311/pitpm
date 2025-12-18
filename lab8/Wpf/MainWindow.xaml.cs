using System.Windows;
using TestWPF.Model;
using TestWPF.View;
using TestWPF.ViewModel;

namespace TestWPF
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel = new();
        private AuthorizationPage _authorizationPage;
        private WelcomePage _welcomePage;

        public MainWindow()
        {
            InitializeComponent();

            _authorizationPage = new AuthorizationPage(_viewModel);
            _authorizationPage.SignInEvent += AuthorizationPage_SignInEvent;

            MainFrame.Navigate(_authorizationPage);
        }

        private void AuthorizationPage_SignInEvent()
        {
            User? user = _viewModel.Users.Find(u => u.Login == _viewModel.EnteredLogin);

            if (user is null)
            {
                MessageBox.Show("Пользователь с таким логином не зарегистрирован.");
                return;
            }

            if (user.Password == _viewModel.EnteredPassword)
            {
                _viewModel.CurrentUserRole = user.Role;
                MainFrame.Navigate(new WelcomePage(_viewModel));
            }
            else
                MessageBox.Show("Пароль неверный");
        }
    }
}