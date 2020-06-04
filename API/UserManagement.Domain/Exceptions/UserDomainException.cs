using System;
using System.Globalization;

namespace UserManagement.Application.Helpers
{
    public class UserDomainException : Exception
    {
        public UserDomainException() : base() { }

        public UserDomainException(string message) : base(message) { }

        public UserDomainException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
