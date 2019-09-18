using System;

namespace Doctrina.ExperienceApi.Json
{
    public class JsonModelException : Exception
    {
        public JsonModelException(string message)
            : base(message)
        {
        }
    }
}
