using System;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace InstaPhone.ViewModel
{
    public class AuthViewModel : ViewModelBase
    {
        public AuthViewModel()
        {
            //TODO: загружать из настроек
            _isLogin = false;
            Login = new RelayCommand(LoginCommandExecute, LoginCommandCanExecute);
            Logout = new RelayCommand(LogoutCommandExecute, LogoutCommandCanExecute);

            SourceUri = new Uri("https://instagram.com/oauth/authorize/?client_id=2220fd0b2b9542a6b6483af14302077d&redirect_uri=http://localhost&response_type=token");
        }

        public Uri SourceUri { get; set; }

        private bool _isLogin;
        public RelayCommand Login { get; set; }
        public RelayCommand Logout { get; set; }

        private bool LogoutCommandCanExecute()
        {
            return _isLogin;
        }

        private void LogoutCommandExecute()
        {
            throw new NotImplementedException();
        }

        private bool LoginCommandCanExecute()
        {
            return !_isLogin;
        }

        public RelayCommand<NavigationEventArgs> NavigationCommand { get; set; }

        private void LoginCommandExecute()
        {
           
        }
    }
}