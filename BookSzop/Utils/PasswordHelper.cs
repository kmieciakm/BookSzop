using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Utils
{
    static class PasswordHelper
    {
        public static bool ValidatePassword(string password, out string errorMessagee)
        {
            const int MIN_LENGTH = 8;
            const int MAX_LENGTH = 15;

            if (password == null)
            {
                errorMessagee = "Password cannot be empty";
                return false;
            }

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }

                if (!hasUpperCaseLetter || !hasLowerCaseLetter || !hasDecimalDigit)
                {
                    errorMessagee = "Password must contain lowercase, uppercase character, and a digit";
                    return false;
                }
            }
            else
            {
                errorMessagee = $"Password must be {MIN_LENGTH} to {MAX_LENGTH} characters long";
                return false;
            }

            errorMessagee = "";
            return true;

        }
    }
}
