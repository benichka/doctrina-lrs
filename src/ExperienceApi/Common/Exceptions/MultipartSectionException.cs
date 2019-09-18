using System;

namespace Doctrina.ExperienceApi.Consumer.Exceptions
{
    public class MultipartSectionException : Exception
    {
        public MultipartSectionException(string message)
            : base(message)
        {
        }
    }
}
