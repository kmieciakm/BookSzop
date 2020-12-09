using ShopService.Authentication;
using ShopService.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using Xunit;
using AuthenticationException = ShopService.Authentication.AuthenticationException;

namespace ShopService_UnitTests.TestCases
{
    public class AuthenticationTestCase
    {
        private IAuthenticationManager AuthenticationManager { get; }

        public AuthenticationTestCase(IAuthenticationManager authenticationManager)
        {
            AuthenticationManager = authenticationManager;
        }

        [Fact]
        public void AuthenticateUser_CorrectCredentials()
        {
            Assert.True(AuthenticationManager.CheckUserCredentials("admin", "admin123"));
            Assert.True(AuthenticationManager.CheckUserCredentials("user", "user123"));
        }

        [Fact]
        public void AuthenticateUser_WrongCredentials()
        {
            Assert.False(AuthenticationManager.CheckUserCredentials("hacker", "hacker123"));
        }

        [Fact]
        public void CheckAdminAccess_Admin()
        {
            Assert.True(AuthenticationManager.CheckAdminAccess(1));
        }

        [Fact]
        public void CheckAdminAccess_NotAdmin()
        {
            Assert.False(AuthenticationManager.CheckAdminAccess(2));
        }

        [Fact]
        public void RegisterUser_CorrectUser()
        {
            UserCreate newUser = new UserCreate()
            {
                FirstName = "NewUser",
                LastName = "The New",
                Login = "newTestUser",
                Password = "test",
                ConfirmPassword = "test"
            };

            AuthenticationManager.RegisterUser(newUser);
            Assert.True(AuthenticationManager.CheckUserCredentials(newUser.Login, newUser.Password));
        }

        [Fact]
        public void RegisterUser_WrongUser()
        {
            UserCreate newUser = new UserCreate()
            {
                FirstName = "User",
                LastName = "The User",
                Login = "user",
                Password = "123"
            };

            Assert.Throws<AuthenticationException>(() => AuthenticationManager.RegisterUser(newUser));
        }
    }
}
