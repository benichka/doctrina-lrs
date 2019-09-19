using System;

namespace Doctrina.ExperienceApi.Data.Consumer.Exceptions
{
    public class MultipartSectionException : Exception
    {
        public MultipartSectionException(string message)
            : base(message)
        {
        }
    }
}
