using System;

namespace GDPRiS.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
}