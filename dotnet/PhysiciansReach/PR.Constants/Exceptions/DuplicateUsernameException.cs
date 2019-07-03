using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Constants.Exceptions
{
    public class DuplicateUsernameException : Exception
    {
        public DuplicateUsernameException()
        {
        }

        public DuplicateUsernameException(string message)
            : base(message)
        {
        }

        public DuplicateUsernameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
