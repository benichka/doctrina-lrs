using System;

namespace Doctrina.ExperienceApi.Exceptions
{
    public class InvalidObjectTypeException : Exception
    {
        public InvalidObjectTypeException(string type) 
            : base($"'{type}' is not a valid ObjectType.")
        {
        }
    }
}
