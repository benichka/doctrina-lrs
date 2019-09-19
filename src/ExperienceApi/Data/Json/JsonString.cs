﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Doctrina.ExperienceApi.Data.Json
{
    public struct JsonString
    {
        private string _jsonString;

        public JsonString(string jsonString)
        {
            _jsonString = jsonString;
        }

        public JToken ToJToken()
        {
            return Deserialize<JToken>();
        }

        public JArray ToJArray()
        {
            return Deserialize<JArray>();
        }

        public T Deserialize<T>()
        {
            using (var stringReader = new StringReader(_jsonString))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    var jsonSerializer = new ApiJsonSerializer();
                    return jsonSerializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static implicit operator JsonString(string jsonString)
        {
            return new JsonString(jsonString);
        }

        public static implicit operator string(JsonString jsonString)
        {
            return jsonString.ToString();
        }

        public override string ToString()
        {
            return _jsonString;
        }
    }
}
