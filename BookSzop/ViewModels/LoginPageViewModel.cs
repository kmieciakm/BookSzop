using BookSzop.ViewModels.Base;
using ShopService.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public IAuthenticationManager _AuthenticationManager { get; }

        public LoginPageViewModel(IAuthenticationManager authenticationManager)
        {
            _AuthenticationManager = authenticationManager;
        }
    }
}
