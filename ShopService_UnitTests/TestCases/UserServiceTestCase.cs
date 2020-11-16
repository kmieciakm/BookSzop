using ShopService.UserServ;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShopService_UnitTests.TestCases
{
    public class UserServiceTestCase
    {
        public IUserService _UserService { get; }

        public UserServiceTestCase(IUserService userService)
        {
            _UserService = userService;
        }

        [Fact]
        public void UserProperBooksAfterRefund()
        {
            Assert.Single(_UserService.GetBooksOfUser(2));
        }
    }
}