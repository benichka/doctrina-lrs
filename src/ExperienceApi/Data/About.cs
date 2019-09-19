﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Doctrina.ExperienceApi.Data
{
    [JsonObject]
    public class About
    {
        [JsonProperty("version")]
        public IEnumerable<string> Version { get; set; }

        [JsonProperty("extensions")]
        public ExtensionsDictionary Extensions { get; set; }
    }
}
