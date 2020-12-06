using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using BookSzop.Views;
using ShopService.Authentication;
using ShopService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IAuthenticationManager _AuthenticationManager { get; }
        private UserPage _UserPage { get; }

        public LoginPageViewModel(IAuthenticationManager authenticationManager, UserPage userPage)
        {
            _AuthenticationManager = authenticationManager;
            _UserPage = userPage;
            _loginModel = new LoginModel();
            _userCreateModel = new UserCreate();
        }

        #region Login
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

        public ICommand LoginCommand
        {
            get => new RelayCommand(param => {
                var loginCorrect = _AuthenticationManager.CheckUserCredentials(_loginModel.Login, _loginModel.Password);
                var userId = _AuthenticationManager.GetUserIdByLogin(_loginModel.Login);
                if (loginCorrect && userId.HasValue)
                {
                    SessionHelper.SaveUserSession(userId.Value);
                    NavigationHelper.Navigate(_UserPage);
                }
                else
                {
                    Message = "Login credentials are incorrect.";
                }
            });
        }
        #endregion

        #region Register
        private IUserCreate _userCreateModel;

        public string Firstname
        {
            get => _userCreateModel.FirstName;
            set
            {
                _userCreateModel.FirstName = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }
        public string Lastname
        {
            get => _userCreateModel.LastName;
            set
            {
                _userCreateModel.LastName = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }
        public string LoginRegister
        {
            get => _userCreateModel.Login;
            set
            {
                _userCreateModel.Login = value;
                OnPropertyChanged(nameof(LoginRegister));
            }
        }
        public string PasswordRegister
        {
            get => _userCreateModel.Password;
            set
            {
                _userCreateModel.Password = value;
                OnPropertyChanged(nameof(PasswordRegister));
            }
        }
        public string ConfirmPasswordRegister
        {
            get => _userCreateModel.ConfirmPassword;
            set
            {
                _userCreateModel.ConfirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPasswordRegister));
            }
        }

        public ICommand RegisterCommand
        {
            get => new RelayCommand(param => {
                try
                {
                    _AuthenticationManager.RegisterUser(_userCreateModel);
                    Message = "Registraction succeeded, please login now.";
                }
                catch (AuthenticationException authException)
                {
                    Message = authException.Message;
                }
            });
        }
        #endregion
    }
}
