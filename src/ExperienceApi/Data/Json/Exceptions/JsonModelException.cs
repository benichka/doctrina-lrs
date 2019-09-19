using System;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class JsonModelException : Exception
    {
        public JsonModelException(string message)
            : base(message)
        {
        }
    }
}
