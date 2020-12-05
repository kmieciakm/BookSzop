using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models
{
    public interface IUserCreate
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }

        bool ConfirmationPasswordCorrect();
    }
}
