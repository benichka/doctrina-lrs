﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Doctrina.xAPI.Json.Converters
{
    /// <summary>
    /// Ensures DateTime is well formed
    /// </summary>
    public class DateTimeJsonConverter : ApiJsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime)
                || objectType == typeof(DateTime?)
                || objectType == typeof(DateTimeOffset)
                || objectType == typeof(DateTimeOffset?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            if (token.Type != JTokenType.Date || token.Type != JTokenType.String)
            {

            }

            string strDateTime = token.Value<string>();

            if (strDateTime == null)
                return null;

            if (strDateTime.EndsWith("-00:00")
                || strDateTime.EndsWith("-0000")
                || strDateTime.EndsWith("-00"))
            {
                throw new JsonSerializationException($"'{strDateTime}' does not allow an offset of -00:00, -0000, -00");
            }

            if (DateTimeOffset.TryParse(strDateTime, out DateTimeOffset result))
            {
                return result;
            }
            else
            {
                throw new JsonSerializationException($"'{strDateTime}' is not a well formed DateTime string.");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var dateTime = value as DateTimeOffset?;
            var jvalue = new JValue(dateTime?.ToString("o"));
            jvalue.WriteTo(writer);
        }

        public override bool CanRead => true;
        public override bool CanWrite => true;
    }
}
