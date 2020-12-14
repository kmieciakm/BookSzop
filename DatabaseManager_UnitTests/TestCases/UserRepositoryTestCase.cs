using DatabaseManager.Repository.Contracts;
using System;
using Xunit;

namespace DatabaseManager_UnitTests.TestCases
{
    public class UserRepositoryTestCase
    {
        private IUserRepository _UserRepository { get; }

        public UserRepositoryTestCase(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        [Fact]
        public void IsLoginFree_FreeLogin()
        {
            Assert.True(_UserRepository.IsLoginFree("such_login_not_exists"));
        }

        [Fact]
        public void IsLoginFree_OccupiedLogin()
        {
            Assert.False(_UserRepository.IsLoginFree("admin"));
        }

        [Fact]
        public void GetUserByLogin_LoginCorrect()
        {
            var user = _UserRepository.GetUserByLogin("user");
            Assert.Equal(2, user.Id);
        }

        [Fact]
        public void GetUserByLogin_InvalidLogin()
        {
            Assert.Null(_UserRepository.GetUserByLogin("such_login_not_exists"));
        }
    }
}
