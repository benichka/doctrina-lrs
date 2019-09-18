using System;

namespace Doctrina.ExperienceApi.LRS.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
