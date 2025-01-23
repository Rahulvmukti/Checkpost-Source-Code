using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam.DAL
{
    public static class ResponseReturnMessages
    {
        public const string Contact_Administrator = "Please contact to your administrator for login.";
        public const string InvalidCredential = "Invalid username or password.";
        public const string LoginAttemptExceed = "You have exceeded the number of login attempts";
    }
}