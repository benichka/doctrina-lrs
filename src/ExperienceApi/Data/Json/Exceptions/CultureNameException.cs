﻿using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class CultureNameException : JsonTokenModelException
    {
        public CultureNameException(JToken token, string cultureName)
            : base(token, $"'{cultureName}' is not a valid RFC5646 Language Tag.")
        {
        }
    }
}
