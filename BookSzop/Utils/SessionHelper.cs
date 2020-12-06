using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Utils
{
    static class SessionHelper
    {
        private static int? _UserId { get; set; } = null;

        public static event EventHandler SessionChanged;
        
        private static void OnSessionChanged()
        {
            SessionChanged?.Invoke(typeof(SessionHelper), EventArgs.Empty);
        }

        public static void SaveUserSession(int userId)
        {
            _UserId = userId;
            OnSessionChanged();
        }

        public static int? GetSessionUserId()
        {
            return _UserId;
        }

        public static void ClearSession()
        {
            _UserId = null;
            OnSessionChanged();
        }
    }
}
