using DatabaseManager.Repository.Contracts;
using System;
using Xunit;

namespace DatabaseManager_UnitTests
{
    public class UserRepositoryTestCase
    {
        private IUserRepository _UserRepository { get; }

        public UserRepositoryTestCase(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        [Fact]
        public void Test_Passed()
        {
            Assert.NotNull(_UserRepository);
        }
    }
}