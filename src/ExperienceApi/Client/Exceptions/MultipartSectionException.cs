using System;

namespace Doctrina.ExperienceApi
{
    public class MultipartSectionException : Exception
    {
        public MultipartSectionException(string message)
            : base(message)
        {
        }
    }
}
