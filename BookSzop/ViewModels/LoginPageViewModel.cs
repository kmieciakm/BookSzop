using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.Models.PagesModels;
using BookSzop.Utils;
using BookSzop.ViewModels.Base;
using ShopService.Authentication;
using ShopService.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IAuthenticationManager _AuthenticationManager { get; }
        private INavigationHelper _Navigation { get; }

        public LoginPageViewModel(IAuthenticationManager authenticationManager, INavigationHelper navigation)
        {
            _AuthenticationManager = authenticationManager;
            _Navigation = navigation;
            _loginModel = new LoginModel();
            _userCreateModel = new UserCreate();
        }

        #region Login
        private LoginModel _loginModel;

        public string UserLogin {
            get => _loginModel.Login;
            set {
                _loginModel.Login = value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }
        public string UserPassword
        {
            get => _loginModel.Password;
            set
            {
                _loginModel.Password = value;
                OnPropertyChanged(nameof(UserPassword));
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
                    var isAdmin = _AuthenticationManager.CheckAdminAccess(userId.Value);
                    SessionHelper.SaveUserSession(userId.Value);
                    ClearFormsData();
                    if (isAdmin)
                    {
                        _Navigation.NavigateToAdminPage();
                    }
                    else
                    {
                        _Navigation.NavigateToUserPage();
                    }
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
                ClearErrors(nameof(Firstname));
                ValidateMinLength(nameof(Firstname), Firstname, 6);
                ValidateMaxLength(nameof(Firstname), Firstname, 40);
                OnPropertyChanged(nameof(Firstname));
            }
        }
        public string Lastname
        {
            get => _userCreateModel.LastName;
            set
            {
                _userCreateModel.LastName = value;
                ClearErrors(nameof(Lastname));
                ValidateMinLength(nameof(Lastname), Lastname, 6);
                ValidateMaxLength(nameof(Lastname), Lastname, 40);
                OnPropertyChanged(nameof(Lastname));
            }
        }
        public string Login
        {
            get => _userCreateModel.Login;
            set
            {
                _userCreateModel.Login = value;
                ClearErrors(nameof(Login));
                ValidateMinLength(nameof(Login), Login, 6);
                ValidateMaxLength(nameof(Login), Login, 24);
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => _userCreateModel.Password;
            set
            {
                _userCreateModel.Password = value;
                ClearErrors(nameof(Password));
                ValidatePasswordPolicy(nameof(Password), Password);
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmationPassword
        {
            get => _userCreateModel.ConfirmPassword;
            set
            {
                _userCreateModel.ConfirmPassword = value;
                ClearErrors(nameof(ConfirmationPassword));
                ValidatePasswordConfirmation(nameof(ConfirmationPassword), ConfirmationPassword, Password);
                OnPropertyChanged(nameof(ConfirmationPassword));
            }
        }

        public ICommand RegisterCommand
        {
            get => new RelayCommand(param => {
                if (IsFormCorrect())
                {
                    try
                    {
                        _AuthenticationManager.RegisterUser(_userCreateModel);
                        Message = "Registraction succeeded, please login now.";
                    }
                    catch (AuthenticationException authException)
                    {
                        Message = authException.Message;
                    }
                }
                else
                {
                    Message = "Registration form contains invalid values, please ensure all fields are filled correctly.";
                }
            });
        }

        private bool IsFormCorrect()
        {
            return
                !(
                    HasErrors ||
                    string.IsNullOrEmpty(Firstname) ||
                    string.IsNullOrEmpty(Lastname) ||
                    string.IsNullOrEmpty(Login) ||
                    string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(ConfirmationPassword)
                );
        }
        #endregion

        private void ClearFormsData()
        {
            UserLogin = "";
            UserPassword = "";
            Firstname = "";
            Lastname = "";
            Login = "";
            Password = "";
            ConfirmationPassword = "";
            Message = "";
        }
    }
}
