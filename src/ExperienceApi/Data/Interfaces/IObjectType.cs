﻿using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data
{
    public interface IStatementObject
    {
        ObjectType ObjectType { get; }

        JToken ToJToken(ApiVersion version, ResultFormat format);
    }
}
