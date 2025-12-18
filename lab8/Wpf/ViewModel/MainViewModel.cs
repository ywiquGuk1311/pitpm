using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestWPF.Model;

namespace TestWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<User> _users;
        public List<User> Users
        {
            get => new()
            {
                new User { Login = "admin", Password = "admin", Role = Role.Administator },
                new User { Login = "kloshi", Password = "qwerty", Role = Role.User },
                new User { Login = "circoniy", Password = "12345", Role = Role.User },
            };
        }

        private string? _enteredLogin;
        public string EnteredLogin
        {
            get => _enteredLogin ?? "";
            set
            {
                _enteredLogin = value;
                OnPropertyChanged();
            }
        }

        private string? _enteredPassword;
        public string EnteredPassword
        {
            get => _enteredPassword ?? "";
            set
            {
                _enteredPassword = value;
                OnPropertyChanged();
            }
        }

        private Role _currentUserRole;
        public Role CurrentUserRole
        {
            get => _currentUserRole;
            set
            {
                _currentUserRole = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
