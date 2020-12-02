using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.ViewModels.Base;
using ShopService.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public IAuthenticationManager _AuthenticationManager { get; }

        public LoginPageViewModel(IAuthenticationManager authenticationManager)
        {
            _AuthenticationManager = authenticationManager;
            _loginModel = new LoginModel();
        }

        public ICommand LoginCommand
        {
            get => new RelayCommand(param => {
                var loginCorrect = _AuthenticationManager.CheckUserCredentials(_loginModel.Login, _loginModel.Password);
                if (loginCorrect)
                {
                    // TODO: redirect to UserView
                    Message = "Login credentials correct.";
                }
                else
                {
                    Message = "Login credentials are incorrect.";
                }
            });
        }

        private LoginModel _loginModel;
        public string Login {
            get => _loginModel.Login;
            set {
                _loginModel.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => _loginModel.Password;
            set
            {
                _loginModel.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Message
        {
            get => _loginModel.Message;
            set
            {
                _loginModel.Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
    }
}
