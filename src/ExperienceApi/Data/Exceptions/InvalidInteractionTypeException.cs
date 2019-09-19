﻿using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class InvalidInteractionTypeException : Exception
    {
        public InvalidInteractionTypeException(string type) 
            : base($"'{type}' is not a valid InteractionType.")
        {
        }
    }
}
