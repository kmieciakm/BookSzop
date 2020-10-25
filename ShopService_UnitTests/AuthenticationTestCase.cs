using ShopService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShopService_UnitTests
{
    public class AuthenticationTestCase
    {
        public IAuthenticationManager AuthenticationManager { get; }

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
    }
}
