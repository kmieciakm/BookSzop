using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models
{
    public class UserCreate : IUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserCreate create &&
                   FirstName == create.FirstName &&
                   LastName == create.LastName &&
                   Login == create.Login &&
                   Password == create.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, Login, Password);
        }

        public bool ConfirmationPasswordCorrect() => Password == ConfirmPassword;
    }
}
