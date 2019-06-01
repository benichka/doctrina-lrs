﻿using Doctrina.xAPI.Json.Converters;

namespace Doctrina.xAPI.Json
{
    public class ApiJsonSerializer : Newtonsoft.Json.JsonSerializer
    {
        public ApiVersion Version { get; }
        public ResultFormat ResultFormat { get; }

        public ApiJsonSerializer()
            : this(ApiVersion.GetLatest())
        {
        }

        public ApiJsonSerializer(ApiVersion version)
            : this(version, ResultFormat.Exact)
        {
        }

        public ApiJsonSerializer(ApiVersion version, ResultFormat format)
        {
            Version = version;
            ResultFormat = format;
            CheckAdditionalContent = true;
            MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Error;
            Converters.Insert(0, new StrictNumberConverter());
            Converters.Insert(0, new StrictStringConverter());
            Converters.Insert(1, new UriJsonConverter());
            Converters.Insert(2, new DateTimeJsonConverter());
            Converters.Insert(3, new StatementObjectConverter());
            Converters.Insert(4, new AgentJsonConverter());
            Converters.Insert(4, new ActivityDefinitionJsonConverter());
            Converters.Insert(4, new ActivityJsonConverter());
            DateParseHandling = Newtonsoft.Json.DateParseHandling.None;
            DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
        }
    }
}
